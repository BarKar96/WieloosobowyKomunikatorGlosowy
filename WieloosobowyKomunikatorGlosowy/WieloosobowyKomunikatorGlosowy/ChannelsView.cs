using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WieloosobowyKomunikatorGlosowy
{
    public partial class ChannelsView : Form
    {
        //ozeki
        private List<Channel> channelsList;
        private Klient k;
        private string serverIP;

        //tcp
        SimpleTcpClient client;

        public ChannelsView()
        {
            
            InitializeComponent();

            //Ozeki
            k = new Klient();
            //k.StartCall("192.168.1.14");

            //TCP
            serverIP = "192.168.1.14";
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }


        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            Console.WriteLine(e.MessageString);
            if (e.MessageString == "OK")
            {
                k.StartCall(serverIP);   
            }
            Console.WriteLine("tutaj koniec");
        }

        private void join_button_Click(object sender, EventArgs e)
        {
            client.WriteLine("ch0");
        }

        private void mute_button_Click(object sender, EventArgs e)
        {

            client.Connect("192.168.1.14", 8910);
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
        
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            foreach (Channel ch in channelsList)
            {
                if (ch.password == null)
                {
                    dataGridView1.Rows.Add(ch.name, ch.description, ch.password, false);
                }
                else
                {
                    dataGridView1.Rows.Add(ch.name, ch.description, ch.password, true);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_endCall_Click(object sender, EventArgs e)
        {
            k.HangUp();
        }
    }
}
