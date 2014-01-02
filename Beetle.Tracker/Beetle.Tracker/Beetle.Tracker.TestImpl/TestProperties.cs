using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    public class TestProperties:Beetle.Tracker.Properties
    {
        public string Group
        {
            get
            {
                return this["GROUP"];
            }
            set
            {
                this["GROUP"] = value;
            }
        }
        public string Host
        {
            get
            {
                return this["HOST"];
            }
            set
            {
                this["HOST"] = value;
            }
        }
        public string Port
        {
            get
            {
                return this["PORT"];
            }
            set
            {
                this["PORT"] = value;
            }
        }
        public string Node
        {
            get
            {
                return this["NODE"];
            }
            set
            {
                this["NODE"] = value;
            }
            
        }
    }
}
