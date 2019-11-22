using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.Logging;
using Inetlab.SMPP.PDU;

namespace CodeSamples
{
    public static class SSLConnectionSample
    {
        public static async Task Run()
        {
            // <Sample>
            using (SmppServer server = new SmppServer(new IPEndPoint(IPAddress.Any, 7777)))
            {
                server.EnabledSslProtocols = SslProtocols.Tls12;
                server.ServerCertificate = new X509Certificate2("server_certificate.p12", "cert_password");

                server.Start();

                server.evClientConnected += (sender, client) =>
                {
                    var clientCertificate = client.ClientCertificate;
                    //You can validate client certificate and disconnect if it is not valid.
                };

                using (SmppClient client = new SmppClient())
                {
                    client.EnabledSslProtocols = SslProtocols.Tls12;
                    //if required you can be authenticated with client certificate
                    client.ClientCertificates.Add(new X509Certificate2("client_certificate.p12", "cert_password"));

                    if (await client.Connect("localhost", 7777))
                    {
                        BindResp bindResp = await client.Bind("username", "password");

                        if (bindResp.Header.Status == CommandStatus.ESME_ROK)
                        {
                            var submitResp = await client.Submit(
                                SMS.ForSubmit()
                                    .From("111")
                                    .To("436641234567")
                                    .Coding(DataCodings.UCS2)
                                    .Text("Hello World!"));

                            if (submitResp.All(x => x.Header.Status == CommandStatus.ESME_ROK))
                            {
                                client.Logger.Info("Message has been sent.");
                            }
                        }

                        await client.Disconnect();
                    }
                }
            }
            //</Sample>
        }
    }
}
