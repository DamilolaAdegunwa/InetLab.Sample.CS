Partial Public Class SmppServerDemo
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
		Me.label19 = New System.Windows.Forms.Label()
		Me.cbSSL = New System.Windows.Forms.CheckBox()
		Me.label6 = New System.Windows.Forms.Label()
		Me.tbPort = New System.Windows.Forms.TextBox()
		Me.bStartServer = New System.Windows.Forms.Button()
		Me.groupBox2 = New System.Windows.Forms.GroupBox()
		Me.tbLog = New System.Windows.Forms.TextBox()
		Me.bStopServer = New System.Windows.Forms.Button()
		Me.comboCertList = New System.Windows.Forms.ComboBox()
		Me.bSendMessage = New System.Windows.Forms.Button()
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.bDisconnect = New System.Windows.Forms.Button()
		Me.lbClients = New System.Windows.Forms.ListBox()
		Me.groupBox2.SuspendLayout()
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' label19
		' 
		Me.label19.Location = New System.Drawing.Point(21, 56)
		Me.label19.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.label19.Name = "label19"
		Me.label19.Size = New System.Drawing.Size(175, 35)
		Me.label19.TabIndex = 34
		Me.label19.Text = "Use SSL"
		Me.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' cbSSL
		' 
		Me.cbSSL.AutoSize = True
		Me.cbSSL.Location = New System.Drawing.Point(217, 63)
		Me.cbSSL.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.cbSSL.Name = "cbSSL"
		Me.cbSSL.Size = New System.Drawing.Size(22, 21)
		Me.cbSSL.TabIndex = 33
		Me.cbSSL.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbSSL.CheckedChanged += new System.EventHandler(this.cbSSL_CheckedChanged);
		' 
		' label6
		' 
		Me.label6.Location = New System.Drawing.Point(21, 16)
		Me.label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(175, 35)
		Me.label6.TabIndex = 32
		Me.label6.Text = "Port"
		Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		' 
		' tbPort
		' 
		Me.tbPort.Location = New System.Drawing.Point(217, 16)
		Me.tbPort.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.tbPort.Name = "tbPort"
		Me.tbPort.Size = New System.Drawing.Size(172, 29)
		Me.tbPort.TabIndex = 31
		Me.tbPort.Text = "7777"
		' 
		' bStartServer
		' 
		Me.bStartServer.Location = New System.Drawing.Point(21, 119)
		Me.bStartServer.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.bStartServer.Name = "bStartServer"
		Me.bStartServer.Size = New System.Drawing.Size(131, 40)
		Me.bStartServer.TabIndex = 35
		Me.bStartServer.Text = "Start Server"
		Me.bStartServer.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bStartServer.Click += new System.EventHandler(this.bStartServer_Click);
		' 
		' groupBox2
		' 
		Me.groupBox2.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.groupBox2.Controls.Add(Me.tbLog)
		Me.groupBox2.Location = New System.Drawing.Point(10, 385)
		Me.groupBox2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.groupBox2.Size = New System.Drawing.Size(1181, 443)
		Me.groupBox2.TabIndex = 36
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Log"
		' 
		' tbLog
		' 
		Me.tbLog.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbLog.Font = New System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(238)))
		Me.tbLog.Location = New System.Drawing.Point(5, 27)
		Me.tbLog.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.tbLog.Multiline = True
		Me.tbLog.Name = "tbLog"
		Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.tbLog.Size = New System.Drawing.Size(1171, 411)
		Me.tbLog.TabIndex = 17
		' 
		' bStopServer
		' 
		Me.bStopServer.Enabled = False
		Me.bStopServer.Location = New System.Drawing.Point(163, 119)
		Me.bStopServer.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.bStopServer.Name = "bStopServer"
		Me.bStopServer.Size = New System.Drawing.Size(131, 40)
		Me.bStopServer.TabIndex = 37
		Me.bStopServer.Text = "Stop Server"
		Me.bStopServer.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bStopServer.Click += new System.EventHandler(this.bStopServer_Click);
		' 
		' comboCertList
		' 
		Me.comboCertList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.comboCertList.Enabled = False
		Me.comboCertList.FormattingEnabled = True
		Me.comboCertList.Location = New System.Drawing.Point(282, 54)
		Me.comboCertList.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.comboCertList.Name = "comboCertList"
		Me.comboCertList.Size = New System.Drawing.Size(602, 32)
		Me.comboCertList.TabIndex = 38
		' 
		' bSendMessage
		' 
		Me.bSendMessage.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.bSendMessage.Enabled = False
		Me.bSendMessage.Location = New System.Drawing.Point(957, 23)
		Me.bSendMessage.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.bSendMessage.Name = "bSendMessage"
		Me.bSendMessage.Size = New System.Drawing.Size(214, 40)
		Me.bSendMessage.TabIndex = 39
		Me.bSendMessage.Text = "Send Message"
		Me.bSendMessage.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bSendMessage.Click += new System.EventHandler(this.bSendMessage_Click);
		' 
		' groupBox1
		' 
		Me.groupBox1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.groupBox1.Controls.Add(Me.bDisconnect)
		Me.groupBox1.Controls.Add(Me.lbClients)
		Me.groupBox1.Controls.Add(Me.bSendMessage)
		Me.groupBox1.Location = New System.Drawing.Point(10, 170)
		Me.groupBox1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.groupBox1.Size = New System.Drawing.Size(1181, 205)
		Me.groupBox1.TabIndex = 41
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Connected Clients"
		' 
		' bDisconnect
		' 
		Me.bDisconnect.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.bDisconnect.Enabled = False
		Me.bDisconnect.Location = New System.Drawing.Point(816, 23)
		Me.bDisconnect.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.bDisconnect.Name = "bDisconnect"
		Me.bDisconnect.Size = New System.Drawing.Size(131, 40)
		Me.bDisconnect.TabIndex = 42
		Me.bDisconnect.Text = "Disconnect"
		Me.bDisconnect.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
		' 
		' lbClients
		' 
		Me.lbClients.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.lbClients.FormattingEnabled = True
		Me.lbClients.ItemHeight = 24
		Me.lbClients.Location = New System.Drawing.Point(5, 74)
		Me.lbClients.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.lbClients.Name = "lbClients"
		Me.lbClients.Size = New System.Drawing.Size(1163, 100)
		Me.lbClients.TabIndex = 41
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.lbClients.SelectedIndexChanged += new System.EventHandler(this.lbClients_SelectedIndexChanged);
		' 
		' SmppServerDemo
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(168F, 168F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
		Me.ClientSize = New System.Drawing.Size(1213, 849)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.comboCertList)
		Me.Controls.Add(Me.bStopServer)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.bStartServer)
		Me.Controls.Add(Me.label19)
		Me.Controls.Add(Me.cbSSL)
		Me.Controls.Add(Me.label6)
		Me.Controls.Add(Me.tbPort)
		Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
		Me.Name = "SmppServerDemo"
		Me.Text = "Inetlab.SMPP SmppServer Demo"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmppServerDemo_FormClosing);
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox2.PerformLayout()
		Me.groupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private label19 As System.Windows.Forms.Label
	Private WithEvents cbSSL As System.Windows.Forms.CheckBox
	Private label6 As System.Windows.Forms.Label
	Private tbPort As System.Windows.Forms.TextBox
	Private WithEvents bStartServer As System.Windows.Forms.Button
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private tbLog As System.Windows.Forms.TextBox
	Private WithEvents bStopServer As System.Windows.Forms.Button
	Private comboCertList As System.Windows.Forms.ComboBox
	Private WithEvents bSendMessage As System.Windows.Forms.Button
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private WithEvents lbClients As System.Windows.Forms.ListBox
	Private WithEvents bDisconnect As System.Windows.Forms.Button
End Class

