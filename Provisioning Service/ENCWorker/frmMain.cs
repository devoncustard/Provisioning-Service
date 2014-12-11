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
using PSSO;
using System.Messaging;
using Renci.SshNet;

namespace ENCWorker
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

        private void btnToggleTimer_Click(object sender, EventArgs e)
        {
            chkTimerEnabled.Checked = (chkTimerEnabled.Checked == true) ? false : true;
            timerProcessQueue.Enabled = chkTimerEnabled.Checked;
        }

        private void timerProcessQueue_Tick(object sender, EventArgs e)
        {
            ProcessQueue();
        }
        private void log(string log)
        {
            txtLog.AppendText(Environment.NewLine + String.Format("{0} : {1}", DateTime.Now.ToLongTimeString(), log));
        }

        private void ProcessQueue()
        {

            var m = GetNextMessage("ENCRequest", "provsvc");

            try
            {
                while (m != null)
                {
                    ProvisionTask task = (ProvisionTask)m.Body;
                    log(String.Format("{0} Request to add {1} to {2}", Environment.NewLine, task.hostname,task.puppetmaster));

                    m = null;
                    UpdateENC(task);
                    task.state++;
                    log(String.Format("Passing task back to decider"));
                    SendMessage(task, "provision", "provsvc");
                    task = null;
                    m = null;
                    m = GetNextMessage("ENCRequest", "ProvSvc");
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

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
            {
                Debug.WriteLine(ex.Message);
            }
            return m;
        }
        private void UpdateENC(ProvisionTask task)
        {
            SshClient sshclient = new SshClient("192.168.1.51", 22, "root", "Ncc1701d");///hmmm need to pull creds from somewhere....
            sshclient.Connect();
            // List<string> results;
            string classes = "";
            foreach (string s in task.puppetclasses)
                classes+=String.Format("{0},",s);
            classes=classes.Substring(0,classes.Length-1);
            log(string.Format("{0} Attempting to remove old node definition", Environment.NewLine));
            SshCommand command3 = sshclient.RunCommand(String.Format("cd /usr/share/puppet-dashboard;rake RAILS_ENV=production node:del name={0}.{1}", task.hostname, task.domain));
            log(string.Format("{0} Adding node definition for {1} with classes ", Environment.NewLine,task.hostname,classes));
            SshCommand command4 = sshclient.RunCommand(String.Format("cd /usr/share/puppet-dashboard;rake RAILS_ENV=production node:add name={0}.{1} classes={2}", task.hostname, task.domain, classes));
            sshclient.Disconnect();
            sshclient.Dispose();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void SendMessage(ProvisionTask task, string queue, string server)
        {
            MessageQueue q = new MessageQueue(String.Format(@"FormatName:direct=OS:{0}\private$\{1}", server, queue));
            q.Send(task);
        }   
    }
}
