﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glue4Net
{
    public class DomainArgs
    {
        public bool UpdateWatch
        {
            get;
            set;
        }
        public string[] WatchFilter
        {
            get;
            set;
        }
        public bool Compiler
        {
            get;
            set;
        }
    }
}
