using System;
using System.Collections.Generic;
using System.Text;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Logging;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public class MessageComposerSample
    {
        private ILog _log = LogManager.GetLogger(typeof(MessageComposerSample).FullName);
        //<EventsSample>
        private readonly SmppClient _client = new SmppClient();
        private readonly MessageComposer _composer = new MessageComposer();

        public MessageComposerSample()
        {
            _client.evDeliverSm += client_evDeliverSm;

            _composer.evFullMessageReceived += OnFullMessageReceived;
            _composer.evFullMessageTimeout += OnFullMessageTimedout;
        }

        private void client_evDeliverSm(object sender, DeliverSm data)
        {
            _composer.AddMessage(data);
        }

        private void OnFullMessageTimedout(object sender, MessageEventHandlerArgs args)
        {
            DeliverSm pdu = args.GetFirst<DeliverSm>();
            _log.Info(string.Format("Incomplete message received from {0}", pdu.SourceAddress));
        }

        private void OnFullMessageReceived(object sender, MessageEventHandlerArgs args)
        {
            DeliverSm pdu = args.GetFirst<DeliverSm>();
            _log.Info(string.Format("Full message received from {0}: {1}", pdu.SourceAddress, args.Text));
        }
        //</EventsSample>


        //<InlineSample>

        private void client_evDeliverSmInline(object sender, DeliverSm data)
        {
            _composer.AddMessage(data);
            if (_composer.IsLastSegment(data))
            {
               string receivedText = _composer.GetFullMessage(data);
            }
        }

        //</InlineSample>
    }
}
