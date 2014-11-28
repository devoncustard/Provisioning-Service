using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;
using System.Net.Http;
using System.Net.Http.Headers;
using Provisioning_Service_Shared_Objects;

namespace Provisioning_Service.Models
{
    public class StackProcessor :IStackProcessor
    {
        string ProvisionQueue = "FormatName:direct=OS:provsvc\\private$\\Provision";
        
        public Guid ProcessStack(StackRequest request)
        {
            MessageQueue provisionqueue=new MessageQueue(ProvisionQueue);
            request.ID = Guid.NewGuid();

            foreach (ProvisionTask task in request.Instances)
            {
                task.taskid = Guid.NewGuid();
                task.parentid = request.ID;
                provisionqueue.Send(task);
            }
            //Need to store request in database
            return request.ID;
        }


    }
}