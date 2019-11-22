Imports System
Imports System.Windows.Forms

Namespace SmppClientDemo
	Public Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Public Sub Main()
			If Environment.OSVersion.Version.Major >= 6 Then
				SetProcessDPIAware()
			End If

			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New SmppClientDemo())
		End Sub

		<System.Runtime.InteropServices.DllImport("user32.dll")>
		Private Function SetProcessDPIAware() As Boolean
		End Function
	End Module
End Namespace