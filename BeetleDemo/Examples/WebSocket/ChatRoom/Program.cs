using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle;
using Beetle.WebSockets;
namespace ChatRoom
{
    class Program : WebSocketJsonServer
    {
        
        protected override void OnError(object sender, ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            Console.WriteLine(e.Exception.Message);
        }
        protected override void OnConnected(object sender, ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            Console.WriteLine("{0} connected", e.Channel.EndPoint);
        }
        protected override void OnDisposed(object sender, ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            Console.WriteLine("{0} disposed", e.Channel.EndPoint);
            JsonMessage msg = new JsonMessage();
            User user = new User();
            user.Name = e.Channel.Name;
            user.ID = e.Channel.ClientID;
            user.IP = e.Channel.EndPoint.ToString();
            msg.type = "unregister";
            msg.data = (User)e.Channel.Tag;
            foreach (IChannel item in this.Server.GetOnlines())
            {
                if (item != e.Channel)
                    item.Send(msg);
            }
        }

        public string Name
        {
            get { return "Websocket ChatRoom"; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Beetle.LICENSE.GetLICENSE());
            Beetle.WebSockets.Controllers.Controller.Register(new Handler());
            TcpUtils.Setup("beetle");
            Program server = new Program();
            server.Open(8089);
            Console.WriteLine("websocket start@8089");
            Console.Read();
        }

       
    }
}

