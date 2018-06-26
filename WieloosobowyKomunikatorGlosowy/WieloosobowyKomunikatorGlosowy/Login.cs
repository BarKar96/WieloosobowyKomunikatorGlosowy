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
    public partial class Login : Form
    {

        SimpleTcpClient client;
        public static string serverIP;
        public string login_name;
        public static DiffieHellman diffieHellman;
        public Login()
        {
            InitializeComponent();
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.Connect(serverIP, 8910);
            this.client.DataReceived += Client_DataReceived;
        }

        public void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string message = e.MessageString.Replace("\u0013", string.Empty);
            if (message == "EXIT")
            {
                MessageBox.Show("Serwer został wyłączony");
                Application.Exit();
            }
            else
            {
                string decryptedMessage = diffieHellman.DecryptMessage(Convert.FromBase64String(message));
                Char delimiter = ';';
                String[] substrings = decryptedMessage.Split(delimiter);

                Console.WriteLine("serwer odpowiedzial: " + message);
                Console.WriteLine("Odszyfrowana wiadomość " + decryptedMessage);
                if (substrings[0] == "LOGOK")
                {
                    client.DataReceived -= Client_DataReceived;
                    ChannelsView.diffieHellman = diffieHellman;
                    ChannelsView frm = new ChannelsView(serverIP, login_name);
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            Hide();
                            frm.Show();
                        }));
                    }
                    else
                    {
                        Hide();
                        frm.Show();
                    }
                }
                else if (substrings[0] == "LOGNOK")
                {
                    MessageBox.Show("Nie można się zalogować za pomocą tych danych!");
                }
            }
        }


        private void exit_Click(object sender, EventArgs e)
        {
            client.WriteLine(diffieHellman.EncryptMessage("EXIT;"));
            Server_IP ser = new Server_IP();
            Hide();
            ser.Show();
        }

        private void log_Click(object sender, EventArgs e)
        {
            string login = text_user.Text;
            string password = SHA.ChangeToSHA2_256(text_password.Text);
            if (login.Length == 0 || text_password.Text.Length == 0)
            {
                MessageBox.Show("Pole login i hasło nie mogą być puste!");
            }
            else
            {
                client.Write(diffieHellman.EncryptMessage("LOG;" + login + ";" + password));
                login_name = login;
            }
        }

        private void register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            client.DataReceived -= Client_DataReceived;
            this.Hide();
            Register.serverIP = serverIP;
            Register.diffieHellman = diffieHellman;
            Register reg = new Register();
            reg.ShowDialog();
        }
    }
}