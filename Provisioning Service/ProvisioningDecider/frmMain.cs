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
using System.Net.Http.Headers;
using System.Web;


using PSSO;

namespace ProvisioningDecider
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
            ProcessProvisionQueue();
        }
        private void ProcessProvisionQueue()
        {


            var m = GetNextMessage("provision", "provsvc");


            while (m != null)
            {

                ProvisionTask task = (ProvisionTask)m.Body;

                switch (task.state)
                {
                    case 0://Posted, no processing
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        task.image = GetImageDetails(task);
                        task.state++;
                        SendMessage(task, "provision", "provsvc");
                        break;
                    case 1://Pass to ADMgmt
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));

                        SendMessage(task,"namerequest", "provsvc");
                        break;
                    case 2:
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        task.state++;
                        SendMessage(task, "provision", "provsvc");
                        break;
                    case 3://Pass to ENC
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        SendMessage(task,"encrequest", "provsvc");
                        break;
                    case 4:
                        log(String.Format("Task ID {0} State {1}", task.taskid, task.state));
                        task.state++;
                        SendMessage(task, "provision", "provsvc");
                        break;

                    case 5://Pass to Provider
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        string provider="";
                        switch (task.provider)
                        {
                            case Provider.Vagrant:
                                provider = "Vag";
                                break;

                        }
                        SendMessage(task,String.Format("{0}provider", provider), "provsvc");
                        break;
                    case 6:
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        task.state++;
                        SendMessage(task, "provision", "provsvc");
                        break;

                    case 7://Pass to RemoteInvoker
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        SendMessage(task,"RemoteInvoke", "provsvc");
                        break;
                    case 8:
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        task.state++;
                        SendMessage(task, "provision", "provsvc");
                        break;
                    case 9://Pass to CertSigner
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        SendMessage(task,"certsignrequest", "provsvc");
                        break;

                    case 10:
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        task.state++;
                        SendMessage(task, "provision", "provsvc");
                        break;
                    case 11://Complete
                        log(String.Format("Task ID {0} State {1}", task.taskid,task.state));
                        SendMessage(task,"complete", "provsvc");
                        break;
                    default:
                        break;



                }
                m = null;
                task = null;

                m = GetNextMessage("provision", "provsvc");
            }

        }
        private PSSO.Image GetImageDetails(ProvisionTask task)
        {
            PSSO.Image image = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(String.Format(@"http://webbake/bakery/api/image/?ImageType=2&CommonName={0}&Provider={1}", task.commonname, (int)task.provider)).Result;
                    image = response.Content.ReadAsAsync<PSSO.Image>().Result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return image;
        }
        private void failed(ProvisionTask task)
        {
            GetProvisionTaskMessage("provision", task.taskid);
            MessageQueue rq = new MessageQueue(@"FormatName:direct=OS:provsvc\private$\failed");
            rq.Formatter = rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
            rq.Send(task);
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
        private void SendMessage(ProvisionTask task, string queue, string server)
        {
            MessageQueue rq = new MessageQueue(String.Format(@"FormatName:direct=OS:{0}\private$\{1}", server, queue));
            //rq.Formatter = rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
            rq.Send(task);
        }
        private System.Messaging.Message GetProvisionTaskMessage(string queuename, Guid taskid)
        {
            MessageQueue rq = new MessageQueue(String.Format(@"FormatName:direct=OS:provsvc\private$\{0}", queuename));
            rq.Formatter = rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
            System.Messaging.Cursor cursor = rq.CreateCursor();
            TimeSpan timeout = new TimeSpan(0, 0, 10);


            System.Messaging.Message m = GetPeek(rq, cursor, PeekAction.Current);
            while (m != null)
            {
                ProvisionTask task = (ProvisionTask)m.Body;
                if (task.taskid == taskid)
                {
                    m = GetMessageByID("Provision", m.Id);
                    rq.Close();
                    return m;
                }
            }
            return null;
        }
        private System.Messaging.Message GetMessageByID(string queuename, string messageid)
        {
            MessageQueue rq = new MessageQueue(String.Format(@"FormatName:direct=OS:provsvc\private$\{0}", queuename));
            rq.Formatter = rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
            System.Messaging.Message m = rq.ReceiveById(messageid);
            rq.Close();
            return m;
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
        private bool RequestHostname(ProvisionTask task)
        {

            try
            {
                MessageQueue rq = new MessageQueue(@"FormatName:direct=OS:provsvc\private$\NameRequest");
                rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
                rq.Send(task);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;

        }
        private void log (string log)
        {
            txtLog.AppendText(Environment.NewLine + String.Format("{0} : {1}", DateTime.Now.ToLongTimeString(), log));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
