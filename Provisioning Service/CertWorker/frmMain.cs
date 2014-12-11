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
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

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
            {
                Debug.WriteLine(ex.Message);
            }
            return m;
        }
        private void ProcessQueue()
        {

            var m = GetNextMessage("CertSignRequest", "provsvc");

            try
            {
                while (m != null)
                {
                    ProvisionTask task = (ProvisionTask)m.Body;
                    log(String.Format("Task {1} received.{0}ProcessingSigning Cert", Environment.NewLine, task.taskid));
                    ProcessCert(task);
                    m = null;
                    task = null;
                    m = GetNextMessage("CertSignRequest", "ProvSvc");
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        private void ProcessCert(ProvisionTask task)
        {
            string certname=String.Format("{0}.{1}",task.hostname,task.domain);
            var s = GetCertStatus(certname,task.puppetmaster,task.environment);
            switch (s.ToString().ToLower())
            {
                case "requested":
                    SignCert(certname, task.puppetmaster, task.environment); 
                    log(String.Format("Cert should be signed, dropping back into queue to be reprocessed"));

                    SendMessage(task,"certsignrequest","provsvc");

                    break;
                    
                case "signed":
                    task.state++;
                    task.certsigned=true;
                    log(String.Format("Passing task back to decider"));
                    SendMessage(task,"provision","provsvc");
                    break;

                default:
                    SendMessage(task, "certsignrequest", "provsvc");
                    log("Dropped to the default for some reason. popping back into the queue");
                    Debug.WriteLine("arrgghh");
                    break;
            }
            
        }
        private void SendMessage(ProvisionTask task, string queue, string server)
        {
            MessageQueue q = new MessageQueue(String.Format(@"FormatName:direct=OS:{0}\private$\{1}", server, queue));
            q.Send(task);
        }   
        private bool SignCert(string certname, string puppetmaster, string environment)
        {
            IgnoreBadCertificates();
            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            using (var client=new HttpClient(handler))
            {
                client.MaxResponseContentBufferSize = 256000;
                //client.DefaultRequestHeaders.Add("Content-Type", "text/pson");
                HttpContent content = new StringContent(@"{""desired_state"":""signed""}", Encoding.UTF8, "text/pson");
                
                var r = client.PutAsync(String.Format("https://{0}:8140/{1}/certificate_status/{2}", puppetmaster, environment, certname.ToLower()), content).Result;
            
            }
            return false;
        }


        private string GetCertStatus(string certname,string puppetmaster,string environment)
        {
            string rv = "not found";
            try
            {
                IgnoreBadCertificates();

                
                var handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;

                using (var client = new HttpClient(handler))
                {
                    client.MaxResponseContentBufferSize = 256000;
                    //client.DefaultRequestHeaders.Accept.Add()
                    client.DefaultRequestHeaders.Add("Accept", "text/pson");

                    var r = client.GetStringAsync(String.Format("https://{0}:8140/{1}/certificate_status/{2}", puppetmaster, environment, certname.ToLower())).Result;
                    var model = JsonConvert.DeserializeObject<RootObject>(r);
                    if (model is RootObject)
                        rv = model.state;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return rv;



        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }



        public  void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }
        private bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        } 
    }
    public class Fingerprints
    {
        public string SHA512 { get; set; }
        public string SHA256 { get; set; }
        public string SHA1 { get; set; }
        public string @default { get; set; }
    }

    public class RootObject
    {
        public List<object> dns_alt_names { get; set; }
        public string fingerprint { get; set; }
        public string state { get; set; }
        public string name { get; set; }
        public Fingerprints fingerprints { get; set; }
    }

}
