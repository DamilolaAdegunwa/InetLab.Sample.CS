Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common
Imports Inetlab.SMPP.Logging
Imports Inetlab.SMPP.PDU

Namespace CodeSamples
	Public Class MessageComposerSample
		Private _log As ILog = LogManager.GetLogger(GetType(MessageComposerSample).FullName)
		'<EventsSample>
		Private ReadOnly _client As New SmppClient()
		Private ReadOnly _composer As New MessageComposer()

		Public Sub New()
			AddHandler _client.evDeliverSm, AddressOf client_evDeliverSm

			AddHandler _composer.evFullMessageReceived, AddressOf OnFullMessageReceived
			AddHandler _composer.evFullMessageTimeout, AddressOf OnFullMessageTimedout
		End Sub

		Private Sub client_evDeliverSm(ByVal sender As Object, ByVal data As DeliverSm)
			_composer.AddMessage(data)
		End Sub

		Private Sub OnFullMessageTimedout(ByVal sender As Object, ByVal args As MessageEventHandlerArgs)
			Dim pdu As DeliverSm = args.GetFirst(Of DeliverSm)()
			_log.Info(String.Format("Incomplete message received from {0}", pdu.SourceAddress))
		End Sub

		Private Sub OnFullMessageReceived(ByVal sender As Object, ByVal args As MessageEventHandlerArgs)
			Dim pdu As DeliverSm = args.GetFirst(Of DeliverSm)()
			_log.Info(String.Format("Full message received from {0}: {1}", pdu.SourceAddress, args.Text))
		End Sub
		'</EventsSample>


		'<InlineSample>

		Private Sub client_evDeliverSmInline(ByVal sender As Object, ByVal data As DeliverSm)
			_composer.AddMessage(data)
			If _composer.IsLastSegment(data) Then
			   Dim receivedText As String = _composer.GetFullMessage(data)
			End If
		End Sub

		'</InlineSample>
	End Class
End Namespace
