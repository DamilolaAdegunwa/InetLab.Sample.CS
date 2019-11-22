using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Builders;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Logging;
using Inetlab.SMPP.PDU;
using System.Windows.Forms;

namespace SmppClientDemo
{

	public partial class SmppClientDemo :Form
	{
        private readonly MessageComposer _messageComposer;
        private readonly ILog _log;

		private readonly SmppClient _client;


	    public SmppClientDemo()
		{
            //HOW TO INSTALL LICENSE FILE
            //====================
            //After purchase you will receive Inetlab.SMPP.license file per E-Mail. 
            //Add this file into the root of project where you have a reference on Inetlab.SMPP.dll. Change "Build Action" of the file to "Embedded Resource". 

            //Set license before using Inetlab.SMPP classes in your code:

            // C#
            // Inetlab.SMPP.LicenseManager.SetLicense(this.GetType().Assembly.GetManifestResourceStream(this.GetType(), "Inetlab.SMPP.license" ));
            //
            // VB.NET
            // Inetlab.SMPP.LicenseManager.SetLicense(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "Inetlab.SMPP.license"))

            InitializeComponent();

            LogManager.SetLoggerFactory(new TextBoxLogFactory(tbLog, LogLevel.Info));

      

            _log = LogManager.GetLogger(GetType().Name);


            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                LogManager.GetLogger("AppDomain").Fatal((Exception)args.ExceptionObject, "Unhandled Exception");
            };

     
            _client = new SmppClient();
			_client.ResponseTimeout = TimeSpan.FromSeconds(60);
		    _client.EnquireLinkInterval = TimeSpan.FromSeconds(20);
        
            _client.evDisconnected += new DisconnectedEventHandler(client_evDisconnected);
            _client.evDeliverSm += new DeliverSmEventHandler(client_evDeliverSm);
            _client.evEnquireLink += new EnquireLinkEventHandler(client_evEnquireLink);
            _client.evUnBind += new UnBindEventHandler(client_evUnBind);
            _client.evDataSm += new DataSmEventHandler(client_evDataSm);
            _client.evRecoverySucceeded += ClientOnRecoverySucceeded;

            _client.evServerCertificateValidation += OnCertificateValidation;


            _messageComposer = new MessageComposer();
            _messageComposer.evFullMessageReceived += OnFullMessageReceived;
            _messageComposer.evFullMessageTimeout += OnFullMessageTimeout;
            
		}


        private void OnCertificateValidation(object sender, CertificateValidationEventArgs args)
	    {
            //accept all certificates
            args.Accepted = true;
	    }


	    /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}

                _client.Dispose();
			}
			base.Dispose( disposing );
		}

	

		private async Task Connect()
		{


            if (_client.Status == ConnectionStatus.Closed)
			{
                _log.Info("Connecting to "+ tbHostname.Text);

                    bConnect.Enabled = false;
                    bDisconnect.Enabled = false;
                    cbReconnect.Enabled = false;


                _client.EsmeAddress = new SmeAddress("", (AddressTON)Convert.ToByte(tbAddrTon.Text), (AddressNPI)Convert.ToByte(tbAddrNpi.Text));
                _client.SystemType   = tbSystemType.Text;

			    _client.ConnectionRecovery = cbReconnect.Checked;
                _client.ConnectionRecoveryDelay = TimeSpan.FromSeconds(3);
			

                if (cbSSL.Checked)
			    {
			        _client.EnabledSslProtocols = SslProtocols.Default;
                    _client.ClientCertificates.Clear();
                    _client.ClientCertificates.Add(new X509Certificate2("client.p12", "12345"));
			    }
			    else
			    {
                    _client.EnabledSslProtocols = SslProtocols.None;
			    }

				bool bSuccess = await _client.Connect(tbHostname.Text, Convert.ToInt32(tbPort.Text));

			    if (bSuccess)
			    {
			        _log.Info("SmppClient connected");

			       await Bind();
			    }
			    else
			    {

			            bConnect.Enabled = true;
			            cbReconnect.Enabled = true;
			            bDisconnect.Enabled = false;

			    }
            }

		}

	    private async Task Bind()
	    {
	        _log.Info("Bind client with SystemId: {0}", tbSystemId.Text);

	        ConnectionMode mode = ConnectionMode.Transceiver;

	        bDisconnect.Enabled = true;
	        mode = (ConnectionMode) cbBindingMode.SelectedItem;


	        BindResp resp = await _client.Bind(tbSystemId.Text, tbPassword.Text, mode);

	        switch (resp.Header.Status)
	        {
	            case CommandStatus.ESME_ROK:
	                _log.Info("Bind succeeded: Status: {0}, SystemId: {1}", resp.Header.Status, resp.SystemId);

	                bSubmit.Enabled = true;

	                break;
	            default:
                    _log.Warn("Bind failed: Status: {0}", resp.Header.Status);

                    await Disconnect();
	               break;
	        }
	    }





	    private async Task Disconnect()
		{
            _log.Info("Disconnect from SMPP server");

            if (_client.Status == ConnectionStatus.Bound)
			{
				await UnBind();
			}

            if (_client.Status == ConnectionStatus.Open)
			{
				await _client.Disconnect();
			}
		}


        private void client_evDisconnected(object sender)
        {
            _log.Info("SmppClient disconnected");

            Sync(this, () =>
            {
                bConnect.Enabled = true;
                bDisconnect.Enabled = false;
                bSubmit.Enabled = false;
                cbReconnect.Enabled = true;


            });
        
        }

        private void ClientOnRecoverySucceeded(object sender, BindResp data)
        {
            _log.Info("Connection has been recovered.");

            Sync(this, () =>
            {
                bConnect.Enabled = false;
                bDisconnect.Enabled = true;
                bSubmit.Enabled = true;
                cbReconnect.Enabled = false;
            });

        }


        private async Task UnBind()
		{
			_log.Info("Unbind SmppClient");
            UnBindResp resp = await _client.UnBind();

			switch (resp.Header.Status)
			{
                case CommandStatus.ESME_ROK:
                    _log.Info("UnBind succeeded: Status: {0}", resp.Header.Status);
					break;
				default:
                    _log.Warn("UnBind failed: Status: {0}", resp.Header.Status);
                    await _client.Disconnect();
					break;
			}

        }



	    private void client_evDeliverSm(object sender, DeliverSm data)
		{
            try
            {
                //Check if we received Delivery Receipt
                if (data.MessageType == MessageTypes.SMSCDeliveryReceipt)
                {
                    //Get MessageId of delivered message
                    string messageId = data.Receipt.MessageId;
                    MessageState deliveryStatus = data.Receipt.State;

                    _log.Info("Delivery Receipt received: {0}", data.Receipt.ToString());
                }
                else
                {

                    // Receive incoming message and try to concatenate all parts
                    if (data.Concatenation != null)
                    {
                        _messageComposer.AddMessage(data);

                        _log.Info("DeliverSm part received: Sequence: {0}, SourceAddress: {1}, Concatenation ( {2} )" +
                                " Coding: {3}, Text: {4}",
                                data.Header.Sequence, data.SourceAddress, data.Concatenation, data.DataCoding, _client.EncodingMapper.GetMessageText(data));
                    }
                    else
                    {
                        _log.Info("DeliverSm received : Sequence: {0}, SourceAddress: {1}, Coding: {2}, Text: {3}", 
                            data.Header.Sequence, data.SourceAddress, data.DataCoding, _client.EncodingMapper.GetMessageText(data));
                    }

                    // Check if an ESME acknowledgement is required
                    if (data.Acknowledgement != SMEAcknowledgement.NotRequested)
                    {
                        // You have to clarify with SMSC support what kind of information they request in ESME acknowledgement.

                        string messageText = data.GetMessageText(_client.EncodingMapper);

                        var smBuilder = SMS.ForSubmit()
                            .From(data.DestinationAddress)
                            .To(data.SourceAddress)
                            .Coding(data.DataCoding)
                            .ConcatenationInUDH(_client.SequenceGenerator.NextReferenceNumber())
                            .Set(m => m.MessageType = MessageTypes.SMEDeliveryAcknowledgement)
                            .Text(new Receipt
                                {
                                    DoneDate = DateTime.Now,
                                    State = MessageState.Delivered,
                                    //  MessageId = data.Response.MessageId,
                                    ErrorCode = "0",
                                    SubmitDate = DateTime.Now,
                                    Text = messageText.Substring(0, Math.Min(20, messageText.Length))
                                }.ToString()
                            );



                       _client.Submit(smBuilder).ConfigureAwait(false);
                    }
                }
            } 
            catch (Exception ex)
            {
                data.Response.Header.Status = CommandStatus.ESME_RX_T_APPN;
                 _log.Error(ex,"Failed to process DeliverSm");
            }
		}


        private void client_evDataSm(object sender, DataSm data)
        {
            _log.Info("DataSm received : Sequence: {0}, SourceAddress: {1}, DestAddress: {2}, Coding: {3}, Text: {4}",
                data.Header.Sequence, data.SourceAddress, data.DestinationAddress, data.DataCoding, data.GetMessageText(_client.EncodingMapper));
        }



        private void OnFullMessageTimeout(object sender, MessageEventHandlerArgs args)
        {
            _log.Info("Incomplete message received From: {0}, Text: {1}", args.GetFirst<DeliverSm>().SourceAddress, args.Text);
        }

        private void OnFullMessageReceived(object sender, MessageEventHandlerArgs args)
	    {
            _log.Info("Full message received From: {0}, To: {1}, Text: {2}", args.GetFirst<DeliverSm>().SourceAddress, args.GetFirst<DeliverSm>().DestinationAddress, args.Text);
	    }



		private void client_evEnquireLink(object sender, EnquireLink data)
		{
            _log.Info("EnquireLink received");
		}


	

		private void client_evUnBind(object sender, UnBind data)
		{
            _log.Info("UnBind request received");
		}

     

 
		private async void bConnect_Click(object sender, EventArgs e)
		{
			await Connect();
		}

		private async void bDisconnect_Click(object sender, EventArgs e)
		{
			await Disconnect();
		}


		private async void bSubmit_Click(object sender, EventArgs e)
		{

            if (_client.Status != ConnectionStatus.Bound) 
			{
				MessageBox.Show("Before sending messages, please connect to SMPP server.");
				return;
			}

            bSubmit.Enabled = false;

            _client.SendSpeedLimit = GetSpeedLimit(tbSubmitSpeed.Text);

            

            if (cbBatch.Checked) 
			{
			    await SubmitBatchMessages();
			} 
			else
			{
                string[] dstAddresses = tbDestAdr.Text.Split(',');

			    if (dstAddresses.Length == 1)
			    {
			       await SubmitSingleMessage();
                }
			    else if (dstAddresses.Length > 1)
                {
                   await SubmitMultiMessage(dstAddresses);
                }
			}

		    bSubmit.Enabled = true;
        }

        private LimitRate GetSpeedLimit(string text)
        {
            if (string.IsNullOrWhiteSpace(tbSubmitSpeed.Text))
            {
                return LimitRate.NoLimit;
            }

            int occurrences;
            if (!int.TryParse(tbSubmitSpeed.Text, out occurrences) || occurrences == 0)
                return LimitRate.NoLimit;

            return new LimitRate(occurrences, TimeSpan.FromSeconds(1));

        }

        private async Task SubmitSingleMessage()
	    {
	        DataCodings coding = GetDataCoding();



	        var sourceAddress = new SmeAddress(tbSrcAdr.Text, (AddressTON)byte.Parse(tbSrcAdrTON.Text), (AddressNPI)byte.Parse(tbSrcAdrNPI.Text));

	        var destinationAddress = new SmeAddress(tbDestAdr.Text, (AddressTON)byte.Parse(tbDestAdrTON.Text), (AddressNPI)byte.Parse(tbDestAdrNPI.Text));

            _log.Info("Submit message To: {0}. Text: {1}", tbDestAdr.Text, tbMessageText.Text);


            ISubmitSmBuilder builder = SMS.ForSubmit()
	            .From(sourceAddress)
	            .To(destinationAddress)
	            .Coding(coding)
	            .Text(tbMessageText.Text)
	            //Or you can set data 
	            //.Data(HexStringToByteArray("024A3A6949A59194813D988151A004004D215D2690568698A22820C49A4106288A126A8A22C2A820C22820C2A82342AC30820C4984106288A12628A22C2A420800"))
                    
	            //Apply italic style for all text  (mobile device should support basic EMS)
	            //.Set(delegate(SubmitSm sm)
	            //         {
	            //             sm.UserDataPdu.Headers.Add(
	            //                 InformationElementIdentifiers.TextFormatting, new byte[] {0, 1, ToByte("00100011")});
	            //         })

	            // Set ValidityPeriod expired in 2 days
	            .ExpireIn(TimeSpan.FromDays(2))

	            //Request delivery receipt
	            .DeliveryReceipt();
	        //Add custom TLV parameter
	        //.AddParameter(0x1403, "free")

	        //Change SubmitSm sequence to your own number.
	        //.Set(delegate(SubmitSm sm) { sm.Sequence = _client.SequenceGenerator.NextSequenceNumber();})

	        SubmitMode mode = GetSubmitMode();
	        switch (mode)
	        {
	            case SubmitMode.Payload:
	                builder.MessageInPayload();
	                break;
	            case SubmitMode.ShortMessageWithSAR:
	                builder.ConcatenationInSAR();
	                break;
	        }

            try
            {
                IList<SubmitSmResp> resp = await _client.Submit(builder);

                if (resp.All(x => x.Header.Status == CommandStatus.ESME_ROK))
                {
                    _log.Info("Submit succeeded. MessageIds: {0}", string.Join(",", resp.Select(x => x.MessageId)));
                }
                else
                {
                    _log.Warn("Submit failed. Status: {0}", string.Join(",", resp.Select(x => x.Header.Status.ToString())));
                }
            }
            catch (Exception ex)
            {
                _log.Error("Submit failed. Error: {0}", ex.Message);
            }

            // When you received success result, you can later query message status on SMSC 
            // if (resp.Count > 0 && resp[0].Status == CommandStatus.ESME_ROK)
            // {
            //     _log.Info("QuerySm for message " + resp[0].MessageId);
            //     QuerySmResp qresp = _client.Query(resp[0].MessageId,
            //         srcTon, srcNpi,srcAdr);
            // }
        }

        private async Task SubmitMultiMessage(string[] dstAddresses)
        {
            DataCodings coding = GetDataCoding();

            byte srcTon = byte.Parse(tbSrcAdrTON.Text);
            byte srcNpi = byte.Parse(tbSrcAdrNPI.Text);
            string srcAdr = tbSrcAdr.Text;

            byte dstTon = byte.Parse(tbDestAdrTON.Text);
            byte dstNpi = byte.Parse(tbDestAdrNPI.Text);

            ISubmitMultiBuilder builder = SMS.ForSubmitMulti()
                .From(srcAdr, (AddressTON)srcTon, (AddressNPI)srcNpi)
                .Coding(coding)
                .Text(tbMessageText.Text)
                //Request delivery receipt
                .DeliveryReceipt();

            foreach (var dstAddress in dstAddresses)
            {
                if (dstAddress == null || dstAddress.Trim().Length == 0) continue;

                builder.To(dstAddress.Trim(), (AddressTON)dstTon, (AddressNPI)dstNpi);
            }

            _log.Info("Submit message to several addresses: {0}. Text: {1}", string.Join(", ",dstAddresses), tbMessageText.Text);


            SubmitMode mode = GetSubmitMode();
            switch (mode)
            {
                case SubmitMode.Payload:
                    builder.MessageInPayload();
                    break;
                case SubmitMode.ShortMessageWithSAR:
                    builder.ConcatenationInSAR();
                    break;
            }

           

            try
            {
                IList<SubmitMultiResp> resp = await _client.Submit(builder);

                if (resp.All(x => x.Header.Status == CommandStatus.ESME_ROK))
                {
                    _log.Info("Submit succeeded. MessageIds: {0}", string.Join(",", resp.Select(x => x.MessageId)));
                }
                else
                {
                    _log.Warn("Submit failed. Status: {0}", string.Join(",", resp.Select(x => x.Header.Status.ToString())));
                }
            }
            catch (Exception ex)
            {
                _log.Error("Submit failed. Error: {0}", ex.Message);
            }

        }




        private async Task SubmitBatchMessages()
	    {
            var sourceAddress = new SmeAddress(tbSrcAdr.Text, (AddressTON)byte.Parse(tbSrcAdrTON.Text), (AddressNPI)byte.Parse(tbSrcAdrNPI.Text));

	        var destinationAddress = new SmeAddress(tbDestAdr.Text, (AddressTON)byte.Parse(tbDestAdrTON.Text), (AddressNPI)byte.Parse(tbDestAdrNPI.Text));


	        string messageText = tbMessageText.Text;

	        SubmitMode mode = GetSubmitMode();

	        DataCodings coding = GetDataCoding();

	        int count = int.Parse(tbRepeatTimes.Text);

            _log.Info("Submit message batch. Count: {0}. Text: {1}", count, messageText);

	        // bulk sms test
	        List<SubmitSm> batch = new List<SubmitSm>();
	        for (int i = 0; i < count; i++)
	        {
	            ISubmitSmBuilder builder = SMS.ForSubmit()
	                .Text(messageText)
	                .From(sourceAddress)
	                .To(destinationAddress)
	                .Coding(coding);

	            switch (mode)
	            {
	                case SubmitMode.Payload:
	                    builder.MessageInPayload();
	                    break;
	                case SubmitMode.ShortMessageWithSAR:
	                    builder.ConcatenationInSAR();
	                    break;
	            }

                batch.AddRange(builder.Create(_client));

	        }


         

            try
            {
                Stopwatch watch = Stopwatch.StartNew();

                var resp = (await _client.Submit(batch)).ToArray();

                watch.Stop();

                if (resp.All(x => x.Header.Status == CommandStatus.ESME_ROK))
                {
                    _log.Info("Batch sending completed. Submitted: {0}, Elapsed: {1} ms, Performance: {2} m/s", batch.Count, watch.ElapsedMilliseconds, batch.Count * 1000f / watch.ElapsedMilliseconds);
                }
                else
                {
                    var wrongStatuses = resp.Where(x => x.Header.Status != CommandStatus.ESME_ROK)
                        .Select(x => x.Header.Status).Distinct();

                    _log.Warn("Submit failed. Wrong Status: {0}", string.Join(", ", wrongStatuses));
                }
            }
            catch (Exception ex)
            {
                _log.Error("Submit failed. Error: {0}", ex.Message);
            }
          

	       

         }

	    private SubmitMode GetSubmitMode()
        {
            return (SubmitMode)Enum.Parse(typeof(SubmitMode), cbSubmitMode.Text);
        }

        private DataCodings GetDataCoding()
        {
            return (DataCodings)Enum.Parse(typeof(DataCodings), cbDataCoding.Text);
        }

        private void SmppClientDemo_Load(object sender, EventArgs e)
        {
            cbSubmitMode.SelectedIndex = 1;
            cbDataCoding.SelectedIndex = 0;

            cbBindingMode.Items.Clear();
            foreach (ConnectionMode mode in Enum.GetValues(typeof(ConnectionMode)))
            {
                if (mode == ConnectionMode.None) continue;
                cbBindingMode.Items.Add(mode);
            }
            cbBindingMode.SelectedItem = ConnectionMode.Transceiver;
        }

       



        private void cbAsync_CheckedChanged(object sender, EventArgs e)
        {
            tbRepeatTimes.Enabled = cbBatch.Checked;
        }



        private void bAbout_Click(object sender, EventArgs e)
        {
            AboutSmppClientDemo frm = new AboutSmppClientDemo();
            frm.ShowDialog();
        }

        private void SmppClientDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.evDisconnected -= client_evDisconnected;
            _client.Dispose();
        }


        public delegate void SyncAction();

        public static void Sync(Control control, SyncAction action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, new object[] { });
                return;
            }

            action();
        }

        private void cbReconnect_CheckedChanged(object sender, EventArgs e)
        {
            _client.ConnectionRecovery = cbReconnect.Checked;

           
        }

       
    }
  }