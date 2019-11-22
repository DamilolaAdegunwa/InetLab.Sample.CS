Imports System
Imports System.IO
Imports System.Net
Imports System.Security.Authentication
Imports System.Windows.Forms
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU

Partial Public Class SmppServerDemo
	Inherits Form

	Private ReadOnly _server As SmppServer
	Private ReadOnly _log As ILog
	Private ReadOnly _messageComposer As MessageComposer

	Public Sub New()


		'HOW TO INSTALL LICENSE FILE
		'====================
		'After purchase you will receive Inetlab.SMPP.license file per E-Mail. 
		'Add this file into the root of project where you have a reference on Inetlab.SMPP.dll. Change "Build Action" of the file to "Embedded Resource". 

		'Set license before using Inetlab.SMPP classes in your code:

		' C#
		' Inetlab.SMPP.LicenseManager.SetLicense(this.GetType().Assembly.GetManifestResourceStream(this.GetType(), "Inetlab.SMPP.license" ));
		'
		' VB.NET
		' Inetlab.SMPP.LicenseManager.SetLicense(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "Inetlab.SMPP.license"))


		InitializeComponent()

		LogManager.SetLoggerFactory(New TextBoxLogFactory(tbLog, LogLevel.Info))



		AddHandler AppDomain.CurrentDomain.UnhandledException, Sub(sender, args)
				LogManager.GetLogger("AppDomain").Fatal(CType(args.ExceptionObject, Exception), "Unhandled Exception")
		End Sub

		_log = LogManager.GetLogger(Me.GetType().Name)

		_server = New SmppServer(New IPEndPoint(IPAddress.Any, Integer.Parse(tbPort.Text)))
		AddHandler _server.evClientConnected, AddressOf server_evClientConnected
		AddHandler _server.evClientDisconnected, AddressOf server_evClientDisconnected
		AddHandler _server.evClientBind, AddressOf server_evClientBind
		AddHandler _server.evClientSubmitSm, AddressOf server_evClientSubmitSm
		AddHandler _server.evClientSubmitMulti, AddressOf server_evClientSubmitMulti
		AddHandler _server.evClientEnquireLink, AddressOf ServerOnClientEnquireLink

		AddHandler _server.evClientCertificateValidation, AddressOf OnClientCertificateValidation

		'Create message composer. It helps to get full text of the concatenated message in the method OnFullMessageReceived
		_messageComposer = New MessageComposer()
		AddHandler _messageComposer.evFullMessageReceived, AddressOf OnFullMessageReceived
		AddHandler _messageComposer.evFullMessageTimeout, AddressOf OnFullMessageTimeout


	End Sub

	Private Sub ServerOnClientEnquireLink(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As EnquireLink)
		_log.Info($"EnquireLink received from {client}")
	End Sub

	Private Sub OnClientCertificateValidation(ByVal sender As Object, ByVal args As CertificateValidationEventArgs)
		'accept all certificates
		args.Accepted = True
	End Sub

	Private messageIdCounter As Long = 0

	Private Sub server_evClientBind(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As Bind)
		_log.Info("Client {0} bind as {1}:{2}", client.RemoteEndPoint, data.SystemId, data.Password)

		'  data.Response.ChangeSystemId("NewServerId");

		'Check SMPP access, and if it is wrong retund non-OK status.
		If data.SystemId = "" Then
			data.Response.Header.Status = CommandStatus.ESME_RINVSYSID
			_log.Info("Client {0} tries to bind with invalid SystemId: {1}", client.RemoteEndPoint, data.SystemId)
			Return
		End If
		If data.Password = "" Then
			_log.Info(String.Format("Client {0} tries to bind with invalid Password.", client.RemoteEndPoint))

			data.Response.Header.Status = CommandStatus.ESME_RINVPASWD
			Return
		End If

		'deny multiple connection with same smpp system id.
		For Each connectedClient In _server.ConnectedClients
			If connectedClient.SystemID = client.SystemID AndAlso connectedClient.Status = ConnectionStatus.Bound Then
				data.Response.Header.Status = CommandStatus.ESME_RALYBND
				_log.Warn("Client {0} tries to establish multiple sessions with the same SystemId", client.RemoteEndPoint)
				Return
			End If
		Next connectedClient

		_log.Info("Client {0} has been bound.", client.RemoteEndPoint)
		'  CommandStatus.ESME_RBINDFAIL - when Bind Failed. 

		UpdateClient(client)
	End Sub

	Private Sub server_evClientSubmitSm(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As SubmitSm)
		Dim messageId As Long = Interlocked.Increment(messageIdCounter)
		' You can set your own MessageId
		data.Response.MessageId = messageId.ToString()

		_log.Info("Client {0} sends message From:{1}, To:{2}, Text: {3}", client.RemoteEndPoint, data.SourceAddress, data.DestinationAddress, data.GetMessageText(client.EncodingMapper))


		_messageComposer.AddMessage(data)



		' Set unsuccess response status
		'data.Response.Status = CommandStatus.ESME_RSUBMITFAIL;


		If data.SMSCReceipt <> SMSCDeliveryReceipt.NotRequested Then
			'Send Delivery Receipt when required

			Dim messageText As String = data.GetMessageText(client.EncodingMapper)

			Dim dlrBuilder = SMS.ForDeliver().From(data.DestinationAddress).To(data.SourceAddress).Receipt(New Receipt With {
				.DoneDate = Date.Now,
				.State = MessageState.Delivered,
				.MessageId = data.Response.MessageId,
				.ErrorCode = "0",
				.SubmitDate = Date.Now,
				.Text = messageText.Substring(0, Math.Min(20, messageText.Length))
			})

			If data.DataCoding = DataCodings.UCS2 Then
				'short_message field cannot contain user data longer than 255 octets,
				'therefore for UCS2 encoding we are sending DLR in message_payload parameter
				dlrBuilder.MessageInPayload()
			End If

		   client.Deliver(dlrBuilder).ConfigureAwait(False)
		End If



	End Sub

	Private Sub server_evClientSubmitMulti(ByVal sender As Object, ByVal client As SmppServerClient, ByVal data As SubmitMulti)

		_log.Info("Client {0} sends message From:{1} to multiple destinations:{2}, Text: {3}", client.RemoteEndPoint, data.SourceAddress, data.DestinationAddresses.Count, data.GetMessageText(client.EncodingMapper))

		_messageComposer.AddMessage(data)

		If data.RegisteredDelivery = 1 Then
			Dim destinationAddress As SmeAddress = TryCast(data.DestinationAddresses(0), SmeAddress)

			Dim messageText As String = data.GetMessageText(client.EncodingMapper)

			'Send Delivery Receipt when required
			Task.Run(Function() client.Deliver(SMS.ForDeliver().From(data.SourceAddress).To(destinationAddress).Coding(data.DataCoding).Receipt(New Receipt With {
				.DoneDate = Date.Now,
				.State = MessageState.Delivered,
				.MessageId = data.Response.MessageId,
				.ErrorCode = "0",
				.SubmitDate = Date.Now,
				.Text = messageText.Substring(0, Math.Max(20, messageText.Length))
			})))
		End If



	End Sub



	Private Sub UpdateClient(ByVal client As SmppServerClient)
		Sync(Me, Sub()
				Dim index As Integer = -1
				For i As Integer = 0 To lbClients.Items.Count - 1
					Dim item As SmppServerClient = TryCast(lbClients.Items(i), SmppServerClient)
					If item IsNot Nothing AndAlso item.RemoteEndPoint.Equals(client.RemoteEndPoint) Then
						index = i
						Exit For
					End If
				Next i
				If index >= 0 Then
					lbClients.Items(index) = client
				Else
					lbClients.Items.Add(client)
				End If
		End Sub)
	End Sub


	Private Sub bStartServer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bStartServer.Click
		bStartServer.Enabled = False
		bStopServer.Enabled = True

		If cbSSL.Checked AndAlso comboCertList.SelectedItem IsNot Nothing Then
			_server.ServerCertificate = DirectCast(comboCertList.SelectedItem, X509Certificate2)
			_server.EnabledSslProtocols = SslProtocols.Default
		Else
			_server.ServerCertificate = Nothing
		End If
		_server.Start()


	End Sub

	Private Sub bStopServer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bStopServer.Click
		_server.Stop()

		bStartServer.Enabled = True
		bStopServer.Enabled = False

		lbClients.Items.Clear()

	End Sub

	Private Sub SmppServerDemo_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
		bStopServer_Click(sender, EventArgs.Empty)
	End Sub


	Private Sub server_evClientConnected(ByVal sender As Object, ByVal client As SmppServerClient)
		'Change number of threads that process received messages. Dafault is 3
		'client.WorkerThreads = 10;

		'Change receive buffer size for client socket
		' client.ReceiveBufferSize = 30 * 1024 * 1024;
		'Change send buffer size for client socket
		'  client.SendBufferSize = 30 * 1024 * 1024;


		'Don't allow this client to send more than one message per second
		'client.ReceiveSpeedLimit = 1;
		'Set maximum number of unhandled messages in the receive queue for this client
		'client.ReceiveQueueLimit = 2;


		client.EncodingMapper.MapEncoding(DataCodings.Class1, New Inetlab.SMPP.Encodings.GSMPackedEncoding())


		_log.Info("Client {0} connected.", client.RemoteEndPoint)


		If client.ClientCertificate IsNot Nothing Then
			_log.Info("Client Certificate {0}, Expire Date: {1}", client.ClientCertificate.Subject, client.ClientCertificate.GetExpirationDateString())
		End If

		Sync(lbClients, Sub()
				lbClients.Items.Add(client)
		End Sub)
	End Sub



	Private Sub OnFullMessageReceived(ByVal sender As Object, ByVal args As MessageEventHandlerArgs)
		_log.Info("SMS Received: {0}", args.Text)

	End Sub

	Private Sub OnFullMessageTimeout(ByVal sender As Object, ByVal args As MessageEventHandlerArgs)
		_log.Info("Incomplete SMS Received: {0}", args.Text)
	End Sub



	Private Sub server_evClientDisconnected(ByVal sender As Object, ByVal client As SmppServerClient)

		_log.Info("Client {0} disconnected.", client.RemoteEndPoint)

		Sync(lbClients, Sub()
				lbClients.Items.Remove(client)
		End Sub)

	End Sub


	Private Sub cbSSL_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbSSL.CheckedChanged
		comboCertList.Enabled = cbSSL.Checked
		comboCertList.Items.Clear()

		If cbSSL.Checked Then
			Dim collection As X509CertificateCollection = New X509Certificate2Collection()


			If File.Exists("server.p12") Then
				collection.Add(New X509Certificate2("server.p12", "12345"))
			End If

			Dim store As New X509Store(StoreName.My, StoreLocation.LocalMachine)
			store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)

			collection.AddRange(store.Certificates)


			For Each x509 As X509Certificate In collection
				comboCertList.Items.Add(x509)
			Next x509

			If comboCertList.Items.Count > 0 Then
				comboCertList.SelectedIndex = 0
			End If

		End If

	End Sub



	Private Sub lbClients_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbClients.SelectedIndexChanged
		Dim isSelected As Boolean = lbClients.SelectedIndex >= 0

		bSendMessage.Enabled = isSelected
		bDisconnect.Enabled = isSelected
	End Sub

	Private Sub bSendMessage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bSendMessage.Click
		Dim client As SmppServerClient = TryCast(lbClients.SelectedItem, SmppServerClient)
		If client IsNot Nothing Then
			Dim form As New SendMessage(client)
			form.ShowDialog()
		End If
	End Sub

	Private Async Sub bDisconnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bDisconnect.Click
		Dim client As SmppServerClient = TryCast(lbClients.SelectedItem, SmppServerClient)
		If client IsNot Nothing Then
			If client.Status = ConnectionStatus.Bound Then
			  Await client.UnBind()
			End If

			If client.Status = ConnectionStatus.Open Then
			  Await client.Disconnect()
			End If
		End If
	End Sub

	Public Delegate Sub SyncAction()

	Public Shared Sub Sync(ByVal control As Control, ByVal action As SyncAction)
		If control.InvokeRequired Then
			control.Invoke(action, New Object() { })
			Return
		End If

		action()
	End Sub

End Class

