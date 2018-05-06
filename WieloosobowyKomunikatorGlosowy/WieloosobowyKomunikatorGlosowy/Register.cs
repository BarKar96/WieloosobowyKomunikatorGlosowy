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
        public Register()
        {
            Klient k = new Klient("bartek");
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
                password = SHA.ChangeToSHA2_256(password);
                string data = Klient.tcp.send("REG;" + login + ";" + password);
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