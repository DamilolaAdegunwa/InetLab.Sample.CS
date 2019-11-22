using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public class SubmitMultiSample
    {
        readonly SmppClient _client = new SmppClient();

        public async Task SendToMultipleRecepients()
        {
            //<SendToMultipleRecepients>
           await _client.Submit(SMS.ForSubmitMulti()
                .ServiceType("test")
                .Text("Test Test")
                .From("MyService")
                .To("1111")
                .To("2222")
                .To("3333")
            );

            //</SendToMultipleRecepients>
        }

        public void SendToPhoneNumbers(List<string> phoneNumbers)
        {
            //<SendToPhoneNumbers>
            var pduBuilder = SMS.ForSubmitMulti()
                .ServiceType("test")
                .Text("Test Test")
                .From("MyService");

            foreach (string phoneNumber in phoneNumbers)
            {
                pduBuilder.To(phoneNumber);
            }

            //</SendToPhoneNumbers>
        }

        public async Task SendToDestinationList()
        {
            //<SendToDestinationList>
            List<IAddress> destList = new List<IAddress>();

            destList.Add(new SmeAddress("11111111111", AddressTON.Unknown, AddressNPI.ISDN));
            destList.Add(new DistributionList("my_destribution_list_on_SMPP_Server"));

            var submitResponses = await _client.Submit(SMS.ForSubmitMulti()
                .ServiceType("test")
                .Text("Test Test")
                .From("MyService")
                .ToDestinations(destList)
            );
            //</SendToDestinationList>
        }
    }
}
