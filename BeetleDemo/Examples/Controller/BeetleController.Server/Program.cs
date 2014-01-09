using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleController.Server
{
    class Program : Beetle.ServerBase<Beetle.Protobuf.Package>
    {
        static Program mServer;

        static void Main(string[] args)
        {
            Console.WriteLine(Beetle.LICENSE.GetLICENSE());
            Beetle.Protobuf.Package.Register(typeof(Program).Assembly);
            Beetle.TcpUtils.Setup("beetle");
            mServer = new Program();
            mServer.Open(9321);
            Beetle.Controllers.Controller.Register(mServer);
            Console.WriteLine("start @9321");
            System.Threading.Thread.Sleep(-1);
        }

        protected override void OnConnected(object sender, Beetle.ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            Console.WriteLine("{0} Connected!", e.Channel.EndPoint);
        }

        protected override void OnDisposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            Console.WriteLine("{0} disposed!", e.Channel.EndPoint);
        }

        protected override void OnError(object sender, Beetle.ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            Console.WriteLine("{0} error {1}!", e.Channel.EndPoint, e.Exception.Message);
        }

        protected override void OnMessageReceive(Beetle.PacketRecieveMessagerArgs e)
        {
            base.OnMessageReceive(e);
            Beetle.Controllers.Controller.Invoke(e.Channel, e.Message);
        }

        public void OnRegister(Beetle.IChannel channel, Register e)
        {
            Console.WriteLine(e.UserName);
            Console.WriteLine(e.EMail);
            e.RegTime = DateTime.Now;
            channel.Send(e);
        }

        protected override void OnServerError(object sender, Beetle.TcpServerErrorArgs e)
        {
            base.OnServerError(sender, e);
        }
    }
}
