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
    public partial class Server_IP : Form
    {
        private SimpleTcpClient client;
        private string serverIP;
        private void SetupTCPClient(string serverIP)
        {
            this.serverIP = serverIP;
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.Connect(serverIP, 8910);

        }
        public Server_IP()
        {
            InitializeComponent();
        }

        
        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public static bool CheckIP(string IP)
        {
            bool check = false;
            if (Regex.Match(IP, @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.ECMAScript).Success)
                check = true;
            return check;
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckIP(server_ip_text.Text) == true)
                {
                    SetupTCPClient(server_ip_text.Text);
                    this.Hide();
                    Login.serverIP = this.serverIP;
                    Login log = new Login();
                    log.ShowDialog();
                }
                else
                    MessageBox.Show("To nie jest poprawny adres IP!");
            }
            catch
            {
                MessageBox.Show("Nie można połączyć się z serwerem!");
            }
        }
    }
}
