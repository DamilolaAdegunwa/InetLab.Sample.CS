Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms

Namespace SmppClientDemo
	Partial Public Class SmppClientDemo

		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing


		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.bConnect = New System.Windows.Forms.Button()
			Me.bDisconnect = New System.Windows.Forms.Button()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.label25 = New System.Windows.Forms.Label()
			Me.cbBindingMode = New System.Windows.Forms.ComboBox()
			Me.label24 = New System.Windows.Forms.Label()
			Me.cbSSL = New System.Windows.Forms.CheckBox()
			Me.label6 = New System.Windows.Forms.Label()
			Me.tbPort = New System.Windows.Forms.TextBox()
			Me.label7 = New System.Windows.Forms.Label()
			Me.tbHostname = New System.Windows.Forms.TextBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.tbSystemType = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.tbPassword = New System.Windows.Forms.TextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.tbSystemId = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.tbAddrNpi = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.tbAddrTon = New System.Windows.Forms.TextBox()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.tbLog = New System.Windows.Forms.TextBox()
			Me.bAbout = New System.Windows.Forms.Button()
			Me.tbMessageText = New System.Windows.Forms.TextBox()
			Me.tbSrcAdr = New System.Windows.Forms.TextBox()
			Me.tbSrcAdrNPI = New System.Windows.Forms.TextBox()
			Me.label8 = New System.Windows.Forms.Label()
			Me.tbSrcAdrTON = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.label10 = New System.Windows.Forms.Label()
			Me.label11 = New System.Windows.Forms.Label()
			Me.bSubmit = New System.Windows.Forms.Button()
			Me.label12 = New System.Windows.Forms.Label()
			Me.label13 = New System.Windows.Forms.Label()
			Me.tbDestAdrNPI = New System.Windows.Forms.TextBox()
			Me.label14 = New System.Windows.Forms.Label()
			Me.tbDestAdrTON = New System.Windows.Forms.TextBox()
			Me.tbDestAdr = New System.Windows.Forms.TextBox()
			Me.cbBatch = New System.Windows.Forms.CheckBox()
			Me.label15 = New System.Windows.Forms.Label()
			Me.tbServiceType = New System.Windows.Forms.TextBox()
			Me.label17 = New System.Windows.Forms.Label()
			Me.cbDataCoding = New System.Windows.Forms.ComboBox()
			Me.label16 = New System.Windows.Forms.Label()
			Me.cbSubmitMode = New System.Windows.Forms.ComboBox()
			Me.tbRepeatTimes = New System.Windows.Forms.TextBox()
			Me.label18 = New System.Windows.Forms.Label()
			Me.cbReconnect = New System.Windows.Forms.CheckBox()
			Me.tbSubmitSpeed = New System.Windows.Forms.TextBox()
			Me.label20 = New System.Windows.Forms.Label()
			Me.label21 = New System.Windows.Forms.Label()
			Me.label22 = New System.Windows.Forms.Label()
			Me.label23 = New System.Windows.Forms.Label()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.SuspendLayout()
			' 
			' bConnect
			' 
			Me.bConnect.Location = New System.Drawing.Point(18, 215)
			Me.bConnect.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.bConnect.Name = "bConnect"
			Me.bConnect.Size = New System.Drawing.Size(124, 38)
			Me.bConnect.TabIndex = 0
			Me.bConnect.Text = "Connect"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
			' 
			' bDisconnect
			' 
			Me.bDisconnect.Enabled = False
			Me.bDisconnect.Location = New System.Drawing.Point(152, 215)
			Me.bDisconnect.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.bDisconnect.Name = "bDisconnect"
			Me.bDisconnect.Size = New System.Drawing.Size(124, 38)
			Me.bDisconnect.TabIndex = 1
			Me.bDisconnect.Text = "Disconnect"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.label25)
			Me.groupBox1.Controls.Add(Me.cbBindingMode)
			Me.groupBox1.Controls.Add(Me.label24)
			Me.groupBox1.Controls.Add(Me.cbSSL)
			Me.groupBox1.Controls.Add(Me.label6)
			Me.groupBox1.Controls.Add(Me.tbPort)
			Me.groupBox1.Controls.Add(Me.label7)
			Me.groupBox1.Controls.Add(Me.tbHostname)
			Me.groupBox1.Controls.Add(Me.label5)
			Me.groupBox1.Controls.Add(Me.tbSystemType)
			Me.groupBox1.Controls.Add(Me.label4)
			Me.groupBox1.Controls.Add(Me.tbPassword)
			Me.groupBox1.Controls.Add(Me.label3)
			Me.groupBox1.Controls.Add(Me.tbSystemId)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Controls.Add(Me.tbAddrNpi)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.tbAddrTon)
			Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
			Me.groupBox1.Location = New System.Drawing.Point(0, 0)
			Me.groupBox1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.groupBox1.Size = New System.Drawing.Size(1180, 212)
			Me.groupBox1.TabIndex = 0
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Binding settings"
			' 
			' label25
			' 
			Me.label25.Location = New System.Drawing.Point(23, 164)
			Me.label25.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label25.Name = "label25"
			Me.label25.Size = New System.Drawing.Size(119, 33)
			Me.label25.TabIndex = 44
			Me.label25.Text = "Mode"
			Me.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' cbBindingMode
			' 
			Me.cbBindingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbBindingMode.FormattingEnabled = True
			Me.cbBindingMode.Items.AddRange(New Object() { "Transceiver", "Transmitter", "Receiver"})
			Me.cbBindingMode.Location = New System.Drawing.Point(164, 164)
			Me.cbBindingMode.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.cbBindingMode.Name = "cbBindingMode"
			Me.cbBindingMode.Size = New System.Drawing.Size(165, 32)
			Me.cbBindingMode.TabIndex = 5
			' 
			' label24
			' 
			Me.label24.Location = New System.Drawing.Point(637, 119)
			Me.label24.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label24.Name = "label24"
			Me.label24.Size = New System.Drawing.Size(58, 33)
			Me.label24.TabIndex = 31
			Me.label24.Text = "TON"
			Me.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' cbSSL
			' 
			Me.cbSSL.AutoSize = True
			Me.cbSSL.Location = New System.Drawing.Point(553, 30)
			Me.cbSSL.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.cbSSL.Name = "cbSSL"
			Me.cbSSL.Size = New System.Drawing.Size(117, 29)
			Me.cbSSL.TabIndex = 2
			Me.cbSSL.Text = "Use SSL"
			Me.cbSSL.UseVisualStyleBackColor = True
			' 
			' label6
			' 
			Me.label6.Location = New System.Drawing.Point(399, 28)
			Me.label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(49, 33)
			Me.label6.TabIndex = 28
			Me.label6.Text = "Port"
			Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbPort
			' 
			Me.tbPort.Location = New System.Drawing.Point(458, 30)
			Me.tbPort.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbPort.Name = "tbPort"
			Me.tbPort.Size = New System.Drawing.Size(65, 29)
			Me.tbPort.TabIndex = 1
			Me.tbPort.Text = "7777"
			' 
			' label7
			' 
			Me.label7.Location = New System.Drawing.Point(21, 28)
			Me.label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(133, 33)
			Me.label7.TabIndex = 26
			Me.label7.Text = "Hostname"
			Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbHostname
			' 
			Me.tbHostname.Location = New System.Drawing.Point(164, 30)
			Me.tbHostname.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbHostname.Name = "tbHostname"
			Me.tbHostname.Size = New System.Drawing.Size(221, 29)
			Me.tbHostname.TabIndex = 0
			Me.tbHostname.Text = "localhost"
			' 
			' label5
			' 
			Me.label5.Location = New System.Drawing.Point(483, 74)
			Me.label5.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(149, 33)
			Me.label5.TabIndex = 24
			Me.label5.Text = "System Type"
			Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbSystemType
			' 
			Me.tbSystemType.Location = New System.Drawing.Point(642, 74)
			Me.tbSystemType.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbSystemType.Name = "tbSystemType"
			Me.tbSystemType.Size = New System.Drawing.Size(165, 29)
			Me.tbSystemType.TabIndex = 6
			' 
			' label4
			' 
			Me.label4.Location = New System.Drawing.Point(21, 119)
			Me.label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(100, 33)
			Me.label4.TabIndex = 22
			Me.label4.Text = "Password"
			Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbPassword
			' 
			Me.tbPassword.Location = New System.Drawing.Point(164, 119)
			Me.tbPassword.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbPassword.Name = "tbPassword"
			Me.tbPassword.PasswordChar = "*"c
			Me.tbPassword.Size = New System.Drawing.Size(165, 29)
			Me.tbPassword.TabIndex = 4
			Me.tbPassword.Text = "password"
			' 
			' label3
			' 
			Me.label3.Location = New System.Drawing.Point(21, 75)
			Me.label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(133, 33)
			Me.label3.TabIndex = 20
			Me.label3.Text = "SystemId"
			Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbSystemId
			' 
			Me.tbSystemId.Location = New System.Drawing.Point(164, 74)
			Me.tbSystemId.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbSystemId.Name = "tbSystemId"
			Me.tbSystemId.Size = New System.Drawing.Size(165, 29)
			Me.tbSystemId.TabIndex = 3
			Me.tbSystemId.Text = "login"
			' 
			' label2
			' 
			Me.label2.Location = New System.Drawing.Point(774, 119)
			Me.label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(51, 33)
			Me.label2.TabIndex = 18
			Me.label2.Text = "NPI"
			Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbAddrNpi
			' 
			Me.tbAddrNpi.Location = New System.Drawing.Point(836, 119)
			Me.tbAddrNpi.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbAddrNpi.Name = "tbAddrNpi"
			Me.tbAddrNpi.Size = New System.Drawing.Size(41, 29)
			Me.tbAddrNpi.TabIndex = 8
			Me.tbAddrNpi.Text = "0"
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(483, 119)
			Me.label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(144, 33)
			Me.label1.TabIndex = 16
			Me.label1.Text = "Address"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbAddrTon
			' 
			Me.tbAddrTon.Location = New System.Drawing.Point(705, 117)
			Me.tbAddrTon.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbAddrTon.Name = "tbAddrTon"
			Me.tbAddrTon.Size = New System.Drawing.Size(51, 29)
			Me.tbAddrTon.TabIndex = 7
			Me.tbAddrTon.Text = "0"
			' 
			' groupBox2
			' 
			Me.groupBox2.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.groupBox2.Controls.Add(Me.tbLog)
			Me.groupBox2.Location = New System.Drawing.Point(0, 570)
			Me.groupBox2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.groupBox2.Size = New System.Drawing.Size(1180, 462)
			Me.groupBox2.TabIndex = 33
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
			Me.tbLog.Size = New System.Drawing.Size(1170, 430)
			Me.tbLog.TabIndex = 0
			' 
			' bAbout
			' 
			Me.bAbout.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.bAbout.Location = New System.Drawing.Point(1027, 219)
			Me.bAbout.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.bAbout.Name = "bAbout"
			Me.bAbout.Size = New System.Drawing.Size(124, 38)
			Me.bAbout.TabIndex = 3
			Me.bAbout.Text = "About"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bAbout.Click += new System.EventHandler(this.bAbout_Click);
			' 
			' tbMessageText
			' 
			Me.tbMessageText.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbMessageText.Location = New System.Drawing.Point(78, 165)
			Me.tbMessageText.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbMessageText.Multiline = True
			Me.tbMessageText.Name = "tbMessageText"
			Me.tbMessageText.Size = New System.Drawing.Size(1019, 89)
			Me.tbMessageText.TabIndex = 19
			Me.tbMessageText.Text = "test sms text"
			' 
			' tbSrcAdr
			' 
			Me.tbSrcAdr.Location = New System.Drawing.Point(228, 33)
			Me.tbSrcAdr.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbSrcAdr.Name = "tbSrcAdr"
			Me.tbSrcAdr.Size = New System.Drawing.Size(238, 29)
			Me.tbSrcAdr.TabIndex = 6
			Me.tbSrcAdr.Text = "MySMSService"
			' 
			' tbSrcAdrNPI
			' 
			Me.tbSrcAdrNPI.Location = New System.Drawing.Point(671, 33)
			Me.tbSrcAdrNPI.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbSrcAdrNPI.Name = "tbSrcAdrNPI"
			Me.tbSrcAdrNPI.Size = New System.Drawing.Size(37, 29)
			Me.tbSrcAdrNPI.TabIndex = 10
			Me.tbSrcAdrNPI.Text = "0"
			' 
			' label8
			' 
			Me.label8.Location = New System.Drawing.Point(489, 31)
			Me.label8.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(61, 33)
			Me.label8.TabIndex = 7
			Me.label8.Text = "TON"
			Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbSrcAdrTON
			' 
			Me.tbSrcAdrTON.Location = New System.Drawing.Point(561, 33)
			Me.tbSrcAdrTON.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbSrcAdrTON.Name = "tbSrcAdrTON"
			Me.tbSrcAdrTON.Size = New System.Drawing.Size(37, 29)
			Me.tbSrcAdrTON.TabIndex = 8
			Me.tbSrcAdrTON.Text = "5"
			' 
			' label9
			' 
			Me.label9.Location = New System.Drawing.Point(613, 31)
			Me.label9.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(51, 33)
			Me.label9.TabIndex = 9
			Me.label9.Text = "NPI"
			Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' label10
			' 
			Me.label10.Location = New System.Drawing.Point(15, 165)
			Me.label10.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(58, 33)
			Me.label10.TabIndex = 18
			Me.label10.Text = "Text"
			Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' label11
			' 
			Me.label11.Location = New System.Drawing.Point(15, 31)
			Me.label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(121, 33)
			Me.label11.TabIndex = 4
			Me.label11.Text = "Source"
			Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' bSubmit
			' 
			Me.bSubmit.Enabled = False
			Me.bSubmit.Location = New System.Drawing.Point(18, 528)
			Me.bSubmit.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.bSubmit.Name = "bSubmit"
			Me.bSubmit.Size = New System.Drawing.Size(194, 38)
			Me.bSubmit.TabIndex = 26
			Me.bSubmit.Text = "Submit"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
			' 
			' label12
			' 
			Me.label12.Location = New System.Drawing.Point(13, 68)
			Me.label12.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(121, 33)
			Me.label12.TabIndex = 11
			Me.label12.Text = "Destination"
			Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' label13
			' 
			Me.label13.Location = New System.Drawing.Point(613, 68)
			Me.label13.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(47, 33)
			Me.label13.TabIndex = 16
			Me.label13.Text = "NPI"
			Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbDestAdrNPI
			' 
			Me.tbDestAdrNPI.Location = New System.Drawing.Point(671, 70)
			Me.tbDestAdrNPI.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbDestAdrNPI.Name = "tbDestAdrNPI"
			Me.tbDestAdrNPI.Size = New System.Drawing.Size(37, 29)
			Me.tbDestAdrNPI.TabIndex = 17
			Me.tbDestAdrNPI.Text = "1"
			' 
			' label14
			' 
			Me.label14.Location = New System.Drawing.Point(489, 68)
			Me.label14.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(61, 33)
			Me.label14.TabIndex = 14
			Me.label14.Text = "TON"
			Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbDestAdrTON
			' 
			Me.tbDestAdrTON.Location = New System.Drawing.Point(561, 70)
			Me.tbDestAdrTON.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbDestAdrTON.Name = "tbDestAdrTON"
			Me.tbDestAdrTON.Size = New System.Drawing.Size(37, 29)
			Me.tbDestAdrTON.TabIndex = 15
			Me.tbDestAdrTON.Text = "1"
			' 
			' tbDestAdr
			' 
			Me.tbDestAdr.Location = New System.Drawing.Point(228, 70)
			Me.tbDestAdr.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbDestAdr.Name = "tbDestAdr"
			Me.tbDestAdr.Size = New System.Drawing.Size(238, 29)
			Me.tbDestAdr.TabIndex = 13
			Me.tbDestAdr.Text = "436641234567"
			' 
			' cbBatch
			' 
			Me.cbBatch.Location = New System.Drawing.Point(240, 532)
			Me.cbBatch.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.cbBatch.Name = "cbBatch"
			Me.cbBatch.Size = New System.Drawing.Size(196, 32)
			Me.cbBatch.TabIndex = 27
			Me.cbBatch.Text = "Batch submit"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBatch.CheckedChanged += new System.EventHandler(this.cbAsync_CheckedChanged);
			' 
			' label15
			' 
			Me.label15.Location = New System.Drawing.Point(741, 32)
			Me.label15.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label15.Name = "label15"
			Me.label15.Size = New System.Drawing.Size(91, 33)
			Me.label15.TabIndex = 22
			Me.label15.Text = "Service Type"
			Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbServiceType
			' 
			Me.tbServiceType.Location = New System.Drawing.Point(825, 31)
			Me.tbServiceType.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbServiceType.Name = "tbServiceType"
			Me.tbServiceType.Size = New System.Drawing.Size(238, 29)
			Me.tbServiceType.TabIndex = 23
			' 
			' label17
			' 
			Me.label17.Location = New System.Drawing.Point(15, 113)
			Me.label17.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label17.Name = "label17"
			Me.label17.Size = New System.Drawing.Size(119, 33)
			Me.label17.TabIndex = 20
			Me.label17.Text = "Data coding"
			Me.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' cbDataCoding
			' 
			Me.cbDataCoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbDataCoding.FormattingEnabled = True
			Me.cbDataCoding.Items.AddRange(New Object() { "Default", "Latin1", "OctetUnspecified", "UCS2", "UnicodeFlashSMS", "DefaultFlashSMS"})
			Me.cbDataCoding.Location = New System.Drawing.Point(134, 113)
			Me.cbDataCoding.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.cbDataCoding.Name = "cbDataCoding"
			Me.cbDataCoding.Size = New System.Drawing.Size(238, 32)
			Me.cbDataCoding.TabIndex = 21
			' 
			' label16
			' 
			Me.label16.Location = New System.Drawing.Point(436, 113)
			Me.label16.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(89, 33)
			Me.label16.TabIndex = 24
			Me.label16.Text = "Mode"
			Me.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' cbSubmitMode
			' 
			Me.cbSubmitMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbSubmitMode.FormattingEnabled = True
			Me.cbSubmitMode.Items.AddRange(New Object() { "Payload", "ShortMessage", "ShortMessageWithSAR"})
			Me.cbSubmitMode.Location = New System.Drawing.Point(520, 113)
			Me.cbSubmitMode.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.cbSubmitMode.Name = "cbSubmitMode"
			Me.cbSubmitMode.Size = New System.Drawing.Size(238, 32)
			Me.cbSubmitMode.TabIndex = 25
			' 
			' tbRepeatTimes
			' 
			Me.tbRepeatTimes.Enabled = False
			Me.tbRepeatTimes.Location = New System.Drawing.Point(448, 530)
			Me.tbRepeatTimes.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbRepeatTimes.Name = "tbRepeatTimes"
			Me.tbRepeatTimes.Size = New System.Drawing.Size(76, 29)
			Me.tbRepeatTimes.TabIndex = 28
			Me.tbRepeatTimes.Text = "1000"
			' 
			' label18
			' 
			Me.label18.AutoSize = True
			Me.label18.Location = New System.Drawing.Point(537, 537)
			Me.label18.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label18.Name = "label18"
			Me.label18.Size = New System.Drawing.Size(58, 25)
			Me.label18.TabIndex = 29
			Me.label18.Text = "times"
			' 
			' cbReconnect
			' 
			Me.cbReconnect.AutoSize = True
			Me.cbReconnect.Location = New System.Drawing.Point(298, 222)
			Me.cbReconnect.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.cbReconnect.Name = "cbReconnect"
			Me.cbReconnect.Size = New System.Drawing.Size(131, 29)
			Me.cbReconnect.TabIndex = 2
			Me.cbReconnect.Text = "Reconnect"
			Me.cbReconnect.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbReconnect.CheckedChanged += new System.EventHandler(this.cbReconnect_CheckedChanged);
			' 
			' tbSubmitSpeed
			' 
			Me.tbSubmitSpeed.Location = New System.Drawing.Point(766, 535)
			Me.tbSubmitSpeed.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.tbSubmitSpeed.Name = "tbSubmitSpeed"
			Me.tbSubmitSpeed.Size = New System.Drawing.Size(67, 29)
			Me.tbSubmitSpeed.TabIndex = 31
			Me.tbSubmitSpeed.Text = "0"
			' 
			' label20
			' 
			Me.label20.AutoSize = True
			Me.label20.Location = New System.Drawing.Point(620, 537)
			Me.label20.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(136, 25)
			Me.label20.TabIndex = 30
			Me.label20.Text = "Submit Speed"
			' 
			' label21
			' 
			Me.label21.AutoSize = True
			Me.label21.Location = New System.Drawing.Point(847, 537)
			Me.label21.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label21.Name = "label21"
			Me.label21.Size = New System.Drawing.Size(139, 25)
			Me.label21.TabIndex = 32
			Me.label21.Text = "messages/sec"
			' 
			' label22
			' 
			Me.label22.Location = New System.Drawing.Point(129, 31)
			Me.label22.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(98, 33)
			Me.label22.TabIndex = 5
			Me.label22.Text = "Address"
			Me.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' label23
			' 
			Me.label23.Location = New System.Drawing.Point(129, 68)
			Me.label23.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(98, 33)
			Me.label23.TabIndex = 12
			Me.label23.Text = "Address"
			Me.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.label23)
			Me.groupBox3.Controls.Add(Me.cbSubmitMode)
			Me.groupBox3.Controls.Add(Me.label22)
			Me.groupBox3.Controls.Add(Me.label16)
			Me.groupBox3.Controls.Add(Me.tbMessageText)
			Me.groupBox3.Controls.Add(Me.label10)
			Me.groupBox3.Controls.Add(Me.cbDataCoding)
			Me.groupBox3.Controls.Add(Me.label17)
			Me.groupBox3.Controls.Add(Me.tbDestAdr)
			Me.groupBox3.Controls.Add(Me.tbServiceType)
			Me.groupBox3.Controls.Add(Me.label15)
			Me.groupBox3.Controls.Add(Me.tbSrcAdr)
			Me.groupBox3.Controls.Add(Me.label12)
			Me.groupBox3.Controls.Add(Me.tbSrcAdrTON)
			Me.groupBox3.Controls.Add(Me.label13)
			Me.groupBox3.Controls.Add(Me.label8)
			Me.groupBox3.Controls.Add(Me.tbDestAdrNPI)
			Me.groupBox3.Controls.Add(Me.tbSrcAdrNPI)
			Me.groupBox3.Controls.Add(Me.label14)
			Me.groupBox3.Controls.Add(Me.label9)
			Me.groupBox3.Controls.Add(Me.tbDestAdrTON)
			Me.groupBox3.Controls.Add(Me.label11)
			Me.groupBox3.Location = New System.Drawing.Point(12, 258)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(1156, 262)
			Me.groupBox3.TabIndex = 34
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Submit settings"
			' 
			' SmppClientDemo
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(168F, 168F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
			Me.ClientSize = New System.Drawing.Size(1180, 1034)
			Me.Controls.Add(Me.label21)
			Me.Controls.Add(Me.label20)
			Me.Controls.Add(Me.tbSubmitSpeed)
			Me.Controls.Add(Me.label18)
			Me.Controls.Add(Me.tbRepeatTimes)
			Me.Controls.Add(Me.cbBatch)
			Me.Controls.Add(Me.bSubmit)
			Me.Controls.Add(Me.bAbout)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.Controls.Add(Me.bDisconnect)
			Me.Controls.Add(Me.bConnect)
			Me.Controls.Add(Me.cbReconnect)
			Me.Controls.Add(Me.groupBox3)
			Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
			Me.Name = "SmppClientDemo"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Inetlab.SMPP SmppClient Demo"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmppClientDemo_FormClosing);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.SmppClientDemo_Load);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		#End Region


		Private WithEvents bConnect As System.Windows.Forms.Button
		Private WithEvents bDisconnect As System.Windows.Forms.Button
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private label6 As System.Windows.Forms.Label
		Private tbPort As System.Windows.Forms.TextBox
		Private label7 As System.Windows.Forms.Label
		Private tbHostname As System.Windows.Forms.TextBox
		Private label5 As System.Windows.Forms.Label
		Private tbSystemType As System.Windows.Forms.TextBox
		Private label4 As System.Windows.Forms.Label
		Private tbPassword As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private tbSystemId As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private tbAddrNpi As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private tbAddrTon As System.Windows.Forms.TextBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private tbLog As TextBox
		Private WithEvents bAbout As System.Windows.Forms.Button
		Private tbMessageText As System.Windows.Forms.TextBox
		Private tbSrcAdr As System.Windows.Forms.TextBox
		Private label8 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private label11 As System.Windows.Forms.Label
		Private WithEvents bSubmit As System.Windows.Forms.Button
		Private label12 As System.Windows.Forms.Label
		Private label13 As System.Windows.Forms.Label
		Private tbDestAdrNPI As System.Windows.Forms.TextBox
		Private label14 As System.Windows.Forms.Label
		Private tbDestAdrTON As System.Windows.Forms.TextBox
		Private tbSrcAdrNPI As System.Windows.Forms.TextBox
		Private tbSrcAdrTON As System.Windows.Forms.TextBox
		Private tbDestAdr As System.Windows.Forms.TextBox
		Private WithEvents cbBatch As System.Windows.Forms.CheckBox
		Private label15 As System.Windows.Forms.Label
		Private tbServiceType As System.Windows.Forms.TextBox
		Private label17 As Label
		Private cbDataCoding As ComboBox
		Private label16 As Label
		Private cbSubmitMode As ComboBox
		Private tbRepeatTimes As TextBox
		Private label18 As Label
		Private cbSSL As CheckBox
		Private WithEvents cbReconnect As CheckBox
		Private tbSubmitSpeed As TextBox
		Private label20 As Label
		Private label21 As Label
		Private label22 As Label
		Private label23 As Label
		Private label24 As Label
		Private cbBindingMode As ComboBox
		Private label25 As Label
		Private groupBox3 As GroupBox
	End Class
End Namespace
