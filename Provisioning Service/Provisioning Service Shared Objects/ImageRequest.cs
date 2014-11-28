using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Provisioning_Service_Shared_Objects
{
    public class ImageRequest
    {
        public string CommonName { get; set; }
        public int ImageType { get; set; }
        public int Provider { get; set; }
    }
}