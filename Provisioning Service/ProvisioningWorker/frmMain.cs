using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Provisioning_Service_Shared_Objects;


namespace ProvisioningWorker
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void buttonToggleTimer_Click(object sender, EventArgs e)
        {
            checkBoxTimerState.Checked = (checkBoxTimerState.Checked == true) ? false : true;
            timerProcessQueue.Enabled = checkBoxTimerState.Checked;

        }

        private void buttonProcessQueueOnce_Click(object sender, EventArgs e)
        {

        }

        private void timerProcessProvisionQueue_Tick(object sender, EventArgs e)
        {
            ProcessProvisionQueue();
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



