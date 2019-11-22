using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Parameters;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{

    /// <summary> The sample with HUAWEI USSD protocol </summary>
    public class USSDSample
    {

        SmppClient _client;

        public USSDSample(SmppClient client)
        {
            _client = client;
            TLVCollection.RegisterParameter<UssdServiceOpParameter>(0x0501);

            _client.evDeliverSm += OnMessageReceivedFromMS;

        }

        private void OnMessageReceivedFromMS(object sender, DeliverSm data)
        {
            if (data.ServiceType == "USSD")
            {
               

               var opParameter = data.Parameters.Of<UssdServiceOpParameter>().FirstOrDefault();
               if (opParameter!=null)
               {

                   if (opParameter.Operation == USSDOperation.PSSRIndication)
                   {
                       //Begin Message
                       //The operation type can only be Request USSDOperation.USSRRequest or USSDOperation.USSNRequest.

                   }
                   else if (opParameter.Operation == USSDOperation.USSRConfirm)
                   {
                        //Continue message
                        //The operation type can only be Response. In the subsequent message exchange, the MS can only respond to the Request or Notify message of the ESME.
                        //USSDOperation.USSRRequest or USSDOperation.USSNRequest??
                   }
                   else if (opParameter.Operation == USSDOperation.PSSRResponse || opParameter.Operation == USSDOperation.USSNConfirm)
                   {
                       //End Message
                       //The operation type can only be Response.
                       // 
                        //USSDOperation.PSSRResponse
                    }
                }

            }
 
           
        }

        public void SendBeginMessage()
        {
            //When an ESME sends a Begin message, the operation type is Request or Notify.

        }

        public void SendContinueMessage()
        {
            //When an ESME sends a Continue message, the operation type is Request or Notify.
        }

        public void SendEndMessage()
        {
            //The End message can only be sent by an ESME to the USSDC. The message indicates the end of a USSD session.
            //  −	If the session is initiated by an ESME, the operation type must be Release.In this case, the contents of USSDString are ignored.
            //The ESME sends the End message only when it receives a Begin or Continue message from the MS.

        }

        public void SendAbortMessage()
        {
            //During a session, an ESME or MS can send an Abort message to end the session any time.
        }

        public void SendCommmand()
        {
            SubmitSm submitSm = new SubmitSm();
            submitSm.ServiceType = "USSD";
            submitSm.Parameters.Add(new UssdServiceOpParameter(USSDOperation.USSRRequest));
            submitSm.Parameters.Add(new TLV(OptionalTags.ItsSessionInfo, new byte[] { 11 }));
            submitSm.Parameters.Add(new TLV(OptionalTags.MoreMessagesToSend, new byte[] { 1 }));
        }

        public Task SendCommmand2()
        {
            string shortCode = "7777";

       //     _client.EsmeAddress = new SmeAddress(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private);

            _client.Bind("username", "password", ConnectionMode.Transceiver);


           return _client.Submit(SMS.ForSubmit()
                .From(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private)
                .To("+79171234567")
                .Text("USSD text")
                .Coding(DataCodings.Class1MEMessage8bit)
                .AddParameter(new UssdServiceOpParameter(USSDOperation.USSRRequest)) //USSR request
                .AddParameter(OptionalTags.ItsSessionInfo, new byte[] { 11 })
                );

        }

        public void SendCommmand3()
        {
            string shortCode = "7777";


            _client.Bind("username", "password", ConnectionMode.Transceiver);


            _client.Submit(SMS.ForSubmit()
                .ServiceType("USSD")
                .From(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private)
                .To("+79171234567")
                .Text("USSD text")
                .Coding((DataCodings)0x15)
                .AddParameter(new UssdServiceOpParameter(USSDOperation.USSRRequest)) //USSR request
                .AddParameter(0x4001, Encoding.ASCII.GetBytes("111111111111")) // ussd_imsi 
                );

        }

        public void SendPSSRResponse()
        {
            string shortCode = "7777";
            ushort ussdSessionId = 0;
            bool sessionEnd = true;

            _client.Submit(SMS.ForSubmit()
                .ServiceType("USSD")
                .From(shortCode, AddressTON.NetworkSpecific, AddressNPI.Private)
                .To("+79171234567")
                .Text("USSD text")
                .Coding((DataCodings)0x15)
                .AddParameter(OptionalTags.UssdServiceOp, new byte[] { 17 }) //PSSR response
                .AddParameter(OptionalTags.ItsSessionInfo, GetItsSessionInfoValue(ussdSessionId, sessionEnd))
                );
        }

        public void GetItsSessionInfoFromDeliverSm(DeliverSm deliverSm)
        {
            byte[] bytes = deliverSm.Parameters[OptionalTags.ItsSessionInfo];
            ushort id = GetUssdSessionId(bytes);
            bool end = GetUssdSessionEnd(bytes);
        }

        private ushort GetUssdSessionId(byte[] bytes)
        {
            return BitConverter.ToUInt16(new[] { bytes[0], (byte)(bytes[1] >> 1) }, 0);
        }

        private bool GetUssdSessionEnd(byte[] bytes)
        {
            return (bytes[1] & 0x1) == 0x1;
        }

        private byte[] GetItsSessionInfoValue(ushort ussdSessionId, bool sessionEnd)
        {
            byte[] bytes = BitConverter.GetBytes(ussdSessionId);

            bytes[1] = (byte)(bytes[1] << 1);
            if (sessionEnd)
                bytes[1] = (byte)(bytes[1] | 1);

            return bytes;
        }

        private void SendAnswerToHuaweiUSSDWithServiceType(DeliverSm deliverSm)
        {
            if (deliverSm.ServiceType == "PSSRR")
            {
                _client.Submit(SMS.ForSubmit()
                    .ServiceType("PSSRC")
                    .To(deliverSm.SourceAddress)
                    .Coding(deliverSm.DataCoding)
                    .Text("USSD response")
                );
            }

            if (deliverSm.ServiceType == "RELC")
            {
                //USSD Dialogue has been released
            }
        }



    }

    public class ItsSessionInfoParameter : TLV
    {
        public byte Session { get; }
        public byte Sequence { get; }
        public bool EndOfSession { get; }

        public ItsSessionInfoParameter(byte session, byte sequence, bool endOfSession)
        {
            Session = session;
            Sequence = sequence;
            EndOfSession = endOfSession;

            TagValue = OptionalTags.ItsSessionInfo;

            Value = new byte[2];
            Value[0] = session;

            Value[1] = (byte)(Value[1] << 1);
            if (endOfSession)
                Value[1] = (byte)(Value[1] | 1);
        }
    }

    public class UssdServiceOpParameter : TLV
    {
        public USSDOperation Operation { get; }
        public UssdServiceOpParameter(USSDOperation operation)
        {
            TagValue = OptionalTags.UssdServiceOp;
            Operation = operation;
            Value = new byte[] {operation.Value};
        }

        //internal UssdServiceOpParameter(byte[] data)
        //{
        //    TagValue = OptionalTags.UssdServiceOp;
        //    Value = data;

        //    MessageId = BufferReader.ReadCString(data, Encoding.ASCII);
        //}
    }


    public partial class  USSDOperation
    {
        public static readonly USSDOperation PSSDIndication = new USSDOperation(0);

        /// <summary> The Process Supplementary Service request (PSSR) from Mobile User to an application. </summary>
        public static readonly USSDOperation PSSRIndication = new USSDOperation(1);

        /// <summary> Unstructured Supplementary Service Request. Request from service and continue dialog.</summary>
        public static readonly USSDOperation USSRRequest = new USSDOperation(2);

        /// <summary> Unstructured Supplementary Service Notify. Notification from service without dialog.  </summary>
        /// <para>
        /// The Notify message differs from the Request message in that an Mobile User responds to a Notify message automatically.
        /// </para>
        public static readonly USSDOperation USSNRequest = new USSDOperation(3);

        public static readonly USSDOperation PSSDResponse = new USSDOperation(16);

        /// <summary> The response to Process Supplementary Service request (PSSRIndication). The reply from service. Dialog/Session ends. </summary>
        public static readonly USSDOperation PSSRResponse = new USSDOperation(17);

        /// <summary> Reply from MS.</summary>
        public static readonly USSDOperation USSRConfirm = new USSDOperation(18);

        public static readonly USSDOperation USSNConfirm = new USSDOperation(19);

        public byte Value { get; }

        public USSDOperation(byte value)
        {
            Value = value;
        }

    }

    //Defined by Huawei
    public partial class USSDOperation
    {

        /// <summary> Session released by SP side when SP initiated USSD session </summary>
        public static USSDOperation SessionReleaseBySP = new USSDOperation(32);

        /// <summary> Abort for exception </summary>
        public static USSDOperation Abort = new USSDOperation(33);
    }



}
