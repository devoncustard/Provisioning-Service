 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADMgmtWorker.Interfaces;

namespace ADMgmtWorker.Classes
{
    public class TestMap : IADMapping
    {
        private Dictionary<string, string> Domains = new Dictionary<string, string>();
        private Dictionary<string, string> DomainTLA = new Dictionary<string, string>();
        private Dictionary<string, string> EnvironmentSLA = new Dictionary<string, string>();
        private Dictionary<string, string> RoleTLA = new Dictionary<string, string>();
        private Dictionary<string, string> LocationTLA = new Dictionary<string, string>();
        private Dictionary<string, string> RoleOU = new Dictionary<string, string>();



        public TestMap()
        {
            Domains.Add("mojo", "mojo.local");
            Domains.Add("mojo.local", "mojo.local");
            DomainTLA.Add("mojo.local", "MSD");
            EnvironmentSLA.Add("production", "P");
            EnvironmentSLA.Add("oat", "O");
            EnvironmentSLA.Add("dev", "D");
            EnvironmentSLA.Add("systest", "T");
            EnvironmentSLA.Add("mtest", "T");
            RoleTLA.Add("web", "WEB");
            RoleTLA.Add("app", "APP");
            RoleTLA.Add("sql", "DBS");
            LocationTLA.Add("benifold", "BEN");
            RoleOU.Add("web", "Web");
            RoleOU.Add("sql", "Database");

        }

        public string MapDomain(string name)
        {
            string domain = "not found";
            Domains.TryGetValue(name.ToLower(), out domain);
            return domain;
        }

        public string DomainToTLA(string FQDN)
        {
            string retval = "not found";
            DomainTLA.TryGetValue(FQDN.ToLower(), out retval);
            return retval;
        }
        public string LocationToTLA(string Location)
        {
            string retval = "not found";
            LocationTLA.TryGetValue(Location.ToLower(), out retval);
            return retval;
        }
        public string RoleToTLA(string Role)
        {
            string retval = "not found";
            RoleTLA.TryGetValue(Role.ToLower(), out retval);
            return retval;
        }
        public string EnvironmentToSLA(string Environment)
        {
            string retval = "not found";
            EnvironmentSLA.TryGetValue(Environment.ToLower(), out retval);
            return retval;
        }
        public string RoleToOU(string Role)
        {
            string retval = "not found";
            RoleOU.TryGetValue(Role.ToLower(), out retval);
            return retval;
        }


    }
}
