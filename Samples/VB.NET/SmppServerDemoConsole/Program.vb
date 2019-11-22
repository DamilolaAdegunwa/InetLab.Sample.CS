Imports System
Imports System.Net
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU

Friend Class Program
	Private _server As SmppServer

	Shared Sub Main(ByVal args() As String)
		LogManager.SetLoggerFactory(New ConsoleLogFactory(LogLevel.Info))

		Console.Title = "SmppServer Demo"

		Dim p As New Program()
		p.Run()
		Console.ReadLine()
	End Sub

	Private Sub Run()
		_server = New SmppServer(New IPEndPoint(IPAddress.Any, 7777))


		AddHandler _server.evClientConnected, Sub(sender, client)
		End Sub
		AddHandler _server.evClientDisconnected, Sub(sender, client)
		End Sub
		AddHandler _server.evClientSubmitSm, AddressOf WhenServerReceivesPDU
		_server.Start()
	End Sub

	Private Async Sub WhenServerReceivesPDU(ByVal sender As Object, ByVal serverClient As SmppServerClient, ByVal data As SubmitSm)


		If data.RegisteredDelivery = 1 Then

			'Send Delivery Receipt when required
			Await serverClient.Deliver(SMS.ForDeliver().From(data.SourceAddress).To(data.DestinationAddress).Coding(data.DataCoding).Receipt(New Receipt With {
				.DoneDate = Date.Now,
				.State = MessageState.Delivered,
				.MessageId = data.Response.MessageId,
				.ErrorCode = "0",
				.SubmitDate = Date.Now
			})).ConfigureAwait(False)
		End If
	End Sub

End Class
