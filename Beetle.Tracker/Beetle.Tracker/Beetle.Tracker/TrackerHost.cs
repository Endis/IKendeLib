using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class TrackerHost
    {
        public string IPAddress
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }

        private Beetle.Clients.SyncNode mClient;

        public Beetle.Clients.SyncNode Client
        {
            get
            {
                if (mClient == null)
                {
                    mClient = new Clients.SyncNode(IPAddress, Port, 2);
                    mClient.DetectTime = 5;
                    
                }
                return mClient;
            }
        }
    }
}
