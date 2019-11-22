Imports System
Imports System.Linq
Imports System.Net
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU
Imports NLog.Config
Imports NLog.Targets

Friend Class Program
	Shared Sub Main(ByVal args() As String)
		Dim configuration As New LoggingConfiguration()
		Dim consoleTarget = New ConsoleTarget("console")

		configuration.AddTarget(consoleTarget)
		configuration.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, consoleTarget)

		NLog.LogManager.Configuration = configuration

		LogManager.SetLoggerFactory(New NLogLoggerFactory())


		SendHelloWorld().Wait()

		Console.ReadLine()
	End Sub

	' <SendHelloWorld>
	Public Shared Async Function SendHelloWorld() As Task

		Using server As New SmppServer(New IPEndPoint(IPAddress.Any, 7777))
			server.Start()

			Using client As New SmppClient()

				If Await client.Connect("localhost", 7777) Then
					Dim bindResp As BindResp = Await client.Bind("1", "2")

					If bindResp.Header.Status = CommandStatus.ESME_ROK Then
						Dim submitResp = Await client.Submit(SMS.ForSubmit().From("111").To("222").Coding(DataCodings.UCS2).Text("Hello World!"))

						If submitResp.All(Function(x) x.Header.Status = CommandStatus.ESME_ROK) Then
							client.Logger.Info("Message has been sent.")
						End If
					End If

					Await client.Disconnect()
				End If
			End Using
		End Using
	End Function
	'</SendHelloWorld>
End Class

