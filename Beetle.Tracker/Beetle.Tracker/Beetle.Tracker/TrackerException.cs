using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class TrackerException:Exception
    {
        public TrackerException()
        {
        }
        public TrackerException(string message)
            : base(message)
        {
        }
        public TrackerException(string message, Exception innerError)
            : base(message, innerError)
        {

        }
    }
}
