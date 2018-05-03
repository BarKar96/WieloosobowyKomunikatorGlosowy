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
        private int currentChannel = 0;
        private int nextChannel = 0;
        //tcp
        SimpleTcpClient client;

        public ChannelsView()
        {
            
            InitializeComponent();
            serverIP = "192.168.1.28";
            channelsList = new List<Channel>();
            channelsList.Add(new Channel("m", "d", "p"));
            channelsList.Add(new Channel("m", "d", "p"));
            channelsList.Add(new Channel("m", "d", "p"));
            channelsList.Add(new Channel("m", "d", "p"));
            refreshGridView();
            //Ozeki
            k = new Klient();

            //TCP
            SetupTCPClient();
        }

        private void SetupTCPClient()
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            client.Connect(serverIP, 8910);
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            Console.WriteLine("serwer odpowiedzial: "+ e.MessageString);           
            if (e.MessageString == "OK")
            {
                k.SetupDevices();
                k.StartCall(serverIP);
               
            }
            if (e.MessageString == "BYE")
            {
                k.HangUp();
            }
            else
            {
                Console.WriteLine("blad przechodzenia na kanal");
            }
            //Console.WriteLine("tutaj koniec");
        }

        private void join_button_Click(object sender, EventArgs e)
        {
            
            client.WriteLine("ch" + dataGridView1.CurrentCell.RowIndex);
            currentChannel = dataGridView1.CurrentCell.RowIndex;
        }

        private void mute_button_Click(object sender, EventArgs e)
        {

            
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
        
        }
        private void refreshGridView()
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
        private void refresh_button_Click(object sender, EventArgs e)
        {
            refreshGridView();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_endCall_Click(object sender, EventArgs e)
        {
            client.WriteLine("BYE"+ currentChannel);
        }


    }
}
