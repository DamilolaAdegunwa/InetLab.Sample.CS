Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Parameters
Imports Inetlab.SMPP.PDU

Namespace CodeExamples
	Public Class SMPPGateway
		Private ReadOnly _proxyClient As SmppClient
		Private ReadOnly _proxyServer As SmppServer

		Private ReadOnly _storage As IStorage = New InMemoryStorage()

		Public Sub New()
			_proxyServer = New SmppServer(New IPEndPoint(IPAddress.Any, 7776))
			_proxyServer.Name = "Proxy" & _proxyServer.Name
			AddHandler _proxyServer.evClientSubmitSm, AddressOf WhenReceiveSubmitSmFromClient

			_proxyClient = New SmppClient()

			_proxyClient.Name = "Proxy" & _proxyClient.Name

			AddHandler _proxyClient.evDeliverSm, AddressOf WhenDeliveryReceiptReceivedFromSMSC

			AddHandler _storage.ReceiptReadyForDelivery, AddressOf WhenReceiptIsReadyForDelivery
		End Sub




		Private Async Sub WhenReceiveSubmitSmFromClient(ByVal sender As Object, ByVal serverClient As SmppServerClient, ByVal receivedPdu As SubmitSm)
			_storage.SubmitReceived(receivedPdu)

			Dim pduToSMSC As SubmitSm = receivedPdu.Clone()

			'reset sequence number to allow proxyClient to assigne next sequence number from his generator
			pduToSMSC.Header.Sequence = 0

			Dim respFromSMSC As SubmitSmResp = Await _proxyClient.Submit(pduToSMSC)

			_storage.SubmitForwarded(receivedPdu, respFromSMSC)

			If receivedPdu.SMSCReceipt = SMSCDeliveryReceipt.NotRequested Then
				_storage.DeliveryReceiptNotRequested(receivedPdu.Response.MessageId)
			End If
		End Sub

		Private Sub WhenDeliveryReceiptReceivedFromSMSC(ByVal sender As Object, ByVal data As DeliverSm)
			If data.MessageType = MessageTypes.SMSCDeliveryReceipt Then
				_storage.ReceiptReceived(data)
			End If
		End Sub

		Private Async Sub WhenReceiptIsReadyForDelivery(ByVal sender As Object, ByVal data As DeliverSm)
			'Find client that should receive the delivery receipt.
			Dim client = _proxyServer.ConnectedClients.FirstOrDefault()
			If client IsNot Nothing Then

				Dim resp As DeliverSmResp = Await client.Deliver(data)

				_storage.ReceiptDelivered(data.Receipt.MessageId)

			End If
		End Sub

		Public Async Function Run(ByVal smscUrl As Uri) As Task

			_proxyServer.Start()

			Await _proxyClient.Connect(smscUrl.Host, smscUrl.Port)

			Dim bindResp As BindResp = Await _proxyClient.Bind("proxy", "test")


		End Function

	End Class

	Friend Interface IStorage

		''' <summary> Store sequence number and message id of the response for received SubmitSm.</summary>
		'''
		''' <param name="data"> The data. </param>
		Sub SubmitReceived(ByVal data As SubmitSm)


		''' <summary> Save MessageId from SMSC.</summary>
		''' <remarks>
		'''          <para>Raise <see cref="ReceiptReadyForDelivery"/> event when delivery receipt was already received before SubmitSmResp</para>
		''' </remarks>
		''' <param name="reqFromClient"> The request PDU received from client </param>
		''' <param name="remoteResp">    The response PDU received from SMSC. </param>
		Sub SubmitForwarded(ByVal reqFromClient As SubmitSm, ByVal remoteResp As SubmitSmResp)

		''' <summary> Process Delivery Receipt. </summary>
		''' <remarks>
		'''    <para> When SubmitSmResp is not yet received, store delivery receipt for further delivery. </para>
		'''    <para> When SubmitSmResp is already received, raise <see cref="ReceiptReadyForDelivery"/> event </para>
		''' </remarks>
		''' <param name="data"> The delivery receipt PDU </param>
		Sub ReceiptReceived(ByVal data As DeliverSm)


		''' <summary> Receipt delivered to client. </summary>
		''' <remarks>
		'''  <para>Update message state to finished.</para>
		''' </remarks>
		''' <param name="localMessageId"> MessageId from Gateway's SMPP server. </param>
		Sub ReceiptDelivered(ByVal localMessageId As String)


		''' <summary> Delivery receipt for this message will never come. Change message state to finished. </summary>
		'''
		''' <param name="localMessageId"> MessageId from Gateway's SMPP server </param>
		Sub DeliveryReceiptNotRequested(ByVal localMessageId As String)


		''' <summary> Occurs when both SubmitSmResp and DeliverSm received from SMSC.</summary>
		Event ReceiptReadyForDelivery As EventHandler(Of DeliverSm)

	End Interface

	Friend Class InMemoryStorage
		Implements IStorage

		Private Class MessageEntry

			''' <summary> Sequence number used to send SubmitSm from Gateway to SMSC</summary>
			Public LocalSequence As UInteger

			''' <summary> MessageId sent to from Gateway to connected client. </summary>
			Public LocalMessageId As String

			Public RemoteSequence As UInteger


			''' <summary> MessageId received from SMSC. </summary>
			Public RemoteMessageId As String


			''' <summary> Store delivery receipt for further sending to corresponding client. </summary>
			Public Property Receipt() As DeliverSm
		End Class

		'simulate DB indexes with additional dictionaries.
		Private ReadOnly _localSequences As New ConcurrentDictionary(Of UInteger, MessageEntry)()
		Private ReadOnly _remoteMessage As New ConcurrentDictionary(Of String, MessageEntry)()
		Private ReadOnly _localMessage As New ConcurrentDictionary(Of String, MessageEntry)()


		Public Sub SubmitReceived(ByVal data As SubmitSm) Implements IStorage.SubmitReceived

			Dim entry = New MessageEntry With {
				.LocalSequence = data.Header.Sequence,
				.LocalMessageId = data.Response.MessageId
			}

			If Not _localSequences.TryAdd(data.Header.Sequence, entry) Then

			End If

			If Not _localMessage.TryAdd(data.Response.MessageId, entry) Then

			End If

		End Sub

		Public Sub SubmitForwarded(ByVal req As SubmitSm, ByVal remoteResp As SubmitSmResp) Implements IStorage.SubmitForwarded
			Dim entry As MessageEntry = Nothing
			If Not _localSequences.TryGetValue(req.Header.Sequence, entry) Then
				Throw New Exception("Cannot find message with local sequence " & req.Header.Sequence)
			End If

			entry.RemoteSequence = remoteResp.Header.Sequence
			entry.RemoteMessageId = remoteResp.MessageId

			Dim remoteEntry As MessageEntry = Nothing
			If _remoteMessage.TryRemove(remoteResp.MessageId, remoteEntry) Then
				entry.Receipt = remoteEntry.Receipt
				OnReceiptReadyForForward(entry)
			Else
				_remoteMessage.TryAdd(remoteResp.MessageId, entry)
			End If
		End Sub




		Public Sub ReceiptDelivered(ByVal localMessageId As String) Implements IStorage.ReceiptDelivered
			Dim entry As MessageEntry = Nothing
			If _localMessage.TryRemove(localMessageId, entry) Then
				If entry.RemoteMessageId IsNot Nothing Then
					Dim entryRemote As MessageEntry = Nothing
					_remoteMessage.TryRemove(entry.RemoteMessageId, entryRemote)
				End If

				Dim entryLocal As MessageEntry = Nothing
				If _localSequences.TryRemove(entry.LocalSequence, entryLocal) Then

				End If
			End If
		End Sub

		Public Sub DeliveryReceiptNotRequested(ByVal localMessageId As String) Implements IStorage.DeliveryReceiptNotRequested
			Dim entry As MessageEntry = Nothing
			If _localMessage.Remove(localMessageId, entry) Then
				If entry.RemoteMessageId IsNot Nothing Then
					Dim entryRemote As MessageEntry = Nothing
					_remoteMessage.TryRemove(entry.RemoteMessageId, entryRemote)
				End If

				Dim entryLocal As MessageEntry = Nothing
				If _localSequences.TryRemove(entry.LocalSequence, entryLocal) Then

				End If

			End If
		End Sub

		Public Sub ReceiptReceived(ByVal data As DeliverSm) Implements IStorage.ReceiptReceived
			Dim entry As MessageEntry = Nothing

			If _remoteMessage.TryGetValue(data.Receipt.MessageId, entry) Then
				entry.Receipt = data

				OnReceiptReadyForForward(entry)
			Else
				_remoteMessage.TryAdd(data.Receipt.MessageId, New MessageEntry With {
					.RemoteMessageId = data.Receipt.MessageId,
					.Receipt = data
				})
			End If

		End Sub

		Private Sub OnReceiptReadyForForward(ByVal entry As MessageEntry)
			If ReceiptReadyForDeliveryEvent IsNot Nothing Then
				Dim receipt As DeliverSm = entry.Receipt.Clone()
				receipt.Receipt.MessageId = entry.LocalMessageId

				receipt.Parameters(OptionalTags.ReceiptedMessageId) = EncodingMapper.Default.GetMessageBytes(receipt.Receipt.MessageId, receipt.DataCoding)

				receipt.Header.Sequence = 0

				RaiseEvent ReceiptReadyForDelivery(Me, receipt)
			End If
		End Sub

		Public Event ReceiptReadyForDelivery As EventHandler(Of DeliverSm) Implements IStorage.ReceiptReadyForDelivery



	End Class

	Public Module SmppPDUExtensions
		<System.Runtime.CompilerServices.Extension> _
		Public Function Clone(Of TPdu As SmppPDU)(ByVal pdu As TPdu) As TPdu
			Using stream As New MemoryStream()
				Dim writer As New SmppWriter(stream)
				writer.WritePDU(pdu)

				stream.Position = 0

				Dim reader As New SmppStreamReader(stream)
				Return CType(reader.ReadPDU(), TPdu)
			End Using
		End Function
	End Module
End Namespace
