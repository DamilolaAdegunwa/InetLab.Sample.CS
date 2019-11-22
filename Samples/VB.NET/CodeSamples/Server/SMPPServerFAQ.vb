Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.PDU

Namespace CodeSamples.Server
	Public Class SMPPServerFAQ
		Private ReadOnly _server As New SmppServer(New IPEndPoint(IPAddress.Any, 7777))
		Private ReadOnly _messageStore As IServerMessageStore

		Public Sub New()
			_messageStore = New TestMessageStore()
			AddHandler _server.evClientSubmitSm, AddressOf ServerOnClientSubmitSm
			AddHandler _server.evClientBind, AddressOf OnClientBind
		End Sub

		'<DeliverMessagesOnBind>
		Private Sub OnClientBind(ByVal sender As Object, ByVal client As SmppServerClient, ByVal pdu As Bind)
			If client.BindingMode = ConnectionMode.Transceiver OrElse client.BindingMode = ConnectionMode.Receiver Then
				'Start messages delivery

				Dim messagesTask As Task = DeliverMessagesAsync(client, pdu)

			End If
		End Sub


		Private Async Function DeliverMessagesAsync(ByVal client As SmppServerClient, ByVal pdu As Bind) As Task
			Dim messages = _messageStore.GetMessagesForClient(pdu.SystemId, pdu.SystemType)

			For Each message As TextMessage In messages
				Dim pduBuilder = SMS.ForDeliver().From(message.PhoneNumber).To(message.ServiceAddress).Text(message.Text)

				Dim responses = Await client.Deliver(pduBuilder)

				_messageStore.UpdateMessageState(message.Id, responses)
			Next message
		End Function

		Public Interface IServerMessageStore
			Function GetMessagesForClient(ByVal systemId As String, ByVal systemType As String) As IEnumerable(Of TextMessage)
			Sub UpdateMessageState(ByVal messageId As String, ByVal responses() As DeliverSmResp)
		End Interface

		Public Class TextMessage
			Public Property Id() As String
			Public Property PhoneNumber() As String
			Public Property Text() As String

			Public Property ServiceAddress() As String
		End Class

		'</DeliverMessagesOnBind>

		'How to send message to connected client
		'
		'<DeliverToClient>
		Public Async Function DeliverToClient(ByVal message As SmppServerSample.TextMessage) As Task
			Dim systemId As String = GetSystemIdByServiceAddress(message.ServiceAddress)

			Dim client As SmppServerClient = FindClient(systemId)

			Await client.Deliver(SMS.ForDeliver().From(message.PhoneNumber).To(message.ServiceAddress).Text(message.Text))

		End Function
		'</DeliverToClient>

		Private Function FindClient(ByVal systemId As String) As SmppServerClient
			Return _server.ConnectedClients.FirstOrDefault(Function(c) c.SystemID = systemId)
		End Function

		Private Function GetSystemIdByServiceAddress(ByVal serviceAddess As String) As String
			If serviceAddess = "5555" Then
				Return "ServiceSample"
			End If

			Return Nothing
		End Function

		'How to set MessageId
		' 
		'<SetMessageIdForSubmitSm>
		Private Sub ServerOnClientSubmitSm(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As SubmitSm)
			data.Response.MessageId = Guid.NewGuid().ToString().Substring(0, 8)
		End Sub
		'</SetMessageIdForSubmitSm>


		Public Class TestMessageStore
			Implements IServerMessageStore

			Private ReadOnly _messages As New List(Of TextMessage)()

			Public Sub New()
				_messages.Add(New TextMessage With {
					.Id = "1",
					.ServiceAddress = "5555",
					.PhoneNumber = "7917123456",
					.Text = "test 1"
				})

				_messages.Add(New TextMessage With {
					.Id = "2",
					.ServiceAddress = "5555",
					.PhoneNumber = "7917654321",
					.Text = "test 2"
				})
			End Sub

			Public Function GetMessagesForClient(ByVal systemId As String, ByVal systemType As String) As IEnumerable(Of TextMessage) Implements IServerMessageStore.GetMessagesForClient
				Return _messages
			End Function

			Public Sub UpdateMessageState(ByVal messageId As String, ByVal responses() As DeliverSmResp) Implements IServerMessageStore.UpdateMessageState
				_messages.RemoveAll(Function(m) m.Id = messageId)
			End Sub
		End Class
	End Class


End Namespace
