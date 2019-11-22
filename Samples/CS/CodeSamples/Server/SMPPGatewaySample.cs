using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Parameters;
using Inetlab.SMPP.PDU;

namespace CodeSamples.Server
{
    public class SMPPGateway
    {
        private readonly SmppClient _proxyClient;
        private readonly SmppServer _proxyServer;

        private readonly IStorage _storage = new InMemoryStorage();

        public SMPPGateway()
        {
            _proxyServer = new SmppServer(new IPEndPoint(IPAddress.Any, 7776));
            _proxyServer.Name = "Proxy" + _proxyServer.Name;
            _proxyServer.evClientSubmitSm += WhenReceiveSubmitSmFromClient;

            _proxyClient = new SmppClient();

            _proxyClient.Name = "Proxy" + _proxyClient.Name;

            _proxyClient.evDeliverSm += WhenDeliveryReceiptReceivedFromSMSC;

            _storage.ReceiptReadyForDelivery += WhenReceiptIsReadyForDelivery;
        }

        


        private async void WhenReceiveSubmitSmFromClient(object sender, SmppServerClient serverClient, SubmitSm receivedPdu)
        {
            _storage.SubmitReceived(receivedPdu);

            SubmitSm pduToSMSC = receivedPdu.Clone();

            //reset sequence number to allow proxyClient to assigne next sequence number from his generator
            pduToSMSC.Header.Sequence = 0;

            SubmitSmResp respFromSMSC = await _proxyClient.Submit(pduToSMSC);

            _storage.SubmitForwarded(receivedPdu, respFromSMSC);

            if (receivedPdu.SMSCReceipt == SMSCDeliveryReceipt.NotRequested)
            {
                _storage.DeliveryReceiptNotRequested(receivedPdu.Response.MessageId);
            }
        }

        private void WhenDeliveryReceiptReceivedFromSMSC(object sender, DeliverSm data)
        {
            if (data.MessageType == MessageTypes.SMSCDeliveryReceipt)
            {
                _storage.ReceiptReceived(data);
            }
        }

        private async void WhenReceiptIsReadyForDelivery(object sender, DeliverSm data)
        {
            //Find client that should receive the delivery receipt.
            var client = _proxyServer.ConnectedClients.FirstOrDefault();
            if (client != null)
            {

                DeliverSmResp resp = await client.Deliver(data);

                _storage.ReceiptDelivered(data.Receipt.MessageId);

            }
        }

        public async Task Run(Uri smscUrl)
        {

            _proxyServer.Start();

            await _proxyClient.Connect(smscUrl.Host, smscUrl.Port);

            BindResp bindResp = await _proxyClient.Bind("proxy", "test");


        }

    }

    internal interface IStorage
    {

        /// <summary> Store sequence number and message id of the response for received SubmitSm.</summary>
        ///
        /// <param name="data"> The data. </param>
        void SubmitReceived(SubmitSm data);


        /// <summary> Save MessageId from SMSC.</summary>
        /// <remarks>
        ///          <para>Raise <see cref="ReceiptReadyForDelivery"/> event when delivery receipt was already received before SubmitSmResp</para>
        /// </remarks>
        /// <param name="reqFromClient"> The request PDU received from client </param>
        /// <param name="remoteResp">    The response PDU received from SMSC. </param>
        void SubmitForwarded(SubmitSm reqFromClient, SubmitSmResp remoteResp);

        /// <summary> Process Delivery Receipt. </summary>
        /// <remarks>
        ///    <para> When SubmitSmResp is not yet received, store delivery receipt for further delivery. </para>
        ///    <para> When SubmitSmResp is already received, raise <see cref="ReceiptReadyForDelivery"/> event </para>
        /// </remarks>
        /// <param name="data"> The delivery receipt PDU </param>
        void ReceiptReceived(DeliverSm data);


        /// <summary> Receipt delivered to client. </summary>
        /// <remarks>
        ///  <para>Update message state to finished.</para>
        /// </remarks>
        /// <param name="localMessageId"> MessageId from Gateway's SMPP server. </param>
        void ReceiptDelivered(string localMessageId);


        /// <summary> Delivery receipt for this message will never come. Change message state to finished. </summary>
        ///
        /// <param name="localMessageId"> MessageId from Gateway's SMPP server </param>
        void DeliveryReceiptNotRequested(string localMessageId);


        /// <summary> Occurs when both SubmitSmResp and DeliverSm received from SMSC.</summary>
        event EventHandler<DeliverSm> ReceiptReadyForDelivery;

    }

    internal class InMemoryStorage : IStorage
    {
        class MessageEntry
        {

            /// <summary> Sequence number used to send SubmitSm from Gateway to SMSC</summary>
            public uint LocalSequence;

            /// <summary> MessageId sent to from Gateway to connected client. </summary>
            public string LocalMessageId;

            public uint RemoteSequence;


            /// <summary> MessageId received from SMSC. </summary>
            public string RemoteMessageId;


            /// <summary> Store delivery receipt for further sending to corresponding client. </summary>
            public DeliverSm Receipt { get; set; }
        }

        //simulate DB indexes with additional dictionaries.
        readonly ConcurrentDictionary<uint, MessageEntry> _localSequences = new ConcurrentDictionary<uint, MessageEntry>();
        readonly ConcurrentDictionary<string, MessageEntry> _remoteMessage = new ConcurrentDictionary<string, MessageEntry>();
        readonly ConcurrentDictionary<string, MessageEntry> _localMessage = new ConcurrentDictionary<string, MessageEntry>();

        
        public void SubmitReceived(SubmitSm data)
        {

            var entry = new MessageEntry
            {
                LocalSequence = data.Header.Sequence,
                LocalMessageId = data.Response.MessageId
            };

            if (!_localSequences.TryAdd(data.Header.Sequence, entry))
            {

            }

            if (!_localMessage.TryAdd(data.Response.MessageId, entry))
            {

            }

        }

        public void SubmitForwarded(SubmitSm req, SubmitSmResp remoteResp)
        {
            MessageEntry entry;
            if (!_localSequences.TryGetValue(req.Header.Sequence, out entry))
            {
                throw new Exception("Cannot find message with local sequence " + req.Header.Sequence);
            }

            entry.RemoteSequence = remoteResp.Header.Sequence;
            entry.RemoteMessageId = remoteResp.MessageId;

            MessageEntry remoteEntry;
            if (_remoteMessage.TryRemove(remoteResp.MessageId, out remoteEntry))
            {
                entry.Receipt = remoteEntry.Receipt;
                OnReceiptReadyForForward(entry);
            }
            else
            {
                _remoteMessage.TryAdd(remoteResp.MessageId, entry);
            }
        }




        public void ReceiptDelivered(string localMessageId)
        {
            MessageEntry entry;
            if (_localMessage.TryRemove(localMessageId, out entry))
            {
                if (entry.RemoteMessageId != null)
                {
                    MessageEntry entryRemote;
                    _remoteMessage.TryRemove(entry.RemoteMessageId, out entryRemote);
                }

                MessageEntry entryLocal;
                if (_localSequences.TryRemove(entry.LocalSequence, out entryLocal))
                {

                }
            }
        }

        public void DeliveryReceiptNotRequested(string localMessageId)
        {
            MessageEntry entry;
            if (_localMessage.Remove(localMessageId, out entry))
            {
                if (entry.RemoteMessageId != null)
                {
                    MessageEntry entryRemote;
                    _remoteMessage.TryRemove(entry.RemoteMessageId, out entryRemote);
                }

                MessageEntry entryLocal;
                if (_localSequences.TryRemove(entry.LocalSequence, out entryLocal))
                {

                }

            }
        }

        public void ReceiptReceived(DeliverSm data)
        {
            MessageEntry entry;

            if (_remoteMessage.TryGetValue(data.Receipt.MessageId, out entry))
            {
                entry.Receipt = data;

                OnReceiptReadyForForward(entry);
            }
            else
            {
                _remoteMessage.TryAdd(data.Receipt.MessageId, new MessageEntry
                {
                    RemoteMessageId = data.Receipt.MessageId,
                    Receipt = data
                });
            }

        }

        private void OnReceiptReadyForForward(MessageEntry entry)
        {
            if (ReceiptReadyForDelivery != null)
            {
                DeliverSm receipt = entry.Receipt.Clone();
                receipt.Receipt.MessageId = entry.LocalMessageId;

                receipt.Parameters[OptionalTags.ReceiptedMessageId] =
                    EncodingMapper.Default.GetMessageBytes(receipt.Receipt.MessageId, receipt.DataCoding);

                receipt.Header.Sequence = 0;

                ReceiptReadyForDelivery(this, receipt);
            }
        }

        public event EventHandler<DeliverSm> ReceiptReadyForDelivery;



    }

    public static class SmppPDUExtensions
    {
        public static TPdu Clone<TPdu>(this TPdu pdu) where TPdu : SmppPDU
        {
            using (MemoryStream stream = new MemoryStream())
            {
                SmppWriter writer = new SmppWriter(stream);
                writer.WritePDU(pdu);

                stream.Position = 0;

                SmppStreamReader reader = new SmppStreamReader(stream);
                return (TPdu)reader.ReadPDU();
            }
        }
    }
}
