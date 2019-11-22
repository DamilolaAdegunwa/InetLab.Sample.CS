Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP

Namespace CodeExamples
	Public Module SendSmsExamples
		Public Async Function SendText(ByVal client As SmppClient) As Task
		  Dim resp = Await client.Submit(SMS.ForSubmit().From("short_code").To("436641234567").Text("test text"))
		End Function

		Public Async Function SendBinary(ByVal client As SmppClient) As Task
			Dim data() As Byte = ByteArray.FromHexString("FFFF002830006609EC592F55DCE9010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005C67")

			Dim resp = Await client.Submit(SMS.ForSubmit().From("short_code").To("436641234567").Data(data))
		End Function
	End Module
End Namespace
