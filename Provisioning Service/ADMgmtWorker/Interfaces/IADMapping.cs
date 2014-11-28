using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMgmtWorker.Interfaces
{

    public interface IADMapping
    {

        string MapDomain(string name);
        string DomainToTLA(string FQDN);
        string LocationToTLA(string Location);
        string RoleToTLA(string Role);
        string EnvironmentToSLA(string Environment);
        string RoleToOU(string Role);
    }
}
