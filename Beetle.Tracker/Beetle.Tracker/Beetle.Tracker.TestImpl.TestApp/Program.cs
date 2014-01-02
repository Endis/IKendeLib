using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl.TestApp
{
    class Program
    {
        private static Beetle.Tracker.AppToTracker<TestImpl.TestFormater, TestProperties> mToTracker;
        static void Main(string[] args)
        {
            mToTracker = new AppToTracker<TestFormater, TestProperties>("trackerSection");
            mToTracker.Register = (o,e) => { };
            mToTracker.Start();
        }
    }
}
