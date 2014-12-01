using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSSO
{
 
    public class Image
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string CommonName { get; set; }
        public ImageType ImageType { get; set; }
        public string BakedOn { get; set; }
        public Provider Provider { get; set; }
        public OSFamily OS_Family { get; set; }
        public string OS_Version { get; set; }
        public bool Approved{get;set;}
            
    }
}