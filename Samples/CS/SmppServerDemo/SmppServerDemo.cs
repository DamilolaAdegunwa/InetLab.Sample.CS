using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Logging;
using Inetlab.SMPP.PDU;

namespace SmppServerDemo
{
    public partial class SmppServerDemo : Form
    {
        private readonly SmppServer _server;
        private readonly ILog _log;
        private readonly MessageComposer _messageComposer;

        public SmppServerDemo()
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

            

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                LogManager.GetLogger("AppDomain").Fatal((Exception)args.ExceptionObject, "Unhandled Exception");
            };

            _log = LogManager.GetLogger(GetType().Name);

            _server = new SmppServer(new IPEndPoint(IPAddress.Any, int.Parse(tbPort.Text)));
            _server.evClientConnected += server_evClientConnected;
            _server.evClientDisconnected += server_evClientDisconnected;
            _server.evClientBind += server_evClientBind;
            _server.evClientSubmitSm += server_evClientSubmitSm;
            _server.evClientSubmitMulti += server_evClientSubmitMulti;
            _server.evClientEnquireLink += ServerOnClientEnquireLink;

            _server.evClientCertificateValidation += OnClientCertificateValidation;

            //Create message composer. It helps to get full text of the concatenated message in the method OnFullMessageReceived
            _messageComposer = new MessageComposer();
            _messageComposer.evFullMessageReceived += OnFullMessageReceived;
            _messageComposer.evFullMessageTimeout += OnFullMessageTimeout;


        }

        private void ServerOnClientEnquireLink(object sender, SmppServerClient client, EnquireLink data)
        {
            _log.Info($"EnquireLink received from {client}");
        }

        private void OnClientCertificateValidation(object sender, CertificateValidationEventArgs args)
        {
            //accept all certificates
            args.Accepted = true;
        }

        private long messageIdCounter = 0;

        private void server_evClientBind(object sender, SmppServerClient client, Bind data)
        {
            _log.Info("Client {0} bind as {1}:{2}", client.RemoteEndPoint, data.SystemId, data.Password);

            //  data.Response.ChangeSystemId("NewServerId");

            //Check SMPP access, and if it is wrong retund non-OK status.
            if (data.SystemId == "")
            {
                data.Response.Header.Status = CommandStatus.ESME_RINVSYSID;
                _log.Info("Client {0} tries to bind with invalid SystemId: {1}", client.RemoteEndPoint, data.SystemId);
                return;
            }
            if (data.Password == "")
            {
                _log.Info(string.Format("Client {0} tries to bind with invalid Password.", client.RemoteEndPoint));

                data.Response.Header.Status = CommandStatus.ESME_RINVPASWD;
                return;
            }

            //deny multiple connection with same smpp system id.
            foreach (var connectedClient in _server.ConnectedClients)
            {
                if (connectedClient.SystemID == client.SystemID && connectedClient.Status == ConnectionStatus.Bound)
                {
                    data.Response.Header.Status = CommandStatus.ESME_RALYBND;
                    _log.Warn("Client {0} tries to establish multiple sessions with the same SystemId", client.RemoteEndPoint);
                    return;
                }
            }

            _log.Info("Client {0} has been bound.", client.RemoteEndPoint);
            //  CommandStatus.ESME_RBINDFAIL - when Bind Failed. 

            UpdateClient(client);
        }

        private void server_evClientSubmitSm(object sender, SmppServerClient client, SubmitSm data)
        {
            long messageId = Interlocked.Increment(ref messageIdCounter);
            // You can set your own MessageId
            data.Response.MessageId = messageId.ToString();

            _log.Info("Client {0} sends message From:{1}, To:{2}, Text: {3}",
                client.RemoteEndPoint,  data.SourceAddress, data.DestinationAddress,
                data.GetMessageText(client.EncodingMapper));

          
            _messageComposer.AddMessage(data);

           

            // Set unsuccess response status
            //data.Response.Status = CommandStatus.ESME_RSUBMITFAIL;


            if (data.SMSCReceipt != SMSCDeliveryReceipt.NotRequested)
            {
                //Send Delivery Receipt when required

                string messageText = data.GetMessageText(client.EncodingMapper);

                var dlrBuilder = SMS.ForDeliver()
                    .From(data.DestinationAddress)
                    .To(data.SourceAddress)
                    .Receipt(new Receipt
                    {
                        DoneDate = DateTime.Now,
                        State = MessageState.Delivered,
                        MessageId = data.Response.MessageId,
                        ErrorCode = "0",
                        SubmitDate = DateTime.Now,
                        Text = messageText.Substring(0, Math.Min(20, messageText.Length))
                    });

                if (data.DataCoding == DataCodings.UCS2)
                {
                    //short_message field cannot contain user data longer than 255 octets,
                    //therefore for UCS2 encoding we are sending DLR in message_payload parameter
                    dlrBuilder.MessageInPayload();
                }

               client.Deliver(dlrBuilder).ConfigureAwait(false);
            }

            

        }

        void server_evClientSubmitMulti(object sender, SmppServerClient client, SubmitMulti data)
        {

            _log.Info("Client {0} sends message From:{1} to multiple destinations:{2}, Text: {3}",
                                       client.RemoteEndPoint, data.SourceAddress, data.DestinationAddresses.Count, 
                                       data.GetMessageText(client.EncodingMapper));

            _messageComposer.AddMessage(data);

            if (data.RegisteredDelivery == 1)
            {
                SmeAddress destinationAddress = data.DestinationAddresses[0] as SmeAddress;

                string messageText = data.GetMessageText(client.EncodingMapper);

                //Send Delivery Receipt when required
                Task.Run(()=> client.Deliver(
                    SMS.ForDeliver()
                        .From(data.SourceAddress)
                        .To(destinationAddress)
                        .Coding(data.DataCoding)
                        .Receipt(new Receipt
                            {
                                DoneDate = DateTime.Now,
                                State = MessageState.Delivered,
                                MessageId = data.Response.MessageId,
                                ErrorCode = "0",
                                SubmitDate = DateTime.Now,
                                Text = messageText.Substring(0, Math.Max(20, messageText.Length))
                            }
                        )
                ));
            }

         

        }

     

        private void UpdateClient(SmppServerClient client)
        {
            Sync(this, () =>
            {
                int index = -1;
                for (int i = 0; i < lbClients.Items.Count; i++)
                {
                    SmppServerClient item = lbClients.Items[i] as SmppServerClient;
                    if (item != null && item.RemoteEndPoint.Equals(client.RemoteEndPoint))
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0)
                {
                    lbClients.Items[index] = client;
                }
                else
                {
                    lbClients.Items.Add(client);
                }
            });
        }


        private void bStartServer_Click(object sender, EventArgs e)
        {
            bStartServer.Enabled = false;
            bStopServer.Enabled = true;

            if (cbSSL.Checked && comboCertList.SelectedItem != null)
            {
                _server.ServerCertificate = (X509Certificate2) comboCertList.SelectedItem;
                _server.EnabledSslProtocols = SslProtocols.Default;
            }
            else
            {
                _server.ServerCertificate = null;
            }
            _server.Start();
     
            
        }

        private void bStopServer_Click(object sender, EventArgs e)
        {
            _server.Stop();

            bStartServer.Enabled = true;
            bStopServer.Enabled = false;
      
            lbClients.Items.Clear();
           
        }

        private void SmppServerDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            bStopServer_Click(sender, EventArgs.Empty);
        }


        void server_evClientConnected(object sender, SmppServerClient client)
        {
            //Change number of threads that process received messages. Dafault is 3
            //client.WorkerThreads = 10;

            //Change receive buffer size for client socket
            // client.ReceiveBufferSize = 30 * 1024 * 1024;
            //Change send buffer size for client socket
            //  client.SendBufferSize = 30 * 1024 * 1024;


            //Don't allow this client to send more than one message per second
            //client.ReceiveSpeedLimit = 1;
            //Set maximum number of unhandled messages in the receive queue for this client
            //client.ReceiveQueueLimit = 2;


            client.EncodingMapper.MapEncoding(DataCodings.Class1, new Inetlab.SMPP.Encodings.GSMPackedEncoding());


            _log.Info("Client {0} connected.", client.RemoteEndPoint);


            if (client.ClientCertificate != null)
            {
                _log.Info("Client Certificate {0}, Expire Date: {1}", client.ClientCertificate.Subject, client.ClientCertificate.GetExpirationDateString());
            }

            Sync(lbClients, () =>
            {
                lbClients.Items.Add(client);
            });
        }

       

        private void OnFullMessageReceived(object sender, MessageEventHandlerArgs args)
        {
            _log.Info("SMS Received: {0}", args.Text);
          
        }

        private void OnFullMessageTimeout(object sender, MessageEventHandlerArgs args)
        {
            _log.Info("Incomplete SMS Received: {0}", args.Text);
        }



        private void server_evClientDisconnected(object sender, SmppServerClient client)
        {

            _log.Info("Client {0} disconnected.", client.RemoteEndPoint);

            Sync(lbClients, () =>
            {
                lbClients.Items.Remove(client);
            });

        }


        private void cbSSL_CheckedChanged(object sender, EventArgs e)
        {
            comboCertList.Enabled = cbSSL.Checked;
            comboCertList.Items.Clear();

            if (cbSSL.Checked)
            {
                X509CertificateCollection collection = new X509Certificate2Collection();


                if (File.Exists("server.p12"))
                {
                    collection.Add(new X509Certificate2("server.p12", "12345"));
                }

                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                collection.AddRange(store.Certificates);
  
              
                foreach (X509Certificate x509 in collection)
                {
                    comboCertList.Items.Add(x509);
                }

                if (comboCertList.Items.Count > 0) comboCertList.SelectedIndex = 0;

            }

        }



        private void lbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isSelected = lbClients.SelectedIndex >= 0;

            bSendMessage.Enabled = isSelected;
            bDisconnect.Enabled = isSelected;
        }

        private void bSendMessage_Click(object sender, EventArgs e)
        {
            SmppServerClient client = lbClients.SelectedItem as SmppServerClient;
            if (client != null)
            {
                SendMessage form = new SendMessage(client);
                form.ShowDialog();
            }
        }

        private async void bDisconnect_Click(object sender, EventArgs e)
        {
            SmppServerClient client = lbClients.SelectedItem as SmppServerClient;
            if (client != null)
            {
                if (client.Status == ConnectionStatus.Bound)
                {
                  await client.UnBind();
                }

                if (client.Status == ConnectionStatus.Open)
                {
                  await client.Disconnect();
                }
            }
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

    }

 
}