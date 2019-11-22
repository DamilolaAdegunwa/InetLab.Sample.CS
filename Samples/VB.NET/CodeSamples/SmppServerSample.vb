Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class SmppServerSample
		Private ReadOnly _server As New SmppServer(New IPEndPoint(IPAddress.Any, 7777))

		Private ReadOnly _messageStore As IServerMessageStore

		Public Sub New(ByVal messageStore As IServerMessageStore)
			_messageStore = messageStore
			AddHandler _server.evClientBind, AddressOf ServerOnEvClientBind
			AddHandler _server.evClientSubmitSm, AddressOf ServerOnClientSubmitSm
			AddHandler _server.evClientSubmitMulti, AddressOf ServerOnClientSubmitMulti
		End Sub



		'<DeliverMessagesOnBind>
		Private Sub ServerOnEvClientBind(ByVal sender As Object, ByVal client As SmppServerClient, ByVal pdu As Bind)
			If ClientAllowed(client, pdu) Then
				'Check if bound client can receive messages
				If client.BindingMode = ConnectionMode.Transceiver OrElse client.BindingMode = ConnectionMode.Receiver Then
					'Start messages delivery
					DeliverMessages(client, pdu).ConfigureAwait(False)

					'Start sending delivery reciepts
					DeliverReceipts(client, pdu).ConfigureAwait(False)
				End If
			Else
				pdu.Response.Header.Status = CommandStatus.ESME_RBINDFAIL
			End If
		End Sub


		Private Function ClientAllowed(ByVal client As SmppServerClient, ByVal pdu As Bind) As Boolean
			'allow only one connection for one SMPP account.

			Dim activeSessions As Integer = _server.ConnectedClients.Where(Function(c) c.SystemID = pdu.SystemId).Count()

			Dim alreadyConnected As Boolean = activeSessions > 0
			If alreadyConnected Then
				Return False
			End If

			Return pdu.SystemId = pdu.Password
		End Function

		Private Async Function DeliverMessages(ByVal client As SmppServerClient, ByVal pdu As Bind) As Task
			Dim messages = _messageStore.GetMessagesForClient(pdu.SystemId, pdu.SystemType)

			For Each message As TextMessage In messages
				Dim pduBuilder = SMS.ForDeliver().From(message.PhoneNumber).To(message.ServiceAddress).Text(message.Text)

			   Dim responses = Await client.Deliver(pduBuilder)

				_messageStore.UpdateMessageState(message.Id, responses)
			Next message
		End Function

		Private Async Function DeliverReceipts(ByVal client As SmppServerClient, ByVal pdu As Bind) As Task
			Dim messages = _messageStore.GetDeliveryReceiptsForClient(pdu.SystemId, pdu.SystemType)

			For Each message In messages
			   Dim responses = Await client.Deliver(SMS.ForDeliver().From(message.PhoneNumber).To(message.ServiceAddress).Receipt(message.Receipt))

				_messageStore.UpdateDeliveryReceiptState(message.Id, responses)
			Next message
		End Function

		'</DeliverMessagesOnBind>

		'<DeliverToClient>
		Public Async Function DeliverToClient(ByVal message As TextMessage) As Task
			Dim systemId As String = GetSystemIdByServiceAddress(message.ServiceAddress)

			Dim client As SmppServerClient = FindClient(systemId)

		   Await client.Deliver(SMS.ForDeliver().From(message.PhoneNumber).To(message.ServiceAddress).Text(message.Text))

		End Function
		'</DeliverToClient>

		Private Function GetSystemIdByServiceAddress(ByVal serviceAddess As String) As String
			Return "MyServiceSample"
		End Function

		Private Function FindClient(ByVal systemId As String) As SmppServerClient

			Return _server.ConnectedClients.FirstOrDefault(Function(c) c.SystemID = systemId)
		End Function

		'<SetMessageIdForSubmitSm>
		Private Sub ServerOnClientSubmitSm(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As SubmitSm)
			data.Response.MessageId = Guid.NewGuid().ToString().Substring(0, 8)
		End Sub
		'</SetMessageIdForSubmitSm>

		'<SetMessageIdForSubmitMulti>
		Private Sub ServerOnClientSubmitMulti(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As SubmitMulti)
			data.Response.MessageId = Guid.NewGuid().ToString().Substring(0, 8)
		End Sub
		'</SetMessageIdForSubmitMulti>

		Public Interface IServerMessageStore
			Function GetMessagesForClient(ByVal systemId As String, ByVal systemType As String) As IEnumerable(Of TextMessage)
			Sub UpdateMessageState(ByVal messageId As String, ByVal responses() As DeliverSmResp)

			Function GetDeliveryReceiptsForClient(ByVal systemId As String, ByVal systemType As String) As IEnumerable(Of DeliveryReceiptMessage)
			Sub UpdateDeliveryReceiptState(ByVal id As String, ByVal responses() As DeliverSmResp)
		End Interface

		Public Class DeliveryReceiptMessage
			Public Property Id() As String

			Public Property PhoneNumber() As String
			Public Property ServiceAddress() As String
			Public Property Receipt() As Receipt

		End Class

		Public Class TextMessage
			Public Property Id() As String
			Public Property PhoneNumber() As String
			Public Property Text() As String

			Public Property ServiceAddress() As String
		End Class
	End Class


End Namespace
