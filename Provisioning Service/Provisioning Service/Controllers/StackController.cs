using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Provisioning_Service.Models;
using Provisioning_Service_Shared_Objects;

namespace Provisioning_Service.Controllers
{
    public class StackController : ApiController
    {

        public static StackProcessor processor = new StackProcessor();

        public string GetStack(string id)
        {
            return id;
        }
        
        [HttpPost]
        public Guid PostStack(StackRequest request)
        {
            
            Guid id=processor.ProcessStack(request);
            return id;
        }
          
    }
}
