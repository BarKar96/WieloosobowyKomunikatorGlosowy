using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WieloosobowyKomunikatorGlosowy
{
    public partial class ChannelsView : Form
    {
        //ozeki
        private List<Channel> channelList;
        private Klient k;
        private string serverIP;
        private int currentChannel = 0;
        //tcp
        SimpleTcpClient client;
        public ChannelsView()
        {
            
            InitializeComponent();
            //serverIP = "192.168.1.15";
            serverIP = "192.168.0.100";
            channelList = new List<Channel>();

            
            //Ozeki
            k = new Klient("bartek");
            //TCP
            SetupTCPClient();
            
            client.WriteLine("HI;");
            
            
        }

        private void SetupTCPClient()
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            client.Connect(serverIP, 8910); //przydałby się jakiś try catch jakby serwer był wyłączony
           
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            Char delimiter = ';';
            String[] substrings = e.MessageString.Split(delimiter);

            Console.WriteLine("serwer odpowiedzial: " + e.MessageString);
            if (e.MessageString == "OK")
            {
                k.StartCall(serverIP);
            }
            if (e.MessageString == "NOK")
            {
                MessageBox.Show("Hasło niepoprawne. Spróbuj ponownie.");
            }
            if (e.MessageString == "BYE")
            {
                k.HangUp();
            }
            if (substrings[0] == "CHI")
            {
                if (currentChannel == Int32.Parse(substrings[1]))
                {
                    label2.Text = substrings[2];
                    label5.Text = substrings[3];

                    for (int i = 5; i < substrings.Length; i++)
                    {
                        lb_UserList.Items.Add(substrings[i]);
                    }
                }
            }
            if (substrings[0] == "CHB")
            {
                Char de = '|';
                String[] strings = e.MessageString.Split(de);
                de = ';';
                Console.WriteLine(strings.Length);
                for (int i = 1; i<strings.Length-1; i++)
                {
                    String[] temp = strings[i].Split(de);
                    bool password = false;
                    if (temp[3] == "T")
                        password = true;
                    channelList.Add(new Channel(temp[0], temp[1], temp[2], password));
                    Console.WriteLine("add " + temp[0]);
                }
                refreshGridView();


            }
        }
        private void join_button_Click(object sender, EventArgs e)
        {
            string password = "";
            currentChannel = dataGridView1.CurrentCell.RowIndex;
            DataGridViewCheckBoxCell chechbox = dataGridView1.Rows[currentChannel].Cells["Haslo"] as DataGridViewCheckBoxCell;
            if (Convert.ToBoolean(chechbox.Value))
            {
                password = Prompt.ShowDialog("Hasło wymagane", "Podaj hasło");
            }
            client.WriteLine(k.name + ";CH;" + dataGridView1.CurrentCell.RowIndex + ";" + SHA.ChangeToSHA2_256(password) + ";"); 
        }

        private void mute_button_Click(object sender, EventArgs e)
        {
            foreach (Channel ch in channelList)
            {
                Console.WriteLine(ch.name);
            }
                refreshGridView();

        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
        }
        private void refreshGridView()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            foreach (Channel ch in channelList)
            {
                dataGridView1.Rows.Add(ch.name, ch.number_user, ch.description, ch.password);
            }
            
        }
        private void refresh_button_Click(object sender, EventArgs e)
        {
            refreshGridView();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            k.HangUp();
            Application.Exit();
        }

        private void btn_endCall_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            label5.Text = "";
            lb_UserList.Items.Clear();
            client.WriteLine(k.name+";BYE;"+ currentChannel);
        }
    }
}
