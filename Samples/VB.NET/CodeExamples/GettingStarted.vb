Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class GettingStarted
		Public Sub New()
			'<AttachToDeliverSm>
			AddHandler _client.evDeliverSm, AddressOf client_evDeliverSm
			'</AttachToDeliverSm>
		End Sub


		Private ReadOnly _log As ILog = LogManager.GetLogger(Of GettingStarted)()

		'<ConnectToServer>
		Private ReadOnly _client As New SmppClient()

		Public Async Function Connect() As Task
			If Await _client.Connect("smpp.server.com", 7777) Then
				_log.Info("Connected to SMPP server")

				Dim bindResp As BindResp = Await _client.Bind("username", "password", ConnectionMode.Transceiver)

				If bindResp.Header.Status = CommandStatus.ESME_ROK Then
					_log.Info("Bound with SMPP server")
				End If
			End If
		End Function
		'</ConnectToServer>

		'<SendMessage>
		Public Async Function SendMessage() As Task
			Dim responses As IList(Of SubmitSmResp) = Await _client.Submit(SMS.ForSubmit().Text("Test Test Test Test Test Test Test Test Test Test").From("1111").To("79171234567").Coding(DataCodings.UCS2).DeliveryReceipt())
		End Function
		'</SendMessage>

		' <CreateSubmitSm>
		Public Function CreateSubmitSm() As SubmitSm
			Dim sm As New SubmitSm()
			sm.UserData.ShortMessage = _client.EncodingMapper.GetMessageBytes("Test Test Test Test Test Test Test Test Test Test", DataCodings.Default)
			sm.SourceAddress = New SmeAddress("1111", AddressTON.NetworkSpecific, AddressNPI.Unknown)
			sm.DestinationAddress = New SmeAddress("79171234567", AddressTON.Unknown, AddressNPI.ISDN)
			sm.DataCoding = DataCodings.UCS2
			sm.SMSCReceipt = SMSCDeliveryReceipt.SuccessOrFailure

			Return sm
		End Function
		' </CreateSubmitSm>

		' <ReceiveMessage>
		Private ReadOnly _composer As New MessageComposer()

		Private Sub client_evDeliverSm(ByVal sender As Object, ByVal data As DeliverSm)
			Try
				'Check if we received Delivery Receipt
				If data.MessageType = MessageTypes.SMSCDeliveryReceipt Then
					'Get MessageId of delivered message
					Dim messageId As String = data.Receipt.MessageId
					Dim deliveryStatus As MessageState = data.Receipt.State
				Else
					' Receive incoming message and try to concatenate all parts
					If data.Concatenation IsNot Nothing Then
						_composer.AddMessage(data)

						_log.Info("DeliverSm part received : Sequence: {0} SourceAddr: {1} Concatenation ( {2} ) Coding: {3} Text: {4}", data.Header.Sequence, data.SourceAddress, data.Concatenation, data.DataCoding, data.MessageText)


						If _composer.IsLastSegment(data) Then
							Dim fullMessage As String = _composer.GetFullMessage(data)
							_log.Info("Full message: " & fullMessage)
						End If
					Else
						_log.Info("DeliverSm received : Sequence: {0} SourceAddr : {1} Coding : {2} MessageText : {3}", data.Header.Sequence, data.SourceAddress, data.DataCoding, data.MessageText)
					End If
				End If
			Catch ex As Exception
				data.Response.Header.Status = CommandStatus.ESME_RX_T_APPN
				_log.Error("Failed to process DeliverSm", ex)
			End Try

			' </ReceiveMessage>
		End Sub
	End Class
End Namespace
