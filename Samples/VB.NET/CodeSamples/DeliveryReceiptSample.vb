Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class DeliveryReceiptSample
		Private ReadOnly _client As New SmppClient()

		Private ReadOnly _config As SmppConfig
		Private ReadOnly _clientMessageStore As IClientMessageStore

		Public Sub New(ByVal config As SmppConfig, ByVal clientMessageStore As IClientMessageStore)
			_config = config
			_clientMessageStore = clientMessageStore
			AddHandler _client.evDeliverSm, AddressOf ClientOnEvDeliverSm
		End Sub

		'<SendMessage>
		Public Async Function SendMessage(ByVal message As TextMessage) As Task
			Dim list As IList(Of SubmitSm) = SMS.ForSubmit().From(_config.ShortCode).To(message.PhoneNumber).Text(message.Text).DeliveryReceipt().Create(_client)

			For Each sm As SubmitSm In list
				sm.Header.Sequence = _client.SequenceGenerator.NextSequenceNumber()
				_clientMessageStore.SaveSequence(message.Id, sm.Header.Sequence)
			Next sm

		  Dim responses = Await _client.Submit(list)

			For Each resp As SubmitSmResp In responses
				_clientMessageStore.SaveMessageId(message.Id, resp.MessageId)
			Next resp
		End Function
		'</SendMessage>


		'<EvDeliverSm>
		Private Sub ClientOnEvDeliverSm(ByVal sender As Object, ByVal data As DeliverSm)
			If data.MessageType = MessageTypes.SMSCDeliveryReceipt Then
				_clientMessageStore.UpdateMessageStatus(data.Receipt.MessageId, data.Receipt.State)
			End If
		End Sub
		'</EvDeliverSm>
		' 
		Public Interface IClientMessageStore
			Function GetNextMessage() As TextMessage

			Sub SaveSequence(ByVal id As String, ByVal sequence As UInteger)
			Sub SaveMessageId(ByVal id As String, ByVal smppMessageId As String)
			Sub UpdateMessageStatus(ByVal smppMessageId As String, ByVal state As MessageState)
		End Interface

		Public Class TextMessage
			Public Property Id() As String
			Public Property PhoneNumber() As String
			Public Property Text() As String

			Public Property ServiceAddress() As String
		End Class
	End Class


End Namespace
