using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl.TestClient
{
    class Program
    {
        private static Beetle.Tracker.TrackerClient mClient;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(Beetle.LICENSE.GetLICENSE());
                Beetle.TcpUtils.Setup("beetle");
                mClient = new TrackerClient("trackerClientSection");
                System.Threading.Thread.Sleep(1000);
                AppHost host = mClient.GetHost();
                Console.WriteLine(host);
            }
            catch (Exception e_)
            {
                Console.WriteLine(e_.Message);
            }
            Console.Read();
        }
    }
}
