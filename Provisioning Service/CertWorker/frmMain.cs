using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Provisioning_Service_Shared_Objects;
using System.Messaging;
using Renci.SshNet;

namespace CertWorker
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void btnEnableTimer_Click(object sender, EventArgs e)
        {
            chkTimerEnabled.Checked = (chkTimerEnabled.Checked == true) ? false : true;
            timerProcessQueue.Enabled = true;
        }
        private void log(string log)
        {
            txtLog.AppendText(Environment.NewLine + String.Format("{0} : {1}", DateTime.Now.ToLongTimeString(), log));
        }

        private void timerProcessQueue_Tick(object sender, EventArgs e)
        {
            ProcessQueue();
        }
        private System.Messaging.Message GetNextMessage(string queue, string server)
        {
            System.Messaging.Message m = null;
            try
            {
                MessageQueue rq = new MessageQueue(String.Format(@"FormatName:direct=OS:{0}\private$\{1}", server, queue));
                rq.Formatter = rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
                m = rq.Receive(new TimeSpan(0, 0, 0));
            }
            catch (Exception ex)
            { }
            return m;
        }
        private void ProcessQueue()
        {

            var m = GetNextMessage("ENCRequest", "provsvc");

            try
            {
                while (m != null)
                {
                    ProvisionTask task = (ProvisionTask)m.Body;
                    log(String.Format("Task {1} received.{0}Updating ENC", Environment.NewLine, task.taskid));

                    m = null;
                    UpdateENC(task);

                    m = GetNextMessage("ENCRequest", "ProvSvc");
                    task.state++;
                    log(String.Format("Passing task back to decider"));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


    }
}
