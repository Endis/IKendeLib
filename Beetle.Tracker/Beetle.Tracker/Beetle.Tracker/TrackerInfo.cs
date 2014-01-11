using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    [Serializable]
    public class TrackerInfo
    {
        public string TypeName
        {
            get;
            set;
        }
        public string Data
        {
            get;
            set;
        }
    }
}
