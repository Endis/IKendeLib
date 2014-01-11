using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    [Serializable]
    public class Node:MarshalByRefObject
    {
        public string Name { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public DateTime LastTrackTime
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(string))
                return Name.Equals(obj.ToString());
            if(obj is Node)
                return Name.Equals(((Node)obj).Name);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
