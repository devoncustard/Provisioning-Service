using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using PSSO;

namespace Tramp
{
    class VagrantVM
    {
        public int boot_timeout { get; set; }
        public string guest { get; set; }
        public string box { get; set; }
        public string communicator { get; set; }
        public string riusername { get; set; }
        public string ripassword { get; set; }
        public int maxtries { get; set; }
        public int winrmtimeout { get; set; }
        public string vbname { get; set; }
        public int ram { get; set; }
        public int cpus { get; set; }
        public ProvisionTask task {get;set;}
        public string vagrantfile;
        public string vagrantfolder;
        public VagrantVM()
        { }
        public VagrantVM(ProvisionTask task)
        {
            this.task = task;
            vbname = task.hostname;
            communicator=(task.image.OS_Family==OSFamily.Windows)?"winrm":"ssh";
            ram=task.memory;
            cpus=task.cpus;
            box=task.image.Id;
            guest=task.hostname;
            riusername="Vagrant";
            ripassword="Vag-rant1";

        }

    }
}
