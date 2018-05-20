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
    public partial class Register : Form
    {
        private SimpleTcpClient client;
        public static string serverIP;
        public Register()
        {
            InitializeComponent();
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.Connect(serverIP, 8910);
            client.DataReceived += Client_DataReceived;
        }

        public void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            Char delimiter = ';';
            String[] substrings = e.MessageString.Split(delimiter);

            Console.WriteLine("serwer odpowiedzial: " + e.MessageString);
            if (e.MessageString == "REGOK")
            {
                MessageBox.Show("Rejestracja udana! Możesz się zalogować.");
                Login log = new Login();
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        Hide();
                        log.Show();
                    }));
                }
                else
                {
                    Hide();
                    log.Show();
                }

            }
            else if (e.MessageString == "REGNOK")
            {
                MessageBox.Show("Rejestracja nieudana! Login zajęty");
            }
        }

        private void Return_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string login = text_user.Text;
            string password = text_password.Text;
            string password2 = TextRepeatPassword.Text;
            if (login.Length == 0 || password.Length == 0 || password2.Length == 0)
            {
                MessageBox.Show("Wszystkie pola są wymagane!");
            }
            else if (password != password2)
            {
                MessageBox.Show("Hasła się nie zgadzają!");
            }
            else
            {
                password = SHA.ChangeToSHA2_256(password);
                client.WriteLine("REG;" + login + ";" + password);
            }
        }
    }
}