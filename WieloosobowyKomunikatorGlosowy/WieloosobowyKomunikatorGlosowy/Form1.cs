﻿using Ozeki.VoIP;
using Ozeki.Media;
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
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            Klient k = new Klient();
            k.StartCall("192.168.1.14");
        }

        private void lb_Log_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
