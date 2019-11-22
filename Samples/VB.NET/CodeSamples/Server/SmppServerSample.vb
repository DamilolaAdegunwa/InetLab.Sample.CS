Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.PDU

Namespace CodeSamples.Server
	Public Class SmppServerSample
		Private ReadOnly _server As New SmppServer(New IPEndPoint(IPAddress.Any, 7777))

		Private ReadOnly _messageStore As IServerMessageStore

		Public Sub New()
			_messageStore = New DummyMessageStore()

			AddHandler _server.evClientBind, AddressOf ServerOnClientBind
			AddHandler _server.evClientSubmitSm, AddressOf ServerOnClientSubmitSm

			_server.Start()
		End Sub


		Private Sub ServerOnClientBind(ByVal sender As Object, ByVal client As SmppServerClient, ByVal pdu As Bind)
			'Set server name.
			pdu.Response.SystemId = "MySMPPServer"

			'check if client is allowed to create SMPP session.
			If IsClientAllowed(pdu) Then
				Console.WriteLine($"Client {client.SystemID} has bound")

				'Check if bound client can receive messages
				If client.BindingMode = ConnectionMode.Transceiver OrElse client.BindingMode = ConnectionMode.Receiver Then
					'Start messages delivery

					Dim messagesTask As Task = DeliverMessagesAsync(client, pdu)
				End If
			Else
				Console.WriteLine($"New session from client {client.SystemID} is denied.")
				pdu.Response.Header.Status = CommandStatus.ESME_RBINDFAIL
			End If
		End Sub


		Private Function IsClientAllowed(ByVal pdu As Bind) As Boolean
			'allow only one connection for one SMPP account.

			Dim activeSessions As Integer = _server.ConnectedClients.Where(Function(c) c.SystemID = pdu.SystemId).Count()

			Dim alreadyConnected As Boolean = activeSessions > 0
			If alreadyConnected Then
				Return False
			End If

			Return pdu.SystemId = pdu.Password

		End Function

		Private Async Function DeliverMessagesAsync(ByVal client As SmppServerClient, ByVal pdu As Bind) As Task
			Dim messages = _messageStore.GetMessagesForClient(pdu.SystemId, pdu.SystemType)

			For Each message As TextMessage In messages
				Dim pduBuilder = SMS.ForDeliver().From(message.PhoneNumber).To(message.ServiceAddress).Text(message.Text)

				Dim responses = Await client.Deliver(pduBuilder)

				If responses.All(Function(x) x.Header.Status = CommandStatus.ESME_ROK) Then
					_messageStore.MessageWasDelivered(message.Id)
				End If
			Next message

			Console.WriteLine($"Dummy messages have been sent to the client with systemId {pdu.SystemId}.")
		End Function

		Private Sub ServerOnClientSubmitSm(ByVal sender As Object, ByVal client As SmppServerClient, ByVal pdu As SubmitSm)
			Console.WriteLine($"Inbound message from {client.SystemID}")

			'Set server message id for the pdu;
			pdu.Response.MessageId = Guid.NewGuid().ToString().Substring(0, 8)
		End Sub


		'Represents message storage. You need to implement this interface with desired database.
		Public Interface IServerMessageStore
			Function GetMessagesForClient(ByVal systemId As String, ByVal systemType As String) As IEnumerable(Of TextMessage)
			Sub MessageWasDelivered(ByVal messageId As String)

		End Interface

		Public Class TextMessage
			Public Property Id() As String
			Public Property PhoneNumber() As String
			Public Property Text() As String

			Public Property ServiceAddress() As String
		End Class

		Public Class DummyMessageStore
			Implements IServerMessageStore

			Private ReadOnly _messages As New List(Of TextMessage)()

			Public Sub New()
				_messages.Add(New TextMessage With {
					.Id = "1",
					.ServiceAddress = "5555",
					.PhoneNumber = "7917123456",
					.Text = "test 1"
				})

			End Sub

			Public Function GetMessagesForClient(ByVal systemId As String, ByVal systemType As String) As IEnumerable(Of TextMessage) Implements IServerMessageStore.GetMessagesForClient
				Return _messages
			End Function

			Public Sub MessageWasDelivered(ByVal messageId As String) Implements IServerMessageStore.MessageWasDelivered
				_messages.RemoveAll(Function(m) m.Id = messageId)
			End Sub
		End Class
	End Class


End Namespace
