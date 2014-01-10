using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    public class TestTrackerModule:Glue4Net.IAppModule
    {
        public string Name
        {
            get { return "Test Tracker Module"; }
        }

        private TestTackerHandler mTrackerHandler = new TestTackerHandler();

        public Glue4Net.IEventLog Log
        {
            get;
            set;
        }

        public void Load()
        {
            Glue4Net.Context.Current.CreateProxyObjectHandler = (name) => {
                return mTrackerHandler;
            };
        }

        public void UnLoad()
        {

        }
    }
}
