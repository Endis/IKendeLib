using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace KGlue
{
    public interface IAppAdapter
    {
        void Start(string[] args);
        string Name { get; }
        void Stop();
    }
}
