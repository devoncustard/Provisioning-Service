using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENC2.Models
{
    public class Node
    {
        public string hostname { get; set; }
        public string classes { get; set; }
        public string parameters { get; set; }
        public string environment { get; set; }

        public string ENCOutput()
        {
            string encoutput = "---\nclasses:\n";


            //classes
            foreach (string s in classes.Split(','))
            {
                encoutput += "\t- s\n";
            }
            //parameters

            encoutput += "parameters:\n";
            foreach (string p in parameters.Split('`'))
            {
                encoutput += String.Format("\t{0}:\n", p); ;
                foreach (string i in p.Split('~'))
                {
                    encoutput += String.Format("\t\t- {0}\n", i);
                }

            }
            //environment
            encoutput += String.Format("environment: {0}\n", environment);
            return encoutput;
        }
        public Node(string _hostname, string _classes, string _parameters, string _environment)
        {
            hostname = _hostname;
            classes = _classes;
            parameters = _parameters;
            environment = _environment;
        }
    }
}