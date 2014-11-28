using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provisioning_Service_Shared_Objects;

namespace Provisioning_Service.Models
{
    interface IStackProcessor
    {
        Guid ProcessStack(StackRequest stackrequest);
       
    }
}
