using System;
using System.Collections.Generic;
using System.Text;
using Messages;
namespace Package.Server
{
    class Program:Beetle.ServerBase<Messages.HeadSizePackage>
    {
        static void Main(string[] args)
        {
 
        
            Console.Write(Beetle.LICENSE.GetLICENSE());
            Beetle.TcpUtils.Setup("beetle");
            Program server = new Program();
            server.Open(9450);
            Console.WriteLine("server start @9450");
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
            Console.WriteLine("{0} error {1}", e.Channel.EndPoint,e.Exception.Message);
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
