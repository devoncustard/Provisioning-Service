using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSSO
{
    public class ProvisionTask
    {
        public Guid parentid { get; set; }
        public Guid taskid { get; set; }
        public string hostname { get; set; }
        public string role { get; set; }
        public string location { get; set; }
        public string environment { get; set; }
        public string commonname { get; set; }
        public Provider provider { get; set; }
        public string puppetmaster { get; set; }
        public string domain { get; set; }
        public string target { get; set; }
        public int cpus { get; set; }
        public int memory { get; set; }
        public Image image { get; set; } 
        public int state {get;set;}
        public string puppetversion { get; set; }
        public string IPAddress { get; set; }
        public string identifier { get; set; }
        public List<string> puppetclasses { get; set; }
    }
}