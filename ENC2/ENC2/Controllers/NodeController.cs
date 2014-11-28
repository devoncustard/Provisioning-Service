using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ENC2.Models;

namespace ENC2.Controllers
{
    public class NodeController : ApiController
    {
        static readonly INodeRepository repository = new NodeRepository();





        public string GetNode( [FromUri] string hostname)
        {
            Nodes n = repository.Get(hostname);
             
            if (n == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return n.Enctext;


        }
        [HttpPost]
        public void PostNode (wnode node)
        {
            Nodes newnode = new Nodes();
            newnode.Node = node.hostname;
            newnode.Enctext = node.enctext;
            repository.Add(newnode);

        }
        public void PutNode (string hostname, string enctext)
        {
            Nodes node = repository.Get(hostname);
            node.Enctext = enctext;
            repository.Update(node);


        }
        public void DeleteNode(string hostname)
        {
            repository.Remove(hostname);
        }

    }


}
