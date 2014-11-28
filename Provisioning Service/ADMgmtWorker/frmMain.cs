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
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections;
using System.Messaging;
using ADMgmtWorker.Interfaces;
using ADMgmtWorker.Classes;
using Provisioning_Service_Shared_Objects;
using System.DirectoryServices;


namespace ADMgmtWorker
{
    public partial class frmMain : Form
    {
        private readonly IADMapping map = new TestMap();
        private string aduser=@"mojo\joe";
        private string adpass="Ncc-1701d";

        public frmMain()
        {
            InitializeComponent();
        }

        private void buttonToggleTimer_Click(object sender, EventArgs e)
        {
            chkTimerEnabled.Checked = (chkTimerEnabled.Checked == true) ? false : true;
            timerProcessQueue.Enabled = chkTimerEnabled.Checked;
        }
        private void log(string log)
        {
            txtLog.AppendText(Environment.NewLine + String.Format("{0} : {1}", DateTime.Now.ToLongTimeString(), log));
        }
        private void timerProcessNameRequests_Tick(object sender, EventArgs e)
        {
            ProcessNameRequestQueue();
        }

        private void ProcessNameRequestQueue()
        {

            MessageQueue rq = new MessageQueue(@"FormatName:direct=OS:provsvc\private$\NameRequest");
            
            rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
            TimeSpan timeout = new TimeSpan(0, 0, 0);
            try
            {
                System.Messaging.Message m = null;
                try
                {
                    m = rq.Receive(timeout);
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                while (m != null)
                {
                    ProvisionTask task = (ProvisionTask)m.Body;
                    log(String.Format("Task {0} requesting a new domain object", task.taskid));
                    var result = RequestName(task);
                    task.state = (result == "not found") ? 99 : 2;
                    task.hostname = result;
                    log(String.Format("Domain object {0} created. Passing task back to decider", task.hostname));

                    var resultq = new MessageQueue(@"FormatName:direct=OS:provsvc\private$\provision");
                    resultq.Send(task);

                    m = null;
                    try
                    {

                        m = rq.Receive(timeout);
                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                log(String.Format("Exception caught : {1}{0}{2}", Environment.NewLine, ex.Message, ex.InnerException));
            }

        }




        private string RequestName(ProvisionTask task)
        {
            string oupath="";
            string servername="";
            string domain = map.MapDomain(task.domain);
            string searchpath = String.Format("LDAP://{0}/{1}", domain, DomainToLDAP(domain));
            //DomainToLDAP(domain);
            DirectoryEntry de =new DirectoryEntry(searchpath,aduser,adpass,AuthenticationTypes.Sealing&AuthenticationTypes.Secure);
            DirectorySearcher ds=new DirectorySearcher(de);
            string namepart=string.Format("{0}{1}{2}{3}",map.DomainToTLA(domain),map.LocationToTLA(task.location),map.RoleToTLA(task.role),map.EnvironmentToSLA(task.environment));
            ds.Filter=String.Format("(&(objectCategory=computer)(name={0}*))",namepart);
            var result=ds.FindAll();
            
            SearchResult[] r=new SearchResult[result.Count];
            result.CopyTo(r,0);
            int i=0;
            while(i<1000)
            {
                i++;
                if (Array.Find (r,p=>p.Path.Contains(String.Format("CN={0}{1}",namepart,i.ToString("D3"))))==null)
                    break;
            }
            if (i<1000)
            {
                string addpath = String.Format("LDAP://{0}/OU={1},OU=Servers,OU={2},{3}", map.MapDomain(task.domain), map.RoleToOU(task.role), map.DomainToTLA(task.domain), DomainToLDAP(task.domain));
                de = new DirectoryEntry(addpath, aduser, adpass, AuthenticationTypes.Secure & AuthenticationTypes.Sealing);
                string newname = String.Format("{0}{1}V", namepart, i.ToString("D3"));
                DirectoryEntry newserver=de.Children.Add("CN="+newname,"computer");
                newserver.CommitChanges();
                newserver.Properties["userAccountcontrol"].Value = 0x1000;
                newserver.CommitChanges();
                return newname;
            }
        

                
            return "unable to create";
        }

        public string DomainToLDAP(string domain)
        {
            string[] DC = domain.Split('.');
            string retval="";
            foreach (string part in DC)
                retval += string.Format("DC={0},", part);
            return retval.Substring(0, retval.Length - 1);

        }

        private System.Messaging.Message GetPeek(MessageQueue q, System.Messaging.Cursor c, PeekAction action)
        {
            System.Messaging.Message ret = null;
            try
            {
                ret = q.Peek(new TimeSpan(1), c, action);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return ret;
        }

        private System.Messaging.Message GetMessage(MessageQueue q, string id)
        {
            System.Messaging.Message ret = null;

            ret = q.ReceiveById(id);

            return ret;


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
    }
}
