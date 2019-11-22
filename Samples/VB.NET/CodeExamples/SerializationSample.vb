Imports System
Imports System.IO
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.PDU

Namespace CodeExamples
	Public Class SerializationSample

		''' <summary> Deserialize this saved data to the SubmitSm </summary>
		'''
		''' <param name="client"> The client. </param>
		''' <param name="data">   The serialized SubmitSm PDU. </param>
		'''
		''' <returns> A SubmitSm. </returns>
		''' 
		Public Function Deserialize(ByVal client As SmppClient, ByVal data() As Byte) As SubmitSm
			Dim reader As New SmppReader(client.EncodingMapper)

			Return CType(reader.ReadPDU(data), SubmitSm)

		End Function



		''' <summary> Serialize SubmitSm object to the byte array. </summary>
		'''
		''' <param name="client"> The client. </param>
		''' <param name="pdu"> The SubmitSm object. </param>
		'''
		''' <returns> A byte array. </returns>
		Public Function Serialize(ByVal client As SmppClient, ByVal pdu As SubmitSm) As Byte()
			Using stream As New MemoryStream()
				Using writer As New SmppWriter(stream, client.EncodingMapper)
				   writer.WritePDU(pdu)

					Return stream.ToArray()
				End Using
			End Using
		End Function
	End Class
End Namespace
