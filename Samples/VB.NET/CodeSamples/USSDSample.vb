Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Parameters
Imports Inetlab.SMPP.PDU

Namespace CodeSamples

	''' <summary> The sample with HUAWEI USSD protocol </summary>
	Public Class USSDSample

		Private _client As SmppClient

		Public Sub New(ByVal client As SmppClient)
			_client = client
			TLVCollection.RegisterParameter(Of UssdServiceOpParameter)(&H501)

			AddHandler _client.evDeliverSm, AddressOf OnMessageReceivedFromMS

		End Sub

		Private Sub OnMessageReceivedFromMS(ByVal sender As Object, ByVal data As DeliverSm)
			If data.ServiceType = "USSD" Then


			   Dim opParameter = data.Parameters.Of(Of UssdServiceOpParameter)().FirstOrDefault()
			   If opParameter IsNot Nothing Then

				   If opParameter.Operation Is USSDOperation.PSSRIndication Then
					   'Begin Message
					   'The operation type can only be Request USSDOperation.USSRRequest or USSDOperation.USSNRequest.

				   ElseIf opParameter.Operation Is USSDOperation.USSRConfirm Then
						'Continue message
						'The operation type can only be Response. In the subsequent message exchange, the MS can only respond to the Request or Notify message of the ESME.
						'USSDOperation.USSRRequest or USSDOperation.USSNRequest??
				   ElseIf opParameter.Operation Is USSDOperation.PSSRResponse OrElse opParameter.Operation Is USSDOperation.USSNConfirm Then
					   'End Message
					   'The operation type can only be Response.
					   ' 
						'USSDOperation.PSSRResponse
				   End If
			   End If

			End If


		End Sub

		Public Sub SendBeginMessage()
			'When an ESME sends a Begin message, the operation type is Request or Notify.

		End Sub

		Public Sub SendContinueMessage()
			'When an ESME sends a Continue message, the operation type is Request or Notify.
		End Sub

		Public Sub SendEndMessage()
			'The End message can only be sent by an ESME to the USSDC. The message indicates the end of a USSD session.
			'  −	If the session is initiated by an ESME, the operation type must be Release.In this case, the contents of USSDString are ignored.
			'The ESME sends the End message only when it receives a Begin or Continue message from the MS.

		End Sub

		Public Sub SendAbortMessage()
			'During a session, an ESME or MS can send an Abort message to end the session any time.
		End Sub

		Public Sub SendCommmand()
			Dim submitSm As New SubmitSm()
			submitSm.ServiceType = "USSD"
			submitSm.Parameters.Add(New UssdServiceOpParameter(USSDOperation.USSRRequest))
			submitSm.Parameters.Add(New TLV(OptionalTags.ItsSessionInfo, New Byte() { 11 }))
			submitSm.Parameters.Add(New TLV(OptionalTags.MoreMessagesToSend, New Byte() { 1 }))
		End Sub

		Public Function SendCommmand2() As Task
			Dim shortCode As String = "7777"

	   '     _client.EsmeAddress = new SmeAddress(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private);

			_client.Bind("username", "password", ConnectionMode.Transceiver)


		   Return _client.Submit(SMS.ForSubmit().From(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private).To("+79171234567").Text("USSD text").Coding(DataCodings.Class1MEMessage8bit).AddParameter(New UssdServiceOpParameter(USSDOperation.USSRRequest)).AddParameter(OptionalTags.ItsSessionInfo, New Byte() { 11 }))

		End Function

		Public Sub SendCommmand3()
			Dim shortCode As String = "7777"


			_client.Bind("username", "password", ConnectionMode.Transceiver)


			_client.Submit(SMS.ForSubmit().ServiceType("USSD").From(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private).To("+79171234567").Text("USSD text").Coding(CType(&H15, DataCodings)).AddParameter(New UssdServiceOpParameter(USSDOperation.USSRRequest)).AddParameter(&H4001, Encoding.ASCII.GetBytes("111111111111")))

		End Sub

		Public Sub SendPSSRResponse()
			Dim shortCode As String = "7777"
			Dim ussdSessionId As UShort = 0
			Dim sessionEnd As Boolean = True

			_client.Submit(SMS.ForSubmit().ServiceType("USSD").From(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private).To("+79171234567").Text("USSD text").Coding(CType(&H15, DataCodings)).AddParameter(OptionalTags.UssdServiceOp, New Byte() { 17 }).AddParameter(OptionalTags.ItsSessionInfo, GetItsSessionInfoValue(ussdSessionId, sessionEnd)))
		End Sub

		Public Sub GetItsSessionInfoFromDeliverSm(ByVal deliverSm As DeliverSm)
			Dim bytes() As Byte = deliverSm.Parameters(OptionalTags.ItsSessionInfo)
			Dim id As UShort = GetUssdSessionId(bytes)
			Dim [end] As Boolean = GetUssdSessionEnd(bytes)
		End Sub

		Private Function GetUssdSessionId(ByVal bytes() As Byte) As UShort
			Return BitConverter.ToUInt16( { bytes(0), CByte(CInt(bytes(1)) >> 1) }, 0)
		End Function

		Private Function GetUssdSessionEnd(ByVal bytes() As Byte) As Boolean
			Return (bytes(1) And &H1) = &H1
		End Function

		Private Function GetItsSessionInfoValue(ByVal ussdSessionId As UShort, ByVal sessionEnd As Boolean) As Byte()
			Dim bytes() As Byte = BitConverter.GetBytes(ussdSessionId)

			bytes(1) = CByte(CInt(bytes(1)) << 1)
			If sessionEnd Then
				bytes(1) = CByte(bytes(1) Or 1)
			End If

			Return bytes
		End Function

		Private Sub SendAnswerToHuaweiUSSDWithServiceType(ByVal deliverSm As DeliverSm)
			If deliverSm.ServiceType = "PSSRR" Then
				_client.Submit(SMS.ForSubmit().ServiceType("PSSRC").To(deliverSm.SourceAddress).Coding(deliverSm.DataCoding).Text("USSD response"))
			End If

			If deliverSm.ServiceType = "RELC" Then
				'USSD Dialogue has been released
			End If
		End Sub



	End Class

	Public Class ItsSessionInfoParameter
		Inherits TLV

		Public ReadOnly Property Session() As Byte
		Public ReadOnly Property Sequence() As Byte
		Public ReadOnly Property EndOfSession() As Boolean

		Public Sub New(ByVal session As Byte, ByVal sequence As Byte, ByVal endOfSession As Boolean)
			Me.Session = session
			Me.Sequence = sequence
			Me.EndOfSession = endOfSession

			TagValue = OptionalTags.ItsSessionInfo

			Value = New Byte(1){}
			Value(0) = session

			Value(1) = CByte(CInt(Value(1)) << 1)
			If endOfSession Then
				Value(1) = CByte(Value(1) Or 1)
			End If
		End Sub
	End Class

	Public Class UssdServiceOpParameter
		Inherits TLV

		Public ReadOnly Property Operation() As USSDOperation
		Public Sub New(ByVal operation As USSDOperation)
			TagValue = OptionalTags.UssdServiceOp
			Me.Operation = operation
			Value = New Byte() {operation.Value}
		End Sub

		'internal UssdServiceOpParameter(byte[] data)
		'{
		'    TagValue = OptionalTags.UssdServiceOp;
		'    Value = data;

		'    MessageId = BufferReader.ReadCString(data, Encoding.ASCII);
		'}
	End Class


	Partial Public Class USSDOperation
		Public Shared ReadOnly PSSDIndication As New USSDOperation(0)

		''' <summary> The Process Supplementary Service request (PSSR) from Mobile User to an application. </summary>
		Public Shared ReadOnly PSSRIndication As New USSDOperation(1)

		''' <summary> Unstructured Supplementary Service Request. Request from service and continue dialog.</summary>
		Public Shared ReadOnly USSRRequest As New USSDOperation(2)

		''' <summary> Unstructured Supplementary Service Notify. Notification from service without dialog.  </summary>
		''' <para>
		''' The Notify message differs from the Request message in that an Mobile User responds to a Notify message automatically.
		''' </para>
		Public Shared ReadOnly USSNRequest As New USSDOperation(3)

		Public Shared ReadOnly PSSDResponse As New USSDOperation(16)

		''' <summary> The response to Process Supplementary Service request (PSSRIndication). The reply from service. Dialog/Session ends. </summary>
		Public Shared ReadOnly PSSRResponse As New USSDOperation(17)

		''' <summary> Reply from MS.</summary>
		Public Shared ReadOnly USSRConfirm As New USSDOperation(18)

		Public Shared ReadOnly USSNConfirm As New USSDOperation(19)

		Public ReadOnly Property Value() As Byte

		Public Sub New(ByVal value As Byte)
			Me.Value = value
		End Sub

	End Class

	'Defined by Huawei
	Partial Public Class USSDOperation

		''' <summary> Session released by SP side when SP initiated USSD session </summary>
		Public Shared SessionReleaseBySP As New USSDOperation(32)

		''' <summary> Abort for exception </summary>
		Public Shared Abort As New USSDOperation(33)
	End Class



End Namespace
