using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encrypt.Server
{
    class Program : Beetle.ServerBase<EncryptPackage>
    {
        static void Main(string[] args)
        {

            Console.WriteLine("License:");
            Console.Write(Beetle.LICENSE.GetLICENSE());
            Beetle.TcpUtils.Setup("beetle");
            Program server = new Program();
            server.Open(8088);
            Console.WriteLine("server start @8088");
            Console.Read();
        }

        protected override void OnConnected(object sender, Beetle.ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            Console.WriteLine("{0} connected", e.Channel.EndPoint);
        }
        protected override void OnDisposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            Console.WriteLine("{0} disposed", e.Channel.EndPoint);
        }
        protected override void OnError(object sender, Beetle.ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            Console.WriteLine("{0} error {1}", e.Channel.EndPoint, e.Exception.Message);
        }
        protected override void OnMessageReceive(Beetle.PacketRecieveMessagerArgs e)
        {
            Register reg = (Register)e.Message;
            reg.ResponseTime = DateTime.Now;
            Console.WriteLine("Name:{0} EMail:{1}", reg.Name, reg.EMail);
            e.Channel.Send(reg);
        }
    }
}
