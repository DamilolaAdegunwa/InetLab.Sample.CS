using System;
using System.Windows.Forms;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;

namespace SmppServerDemo
{
    public partial class SendMessage : Form
    {
        readonly SmppServerClient _client;

        public SendMessage(SmppServerClient client )
        {
            InitializeComponent();

            _client = client;

            if (_client != null)
            {
                lClient.Text = _client.ToString();
            }

        }

        private async void bSubmit_Click(object sender, EventArgs e)
        {
            if (_client != null)
            {
                SmeAddress source = new SmeAddress(tbSrcAdr.Text, (AddressTON)byte.Parse(tbSrcAdrTON.Text), (AddressNPI)byte.Parse(tbSrcAdrNPI.Text));
                SmeAddress destination = new SmeAddress(tbDestAdr.Text, (AddressTON)byte.Parse(tbDestAdrTON.Text), (AddressNPI)byte.Parse(tbDestAdrNPI.Text));

                await _client.Deliver(SMS.ForDeliver()
                    .From(source)
                    .To(destination)
                    .Coding(GetDataCoding())
                    .Text(tbSend.Text)
                    );

                DialogResult = DialogResult.OK;
            }

        }

        private DataCodings GetDataCoding()
        {
            return (DataCodings)Enum.Parse(typeof(DataCodings), cbDataCoding.Text);
        }

        private void SendMessage_Load(object sender, EventArgs e)
        {
            cbDataCoding.SelectedIndex = 0;
        }
    }
}