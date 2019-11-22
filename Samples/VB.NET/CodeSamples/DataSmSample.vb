Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Parameters
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class DataSmSample
		Public Async Function SendDataSm(ByVal client As SmppClient, ByVal text As String) As Task(Of DataSmResp)
			Dim dataSm As New DataSm()
			dataSm.SourceAddress = New SmeAddress("1111")
			dataSm.DestinationAddress = New SmeAddress("79171234567")
			dataSm.DataCoding = DataCodings.UCS2

			Dim data() As Byte = client.EncodingMapper.GetMessageBytes(text, dataSm.DataCoding)

			dataSm.Parameters.Add(New MessagePayloadParameter(data))

			Return Await client.SubmitData(dataSm)
		End Function
	End Class
End Namespace
