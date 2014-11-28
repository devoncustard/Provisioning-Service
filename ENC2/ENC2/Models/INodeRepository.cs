using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENC2.Models
{
    interface INodeRepository
    {                                                                                                    
        Nodes Get(string hostname);
        void Add(Nodes item);
        void Remove(string hostname);
        void Update(Nodes item);
    }
}
