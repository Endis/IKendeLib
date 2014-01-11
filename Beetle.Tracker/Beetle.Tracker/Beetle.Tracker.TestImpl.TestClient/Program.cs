using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl.TestClient
{
    class Program
    {
        private static Beetle.Tracker.TrackerClient mClient;

        private static int mCounter = 0;

        private static int mLastCount = 0;

        static void Main(string[] args)
        {

            Console.WriteLine(Beetle.LICENSE.GetLICENSE());
            Beetle.TcpUtils.Setup("beetle");
            mClient = new TrackerClient("trackerClientSection");
            System.Threading.Thread.Sleep(1000);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
            System.Threading.ThreadPool.QueueUserWorkItem(Test);
          
            while (true)
            {
                Console.WriteLine("秒请求数:{0}/总请求数:{1}",mCounter-mLastCount,mCounter);
                mLastCount = mCounter;
                System.Threading.Thread.Sleep(1000);
            }

            Console.Read();
        }
        private static void Test(object state)
        {
            while (true)
            {
                try
                {
                    AppHost host = mClient.GetHost();
                    System.Threading.Interlocked.Increment(ref mCounter);
                }
                catch (Exception e_)
                {
                    Console.WriteLine(e_.Message);
                }
            }
        }
    }
}
