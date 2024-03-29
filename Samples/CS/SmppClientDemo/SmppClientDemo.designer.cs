using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmppClientDemo
{
    public partial class SmppClientDemo 
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bConnect = new System.Windows.Forms.Button();
            this.bDisconnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cbBindingMode = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cbSSL = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbHostname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSystemType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSystemId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAddrNpi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAddrTon = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.bAbout = new System.Windows.Forms.Button();
            this.tbMessageText = new System.Windows.Forms.TextBox();
            this.tbSrcAdr = new System.Windows.Forms.TextBox();
            this.tbSrcAdrNPI = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSrcAdrTON = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.bSubmit = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbDestAdrNPI = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbDestAdrTON = new System.Windows.Forms.TextBox();
            this.tbDestAdr = new System.Windows.Forms.TextBox();
            this.cbBatch = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbServiceType = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbDataCoding = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbSubmitMode = new System.Windows.Forms.ComboBox();
            this.tbRepeatTimes = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbReconnect = new System.Windows.Forms.CheckBox();
            this.tbSubmitSpeed = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(18, 215);
            this.bConnect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(124, 38);
            this.bConnect.TabIndex = 0;
            this.bConnect.Text = "Connect";
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bDisconnect
            // 
            this.bDisconnect.Enabled = false;
            this.bDisconnect.Location = new System.Drawing.Point(152, 215);
            this.bDisconnect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bDisconnect.Name = "bDisconnect";
            this.bDisconnect.Size = new System.Drawing.Size(124, 38);
            this.bDisconnect.TabIndex = 1;
            this.bDisconnect.Text = "Disconnect";
            this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.cbBindingMode);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.cbSSL);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbHostname);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbSystemType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbSystemId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbAddrNpi);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbAddrTon);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Size = new System.Drawing.Size(1180, 212);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Binding settings";
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(23, 164);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(119, 33);
            this.label25.TabIndex = 44;
            this.label25.Text = "Mode";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbBindingMode
            // 
            this.cbBindingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBindingMode.FormattingEnabled = true;
            this.cbBindingMode.Items.AddRange(new object[] {
            "Transceiver",
            "Transmitter",
            "Receiver"});
            this.cbBindingMode.Location = new System.Drawing.Point(164, 164);
            this.cbBindingMode.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbBindingMode.Name = "cbBindingMode";
            this.cbBindingMode.Size = new System.Drawing.Size(165, 32);
            this.cbBindingMode.TabIndex = 5;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(637, 119);
            this.label24.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 33);
            this.label24.TabIndex = 31;
            this.label24.Text = "TON";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSSL
            // 
            this.cbSSL.AutoSize = true;
            this.cbSSL.Location = new System.Drawing.Point(553, 30);
            this.cbSSL.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbSSL.Name = "cbSSL";
            this.cbSSL.Size = new System.Drawing.Size(117, 29);
            this.cbSSL.TabIndex = 2;
            this.cbSSL.Text = "Use SSL";
            this.cbSSL.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(399, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 33);
            this.label6.TabIndex = 28;
            this.label6.Text = "Port";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(458, 30);
            this.tbPort.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(65, 29);
            this.tbPort.TabIndex = 1;
            this.tbPort.Text = "7777";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(21, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 33);
            this.label7.TabIndex = 26;
            this.label7.Text = "Hostname";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbHostname
            // 
            this.tbHostname.Location = new System.Drawing.Point(164, 30);
            this.tbHostname.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbHostname.Name = "tbHostname";
            this.tbHostname.Size = new System.Drawing.Size(221, 29);
            this.tbHostname.TabIndex = 0;
            this.tbHostname.Text = "localhost";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(483, 74);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 33);
            this.label5.TabIndex = 24;
            this.label5.Text = "System Type";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSystemType
            // 
            this.tbSystemType.Location = new System.Drawing.Point(642, 74);
            this.tbSystemType.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbSystemType.Name = "tbSystemType";
            this.tbSystemType.Size = new System.Drawing.Size(165, 29);
            this.tbSystemType.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(21, 119);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 33);
            this.label4.TabIndex = 22;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(164, 119);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(165, 29);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.Text = "password";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 33);
            this.label3.TabIndex = 20;
            this.label3.Text = "SystemId";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSystemId
            // 
            this.tbSystemId.Location = new System.Drawing.Point(164, 74);
            this.tbSystemId.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbSystemId.Name = "tbSystemId";
            this.tbSystemId.Size = new System.Drawing.Size(165, 29);
            this.tbSystemId.TabIndex = 3;
            this.tbSystemId.Text = "login";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(774, 119);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 33);
            this.label2.TabIndex = 18;
            this.label2.Text = "NPI";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbAddrNpi
            // 
            this.tbAddrNpi.Location = new System.Drawing.Point(836, 119);
            this.tbAddrNpi.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbAddrNpi.Name = "tbAddrNpi";
            this.tbAddrNpi.Size = new System.Drawing.Size(41, 29);
            this.tbAddrNpi.TabIndex = 8;
            this.tbAddrNpi.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(483, 119);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 33);
            this.label1.TabIndex = 16;
            this.label1.Text = "Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbAddrTon
            // 
            this.tbAddrTon.Location = new System.Drawing.Point(705, 117);
            this.tbAddrTon.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbAddrTon.Name = "tbAddrTon";
            this.tbAddrTon.Size = new System.Drawing.Size(51, 29);
            this.tbAddrTon.TabIndex = 7;
            this.tbAddrTon.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tbLog);
            this.groupBox2.Location = new System.Drawing.Point(0, 570);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox2.Size = new System.Drawing.Size(1180, 462);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLog.Location = new System.Drawing.Point(5, 27);
            this.tbLog.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(1170, 430);
            this.tbLog.TabIndex = 0;
            // 
            // bAbout
            // 
            this.bAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAbout.Location = new System.Drawing.Point(1027, 219);
            this.bAbout.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bAbout.Name = "bAbout";
            this.bAbout.Size = new System.Drawing.Size(124, 38);
            this.bAbout.TabIndex = 3;
            this.bAbout.Text = "About";
            this.bAbout.Click += new System.EventHandler(this.bAbout_Click);
            // 
            // tbMessageText
            // 
            this.tbMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessageText.Location = new System.Drawing.Point(78, 165);
            this.tbMessageText.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbMessageText.Multiline = true;
            this.tbMessageText.Name = "tbMessageText";
            this.tbMessageText.Size = new System.Drawing.Size(1019, 89);
            this.tbMessageText.TabIndex = 19;
            this.tbMessageText.Text = "test sms text";
            // 
            // tbSrcAdr
            // 
            this.tbSrcAdr.Location = new System.Drawing.Point(228, 33);
            this.tbSrcAdr.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbSrcAdr.Name = "tbSrcAdr";
            this.tbSrcAdr.Size = new System.Drawing.Size(238, 29);
            this.tbSrcAdr.TabIndex = 6;
            this.tbSrcAdr.Text = "MySMSService";
            // 
            // tbSrcAdrNPI
            // 
            this.tbSrcAdrNPI.Location = new System.Drawing.Point(671, 33);
            this.tbSrcAdrNPI.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbSrcAdrNPI.Name = "tbSrcAdrNPI";
            this.tbSrcAdrNPI.Size = new System.Drawing.Size(37, 29);
            this.tbSrcAdrNPI.TabIndex = 10;
            this.tbSrcAdrNPI.Text = "0";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(489, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 33);
            this.label8.TabIndex = 7;
            this.label8.Text = "TON";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSrcAdrTON
            // 
            this.tbSrcAdrTON.Location = new System.Drawing.Point(561, 33);
            this.tbSrcAdrTON.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbSrcAdrTON.Name = "tbSrcAdrTON";
            this.tbSrcAdrTON.Size = new System.Drawing.Size(37, 29);
            this.tbSrcAdrTON.TabIndex = 8;
            this.tbSrcAdrTON.Text = "5";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(613, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 33);
            this.label9.TabIndex = 9;
            this.label9.Text = "NPI";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(15, 165);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 33);
            this.label10.TabIndex = 18;
            this.label10.Text = "Text";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(15, 31);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 33);
            this.label11.TabIndex = 4;
            this.label11.Text = "Source";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bSubmit
            // 
            this.bSubmit.Enabled = false;
            this.bSubmit.Location = new System.Drawing.Point(18, 528);
            this.bSubmit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(194, 38);
            this.bSubmit.TabIndex = 26;
            this.bSubmit.Text = "Submit";
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(13, 68);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 33);
            this.label12.TabIndex = 11;
            this.label12.Text = "Destination";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(613, 68);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 33);
            this.label13.TabIndex = 16;
            this.label13.Text = "NPI";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDestAdrNPI
            // 
            this.tbDestAdrNPI.Location = new System.Drawing.Point(671, 70);
            this.tbDestAdrNPI.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbDestAdrNPI.Name = "tbDestAdrNPI";
            this.tbDestAdrNPI.Size = new System.Drawing.Size(37, 29);
            this.tbDestAdrNPI.TabIndex = 17;
            this.tbDestAdrNPI.Text = "1";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(489, 68);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 33);
            this.label14.TabIndex = 14;
            this.label14.Text = "TON";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDestAdrTON
            // 
            this.tbDestAdrTON.Location = new System.Drawing.Point(561, 70);
            this.tbDestAdrTON.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbDestAdrTON.Name = "tbDestAdrTON";
            this.tbDestAdrTON.Size = new System.Drawing.Size(37, 29);
            this.tbDestAdrTON.TabIndex = 15;
            this.tbDestAdrTON.Text = "1";
            // 
            // tbDestAdr
            // 
            this.tbDestAdr.Location = new System.Drawing.Point(228, 70);
            this.tbDestAdr.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbDestAdr.Name = "tbDestAdr";
            this.tbDestAdr.Size = new System.Drawing.Size(238, 29);
            this.tbDestAdr.TabIndex = 13;
            this.tbDestAdr.Text = "436641234567";
            // 
            // cbBatch
            // 
            this.cbBatch.Location = new System.Drawing.Point(240, 532);
            this.cbBatch.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbBatch.Name = "cbBatch";
            this.cbBatch.Size = new System.Drawing.Size(196, 32);
            this.cbBatch.TabIndex = 27;
            this.cbBatch.Text = "Batch submit";
            this.cbBatch.CheckedChanged += new System.EventHandler(this.cbAsync_CheckedChanged);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(741, 32);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 33);
            this.label15.TabIndex = 22;
            this.label15.Text = "Service Type";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbServiceType
            // 
            this.tbServiceType.Location = new System.Drawing.Point(825, 31);
            this.tbServiceType.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbServiceType.Name = "tbServiceType";
            this.tbServiceType.Size = new System.Drawing.Size(238, 29);
            this.tbServiceType.TabIndex = 23;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(15, 113);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 33);
            this.label17.TabIndex = 20;
            this.label17.Text = "Data coding";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDataCoding
            // 
            this.cbDataCoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataCoding.FormattingEnabled = true;
            this.cbDataCoding.Items.AddRange(new object[] {
            "Default",
            "Latin1",
            "OctetUnspecified",
            "UCS2",
            "UnicodeFlashSMS",
            "DefaultFlashSMS"});
            this.cbDataCoding.Location = new System.Drawing.Point(134, 113);
            this.cbDataCoding.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbDataCoding.Name = "cbDataCoding";
            this.cbDataCoding.Size = new System.Drawing.Size(238, 32);
            this.cbDataCoding.TabIndex = 21;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(436, 113);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 33);
            this.label16.TabIndex = 24;
            this.label16.Text = "Mode";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSubmitMode
            // 
            this.cbSubmitMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubmitMode.FormattingEnabled = true;
            this.cbSubmitMode.Items.AddRange(new object[] {
            "Payload",
            "ShortMessage",
            "ShortMessageWithSAR"});
            this.cbSubmitMode.Location = new System.Drawing.Point(520, 113);
            this.cbSubmitMode.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbSubmitMode.Name = "cbSubmitMode";
            this.cbSubmitMode.Size = new System.Drawing.Size(238, 32);
            this.cbSubmitMode.TabIndex = 25;
            // 
            // tbRepeatTimes
            // 
            this.tbRepeatTimes.Enabled = false;
            this.tbRepeatTimes.Location = new System.Drawing.Point(448, 530);
            this.tbRepeatTimes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbRepeatTimes.Name = "tbRepeatTimes";
            this.tbRepeatTimes.Size = new System.Drawing.Size(76, 29);
            this.tbRepeatTimes.TabIndex = 28;
            this.tbRepeatTimes.Text = "1000";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(537, 537);
            this.label18.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 25);
            this.label18.TabIndex = 29;
            this.label18.Text = "times";
            // 
            // cbReconnect
            // 
            this.cbReconnect.AutoSize = true;
            this.cbReconnect.Location = new System.Drawing.Point(298, 222);
            this.cbReconnect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbReconnect.Name = "cbReconnect";
            this.cbReconnect.Size = new System.Drawing.Size(131, 29);
            this.cbReconnect.TabIndex = 2;
            this.cbReconnect.Text = "Reconnect";
            this.cbReconnect.UseVisualStyleBackColor = true;
            this.cbReconnect.CheckedChanged += new System.EventHandler(this.cbReconnect_CheckedChanged);
            // 
            // tbSubmitSpeed
            // 
            this.tbSubmitSpeed.Location = new System.Drawing.Point(766, 535);
            this.tbSubmitSpeed.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbSubmitSpeed.Name = "tbSubmitSpeed";
            this.tbSubmitSpeed.Size = new System.Drawing.Size(67, 29);
            this.tbSubmitSpeed.TabIndex = 31;
            this.tbSubmitSpeed.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(620, 537);
            this.label20.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(136, 25);
            this.label20.TabIndex = 30;
            this.label20.Text = "Submit Speed";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(847, 537);
            this.label21.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(139, 25);
            this.label21.TabIndex = 32;
            this.label21.Text = "messages/sec";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(129, 31);
            this.label22.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 33);
            this.label22.TabIndex = 5;
            this.label22.Text = "Address";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(129, 68);
            this.label23.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 33);
            this.label23.TabIndex = 12;
            this.label23.Text = "Address";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.cbSubmitMode);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.tbMessageText);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cbDataCoding);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.tbDestAdr);
            this.groupBox3.Controls.Add(this.tbServiceType);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.tbSrcAdr);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbSrcAdrTON);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tbDestAdrNPI);
            this.groupBox3.Controls.Add(this.tbSrcAdrNPI);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tbDestAdrTON);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(12, 258);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1156, 262);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Submit settings";
            // 
            // SmppClientDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1180, 1034);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.tbSubmitSpeed);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tbRepeatTimes);
            this.Controls.Add(this.cbBatch);
            this.Controls.Add(this.bSubmit);
            this.Controls.Add(this.bAbout);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bDisconnect);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.cbReconnect);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "SmppClientDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inetlab.SMPP SmppClient Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmppClientDemo_FormClosing);
            this.Load += new System.EventHandler(this.SmppClientDemo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Button bDisconnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbHostname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSystemType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSystemId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAddrNpi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAddrTon;
        private System.Windows.Forms.GroupBox groupBox2;
        private TextBox tbLog;
        private System.Windows.Forms.Button bAbout;
        private System.Windows.Forms.TextBox tbMessageText;
        private System.Windows.Forms.TextBox tbSrcAdr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button bSubmit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbDestAdrNPI;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbDestAdrTON;
        private System.Windows.Forms.TextBox tbSrcAdrNPI;
        private System.Windows.Forms.TextBox tbSrcAdrTON;
        private System.Windows.Forms.TextBox tbDestAdr;
        private System.Windows.Forms.CheckBox cbBatch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbServiceType;
        private Label label17;
        private ComboBox cbDataCoding;
        private Label label16;
        private ComboBox cbSubmitMode;
        private TextBox tbRepeatTimes;
        private Label label18;
        private CheckBox cbSSL;
        private CheckBox cbReconnect;
        private TextBox tbSubmitSpeed;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private ComboBox cbBindingMode;
        private Label label25;
        private GroupBox groupBox3;
    }
}
