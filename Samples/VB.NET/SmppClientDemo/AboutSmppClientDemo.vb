Imports System
Imports System.Diagnostics
Imports System.Reflection
Imports System.Windows.Forms

Namespace SmppClientDemo

	Public Class AboutSmppClientDemo
		Inherits Form

		Private WithEvents linkLabel1 As LinkLabel
		Private l1 As Label
		Private label1 As Label
		Private bOK As Button
		Private label2 As Label
		Private label3 As Label
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			Dim ver As New Version()

			InitializeComponent()
			label1.Text = "Version: " & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()

		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.linkLabel1 = New System.Windows.Forms.LinkLabel()
			Me.l1 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.bOK = New System.Windows.Forms.Button()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' linkLabel1
			' 
			Me.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top
			Me.linkLabel1.Location = New System.Drawing.Point(0, 96)
			Me.linkLabel1.Name = "linkLabel1"
			Me.linkLabel1.Size = New System.Drawing.Size(346, 32)
			Me.linkLabel1.TabIndex = 0
			Me.linkLabel1.TabStop = True
			Me.linkLabel1.Text = "https://www.inetlab.com/Products/Inetlab.SMPP.html"
			Me.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			' 
			' l1
			' 
			Me.l1.Dock = System.Windows.Forms.DockStyle.Top
			Me.l1.Location = New System.Drawing.Point(0, 0)
			Me.l1.Name = "l1"
			Me.l1.Size = New System.Drawing.Size(346, 32)
			Me.l1.TabIndex = 1
			Me.l1.Text = "Inetlab.SMPP SmppClient Demo"
			Me.l1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
			' 
			' label1
			' 
			Me.label1.Dock = System.Windows.Forms.DockStyle.Top
			Me.label1.Location = New System.Drawing.Point(0, 32)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(346, 16)
			Me.label1.TabIndex = 3
			Me.label1.Text = "Version"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' bOK
			' 
			Me.bOK.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.bOK.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.bOK.Location = New System.Drawing.Point(120, 160)
			Me.bOK.Name = "bOK"
			Me.bOK.Size = New System.Drawing.Size(112, 23)
			Me.bOK.TabIndex = 5
			Me.bOK.Text = "OK"
			' 
			' label2
			' 
			Me.label2.Dock = System.Windows.Forms.DockStyle.Top
			Me.label2.Location = New System.Drawing.Point(0, 48)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(346, 32)
			Me.label2.TabIndex = 6
			Me.label2.Text = "Copyright © 2006 - 2018 InetLab e.U."
			Me.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
			' 
			' label3
			' 
			Me.label3.Dock = System.Windows.Forms.DockStyle.Top
			Me.label3.Location = New System.Drawing.Point(0, 80)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(346, 16)
			Me.label3.TabIndex = 7
			Me.label3.Text = "All rights reserved."
			Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' AboutSmppClientDemo
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
			Me.ClientSize = New System.Drawing.Size(346, 194)
			Me.Controls.Add(Me.linkLabel1)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.bOK)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.l1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
			Me.Name = "AboutSmppClientDemo"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "About"
			Me.ResumeLayout(False)

		End Sub
		#End Region

		Private Sub linkLabel1_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles linkLabel1.LinkClicked
			Dim psi As New ProcessStartInfo("https://www.inetlab.com/Products/Inetlab.SMPP.html")
			psi.UseShellExecute = True
			Process.Start(psi)
		End Sub


	End Class
End Namespace
