using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    public class TestTackerHandler:Beetle.Tracker.IAppTrackerHandler
    {
        public TestTackerHandler()
        {
            Formater = new TestFormater();
        }
        public IInfoFormater Formater
        {
            get;
            set;
        }

        public IProperties Register(IProperties properties)
        {
            TestProperties tp = new TestProperties();
            tp.FromHeaders(properties.ToHeaders());
            return new Properties();
        }

        public object GetInfo(IProperties properties)
        {
            return null;
        }


        public AppHost GetHost(IProperties properties)
        {
            return null;
        }
    }
}
