Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Headers
Imports Inetlab.SMPP.Parameters
Imports Inetlab.SMPP.PDU

Namespace CodeExamples
	Public Module SendSmsExamples
		Public Async Function SendText(ByVal client As SmppClient) As Task
		  Dim resp = Await client.Submit(SMS.ForSubmit().From("short_code").To("436641234567").Coding(DataCodings.UCS2).Text("test text"))

		End Function

		Public Async Function SendBinary(ByVal client As SmppClient) As Task
			Dim data() As Byte = ByteArray.FromHexString("FFFF002830006609EC592F55DCE9010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005C67")

			Dim resp = Await client.Submit(SMS.ForSubmit().From("short_code").To("436641234567").Data(data))
		End Function

		Public Async Function SendMessageToApplicationPort(ByVal client As SmppClient) As Task

			Dim resp = Await client.Submit(SMS.ForSubmit().From("short_code").To("436641234567").Text("test").Set(Sub(sm) sm.UserData.Headers.Add(New ApplicationPortAddressingScheme16bit(&H1579, &H0))))
		End Function

		Public Sub AddVendorSpecificParameter(ByVal sm As SubmitSm)
			'0x1400 - 0x3FFF Reserved for SMSC Vendor specific optional parameters
			Dim value() As Byte = { &H1}

			sm.Parameters.Add(New TLV(&H1410, value))
		End Sub


		Public Async Function SendSubmitSmWithUDH(ByVal client As SmppClient) As Task
			'If you have UserDataHeader as byte array. You can create SubmitSm manually and pass it to headers collection.

			Dim udh() As Byte = { 5, 0, 3, 50, 1, 1}



			Dim sm As New SubmitSm()
			sm.SourceAddress = New SmeAddress("My Service")
			sm.DestinationAddress = New SmeAddress("+7917123456")
			sm.DataCoding = DataCodings.UCS2
			sm.RegisteredDelivery = 1

			sm.UserData.ShortMessage = client.EncodingMapper.GetMessageBytes("test message", sm.DataCoding)
			sm.UserData.Headers = udh

			Dim resp = Await client.Submit(sm)
		End Function

	End Module
End Namespace
