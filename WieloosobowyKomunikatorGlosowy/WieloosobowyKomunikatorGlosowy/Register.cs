using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace WieloosobowyKomunikatorGlosowy
{
    public partial class Register : Form
    {
        TCP_Connection connection = new TCP_Connection();
        private string ChangeToSHA2_256(string input)
        {
            using (SHA256Managed sha1 = new SHA256Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                string result = sb.ToString();
                return result.ToLower();
            }
        }
        public Register()
        {
            InitializeComponent();
            
        }

        private void Return_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.ShowDialog();
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
                password = ChangeToSHA2_256(password);
                string data = connection.send("REG;" + login + ";" + password);
                if (data == "ACK")
                {
                    MessageBox.Show("Rejestracja udana! Możesz się zalogować.");
                    Login log = new Login();
                    this.Hide();
                    log.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Rejestracja nieudana! Login zajęty");
                }
            }
        }
    }
}