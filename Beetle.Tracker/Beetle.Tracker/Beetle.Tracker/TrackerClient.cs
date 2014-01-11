using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class TrackerClient
    {
        public TrackerClient(string section)
        {
            TrackerClientSection tcs = (TrackerClientSection)System.Configuration.ConfigurationManager.GetSection(section);
            if (tcs == null)
                throw new TrackerException(string.Format("{0} Tracker Client Section notfound!", section));
            mNode = new Clients.SyncNode(tcs.Host, tcs.Port, tcs.Connections);
            mNode.Connect<Beetle.Clients.SyncChannel<HttpExtend.HttpPacket>>();
            mAppName = tcs.AppName;
        }
        public TrackerClient(string host, int port,string appName,int connecitons=5)
        {
            mNode = new Clients.SyncNode(host, port, connecitons);
            mNode.Connect<Beetle.Clients.SyncChannel<HttpExtend.HttpPacket>>();
            mAppName = appName;
        }

        private string mAppName;

        private Beetle.Clients.SyncNode mNode;

        public Beetle.Clients.SyncNode NetNode
        {
            get
            {
                return mNode;
            }
        }

        public AppHost GetHost(IProperties properties =null)
        {
            if(properties ==null)
            {
                properties = new Properties();
            }
            HttpExtend.HttpHeader request = Protocol.Get(mAppName, properties);
            HttpExtend.HttpHeader result = mNode.Send<HttpExtend.HttpHeader>(request);
            if (result.RequestType == "500")
                throw new Exception(result.ActionDetail);
            return new AppHost { Host=result["Host"],Port=int.Parse(result["Port"]) };
        }
    }
}
