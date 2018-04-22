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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

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

        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void log_Click(object sender, EventArgs e)
        {
            string login = text_user.Text;
            string password = ChangeToSHA2_256(text_password.Text);
            if (login.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Pole login i hasło nie mogą być puste!");
            }
            else
            {
                //+ łączenie z bazą danych
                this.Hide();
                Form1 frm = new Form1();
                frm.ShowDialog();
            }
        }

        private void register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.ShowDialog();
        }
    }
}
