using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmppServerConsoleApp
{   
    public class Program
    {
        private readonly SmppServer _server = new SmppServer(new IPEndPoint(IPAddress.Any, 7777));//IPAddress.Parse("127.0.0.1")

        private readonly IServerMessageStore _messageStore;

        static void Main(string[] args)
        {
            Console.WriteLine("welcome to our server!!");
            new Program().smpptest();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        public void smpptest()
        {
            _server.evClientBind += ServerOnClientBind;
        }
        public Program()
        {
            _messageStore = new DummyMessageStore();

            _server.evClientBind += ServerOnClientBind;
            _server.evClientSubmitSm += ServerOnClientSubmitSm;

            _server.Start();

            using (SmppClient client = new SmppClient())
            {
                client.Connect("localhost", 7777);
                client.Bind("admin", "admin");
                Console.WriteLine("Performance: Conected + ");
            }
        }


        private void ServerOnClientBind(object sender, SmppServerClient client, Bind pdu)
        {
            //Set server name.
            pdu.Response.SystemId = "MySMPPServer";

            //check if client is allowed to create SMPP session.
            //if (IsClientAllowed(pdu))
            //{
                Console.WriteLine($"Client {client.SystemID} has bound");

                //Check if bound client can receive messages
                if (client.BindingMode == ConnectionMode.Transceiver || client.BindingMode == ConnectionMode.Receiver)
                {
                    //Start messages delivery

                    Task messagesTask = DeliverMessagesAsync(client, pdu);
                }
            //}
            //else
            //{
            //    Console.WriteLine($"New session from client {client.SystemID} is denied.");
            //    pdu.Response.Header.Status = CommandStatus.ESME_RBINDFAIL;
            //}
        }


        private bool IsClientAllowed(Bind pdu)
        {
            //allow only one connection for one SMPP account.

            int activeSessions = _server.ConnectedClients.Count(c => c.SystemID == pdu.SystemId);

            bool alreadyConnected = activeSessions > 0;
            if (alreadyConnected) return false;

            return pdu.SystemId == pdu.Password;
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

                if (responses.All(x => x.Header.Status == CommandStatus.ESME_ROK))
                {
                    _messageStore.MessageWasDelivered(message.Id);
                }
            }

            Console.WriteLine($"Dummy messages have been sent to the client with systemId {pdu.SystemId}.");
        }

        private void ServerOnClientSubmitSm(object sender, SmppServerClient client, SubmitSm pdu)
        {
            Console.WriteLine($"Inbound message from {client.SystemID}");

            //Set server message id for the pdu;
            pdu.Response.MessageId = Guid.NewGuid().ToString().Substring(0, 8);
        }


        //Represents message storage. You need to implement this interface with desired database.
        public interface IServerMessageStore
        {
            IEnumerable<TextMessage> GetMessagesForClient(string systemId, string systemType);
            void MessageWasDelivered(string messageId);

        }

        public class TextMessage
        {
            public string Id { get; set; }
            public string PhoneNumber { get; set; }
            public string Text { get; set; }

            public string ServiceAddress { get; set; }
        }

        public class DummyMessageStore : IServerMessageStore
        {
            readonly List<TextMessage> _messages = new List<TextMessage>();

            public DummyMessageStore()
            {
                _messages.Add(new TextMessage
                {
                    Id = "1",
                    ServiceAddress = "5555",
                    PhoneNumber = "7917123456",
                    Text = "08131363116"
                });

            }

            public IEnumerable<TextMessage> GetMessagesForClient(string systemId, string systemType)
            {
                return _messages;
            }

            public void MessageWasDelivered(string messageId)
            {
                _messages.RemoveAll(m => m.Id == messageId);
            }
        }
    }

}
