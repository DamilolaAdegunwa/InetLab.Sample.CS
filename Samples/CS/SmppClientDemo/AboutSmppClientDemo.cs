using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SmppClientDemo
{

	public class AboutSmppClientDemo : Form
	{
		private LinkLabel linkLabel1;
		private Label l1;
		private Label label1;
		private Button bOK;
		private Label label2;
		private Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutSmppClientDemo()
		{
			Version ver = new Version();

			InitializeComponent();
			label1.Text = "Version: "+Assembly.GetExecutingAssembly().GetName().Version.ToString();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.l1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.Location = new System.Drawing.Point(0, 96);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(346, 32);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://www.inetlab.com/Products/Inetlab.SMPP.html";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // l1
            // 
            this.l1.Dock = System.Windows.Forms.DockStyle.Top;
            this.l1.Location = new System.Drawing.Point(0, 0);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(346, 32);
            this.l1.TabIndex = 1;
            this.l1.Text = "Inetlab.SMPP SmppClient Demo";
            this.l1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Version";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(120, 160);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(112, 23);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(346, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Copyright © 2006 - 2018 InetLab e.U.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(346, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "All rights reserved.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutSmppClientDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(346, 194);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.l1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutSmppClientDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
            ProcessStartInfo psi = new ProcessStartInfo("https://www.inetlab.com/Products/Inetlab.SMPP.html");
			psi.UseShellExecute = true;
			Process.Start(psi);
		}


	}
}
