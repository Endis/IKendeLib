using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace KGlue
{
    class RunStatus : MarshalByRefObject
    {
        public string Type { get; set; }

        public string Error { get; set; }
    }
}
