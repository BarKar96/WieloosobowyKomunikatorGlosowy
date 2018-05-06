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
        public Login()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void log_Click(object sender, EventArgs e)
        {
            string login = text_user.Text;
            string password = SHA.ChangeToSHA2_256(text_password.Text);
            if (login.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Pole login i hasło nie mogą być puste!");
            }
            else
            {
                //+ łączenie z bazą danych
                this.Hide();
                ChannelsView frm = new ChannelsView();
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