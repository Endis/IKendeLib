using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class TrackerClient
    {
        public TrackerClient(string host, int port)
        {
            mNode = new Clients.TcpSyncNode(host, port, 10);
            mNode.Connect<Beetle.Clients.TcpSyncChannel<HttpExtend.HttpPacket>>();
        }

        private Beetle.Clients.TcpSyncNode mNode;

        public AppHost GetHost(string app,IProperties properties =null)
        {
            if(properties ==null)
            {
                properties = new Properties();
            }
            HttpExtend.HttpHeader request = Protocol.Get(app, properties);
            HttpExtend.HttpHeader result = mNode.Send<HttpExtend.HttpHeader>(request);
            if (result.RequestType == "500")
                throw new Exception(result.Url);
            return new AppHost { Host=result["Host"],Port=int.Parse(result["Port"]) };
        }
    }
}
