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
        private static System.Media.SoundPlayer ring { get; set; }
        private static System.Media.SoundPlayer ring2 { get; set; }
        private static System.Media.SoundPlayer ring3 { get; set; }
        public static DiffieHellman diffieHellman;
        //ozeki
        private List<Channel> channelList;
        private static Klient k = null;
        private string serverIP;
        private int currentChannel = 0;
        private string login;
        //tcp
        private SimpleTcpClient client;
        string callID = null;

        int alreadyOnChannelCounter = 0;
        public ChannelsView(string serverIP, string login)
        {
            
            InitializeComponent();
            this.serverIP = serverIP;
            this.login = login;
            channelList = new List<Channel>();

            ring = new System.Media.SoundPlayer();
            ring.SoundLocation = "q.wav";

            ring2 = new System.Media.SoundPlayer();
            ring2.SoundLocation = "beep.wav";

            ring3 = new System.Media.SoundPlayer();
            ring3.SoundLocation = "neck.wav";

            //Ozeki
            if (k == null)
            {
                k = new Klient();
            }
            //TCP
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.Connect(serverIP, 8910);
            client.DataReceived += Client_DataReceived;
            client.WriteLine(diffieHellman.EncryptMessage("HI;"));
            label5.Text = "0";
            btn_endCall.Visible = false;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            Char delimiter = ';';
            String[] substrings = e.MessageString.Split(delimiter);
            Console.WriteLine("serwer odpowiedzial: " + e.MessageString);
            if (substrings[0] == "CHI")
            {
                if (currentChannel == Int32.Parse(substrings[1]))
                {
                    Console.WriteLine("bylo: " + alreadyOnChannelCounter);
                    if (Int32.Parse(substrings[3]) > alreadyOnChannelCounter)
                    {
                        ring2.Play();
                    }
                    else
                    {
                        ring3.Play();
                    }
                    alreadyOnChannelCounter = Int32.Parse(substrings[3]);
                    Console.WriteLine("jest: " + alreadyOnChannelCounter);
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            label2.Text = substrings[2];
                            label5.Text = substrings[4];
                            lb_UserList.Items.Clear();
                            if (substrings.Length >= 5)
                            {
                                for (int i = 5; i < substrings.Length; i++)
                                {
                                    lb_UserList.Items.Add(substrings[i]);
                                }
                            }
                            refreshGridView();
                        }));
                    }
                    else
                    {
                        label2.Text = substrings[2];
                        label5.Text = substrings[4];
                        lb_UserList.Items.Clear();
                        if (substrings.Length >= 5)
                        {
                            for (int i = 5; i < substrings.Length; i++)
                            {
                                lb_UserList.Items.Add(substrings[i]);
                            }
                        }
                        refreshGridView();
                    }

                    if (substrings.Length >= 5)
                    {
                        int counter = 0;
                        for (int i = 5; i < substrings.Length; i++)
                        {
                            counter++;

                        }
                        dataGridView1[1, Int32.Parse(substrings[1])].Value = counter;
                    }
                }
            }
            else if (substrings[0] == "CHB")
            {
                channelList.Clear();
                Char de = '|';
                String[] strings = e.MessageString.Split(de);
                de = ';';
                Console.WriteLine(strings.Length);
                for (int i = 1; i < strings.Length - 1; i++)
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
            else if (substrings[0] == "EXIT")
            {
                MessageBox.Show("Serwer został wyłączony");
                Application.Exit();
            }
            else
            {
                string message = e.MessageString.Replace("\u0013", string.Empty);
                string decryptedMessage = diffieHellman.DecryptMessage(Convert.FromBase64String(message));
                substrings = decryptedMessage.Split(delimiter);
                Console.WriteLine("Odszyfrowana wiadomość: " + decryptedMessage);
                if (substrings[0] == "PASSOK")
                {
                    if (InvokeRequired)
                    {

                        Invoke(new Action(() =>
                        {
                            join_button.Visible = false;
                            btn_endCall.Visible = true;

                        }));
                    }
                    else
                    {
                        join_button.Visible = false;
                        btn_endCall.Visible = true;
                    }

                    k.StartCall(serverIP);
                }
                if (substrings[0] == "PASSNOK")
                {
                    MessageBox.Show("Hasło niepoprawne. Spróbuj ponownie.");
                }
                if (substrings[0] == "BYE")
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            label5.Text = "";
                            label2.Text = "";
                            lb_UserList.Items.Clear();
                            k.HangUp();
                            alreadyOnChannelCounter = 0;
                            currentChannel = 0;
                            join_button.Visible = true;
                            btn_endCall.Visible = false;
                        }));
                    }
                    else
                    {
                        label5.Text = "";
                        label2.Text = "";
                        lb_UserList.Items.Clear();
                        k.HangUp();
                        alreadyOnChannelCounter = 0;
                        currentChannel = 0;
                        join_button.Visible = true;
                        btn_endCall.Visible = false;
                    }
                }
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
            client.WriteLine(diffieHellman.EncryptMessage(login + ";CH;" + dataGridView1.CurrentCell.RowIndex + ";" + SHA.ChangeToSHA2_256(password))); 
        }
        public bool mute = false;
        private void mute_button_Click(object sender, EventArgs e)
        {
            if (mute == false)
            {
                k.CloseDevices();
                mute = true;
            }
            else
            {
                k.SetupDevices();
                mute = false;
            }
            

        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            if (k.call != null)
            {
                client.WriteLine(diffieHellman.EncryptMessage(login + ";BYE;" + currentChannel + ";" + k.call.CallID));
                k.HangUp();
            }
            this.Hide();
            client.DataReceived -= Client_DataReceived;
            Login log = new Login();
            log.Show();


        }
        private void refreshGridView()
        {
            
            if (InvokeRequired)
            {
                dataGridView1.DataSource = null;
                Invoke(new Action(() =>
                {
                    dataGridView1.Rows.Clear();
                    
                    foreach (Channel ch in channelList)
                    {
                        dataGridView1.Rows.Add(ch.name, ch.number_user, ch.description, ch.password);
                    }
                }));
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                foreach (Channel ch in channelList)
                {
                    dataGridView1.Rows.Add(ch.name, ch.number_user, ch.description, ch.password);
                }
            }
            
        }
        private void refresh_button_Click(object sender, EventArgs e)
        {
            refreshGridView();
        }


        private void button2_Click(object sender, EventArgs e)
        {

            client.WriteLine(diffieHellman.EncryptMessage("EXIT;"));
            if (k.call == null)
            {
                Application.Exit();
            }
            else
            {
                client.WriteLine(diffieHellman.EncryptMessage(login + ";BYE;" + currentChannel + ";" + k.call.CallID));
                k.HangUp();
                Application.Exit();
            }
            
           
            
        }

        private void btn_endCall_Click(object sender, EventArgs e)
        {
            //ring3.Play();
           
            lb_UserList.Items.Clear();
            client.WriteLine(diffieHellman.EncryptMessage(login +";BYE;"+ currentChannel + ";" + k.call.CallID));
           // currentChannel = 0;
        }
    }
}
