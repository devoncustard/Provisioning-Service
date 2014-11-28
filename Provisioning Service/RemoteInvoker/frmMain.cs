using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteInvokeWorker    
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnToggleTimer_Click(object sender, EventArgs e)
        {
            chkTimerEnabled.Checked = (chkTimerEnabled.Checked == true) ? false : true;
            timerProcessRIQueue.Enabled = chkTimerEnabled.Checked;
        }
    }
}
