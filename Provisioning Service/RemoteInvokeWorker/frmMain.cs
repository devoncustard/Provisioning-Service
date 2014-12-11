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
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using PSSO;
using System.Security;
using System.Threading;

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
            timerProcessQueue.Enabled = true;
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

            var m = GetNextMessage("RemoteInvoke", "provsvc");

            try 
            {
                while (m!=null)
                {
                    ProvisionTask task=(ProvisionTask)m.Body;
                    log(String.Format("Task {1} received.{0}OS Family {2} - passing to appropriate handler",Environment.NewLine,task.taskid,task.image.OS_Family));
                        
                    switch(task.image.OS_Family)
                    {
                        case OSFamily.Windows:
                            RIWindows(task);
                            break;
                                   
                    }
                    task.state++;
                    log(String.Format("Passing task back to decider"));
                    
                    SendMessage(task,"Provision","provsvc");
                    m = null;
                    task = null;
                    m=GetNextMessage("RemoteInvoke","ProvSvc");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void SendMessage(ProvisionTask task,string queue,string server)
        {
            MessageQueue q = new MessageQueue(String.Format(@"FormatName:direct=OS:{0}\private$\{1}", server, queue));
            q.Send(task);
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
            { Debug.WriteLine(ex.Message); }
            return m;
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
        private void RIWindows(ProvisionTask task)
        {
            //First lets construct some credentials
            //We're going to cheat and store credentials here
            string user = "vagrant";
            string pass = "Vag-rant1";
            string user2 = "mojo\\joe";
            string pass2 = "Ncc-1701d";
            string shellUri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";

            SecureString securepass = new SecureString();
            foreach (char c in pass.ToCharArray())
                securepass.AppendChar(c);
            PSCredential cred = new PSCredential(user, securepass);
            SecureString securepass2 = new SecureString();
            foreach (char c in pass2.ToCharArray())
                securepass2.AppendChar(c);
            PSCredential cred2 = new PSCredential(user2, securepass2);

            WSManConnectionInfo ci = new WSManConnectionInfo(false, task.IPAddress, 5985, "/wsman", shellUri, cred);

            using (Runspace runspace = RunspaceFactory.CreateRunspace(ci))
            {
                runspace.Open();
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runspace;
                //Renameserver
                log(String.Format("Renaming instance to {0}", task.hostname));
                string script = String.Format("$computerinfo=get-wmiobject -Class Win32_ComputerSystem ; $computerinfo.rename({0}{1}{0})", "\"", task.hostname);
                ps.AddScript(script);
                Collection<PSObject> PsOutput = ps.Invoke();
                ps.Commands.Clear();
                log(String.Format("Activating license"));
                ps.AddScript("cscript c:\\windows\\system32\\slmgr.vbs -ipk \"GDRKC-WG4G4-636M3-3PM3K-K9Y3P\";cscript c:\\windows\\system32\\slmgr.vbs -ato");
                ps.Invoke();
                log("Rebooting instance");
                ps.Commands.Clear();
                ps.AddCommand("restart-computer");
                ps.AddParameter("Force");
                ps.Invoke();
                Thread.Sleep(20000);//Gotta give the instance time to start shutting down
            }
            while (!Test4WinRM(task.IPAddress))
            {
                Thread.Sleep(5000);
            }
            using (Runspace runspace = RunspaceFactory.CreateRunspace(ci))
            {
                runspace.Open();
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runspace;
                log(String.Format("Joining instance to domain {0}", task.domain));
                string script = String.Format("netsh interface ip add dns name=\"Local Area Connection 2\" addr=192.168.1.50", "\"");
                ps.AddScript(script);
                ps.Invoke();
                ps.Commands.Clear();
                ps.AddCommand("add-computer");
                ps.AddParameter("DomainName", task.domain);
                ps.AddParameter("Credential", cred2);
                ps.Invoke();
                ps.Commands.Clear();
                ps.AddCommand("restart-computer");
                ps.AddParameter("Force");
                ps.Invoke();
                Thread.Sleep(20000);//Gotta give the instance time to start shutting down
            }
            while (!Test4WinRM(task.IPAddress))
            {
                Thread.Sleep(5000);
            }
            using (Runspace runspace = RunspaceFactory.CreateRunspace(ci))
            {
                log(String.Format("Installing puppet agent version {0}",task.puppetversion));
                runspace.Open();
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runspace;
                ps.AddScript(String.Format("cmd /c start /wait msiexec /qn /i c:\\installers\\puppetagents\\puppet-{0}-x64.msi PUPPET_MASTER_SERVER={1} PUPPET_AGENT_CERTNAME={2}.{3}", task.puppetversion, task.puppetmaster, task.hostname.ToLower(), task.domain.ToLower()));
                ps.Invoke();
            }
        }

        private bool Test4WinRM(string ip)
        {
            using (PowerShell ps = PowerShell.Create())
            {

                ps.AddCommand("test-wsman");
                ps.AddParameter("Computername", ip);
                Collection<PSObject> PSOutput = ps.Invoke();
                if (ps.Streams.Error.Count > 0)
                    return false;
                else
                    return true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }



    }
}
