Partial Public Class SendMessage
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.label17 = New System.Windows.Forms.Label()
		Me.cbDataCoding = New System.Windows.Forms.ComboBox()
		Me.label12 = New System.Windows.Forms.Label()
		Me.label13 = New System.Windows.Forms.Label()
		Me.tbDestAdrNPI = New System.Windows.Forms.TextBox()
		Me.label14 = New System.Windows.Forms.Label()
		Me.tbDestAdrTON = New System.Windows.Forms.TextBox()
		Me.tbDestAdr = New System.Windows.Forms.TextBox()
		Me.bSubmit = New System.Windows.Forms.Button()
		Me.label11 = New System.Windows.Forms.Label()
		Me.label10 = New System.Windows.Forms.Label()
		Me.label9 = New System.Windows.Forms.Label()
		Me.tbSrcAdrNPI = New System.Windows.Forms.TextBox()
		Me.label8 = New System.Windows.Forms.Label()
		Me.tbSrcAdrTON = New System.Windows.Forms.TextBox()
		Me.tbSrcAdr = New System.Windows.Forms.TextBox()
		Me.tbSend = New System.Windows.Forms.TextBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.lClient = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		' 
		' label17
		' 
		Me.label17.Location = New System.Drawing.Point(9, 113)
		Me.label17.Name = "label17"
		Me.label17.Size = New System.Drawing.Size(71, 20)
		Me.label17.TabIndex = 58
		Me.label17.Text = "Data Coding"
		Me.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' cbDataCoding
		' 
		Me.cbDataCoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbDataCoding.FormattingEnabled = True
		Me.cbDataCoding.Items.AddRange(New Object() { "Default", "Latin1", "OctetUnspecified", "UCS2", "UnicodeFlashSMS", "DefaultFlashSMS"})
		Me.cbDataCoding.Location = New System.Drawing.Point(81, 112)
		Me.cbDataCoding.Name = "cbDataCoding"
		Me.cbDataCoding.Size = New System.Drawing.Size(144, 21)
		Me.cbDataCoding.TabIndex = 57
		' 
		' label12
		' 
		Me.label12.Location = New System.Drawing.Point(9, 86)
		Me.label12.Name = "label12"
		Me.label12.Size = New System.Drawing.Size(72, 20)
		Me.label12.TabIndex = 56
		Me.label12.Text = "Dest_Addr"
		Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' label13
		' 
		Me.label13.Location = New System.Drawing.Point(369, 86)
		Me.label13.Name = "label13"
		Me.label13.Size = New System.Drawing.Size(96, 20)
		Me.label13.TabIndex = 55
		Me.label13.Text = "Dest_Addr_NPI"
		Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' tbDestAdrNPI
		' 
		Me.tbDestAdrNPI.Location = New System.Drawing.Point(465, 86)
		Me.tbDestAdrNPI.Name = "tbDestAdrNPI"
		Me.tbDestAdrNPI.Size = New System.Drawing.Size(24, 20)
		Me.tbDestAdrNPI.TabIndex = 54
		Me.tbDestAdrNPI.Text = "1"
		' 
		' label14
		' 
		Me.label14.Location = New System.Drawing.Point(233, 86)
		Me.label14.Name = "label14"
		Me.label14.Size = New System.Drawing.Size(104, 20)
		Me.label14.TabIndex = 53
		Me.label14.Text = "Dest_Addr_TON"
		Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' tbDestAdrTON
		' 
		Me.tbDestAdrTON.Location = New System.Drawing.Point(337, 86)
		Me.tbDestAdrTON.Name = "tbDestAdrTON"
		Me.tbDestAdrTON.Size = New System.Drawing.Size(24, 20)
		Me.tbDestAdrTON.TabIndex = 52
		Me.tbDestAdrTON.Text = "1"
		' 
		' tbDestAdr
		' 
		Me.tbDestAdr.Location = New System.Drawing.Point(81, 86)
		Me.tbDestAdr.Name = "tbDestAdr"
		Me.tbDestAdr.Size = New System.Drawing.Size(144, 20)
		Me.tbDestAdr.TabIndex = 51
		' 
		' bSubmit
		' 
		Me.bSubmit.Location = New System.Drawing.Point(373, 140)
		Me.bSubmit.Name = "bSubmit"
		Me.bSubmit.Size = New System.Drawing.Size(116, 23)
		Me.bSubmit.TabIndex = 50
		Me.bSubmit.Text = "Submit"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
		' 
		' label11
		' 
		Me.label11.Location = New System.Drawing.Point(9, 62)
		Me.label11.Name = "label11"
		Me.label11.Size = New System.Drawing.Size(72, 20)
		Me.label11.TabIndex = 49
		Me.label11.Text = "Source_Addr"
		Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' label10
		' 
		Me.label10.Location = New System.Drawing.Point(9, 38)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(40, 20)
		Me.label10.TabIndex = 48
		Me.label10.Text = "Text"
		Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' label9
		' 
		Me.label9.Location = New System.Drawing.Point(369, 62)
		Me.label9.Name = "label9"
		Me.label9.Size = New System.Drawing.Size(96, 20)
		Me.label9.TabIndex = 47
		Me.label9.Text = "Source_Addr_NPI"
		Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' tbSrcAdrNPI
		' 
		Me.tbSrcAdrNPI.Location = New System.Drawing.Point(465, 62)
		Me.tbSrcAdrNPI.Name = "tbSrcAdrNPI"
		Me.tbSrcAdrNPI.Size = New System.Drawing.Size(24, 20)
		Me.tbSrcAdrNPI.TabIndex = 46
		Me.tbSrcAdrNPI.Text = "0"
		' 
		' label8
		' 
		Me.label8.Location = New System.Drawing.Point(233, 62)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(104, 20)
		Me.label8.TabIndex = 45
		Me.label8.Text = "Source_Addr_TON"
		Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' tbSrcAdrTON
		' 
		Me.tbSrcAdrTON.Location = New System.Drawing.Point(337, 62)
		Me.tbSrcAdrTON.Name = "tbSrcAdrTON"
		Me.tbSrcAdrTON.Size = New System.Drawing.Size(24, 20)
		Me.tbSrcAdrTON.TabIndex = 44
		Me.tbSrcAdrTON.Text = "0"
		' 
		' tbSrcAdr
		' 
		Me.tbSrcAdr.Location = New System.Drawing.Point(81, 62)
		Me.tbSrcAdr.Name = "tbSrcAdr"
		Me.tbSrcAdr.Size = New System.Drawing.Size(144, 20)
		Me.tbSrcAdr.TabIndex = 43
		' 
		' tbSend
		' 
		Me.tbSend.Location = New System.Drawing.Point(81, 38)
		Me.tbSend.MaxLength = 0
		Me.tbSend.Name = "tbSend"
		Me.tbSend.Size = New System.Drawing.Size(408, 20)
		Me.tbSend.TabIndex = 42
		' 
		' label1
		' 
		Me.label1.Location = New System.Drawing.Point(9, 12)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(71, 20)
		Me.label1.TabIndex = 60
		Me.label1.Text = "Client"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' lClient
		' 
		Me.lClient.Location = New System.Drawing.Point(80, 12)
		Me.lClient.Name = "lClient"
		Me.lClient.Size = New System.Drawing.Size(71, 20)
		Me.lClient.TabIndex = 61
		Me.lClient.Text = "Client"
		Me.lClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' SendMessage
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(96, 96)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
		Me.ClientSize = New System.Drawing.Size(502, 177)
		Me.Controls.Add(Me.lClient)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.label17)
		Me.Controls.Add(Me.cbDataCoding)
		Me.Controls.Add(Me.label12)
		Me.Controls.Add(Me.label13)
		Me.Controls.Add(Me.tbDestAdrNPI)
		Me.Controls.Add(Me.label14)
		Me.Controls.Add(Me.tbDestAdrTON)
		Me.Controls.Add(Me.tbDestAdr)
		Me.Controls.Add(Me.bSubmit)
		Me.Controls.Add(Me.label11)
		Me.Controls.Add(Me.label10)
		Me.Controls.Add(Me.label9)
		Me.Controls.Add(Me.tbSrcAdrNPI)
		Me.Controls.Add(Me.label8)
		Me.Controls.Add(Me.tbSrcAdrTON)
		Me.Controls.Add(Me.tbSrcAdr)
		Me.Controls.Add(Me.tbSend)
		Me.Name = "SendMessage"
		Me.Text = "SendMessage"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.SendMessage_Load);
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private label17 As System.Windows.Forms.Label
	Private cbDataCoding As System.Windows.Forms.ComboBox
	Private label12 As System.Windows.Forms.Label
	Private label13 As System.Windows.Forms.Label
	Private tbDestAdrNPI As System.Windows.Forms.TextBox
	Private label14 As System.Windows.Forms.Label
	Private tbDestAdrTON As System.Windows.Forms.TextBox
	Private tbDestAdr As System.Windows.Forms.TextBox
	Private WithEvents bSubmit As System.Windows.Forms.Button
	Private label11 As System.Windows.Forms.Label
	Private label10 As System.Windows.Forms.Label
	Private label9 As System.Windows.Forms.Label
	Private tbSrcAdrNPI As System.Windows.Forms.TextBox
	Private label8 As System.Windows.Forms.Label
	Private tbSrcAdrTON As System.Windows.Forms.TextBox
	Private tbSrcAdr As System.Windows.Forms.TextBox
	Private tbSend As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
	Private lClient As System.Windows.Forms.Label
End Class