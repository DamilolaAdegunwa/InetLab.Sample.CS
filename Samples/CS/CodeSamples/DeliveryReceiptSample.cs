using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public class DeliveryReceiptSample
    {
        readonly SmppClient _client = new SmppClient();

        private readonly SmppConfig _config;
        private readonly IClientMessageStore _clientMessageStore;

        public DeliveryReceiptSample(SmppConfig config, IClientMessageStore clientMessageStore)
        {
            _config = config;
            _clientMessageStore = clientMessageStore;
            _client.evDeliverSm += ClientOnEvDeliverSm;
        }

        //<SendMessage>
        public async Task SendMessage(TextMessage message)
        {
            IList<SubmitSm> list = SMS.ForSubmit()
                .From(_config.ShortCode)
                .To(message.PhoneNumber)
                .Text(message.Text)
                .DeliveryReceipt()
                .Create(_client);

            foreach (SubmitSm sm in list)
            {
                sm.Header.Sequence = _client.SequenceGenerator.NextSequenceNumber();
                _clientMessageStore.SaveSequence(message.Id, sm.Header.Sequence);
            }

          var responses = await _client.Submit(list);

            foreach (SubmitSmResp resp in responses)
            {
                _clientMessageStore.SaveMessageId(message.Id, resp.MessageId);
            }
        }
        //</SendMessage>


        //<EvDeliverSm>
        private void ClientOnEvDeliverSm(object sender, DeliverSm data)
        {
            if (data.MessageType == MessageTypes.SMSCDeliveryReceipt)
            {
                _clientMessageStore.UpdateMessageStatus(data.Receipt.MessageId, data.Receipt.State);
            }
        }
        //</EvDeliverSm>
        // 
        public interface IClientMessageStore
        {
            TextMessage GetNextMessage();

            void SaveSequence(string id, uint sequence);
            void SaveMessageId(string id, string smppMessageId);
            void UpdateMessageStatus(string smppMessageId, MessageState state);
        }

        public class TextMessage
        {
            public string Id { get; set; }
            public string PhoneNumber { get; set; }
            public string Text { get; set; }

            public string ServiceAddress { get; set; }
        }
    }

  
}
