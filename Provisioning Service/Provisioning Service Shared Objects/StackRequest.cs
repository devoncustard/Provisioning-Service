using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Provisioning_Service_Shared_Objects
{
    public class StackRequest
    {
        public Guid ID { get; set; }
        public List<ProvisionTask> Instances = new List<ProvisionTask>();
        
    }





    public class Storage
    {
        public int size {get;set;}
        
    }

}