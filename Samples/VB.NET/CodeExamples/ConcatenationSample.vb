Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Headers
Imports Inetlab.SMPP.Parameters
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class ConcatenationSample
		Private ReadOnly _config As New SmppConfig()
		Private ReadOnly _client As New SmppClient()

		' <SendConcatenatedMessageInUDH>
		Public Async Function SendConcatenatedMessageInUDH(ByVal message As TextMessage) As Task
			Dim builder = SMS.ForSubmit().From(_config.ShortCode, AddressTON.NetworkSpecific, AddressNPI.Unknown).To(message.PhoneNumber).Text(message.Text)

			Dim resp = Await _client.Submit(builder)
		End Function
		' </SendConcatenatedMessageInUDH>

		' <CreateSumbitSmWithConcatenationInUDH>
		Public Function CreateSumbitSmWithConcatenationInUDH(ByVal referenceNumber As UShort, ByVal totalParts As Byte, ByVal partNumber As Byte, ByVal textSegment As String) As SubmitSm

			Dim sm As New SubmitSm()
			sm.SourceAddress = New SmeAddress("1111")
			sm.DestinationAddress = New SmeAddress("79171234567")
			sm.DataCoding = DataCodings.Default
			sm.RegisteredDelivery = 1
			sm.UserData.ShortMessage = _client.EncodingMapper.GetMessageBytes(textSegment, sm.DataCoding)

			sm.UserData.Headers.Add(New ConcatenatedShortMessage16bit(referenceNumber, totalParts, partNumber))

			Return sm
		End Function
		' </CreateSumbitSmWithConcatenationInUDH>

		Public Async Function SendConcatenatedMessageWithSARParameters(ByVal message As TextMessage) As Task
			' <ConcatenationWithSARParameters>
			Dim builder = SMS.ForSubmit().From(_config.ShortCode, AddressTON.NetworkSpecific, AddressNPI.Unknown).To(message.PhoneNumber).Text(message.Text)

			builder.ConcatenationInSAR()

			Dim resp = Await _client.Submit(builder)

			' </ConcatenationWithSARParameters>
		End Function

		Public Async Function SendConcatenatedMessageInMessagePayload(ByVal message As TextMessage) As Task
			' <ConcatenationInMessagePayload>
			Dim builder = SMS.ForSubmit().From(_config.ShortCode, AddressTON.NetworkSpecific, AddressNPI.Unknown).To(message.PhoneNumber).Text(message.Text)

			builder.MessageInPayload()

			Dim resp = Await _client.Submit(builder)

			' </ConcatenationInMessagePayload>
		End Function

		' <GetConcatenationFromUDH>
		Public Function GetConcatenationFromUDH(ByVal data As SubmitSm) As Concatenation
			Dim udh8 As ConcatenatedShortMessages8bit = data.UserData.Headers.Of(Of ConcatenatedShortMessages8bit)().FirstOrDefault()

			If udh8 Is Nothing Then
				Return Nothing
			End If

			Return New Concatenation(udh8.ReferenceNumber, udh8.Total, udh8.SequenceNumber)
		End Function
		' </GetConcatenationFromUDH>


		' <GetConcatenationFromOptions>
		Public Function GetConcatenationFromTLVOptions(ByVal data As SubmitSm) As Concatenation
			Dim refNumber As UShort = 0
			Dim total As Byte = 0
			Dim seqNum As Byte = 0

			Dim referenceNumber = data.Parameters.Of(Of SARReferenceNumberParamter)().FirstOrDefault()
			If referenceNumber IsNot Nothing Then
				refNumber = referenceNumber.ReferenceNumber
			End If
			Dim totalSegments = data.Parameters.Of(Of SARTotalSegmentsParameter)().FirstOrDefault()
			If totalSegments IsNot Nothing Then
				total = totalSegments.TotalSegments
			End If
			Dim sequenceNumber = data.Parameters.Of(Of SARSequenceNumberParameter)().FirstOrDefault()
			If sequenceNumber IsNot Nothing Then
				seqNum = sequenceNumber.SequenceNumber
			End If

			Return New Concatenation(refNumber, total, seqNum)
		End Function
		' </GetConcatenationFromOptions>

	End Class

	Public Class TextMessage
		Public Property Id() As String
		Public Property PhoneNumber() As String
		Public Property Text() As String

		Public Property ServiceAddress() As String
	End Class

	Public Class SmppConfig
		Public Property ShortCode() As String
	End Class
End Namespace
