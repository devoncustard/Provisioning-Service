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
using System.Collections.ObjectModel;
using Provisioning_Service_Shared_Objects;
using System.IO;
namespace Tramp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //configure messaging component
            
        }
        
        void ProcessQueue()
        {
            MessageQueue q = new MessageQueue(@"FormatName:direct=OS:provsvc\private$\vagprovider");
            q.Formatter = new XmlMessageFormatter(new[] { typeof(ProvisionTask) });
            

            try
            {
                System.Messaging.Message m=null;

                try {
                    m = q.Receive(new TimeSpan(0, 0, 1));
                }
                catch (Exception ex)
                {}
                if (m!=null)
                {
                    ProvisionTask task = (ProvisionTask)m.Body;
                    log(String.Format("Processing provision request for Task {0}. Hostname will be {1}{4}vCPU's {2}{4}RAM {3}", task.taskid, task.hostname, task.cpus, task.memory,Environment.NewLine));
                    var vm = new VagrantVM(task);
                    ProvisionVM(vm);
                    task.state=4;
                    MessageQueue rq = new MessageQueue(@"FormatName:direct=OS:provsvc\private$\Provision");
                    log(String.Format("Instance IP: {1}{0}Instance Identifier: {2}", Environment.NewLine, task.IPAddress, task.identifier));
                    log(String.Format("Task {0} passing back to decider", task.taskid));
                    rq.Send(task);
                    rq.Close();
                    rq.Dispose();
                    vm = null;
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            q.Close();
            q.Dispose();
        }
        private void log(string log)
        {
            txtLog.AppendText(Environment.NewLine + String.Format("{0} : {1}", DateTime.Now.ToLongTimeString(), log));
        }

        private void buttonToggleTimer_Click(object sender, EventArgs e)
        {
            chkTimerEnabled.Checked = (chkTimerEnabled.Checked == true) ? false : true;
            timerProcessQueue.Enabled = chkTimerEnabled.Checked;
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

        private void timerProcessQueue_Tick(object sender, EventArgs e)
        {
            timerProcessQueue.Enabled = false;
            ProcessQueue();

            timerProcessQueue.Enabled = this.chkTimerEnabled.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void ProvisionVM(VagrantVM vm)
        {
            string folder = Properties.Settings.Default.VMFolder;
            try
            {
                vm.vagrantfolder = String.Format("{0}\\{1}", folder, vm.vbname);
                vm.vagrantfile = String.Format("{0}\\vagrantfile", vm.vagrantfolder);

                DownloadBox(vm);
                CreateVagrantFile(vm);
                PowerOnVM(vm);
                GetIPAddress(vm);
                GetIdentifier(vm);
            }
            catch (Exception ex)
            { throw; }
        }
        private void CreateVagrantFile(VagrantVM vm)
        {
            log("Creating vagrantfile");
            try
            {
                Directory.CreateDirectory(vm.vagrantfolder);

                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddCommand("set-location");
                    ps.AddArgument(vm.vagrantfolder);
                    ps.Invoke();
                    ps.Commands.Clear();
                    ps.Commands.AddCommand("vagrant");
                    ps.Commands.AddArgument("init");
                    Collection<PSObject> PSOutput = ps.Invoke();

                }
                File.Delete(vm.vagrantfile);
                using (StreamWriter sw = File.CreateText(vm.vagrantfile))
                {
                    sw.WriteLine("# -*- mode: ruby -*-");
                    sw.WriteLine("# vi: set ft=ruby :");
                    sw.WriteLine("# Vagrantfile API/syntax version. Don't touch unless you know what you're doing!");
                    sw.WriteLine("Vagrant.configure(\"2\") do |config|");
                    sw.WriteLine("    config.vm.boot_timeout=600");
                    sw.WriteLine(String.Format("    config.vm.define :'{0}' do |guest|", vm.vbname));
                    sw.WriteLine(String.Format("        guest.vm.box= \"{0}\" ", vm.box));
                    sw.WriteLine(String.Format("        guest.vm.guest = :{0}", vm.guest));
                    sw.WriteLine(String.Format("        guest.vm.communicator = \"{0}\"", vm.communicator));
                    sw.WriteLine("        guest.windows.halt_timeout=25");
                    sw.WriteLine(String.Format("        guest.winrm.username = \"{0}\"", vm.riusername));
                    sw.WriteLine(String.Format("        guest.winrm.password=\"{0}\"", vm.ripassword));
                    sw.WriteLine("        guest.winrm.max_tries=30");
                    sw.WriteLine("        guest.winrm.timeout=3600");
                    sw.WriteLine("        guest.windows.set_work_network");
                    sw.WriteLine("        guest.vm.network :forwarded_port, guest:5985, host:5985, id:\"winrm\", auto_correct: true");
                    sw.WriteLine("        guest.vm.network :forwarded_port, guest:22, host:22, id:\"ssh\", auto_correct: true");
                    sw.WriteLine("        guest.vm.network \"public_network\", :bridge => 'Intel(R) Ethernet Connection I217-V'");
                    sw.WriteLine("        guest.vm.provider :virtualbox do |vb|");
                    sw.WriteLine(String.Format("			  vb.name=\"{0}\"", vm.vbname));
                    sw.WriteLine("            vb.gui = true");
                    sw.WriteLine(String.Format("            vb.customize [\"modifyvm\", :id, \"--memory\", \"{0}\"]", vm.ram));
                    //sw.WriteLine(String.Format("            vb.customize [\"modifyvm\", :id, \"--name\", \"{0}\"]",vbname));
                    sw.WriteLine("        end");
                    sw.WriteLine("    end");
                    sw.WriteLine("end");
                }
            }
            catch (Exception ex)
            { }

            ;

        }
        private void DownloadBox(VagrantVM vm)
        {
            try
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddCommand("vagrant");
                    ps.AddParameter("box", "list");
                    ps.AddCommand("select-string");
                    ps.AddParameter("-pattern ", vm.task.image.Id);

                    Collection<PSObject> PSOutput = ps.Invoke();

                    if (PSOutput.Count == 0)
                    {
                        log(String.Format("Downloading box from {0}", vm.task.image.Location));
                        ps.Commands.Clear();
                        ps.AddCommand("vagrant");
                        ps.AddArgument("box");
                        ps.AddArgument("add");
                        ps.AddArgument("--name");
                        ps.AddArgument(vm.task.image.Id);
                        ps.AddArgument("--insecure");
                        ps.AddArgument(vm.task.image.Location);
                        PSOutput = ps.Invoke();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        private void PowerOnVM(VagrantVM vm)
        {
            log(String.Format("Powering on {0}", vm.vbname));
            try
            {
                Collection<PSObject> PSOutput;
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddCommand("set-location");
                    ps.AddArgument(vm.vagrantfolder);
                    ps.Invoke();
                    ps.Commands.Clear();
                    ps.AddCommand("vagrant");
                    ps.AddArgument("up");
                    PSOutput = ps.Invoke();
                }
            }
            catch (Exception ex)
            { ; }
        }
        private void GetIPAddress(VagrantVM vm)
        {
            try
            {
                Collection<PSObject> PSOutput;
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddCommand("vboxmanage");
                    ps.AddArgument("showvminfo");
                    ps.AddArgument(vm.vbname);
                    ps.AddCommand("select-string");
                    ps.AddParameter("pattern", "Bridged");
                    PSOutput = ps.Invoke();
                    string output = PSOutput[0].ToString();
                    int mac = output.IndexOf("MAC");
                    string m = output.Substring(mac + 5, 12);
                    ps.Commands.Clear();
                    ps.AddCommand("vboxmanage");
                    ps.AddArgument("guestproperty");
                    ps.AddArgument("enumerate");
                    ps.AddArgument(vm.vbname);
                    ps.AddCommand("select-string");
                    ps.AddParameter("pattern", m);

                    PSOutput = ps.Invoke();
                    string s = (PSOutput[0].ToString().Split('/'))[4];
                    ps.Commands.Clear();
                    ps.AddCommand("vboxmanage");
                    ps.AddArgument("guestproperty");
                    ps.AddArgument("get");
                    ps.AddArgument(vm.vbname);
                    ps.AddArgument(String.Format(@"/VirtualBox/GuestInfo/Net/{0}/V4/IP", s));
                    PSOutput = ps.Invoke();
                    s = PSOutput[0].ToString().Replace("Value: ", "");
                    vm.task.IPAddress = s;
                    log (String.Format("VM Ip address is {0}",vm.task.IPAddress));
                }
            }
            catch (Exception ex)
            { ;}

        }
        private void GetIdentifier(VagrantVM vm)
        {
            vm.task.identifier = vm.vbname;
        }



    }

}
