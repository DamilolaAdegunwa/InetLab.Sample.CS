Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class SubmitMultiSample
		Private ReadOnly _client As New SmppClient()

		Public Async Function SendToMultipleRecepients() As Task
			'<SendToMultipleRecepients>
		   Await _client.Submit(SMS.ForSubmitMulti().ServiceType("test").Text("Test Test").From("MyService").To("1111").To("2222").To("3333"))

			'</SendToMultipleRecepients>
		End Function

		Public Sub SendToPhoneNumbers(ByVal phoneNumbers As List(Of String))
			'<SendToPhoneNumbers>
			Dim pduBuilder = SMS.ForSubmitMulti().ServiceType("test").Text("Test Test").From("MyService")

			For Each phoneNumber As String In phoneNumbers
				pduBuilder.To(phoneNumber)
			Next phoneNumber

			'</SendToPhoneNumbers>
		End Sub

		Public Async Function SendToDestinationList() As Task
			'<SendToDestinationList>
			Dim destList As New List(Of IAddress)()

			destList.Add(New SmeAddress("11111111111", AddressTON.Unknown, AddressNPI.ISDN))
			destList.Add(New DistributionList("my_destribution_list_on_SMPP_Server"))

			Dim submitResponses = Await _client.Submit(SMS.ForSubmitMulti().ServiceType("test").Text("Test Test").From("MyService").ToDestinations(destList))
			'</SendToDestinationList>
		End Function
	End Class
End Namespace
