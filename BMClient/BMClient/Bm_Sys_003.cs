using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace BMClient
{
    public partial class Bm_Sys_003 : Form
    {
        public Bm_Sys_003()
        {
            InitializeComponent();

            txt_fTerminalIp.Text =HubIniFile.getIni("TERMINAL_IP");

            foreach (string comport in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(comport);
            }

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
