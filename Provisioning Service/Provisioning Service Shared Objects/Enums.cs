using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Provisioning_Service_Shared_Objects
{
    [Serializable]
    public enum OSFamily { 
        [XmlEnum("0")]
        None,
        [XmlEnum("1")]
        Windows,
        [XmlEnum("2")]
        AWS,
        [XmlEnum("3")]
        CentOS,
        [XmlEnum("4")]
        Debian,
        [XmlEnum("5")]
        Fedora
    }
    [Serializable]
    public enum ImageType {
        [XmlEnum("0")]
        None,
        [XmlEnum("1")]
        Windows_PreSysPrep,
        [XmlEnum("2")]
        Windows_PostSysPrep
    }
    [Serializable]
    public enum Provider {
        [XmlEnum("0")]
        None,
        [XmlEnum("1")]
        Vagrant,
        [XmlEnum("2")]
        VMWare,
        [XmlEnum("3")]
        AWS,
        [XmlEnum("4")]
        Azure,
        [XmlEnum("5")]
        GoogleCloud
    }
}
