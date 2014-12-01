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
                        //ok some basic processing of the request first. 
                        //lets get image details
                        log(String.Format("Task ID {0} transition to state 0.{1} Requesting AD Computer object.", task.taskid,Environment.NewLine));
                        task.image = GetImageDetails(task);
                        //Reqeust domain object to be created. increment status
                        task.state++;
                        SendMessage("namerequest", "provsvc", task);
                        break;
                    case 1://Domain Object Reqeusted
                        //ignore
                        break;
                    case 2://Domain Object Created
                        
                        log(String.Format("Task ID {0} transition to state 2. Domain object created. Hostname will be {1}.{3} Sending task to {2} provider",task.taskid,task.hostname,task.provider,Environment.NewLine));
                        task.state = 3;
                        string provider="";
                        switch (task.provider)
                        {
                            case Provider.Vagrant:
                                provider = "Vag";
                                break;

                        }
                        SendMessage(String.Format("{0}provider", provider), "provsvc", task);
                        break;
                    case 3:
                        break;
                    case 4://Instance Provisioned
                        log(String.Format("Task ID {0} transition to state 4.Instance has been provisioned. Passing to Remote Invocation", task.taskid));
                        task.state=5;
                        SendMessage("RemoteInvoke", "provsvc", task);
                        break;

                    case 5:
                        break;
                    case 6:
                        log(String.Format("Task ID {0} transition to state 6.Cert needs to be signed", task.taskid));
                        task.state=7;
                        SendMessage("certsignrequest", "provsvc", task);
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10://Cert signed
                        break;
                    case 11://puppet results ready
                        break;
                    default:
                        SendMessage("complete", "provsvc", task);
                        break;



                }
                m = null;


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

                    var response = client.GetAsync(String.Format(@"http://webbake/bakery/api/image/?ImageType=1&CommonName={0}&Provider={1}", task.commonname, (int)task.provider)).Result;
                    image = response.Content.ReadAsAsync<PSSO.Image>().Result;
                }
            }
            catch (Exception ex)
            { }
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
            { }
            return m;
        }
        private void SendMessage(string queue, string server, ProvisionTask task)
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
