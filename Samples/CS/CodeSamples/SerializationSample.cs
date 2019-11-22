using System;
using System.IO;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;

namespace CodeExamples
{
    public class SerializationSample
    {

        /// <summary> Deserialize this saved data to the SubmitSm </summary>
        ///
        /// <param name="client"> The client. </param>
        /// <param name="data">   The serialized SubmitSm PDU. </param>
        ///
        /// <returns> A SubmitSm. </returns>
        /// 
        public SubmitSm Deserialize(SmppClient client, byte[] data)
        {
            SmppReader reader = new SmppReader(client.EncodingMapper);

            return (SubmitSm) reader.ReadPDU(data);

        }



        /// <summary> Serialize SubmitSm object to the byte array. </summary>
        ///
        /// <param name="client"> The client. </param>
        /// <param name="pdu"> The SubmitSm object. </param>
        ///
        /// <returns> A byte array. </returns>
        public byte[] Serialize(SmppClient client, SubmitSm pdu)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (SmppWriter writer = new SmppWriter(stream, client.EncodingMapper))
                {
                   writer.WritePDU(pdu);

                    return stream.ToArray();
                }
            }
        }
    }
}
