using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Messaging;
using PSSO;

namespace Requester
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StackRequest request = new StackRequest();
            ProvisionTask r = new ProvisionTask();
            r.state = 0;
            r.commonname = "W2008R2-Source";
            r.domain = "mojo.local";
            r.environment = "production";
            r.location = "benifold";
            r.provider = Provider.Vagrant;
            r.puppetmaster = "puppet.mojo.local";
            r.puppetversion = "3.7.3";
            r.role = "web";
            r.target = "achilles";
            r.cpus = 2;
            r.memory = 2536;
            r.puppetclasses = new List<string>();
            r.puppetclasses.Add("joe");
            request.Instances.Add(r);

            HttpClient client = new HttpClient();
            
            HttpResponseMessage response = client.PostAsJsonAsync("http://provsvc/provisioning/api/stack", request).Result;

            ;
        }

        private void buttonPollForRequest_Click(object sender, EventArgs e)
        {
            string remoteq = @"FormatName:direct=OS:provsvc\private$\Provision";
            MessageQueue rq = new MessageQueue(remoteq);
            System.Messaging.Cursor cursor = rq.CreateCursor();
            TimeSpan timeout=new TimeSpan(0,0,10);
            rq.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
    
            System.Messaging.Message m;
            
            m = GetPeek(rq, cursor, PeekAction.Current);

            while (m != null) 
            {
                
                ProvisionTask r = (ProvisionTask)m.Body;
                if (r.target.ToLower()==Environment.MachineName.ToLower())
                {
                    m = GetMessage(rq,m.Id);
                }
                m = GetPeek(rq, cursor, PeekAction.Next);
            }
            



            cursor.Close();



        }

        System.Messaging.Message GetPeek(MessageQueue q,System.Messaging.Cursor c,PeekAction action)
        {
            System.Messaging.Message ret = null;
            try
            {
                ret = q.Peek(new TimeSpan(1), c, action);
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
            return ret;
        }

        System.Messaging.Message GetMessage(MessageQueue q,string id)
        {
            System.Messaging.Message ret = null;

            ret = q.ReceiveById(id);

            return ret;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Log(string logmsg)
        {
            textBoxLog.Text = logmsg + "\n\r" + textBoxLog.Text;

        }

 
    }



}
