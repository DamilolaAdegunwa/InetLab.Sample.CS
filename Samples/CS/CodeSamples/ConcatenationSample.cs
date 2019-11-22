using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Headers;
using Inetlab.SMPP.Parameters;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public  class ConcatenationSample
    {
        private readonly SmppConfig _config = new SmppConfig();
        private readonly SmppClient _client = new SmppClient();

        // <SendConcatenatedMessageInUDH>
        public async Task SendConcatenatedMessageInUDH(TextMessage message)
        {
            var builder = SMS.ForSubmit()
                .From(_config.ShortCode, AddressTON.NetworkSpecific, AddressNPI.Unknown)
                .To(message.PhoneNumber)
                .Text(message.Text);

            var resp = await _client.Submit(builder);
        }
        // </SendConcatenatedMessageInUDH>

        // <CreateSumbitSmWithConcatenationInUDH>
        public SubmitSm CreateSumbitSmWithConcatenationInUDH(ushort referenceNumber, byte totalParts, byte partNumber, string textSegment)
        {

            SubmitSm sm = new SubmitSm();
            sm.SourceAddress = new SmeAddress("1111");
            sm.DestinationAddress = new SmeAddress("79171234567");
            sm.DataCoding = DataCodings.Default;
            sm.RegisteredDelivery = 1;
            sm.UserData.ShortMessage = _client.EncodingMapper.GetMessageBytes(textSegment, sm.DataCoding);

            sm.UserData.Headers.Add(new ConcatenatedShortMessage16bit(referenceNumber, totalParts, partNumber));

            return sm;
        }
        // </CreateSumbitSmWithConcatenationInUDH>

        public async Task SendConcatenatedMessageWithSARParameters(TextMessage message)
        {
            // <ConcatenationWithSARParameters>
            var builder = SMS.ForSubmit()
                .From(_config.ShortCode, AddressTON.NetworkSpecific, AddressNPI.Unknown)
                .To(message.PhoneNumber)
                .Text(message.Text);

            builder.ConcatenationInSAR();

            var resp = await _client.Submit(builder);

            // </ConcatenationWithSARParameters>
        }

        public async Task SendConcatenatedMessageInMessagePayload(TextMessage message)
        {
            // <ConcatenationInMessagePayload>
            var builder = SMS.ForSubmit()
                .From(_config.ShortCode, AddressTON.NetworkSpecific, AddressNPI.Unknown)
                .To(message.PhoneNumber)
                .Text(message.Text);

            builder.MessageInPayload();

            var resp = await _client.Submit(builder);

            // </ConcatenationInMessagePayload>
        }

        // <GetConcatenationFromUDH>
        public Concatenation GetConcatenationFromUDH(SubmitSm data)
        {
            ConcatenatedShortMessages8bit udh8 = data.UserData.Headers.Of<ConcatenatedShortMessages8bit>().FirstOrDefault();

            if (udh8 == null) return null;

            return new Concatenation(udh8.ReferenceNumber, udh8.Total, udh8.SequenceNumber);
        }
        // </GetConcatenationFromUDH>


        // <GetConcatenationFromOptions>
        public Concatenation GetConcatenationFromTLVOptions(SubmitSm data)
        {
            ushort refNumber = 0;
            byte total = 0;
            byte seqNum = 0;

            var referenceNumber = data.Parameters.Of<SARReferenceNumberParamter>().FirstOrDefault();
            if (referenceNumber != null)
            {
                refNumber = referenceNumber.ReferenceNumber;
            }
            var totalSegments = data.Parameters.Of<SARTotalSegmentsParameter>().FirstOrDefault();
            if (totalSegments != null)
            {
                total = totalSegments.TotalSegments;
            }
            var sequenceNumber = data.Parameters.Of<SARSequenceNumberParameter>().FirstOrDefault();
            if (sequenceNumber != null)
            {
                seqNum = sequenceNumber.SequenceNumber;
            }

            return new Concatenation(refNumber, total, seqNum);
        }
        // </GetConcatenationFromOptions>

        public class TextMessage
        {
            public string Id { get; set; }
            public string PhoneNumber { get; set; }
            public string Text { get; set; }

            public string ServiceAddress { get; set; }
        }
    }



    public class SmppConfig
    {
        public string ShortCode { get; set; }
    }
}
