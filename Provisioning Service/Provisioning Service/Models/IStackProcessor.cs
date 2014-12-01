using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSSO;

namespace Provisioning_Service.Models
{
    interface IStackProcessor
    {
        Guid ProcessStack(StackRequest stackrequest);
       
    }
}
