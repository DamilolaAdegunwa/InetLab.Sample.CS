Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Security.Authentication
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Module SSLConnectionSample
		Public Async Function Run() As Task
			' <Sample>
			Using server As New SmppServer(New IPEndPoint(IPAddress.Any, 7777))
				server.EnabledSslProtocols = SslProtocols.Tls12
				server.ServerCertificate = New X509Certificate2("server_certificate.p12", "cert_password")

				server.Start()

				AddHandler server.evClientConnected, Sub(sender, client)
					Dim clientCertificate = client.ClientCertificate
					'You can validate client certificate and disconnect if it is not valid.
				End Sub

				Using client As New SmppClient()
					client.EnabledSslProtocols = SslProtocols.Tls12
					'if required you can be authenticated with client certificate
					client.ClientCertificates.Add(New X509Certificate2("client_certificate.p12", "cert_password"))

					If Await client.Connect("localhost", 7777) Then
						Dim bindResp As BindResp = Await client.Bind("username", "password")

						If bindResp.Header.Status = CommandStatus.ESME_ROK Then
							Dim submitResp = Await client.Submit(SMS.ForSubmit().From("111").To("436641234567").Coding(DataCodings.UCS2).Text("Hello World!"))

							If submitResp.All(Function(x) x.Header.Status = CommandStatus.ESME_ROK) Then
								client.Logger.Info("Message has been sent.")
							End If
						End If

						Await client.Disconnect()
					End If
				End Using
			End Using
			'</Sample>
		End Function
	End Module
End Namespace
