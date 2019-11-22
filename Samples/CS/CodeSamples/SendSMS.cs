using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Headers;
using Inetlab.SMPP.Parameters;
using Inetlab.SMPP.PDU;

namespace CodeExamples
{
    public static class SendSmsExamples
    {
        public static async Task SendText(SmppClient client)
        {
          var resp = await client.Submit(
                SMS.ForSubmit()
                    .From("short_code")
                    .To("436641234567")
                    .Coding(DataCodings.UCS2)
                    .Text("test text")
                );

        }

        public static async Task SendBinary(SmppClient client)
        {
            byte[] data = ByteArray.FromHexString(
                "FFFF002830006609EC592F55DCE9010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000005C67");

            var resp = await client.Submit(
                SMS.ForSubmit()
                    .From("short_code")
                    .To("436641234567")
                    .Data(data)
            );
        }

        public static async Task SendMessageToApplicationPort(SmppClient client)
        {

            var resp = await client.Submit(
                SMS.ForSubmit()
                    .From("short_code")
                    .To("436641234567")
                    .Text("test")
                    .Set(sm=>sm.UserData.Headers.Add(new ApplicationPortAddressingScheme16bit(0x1579, 0x0000) ))
            );
        }

        public static void AddVendorSpecificParameter(SubmitSm sm)
        {
            //0x1400 - 0x3FFF Reserved for SMSC Vendor specific optional parameters
            byte[] value = new byte[]{ 0x01};

            sm.Parameters.Add(new TLV(0x1410, value));
        }


        public static async Task SendSubmitSmWithUDH(SmppClient client)
        {
            //If you have UserDataHeader as byte array. You can create SubmitSm manually and pass it to headers collection.

            byte[] udh = new byte[]
            {
                5, //UDHL,
                0, //Concatenation
                3, // Length
                50, //message reference
                1, //total parts,
                1, // part number
            };



            SubmitSm sm = new SubmitSm();
            sm.SourceAddress = new SmeAddress("My Service");
            sm.DestinationAddress = new SmeAddress("+7917123456");
            sm.DataCoding = DataCodings.UCS2;
            sm.RegisteredDelivery = 1;

            sm.UserData.ShortMessage = client.EncodingMapper.GetMessageBytes("test message", sm.DataCoding);
            sm.UserData.Headers = udh;

            var resp = await client.Submit(sm);
        }

    }
}
