using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Parameters;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public class DataSmSample
    {
        public async Task<DataSmResp> SendDataSm(SmppClient client, string text)
        {
            DataSm  dataSm = new DataSm();
            dataSm.SourceAddress = new SmeAddress("1111");
            dataSm.DestinationAddress = new SmeAddress("79171234567");
            dataSm.DataCoding = DataCodings.UCS2;

            byte[] data = client.EncodingMapper.GetMessageBytes(text, dataSm.DataCoding);

            dataSm.Parameters.Add(new MessagePayloadParameter(data));

            return await client.SubmitData(dataSm);
        }
    }
}
