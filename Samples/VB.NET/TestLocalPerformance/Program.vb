Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging

Friend Class Program
	Shared Sub Main(ByVal args() As String)

		LogManager.SetLoggerFactory(New ConsoleLogFactory(LogLevel.Info))

		StartApp().ConfigureAwait(False)

		Console.ReadLine()
	End Sub

	Public Shared Async Function StartApp() As Task

		Using server As New SmppServer(New IPEndPoint(IPAddress.Any, 7777))
			AddHandler server.evClientBind, Sub(sender, client, data)
			End Sub
			AddHandler server.evClientSubmitSm, Sub(sender, client, data)
			End Sub
			server.Start()

			Using client As New SmppClient()
				Await client.Connect("localhost", 7777)

				Await client.Bind("username", "password")

				Console.WriteLine("Performance: " & Await RunTest(client, 50000) & " m/s")
			End Using
		End Using
	End Function

	Public Shared Async Function RunTest(ByVal client As SmppClient, ByVal messagesNumber As Integer) As Task(Of Integer)

		Dim tasks As New List(Of Task)()

		Dim watch As Stopwatch = Stopwatch.StartNew()

		For i As Integer = 0 To messagesNumber - 1
			tasks.Add(client.Submit(SMS.ForSubmit().From("111").To("222").Coding(DataCodings.UCS2).Text("test")))
		Next i

		Await Task.WhenAll(tasks)

		watch.Stop()

		Return Convert.ToInt32(messagesNumber / watch.Elapsed.TotalSeconds)

	End Function
End Class
