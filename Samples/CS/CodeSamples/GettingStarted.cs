using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Logging;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public  class GettingStarted
    {
        public GettingStarted()
        {
            //<AttachToDeliverSm>
            _client.evDeliverSm += new DeliverSmEventHandler(client_evDeliverSm);
            //</AttachToDeliverSm>
        }


        private readonly ILog _log = LogManager.GetLogger<GettingStarted>();

        //<ConnectToServer>
        readonly SmppClient _client = new SmppClient();

        public async Task Connect()
        {
            if (await _client.Connect("smpp.server.com", 7777))
            {
                _log.Info("Connected to SMPP server");

                BindResp bindResp = await _client.Bind("username", "password", ConnectionMode.Transceiver);

                if (bindResp.Header.Status == CommandStatus.ESME_ROK)
                {
                    _log.Info("Bound with SMPP server");
                }
            }
        }
        //</ConnectToServer>

        //<SendMessage>
        public async Task SendMessage()
        {
            IList<SubmitSmResp> responses = await _client.Submit(
                SMS.ForSubmit()
                    .Text("Test Test Test Test Test Test Test Test Test Test")
                    .From("1111")
                    .To("79171234567")
                    .Coding(DataCodings.UCS2)
                    .DeliveryReceipt()
            );
        }
        //</SendMessage>

        // <CreateSubmitSm>
        public SubmitSm CreateSubmitSm()
        {
            SubmitSm sm = new SubmitSm();
            sm.UserData.ShortMessage = _client.EncodingMapper.GetMessageBytes("Test Test Test Test Test Test Test Test Test Test", DataCodings.Default);
            sm.SourceAddress = new SmeAddress("1111", AddressTON.NetworkSpecific, AddressNPI.Unknown);
            sm.DestinationAddress = new SmeAddress("79171234567", AddressTON.Unknown, AddressNPI.ISDN);
            sm.DataCoding = DataCodings.UCS2;
            sm.SMSCReceipt = SMSCDeliveryReceipt.SuccessOrFailure;

            return sm;
        }
        // </CreateSubmitSm>

        // <ReceiveMessage>
        private readonly MessageComposer _composer = new MessageComposer();

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
                }
                else
                {
                    // Receive incoming message and try to concatenate all parts
                    if (data.Concatenation != null)
                    {
                        _composer.AddMessage(data);

                        _log.Info("DeliverSm part received : Sequence: {0} SourceAddr: {1} Concatenation ( {2} ) Coding: {3} Text: {4}",
                            data.Header.Sequence, data.SourceAddress, data.Concatenation, data.DataCoding, _client.EncodingMapper.GetMessageText(data));


                        if (_composer.IsLastSegment(data))
                        {
                            string fullMessage = _composer.GetFullMessage(data);
                            _log.Info("Full message: " + fullMessage);
                        }
                    }
                    else
                    {
                        _log.Info("DeliverSm received : Sequence: {0} SourceAddr : {1} Coding : {2} MessageText : {3}",
                                data.Header.Sequence, data.SourceAddress, data.DataCoding, _client.EncodingMapper.GetMessageText(data));
                    }
                }
            }
            catch (Exception ex)
            {
                data.Response.Header.Status = CommandStatus.ESME_RX_T_APPN;
                _log.Error("Failed to process DeliverSm", ex);
            }

            // </ReceiveMessage>
        }
    }
}
