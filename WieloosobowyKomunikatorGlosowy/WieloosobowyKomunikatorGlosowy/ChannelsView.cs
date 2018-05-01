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
    public partial class ChannelsView : Form
    {
        private List<Channel> channelsList;
        private Klient k;


        public ChannelsView()
        {

            InitializeComponent();
            k = new Klient();
            channelsList = new List<Channel>();
            channelsList.Add(new Channel("c", "c", "a", 12));
            channelsList.Add(new Channel("c", "c", null, 13));
        }

        private void join_button_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            Console.WriteLine(i);
        }

        private void mute_button_Click(object sender, EventArgs e)
        {
           
        }

        private void logout_button_Click(object sender, EventArgs e)
        {

        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            foreach (Channel ch in channelsList)
            {
                if (ch.password == null)
                {
                    dataGridView1.Rows.Add(ch.name, ch.description, ch.password, false);
                }
                else
                {
                    dataGridView1.Rows.Add(ch.name, ch.description, ch.password, true);
                }
            }
        }

       
    }
}
