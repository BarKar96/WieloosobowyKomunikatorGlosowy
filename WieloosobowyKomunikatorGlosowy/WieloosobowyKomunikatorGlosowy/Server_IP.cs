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

        private void connect_button_Click(object sender, EventArgs e)
        {
            try
            {
                SetupTCPClient(server_ip_text.Text);
                this.Hide();
                Login.serverIP = this.serverIP;
                Login log = new Login();
                log.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Nie można połączyć się z serwerem!");
            }
        }
    }
}
