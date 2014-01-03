using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    public class Group
    {
        public string Name { get; set; }

        public List<Node> Nodes
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(string))
                return Name.Equals(obj.ToString());
            if(obj is Group)
                return Name.Equals(((Group)obj).Name);
            return base.Equals(obj);
        }


        public long CursorIndex
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
