using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace KGlue
{

    public delegate void EventDomainExecuting(object sender, DomainExecutingArgs e);

    public class DomainExecutingArgs : EventArgs
    {
        public string App
        {
            get;
            set;
        }
        public string AppFullPath
        {
            get;
            set;
        }
        public ExecutingStatus Status
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }

        public string Message { get; set; }

    }
}
