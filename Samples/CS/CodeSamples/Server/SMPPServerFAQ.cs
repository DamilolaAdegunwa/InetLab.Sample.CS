using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;

namespace CodeSamples.Server
{
    public class SMPPServerFAQ
    {
        private readonly SmppServer _server = new SmppServer(new IPEndPoint(IPAddress.Any, 7777));
        private readonly IServerMessageStore _messageStore;

        public SMPPServerFAQ()
        {
            _messageStore = new TestMessageStore();
            _server.evClientSubmitSm += ServerOnClientSubmitSm;
            _server.evClientBind += OnClientBind;
        }

        //<DeliverMessagesOnBind>
        private void OnClientBind(object sender, SmppServerClient client, Bind pdu)
        {
            if (client.BindingMode == ConnectionMode.Transceiver || client.BindingMode == ConnectionMode.Receiver)
            {
                //Start messages delivery

                Task messagesTask = DeliverMessagesAsync(client, pdu);

            }
        }


        private async Task DeliverMessagesAsync(SmppServerClient client, Bind pdu)
        {
            var messages = _messageStore.GetMessagesForClient(pdu.SystemId, pdu.SystemType);

            foreach (TextMessage message in messages)
            {
                var pduBuilder = SMS.ForDeliver()
                    .From(message.PhoneNumber)
                    .To(message.ServiceAddress)
                    .Text(message.Text);

                var responses = await client.Deliver(pduBuilder);

                _messageStore.UpdateMessageState(message.Id, responses);
            }
        }

        public interface IServerMessageStore
        {
            IEnumerable<TextMessage> GetMessagesForClient(string systemId, string systemType);
            void UpdateMessageState(string messageId, DeliverSmResp[] responses);
        }

        public class TextMessage
        {
            public string Id { get; set; }
            public string PhoneNumber { get; set; }
            public string Text { get; set; }

            public string ServiceAddress { get; set; }
        }

        //</DeliverMessagesOnBind>

        //How to send message to connected client
        //
        //<DeliverToClient>
        public async Task DeliverToClient(SmppServerSample.TextMessage message)
        {
            string systemId = GetSystemIdByServiceAddress(message.ServiceAddress);

            SmppServerClient client = FindClient(systemId);

            await client.Deliver(SMS.ForDeliver()
                .From(message.PhoneNumber)
                .To(message.ServiceAddress)
                .Text(message.Text)
            );

        }
        //</DeliverToClient>

        private SmppServerClient FindClient(string systemId)
        {
            return _server.ConnectedClients.FirstOrDefault(c => c.SystemID == systemId);
        }

        private string GetSystemIdByServiceAddress(string serviceAddess)
        {
            if (serviceAddess == "5555") return "ServiceSample";

            return null;
        }

        //How to set MessageId
        // 
        //<SetMessageIdForSubmitSm>
        private void ServerOnClientSubmitSm(object sender, SmppServerClient client, SubmitSm data)
        {
            data.Response.MessageId = Guid.NewGuid().ToString().Substring(0, 8);
        }
        //</SetMessageIdForSubmitSm>


        public class TestMessageStore : IServerMessageStore
        {
            readonly List<TextMessage> _messages = new List<TextMessage>();

            public TestMessageStore()
            {
                _messages.Add(new TextMessage
                {
                    Id = "1",
                    ServiceAddress = "5555",
                    PhoneNumber = "7917123456",
                    Text = "test 1"
                });

                _messages.Add(new TextMessage
                {
                    Id = "2",
                    ServiceAddress = "5555",
                    PhoneNumber = "7917654321",
                    Text = "test 2"
                });
            }

            public IEnumerable<TextMessage> GetMessagesForClient(string systemId, string systemType)
            {
                return _messages;
            }

            public void UpdateMessageState(string messageId, DeliverSmResp[] responses)
            {
                _messages.RemoveAll(m => m.Id == messageId);
            }
        }
    }

    
}
