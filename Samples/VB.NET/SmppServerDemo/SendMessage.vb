Imports System
Imports System.Windows.Forms
Imports Inetlab.SMPP
Imports Inetlab.SMPP.Common

Partial Public Class SendMessage
	Inherits Form

	Private ReadOnly _client As SmppServerClient

	Public Sub New(ByVal client As SmppServerClient)
		InitializeComponent()

		_client = client

		If _client IsNot Nothing Then
			lClient.Text = _client.ToString()
		End If

	End Sub

	Private Async Sub bSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bSubmit.Click
		If _client IsNot Nothing Then
			Dim source As New SmeAddress(tbSrcAdr.Text, CType(Byte.Parse(tbSrcAdrTON.Text), AddressTON), CType(Byte.Parse(tbSrcAdrNPI.Text), AddressNPI))
			Dim destination As New SmeAddress(tbDestAdr.Text, CType(Byte.Parse(tbDestAdrTON.Text), AddressTON), CType(Byte.Parse(tbDestAdrNPI.Text), AddressNPI))

			Await _client.Deliver(SMS.ForDeliver().From(source).To(destination).Coding(GetDataCoding()).Text(tbSend.Text))

			DialogResult = System.Windows.Forms.DialogResult.OK
		End If

	End Sub

	Private Function GetDataCoding() As DataCodings
		Return DirectCast(System.Enum.Parse(GetType(DataCodings), cbDataCoding.Text), DataCodings)
	End Function

	Private Sub SendMessage_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		cbDataCoding.SelectedIndex = 0
	End Sub
End Class