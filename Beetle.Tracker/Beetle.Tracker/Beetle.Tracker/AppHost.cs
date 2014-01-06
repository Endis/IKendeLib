using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class AppHost
    {
        public string Host
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public System.Net.EndPoint ToEndPoint()
        {
            return new System.Net.IPEndPoint(System.Net.IPAddress.Parse(Host), Port);
        }
        public override string ToString()
        {
            return string.Format("{0}:{1}", Host, Port);
        }
    }
}
