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
using System.Text.RegularExpressions;

namespace WieloosobowyKomunikatorGlosowy
{
    public partial class Register : Form
    {
        private SimpleTcpClient client;
        public static DiffieHellman diffieHellman;
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
                if (substrings[0] == "REGOK")
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
                else if (substrings[0] == "REGNOK")
                {
                    MessageBox.Show("Rejestracja nieudana! Login zajęty");
                }
            }
        }

        private void Return_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }

        public static bool CheckStrength(string password)
        {
            bool check = false;
            if (Regex.Match(password, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", RegexOptions.ECMAScript).Success)
                check = true;
            return check;
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
            else if (CheckStrength(password)==false)
            {
                MessageBox.Show("Hasło za słabe. Musi składać się z 8 znaków oraz zawierać wielką literę i znak specjalny.");
            }
            else if (login.Length < 5)
            {
                MessageBox.Show("Login musi składać się z przynajmniej 5 znaków");
            }
            else
            {
                password = SHA.ChangeToSHA2_256(password);
                client.WriteLine(diffieHellman.EncryptMessage("REG;" + login + ";" + password));
            }
        }
    }
}