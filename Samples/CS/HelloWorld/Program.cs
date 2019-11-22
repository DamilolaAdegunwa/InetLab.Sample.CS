using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Logging;
using Inetlab.SMPP.PDU;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.SetLoggerFactory(new ConsoleLogFactory(LogLevel.Verbose));

            Console.WriteLine("Type 'Hello' to send message");
            Console.ReadLine();
            SendHelloWorld().Wait();
            Console.ReadLine();
               
        }

        // <SendHelloWorld>
        public static async Task SendHelloWorld()
        
        
        {
            using (SmppClient client = new SmppClient())
            {

                if (await client.Connect("192.168.1.190", 7777))
                {
                    BindResp bindResp = await client.Bind("admin", "admin");

                    if (bindResp.Header.Status == CommandStatus.ESME_ROK)
                    {
                        var submitResp = await client.Submit(
                            SMS.ForSubmit()
                                .From("111")
                                .To("222")
                                .Coding(DataCodings.UCS2)
                                .Text("Hello dude!!"));

                        if (submitResp.All(x => x.Header.Status == CommandStatus.ESME_ROK))
                        {
                            client.Logger.Info("Message has been sent.");
                        }
                    }

                   // await client.Disconnect();
                }
            }
        }
        //</SendHelloWorld>
    }
}
