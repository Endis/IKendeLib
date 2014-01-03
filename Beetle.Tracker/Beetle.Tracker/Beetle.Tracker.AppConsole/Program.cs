using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.AppConsole
{
    class Program
    {
        private static Beetle.ServerImpl<HttpExtend.HttpPacket> mServer;

        static void Main(string[] args)
        {
            Beetle.TcpUtils.Setup("beetle");
            Utils.GetLog<Tracker.TrackerServer>().Info(Beetle.LICENSE.GetLICENSE());
            mServer = new ServerImpl<HttpExtend.HttpPacket>("Beetle Tracker", new TrackerServer());
            TrackerServerSection config = TrackerServerSection.Instance;
            if (string.IsNullOrEmpty(config.Host))
                mServer.Open(config.Port);
            else
                mServer.Open(config.Host, config.Port);
           
            System.Threading.Thread.Sleep(-1);

        }
    }
}
