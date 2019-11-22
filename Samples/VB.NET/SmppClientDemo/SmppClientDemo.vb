Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Security.Authentication
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Builders
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU
Imports System.Windows.Forms

Namespace SmppClientDemo

	Partial Public Class SmppClientDemo
		Inherits Form

		Private ReadOnly _messageComposer As MessageComposer
		Private ReadOnly _log As ILog

		Private ReadOnly _client As SmppClient


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



			_log = LogManager.GetLogger(Me.GetType().Name)


			AddHandler AppDomain.CurrentDomain.UnhandledException, Sub(sender, args)
				LogManager.GetLogger("AppDomain").Fatal(CType(args.ExceptionObject, Exception), "Unhandled Exception")
			End Sub


			_client = New SmppClient()
			_client.ResponseTimeout = TimeSpan.FromSeconds(60)
			_client.EnquireLinkInterval = TimeSpan.FromSeconds(20)

			AddHandler _client.evDisconnected, AddressOf client_evDisconnected
			AddHandler _client.evDeliverSm, AddressOf client_evDeliverSm
			AddHandler _client.evEnquireLink, AddressOf client_evEnquireLink
			AddHandler _client.evUnBind, AddressOf client_evUnBind
			AddHandler _client.evDataSm, AddressOf client_evDataSm
			AddHandler _client.evRecoverySucceeded, AddressOf ClientOnRecoverySucceeded

			AddHandler _client.evServerCertificateValidation, AddressOf OnCertificateValidation


			_messageComposer = New MessageComposer()
			AddHandler _messageComposer.evFullMessageReceived, AddressOf OnFullMessageReceived
			AddHandler _messageComposer.evFullMessageTimeout, AddressOf OnFullMessageTimeout

		End Sub


		Private Sub OnCertificateValidation(ByVal sender As Object, ByVal args As CertificateValidationEventArgs)
			'accept all certificates
			args.Accepted = True
		End Sub


		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If

				_client.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub



		Private Async Function Connect() As Task


			If _client.Status = ConnectionStatus.Closed Then
				_log.Info("Connecting to " & tbHostname.Text)

					bConnect.Enabled = False
					bDisconnect.Enabled = False
					cbReconnect.Enabled = False


				_client.EsmeAddress = New SmeAddress("", CType(Convert.ToByte(tbAddrTon.Text), AddressTON), CType(Convert.ToByte(tbAddrNpi.Text), AddressNPI))
				_client.SystemType = tbSystemType.Text

				_client.ConnectionRecovery = cbReconnect.Checked
				_client.ConnectionRecoveryDelay = TimeSpan.FromSeconds(3)


				If cbSSL.Checked Then
					_client.EnabledSslProtocols = SslProtocols.Default
					_client.ClientCertificates.Clear()
					_client.ClientCertificates.Add(New X509Certificate2("client.p12", "12345"))
				Else
					_client.EnabledSslProtocols = SslProtocols.None
				End If

				Dim bSuccess As Boolean = Await _client.Connect(tbHostname.Text, Convert.ToInt32(tbPort.Text))

				If bSuccess Then
					_log.Info("SmppClient connected")

				   Await Bind()
				Else

						bConnect.Enabled = True
						cbReconnect.Enabled = True
						bDisconnect.Enabled = False

				End If
			End If

		End Function

		Private Async Function Bind() As Task
			_log.Info("Bind client with SystemId: {0}", tbSystemId.Text)

			Dim mode As ConnectionMode = ConnectionMode.Transceiver

			bDisconnect.Enabled = True
			mode = DirectCast(cbBindingMode.SelectedItem, ConnectionMode)


			Dim resp As BindResp = Await _client.Bind(tbSystemId.Text, tbPassword.Text, mode)

			Select Case resp.Header.Status
				Case CommandStatus.ESME_ROK
					_log.Info("Bind succeeded: Status: {0}, SystemId: {1}", resp.Header.Status, resp.SystemId)

					bSubmit.Enabled = True

				Case Else
					_log.Warn("Bind failed: Status: {0}", resp.Header.Status)

					Await Disconnect()
			End Select
		End Function





		Private Async Function Disconnect() As Task
			_log.Info("Disconnect from SMPP server")

			If _client.Status = ConnectionStatus.Bound Then
				Await UnBind()
			End If

			If _client.Status = ConnectionStatus.Open Then
				Await _client.Disconnect()
			End If
		End Function


		Private Sub client_evDisconnected(ByVal sender As Object)
			_log.Info("SmppClient disconnected")

			Sync(Me, Sub()
				bConnect.Enabled = True
				bDisconnect.Enabled = False
				bSubmit.Enabled = False
				cbReconnect.Enabled = True


			End Sub)

		End Sub

		Private Sub ClientOnRecoverySucceeded(ByVal sender As Object, ByVal data As BindResp)
			_log.Info("Connection has been recovered.")

			Sync(Me, Sub()
				bConnect.Enabled = False
				bDisconnect.Enabled = True
				bSubmit.Enabled = True
				cbReconnect.Enabled = False
			End Sub)

		End Sub


		Private Async Function UnBind() As Task
			_log.Info("Unbind SmppClient")
			Dim resp As UnBindResp = Await _client.UnBind()

			Select Case resp.Header.Status
				Case CommandStatus.ESME_ROK
					_log.Info("UnBind succeeded: Status: {0}", resp.Header.Status)
				Case Else
					_log.Warn("UnBind failed: Status: {0}", resp.Header.Status)
					Await _client.Disconnect()
			End Select

		End Function



		Private Sub client_evDeliverSm(ByVal sender As Object, ByVal data As DeliverSm)
			Try
				'Check if we received Delivery Receipt
				If data.MessageType = MessageTypes.SMSCDeliveryReceipt Then
					'Get MessageId of delivered message
					Dim messageId As String = data.Receipt.MessageId
					Dim deliveryStatus As MessageState = data.Receipt.State

					_log.Info("Delivery Receipt received: {0}", data.Receipt.ToString())
				Else

					' Receive incoming message and try to concatenate all parts
					If data.Concatenation IsNot Nothing Then
						_messageComposer.AddMessage(data)

						_log.Info("DeliverSm part received: Sequence: {0}, SourceAddress: {1}, Concatenation ( {2} )" & " Coding: {3}, Text: {4}", data.Header.Sequence, data.SourceAddress, data.Concatenation, data.DataCoding, _client.EncodingMapper.GetMessageText(data))
					Else
						_log.Info("DeliverSm received : Sequence: {0}, SourceAddress: {1}, Coding: {2}, Text: {3}", data.Header.Sequence, data.SourceAddress, data.DataCoding, _client.EncodingMapper.GetMessageText(data))
					End If

					' Check if an ESME acknowledgement is required
					If data.Acknowledgement <> SMEAcknowledgement.NotRequested Then
						' You have to clarify with SMSC support what kind of information they request in ESME acknowledgement.

						Dim messageText As String = data.GetMessageText(_client.EncodingMapper)

						Dim smBuilder = SMS.ForSubmit().From(data.DestinationAddress).To(data.SourceAddress).Coding(data.DataCoding).ConcatenationInUDH(_client.SequenceGenerator.NextReferenceNumber()).Set(Sub(m) m.MessageType = MessageTypes.SMEDeliveryAcknowledgement).Text(New Receipt With {
							.DoneDate = Date.Now,
							.State = MessageState.Delivered,
							.ErrorCode = "0",
							.SubmitDate = Date.Now,
							.Text = messageText.Substring(0, Math.Min(20, messageText.Length))
						}.ToString())



					   _client.Submit(smBuilder).ConfigureAwait(False)
					End If
				End If
			Catch ex As Exception
				data.Response.Header.Status = CommandStatus.ESME_RX_T_APPN
				 _log.Error(ex,"Failed to process DeliverSm")
			End Try
		End Sub


		Private Sub client_evDataSm(ByVal sender As Object, ByVal data As DataSm)
			_log.Info("DataSm received : Sequence: {0}, SourceAddress: {1}, DestAddress: {2}, Coding: {3}, Text: {4}", data.Header.Sequence, data.SourceAddress, data.DestinationAddress, data.DataCoding, data.GetMessageText(_client.EncodingMapper))
		End Sub



		Private Sub OnFullMessageTimeout(ByVal sender As Object, ByVal args As MessageEventHandlerArgs)
			_log.Info("Incomplete message received From: {0}, Text: {1}", args.GetFirst(Of DeliverSm)().SourceAddress, args.Text)
		End Sub

		Private Sub OnFullMessageReceived(ByVal sender As Object, ByVal args As MessageEventHandlerArgs)
			_log.Info("Full message received From: {0}, To: {1}, Text: {2}", args.GetFirst(Of DeliverSm)().SourceAddress, args.GetFirst(Of DeliverSm)().DestinationAddress, args.Text)
		End Sub



		Private Sub client_evEnquireLink(ByVal sender As Object, ByVal data As EnquireLink)
			_log.Info("EnquireLink received")
		End Sub




		Private Sub client_evUnBind(ByVal sender As Object, ByVal data As UnBind)
			_log.Info("UnBind request received")
		End Sub




		Private Async Sub bConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bConnect.Click
			Await Connect()
		End Sub

		Private Async Sub bDisconnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bDisconnect.Click
			Await Disconnect()
		End Sub


		Private Async Sub bSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bSubmit.Click

			If _client.Status <> ConnectionStatus.Bound Then
				MessageBox.Show("Before sending messages, please connect to SMPP server.")
				Return
			End If

			bSubmit.Enabled = False

			_client.SendSpeedLimit = GetSpeedLimit(tbSubmitSpeed.Text)



			If cbBatch.Checked Then
				Await SubmitBatchMessages()
			Else
				Dim dstAddresses() As String = tbDestAdr.Text.Split(","c)

				If dstAddresses.Length = 1 Then
				   Await SubmitSingleMessage()
				ElseIf dstAddresses.Length > 1 Then
				   Await SubmitMultiMessage(dstAddresses)
				End If
			End If

			bSubmit.Enabled = True
		End Sub

		Private Function GetSpeedLimit(ByVal text As String) As LimitRate
			If String.IsNullOrWhiteSpace(tbSubmitSpeed.Text) Then
				Return LimitRate.NoLimit
			End If

			Dim occurrences As Integer = Nothing
			If Not Integer.TryParse(tbSubmitSpeed.Text, occurrences) OrElse occurrences = 0 Then
				Return LimitRate.NoLimit
			End If

			Return New LimitRate(occurrences, TimeSpan.FromSeconds(1))

		End Function

		Private Async Function SubmitSingleMessage() As Task
			Dim coding As DataCodings = GetDataCoding()



			Dim sourceAddress = New SmeAddress(tbSrcAdr.Text, CType(Byte.Parse(tbSrcAdrTON.Text), AddressTON), CType(Byte.Parse(tbSrcAdrNPI.Text), AddressNPI))

			Dim destinationAddress = New SmeAddress(tbDestAdr.Text, CType(Byte.Parse(tbDestAdrTON.Text), AddressTON), CType(Byte.Parse(tbDestAdrNPI.Text), AddressNPI))

			_log.Info("Submit message To: {0}. Text: {1}", tbDestAdr.Text, tbMessageText.Text)


			Dim builder As ISubmitSmBuilder = SMS.ForSubmit().From(sourceAddress).To(destinationAddress).Coding(coding).Text(tbMessageText.Text).ExpireIn(TimeSpan.FromDays(2)).DeliveryReceipt()
			'Add custom TLV parameter
			'.AddParameter(0x1403, "free")

			'Change SubmitSm sequence to your own number.
			'.Set(delegate(SubmitSm sm) { sm.Sequence = _client.SequenceGenerator.NextSequenceNumber();})

			Dim mode As SubmitMode = GetSubmitMode()
			Select Case mode
				Case SubmitMode.Payload
					builder.MessageInPayload()
				Case SubmitMode.ShortMessageWithSAR
					builder.ConcatenationInSAR()
			End Select

			Try
				Dim resp As IList(Of SubmitSmResp) = Await _client.Submit(builder)

				If resp.All(Function(x) x.Header.Status = CommandStatus.ESME_ROK) Then
					_log.Info("Submit succeeded. MessageIds: {0}", String.Join(",", resp.Select(Function(x) x.MessageId)))
				Else
					_log.Warn("Submit failed. Status: {0}", String.Join(",", resp.Select(Function(x) x.Header.Status.ToString())))
				End If
			Catch ex As Exception
				_log.Error("Submit failed. Error: {0}", ex.Message)
			End Try

			' When you received success result, you can later query message status on SMSC 
			' if (resp.Count > 0 && resp[0].Status == CommandStatus.ESME_ROK)
			' {
			'     _log.Info("QuerySm for message " + resp[0].MessageId);
			'     QuerySmResp qresp = _client.Query(resp[0].MessageId,
			'         srcTon, srcNpi,srcAdr);
			' }
		End Function

		Private Async Function SubmitMultiMessage(ByVal dstAddresses() As String) As Task
			Dim coding As DataCodings = GetDataCoding()

			Dim srcTon As Byte = Byte.Parse(tbSrcAdrTON.Text)
			Dim srcNpi As Byte = Byte.Parse(tbSrcAdrNPI.Text)
			Dim srcAdr As String = tbSrcAdr.Text

			Dim dstTon As Byte = Byte.Parse(tbDestAdrTON.Text)
			Dim dstNpi As Byte = Byte.Parse(tbDestAdrNPI.Text)

			Dim builder As ISubmitMultiBuilder = SMS.ForSubmitMulti().From(srcAdr, CType(srcTon, AddressTON), CType(srcNpi, AddressNPI)).Coding(coding).Text(tbMessageText.Text).DeliveryReceipt()

			For Each dstAddress In dstAddresses
				If dstAddress Is Nothing OrElse dstAddress.Trim().Length = 0 Then
					Continue For
				End If

				builder.To(dstAddress.Trim(), CType(dstTon, AddressTON), CType(dstNpi, AddressNPI))
			Next dstAddress

			_log.Info("Submit message to several addresses: {0}. Text: {1}", String.Join(", ",dstAddresses), tbMessageText.Text)


			Dim mode As SubmitMode = GetSubmitMode()
			Select Case mode
				Case SubmitMode.Payload
					builder.MessageInPayload()
				Case SubmitMode.ShortMessageWithSAR
					builder.ConcatenationInSAR()
			End Select



			Try
				Dim resp As IList(Of SubmitMultiResp) = Await _client.Submit(builder)

				If resp.All(Function(x) x.Header.Status = CommandStatus.ESME_ROK) Then
					_log.Info("Submit succeeded. MessageIds: {0}", String.Join(",", resp.Select(Function(x) x.MessageId)))
				Else
					_log.Warn("Submit failed. Status: {0}", String.Join(",", resp.Select(Function(x) x.Header.Status.ToString())))
				End If
			Catch ex As Exception
				_log.Error("Submit failed. Error: {0}", ex.Message)
			End Try

		End Function




		Private Async Function SubmitBatchMessages() As Task
			Dim sourceAddress = New SmeAddress(tbSrcAdr.Text, CType(Byte.Parse(tbSrcAdrTON.Text), AddressTON), CType(Byte.Parse(tbSrcAdrNPI.Text), AddressNPI))

			Dim destinationAddress = New SmeAddress(tbDestAdr.Text, CType(Byte.Parse(tbDestAdrTON.Text), AddressTON), CType(Byte.Parse(tbDestAdrNPI.Text), AddressNPI))


			Dim messageText As String = tbMessageText.Text

			Dim mode As SubmitMode = GetSubmitMode()

			Dim coding As DataCodings = GetDataCoding()

			Dim count As Integer = Integer.Parse(tbRepeatTimes.Text)

			_log.Info("Submit message batch. Count: {0}. Text: {1}", count, messageText)

			' bulk sms test
			Dim batch As New List(Of SubmitSm)()
			For i As Integer = 0 To count - 1
				Dim builder As ISubmitSmBuilder = SMS.ForSubmit().Text(messageText).From(sourceAddress).To(destinationAddress).Coding(coding)

				Select Case mode
					Case SubmitMode.Payload
						builder.MessageInPayload()
					Case SubmitMode.ShortMessageWithSAR
						builder.ConcatenationInSAR()
				End Select

				batch.AddRange(builder.Create(_client))

			Next i




			Try
				Dim watch As Stopwatch = Stopwatch.StartNew()

				Dim resp = (Await _client.Submit(batch)).ToArray()

				watch.Stop()

				If resp.All(Function(x) x.Header.Status = CommandStatus.ESME_ROK) Then
					_log.Info("Batch sending completed. Submitted: {0}, Elapsed: {1} ms, Performance: {2} m/s", batch.Count, watch.ElapsedMilliseconds, batch.Count * 1000F / watch.ElapsedMilliseconds)
				Else
					Dim wrongStatuses = resp.Where(Function(x) x.Header.Status <> CommandStatus.ESME_ROK).Select(Function(x) x.Header.Status).Distinct()

					_log.Warn("Submit failed. Wrong Status: {0}", String.Join(", ", wrongStatuses))
				End If
			Catch ex As Exception
				_log.Error("Submit failed. Error: {0}", ex.Message)
			End Try




		End Function

		Private Function GetSubmitMode() As SubmitMode
			Return DirectCast(System.Enum.Parse(GetType(SubmitMode), cbSubmitMode.Text), SubmitMode)
		End Function

		Private Function GetDataCoding() As DataCodings
			Return DirectCast(System.Enum.Parse(GetType(DataCodings), cbDataCoding.Text), DataCodings)
		End Function

		Private Sub SmppClientDemo_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			cbSubmitMode.SelectedIndex = 1
			cbDataCoding.SelectedIndex = 0

			cbBindingMode.Items.Clear()
			For Each mode As ConnectionMode In System.Enum.GetValues(GetType(ConnectionMode))
				If mode = ConnectionMode.None Then
					Continue For
				End If
				cbBindingMode.Items.Add(mode)
			Next mode
			cbBindingMode.SelectedItem = ConnectionMode.Transceiver
		End Sub





		Private Sub cbAsync_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbBatch.CheckedChanged
			tbRepeatTimes.Enabled = cbBatch.Checked
		End Sub



		Private Sub bAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bAbout.Click
			Dim frm As New AboutSmppClientDemo()
			frm.ShowDialog()
		End Sub

		Private Sub SmppClientDemo_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			RemoveHandler _client.evDisconnected, AddressOf client_evDisconnected
			_client.Dispose()
		End Sub


		Public Delegate Sub SyncAction()

		Public Shared Sub Sync(ByVal control As Control, ByVal action As SyncAction)
			If control.InvokeRequired Then
				control.Invoke(action, New Object() { })
				Return
			End If

			action()
		End Sub

		Private Sub cbReconnect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbReconnect.CheckedChanged
			_client.ConnectionRecovery = cbReconnect.Checked


		End Sub


	End Class
End Namespace