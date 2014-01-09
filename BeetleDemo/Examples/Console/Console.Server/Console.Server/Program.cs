using System;
using System.Collections.Generic;
using System.Text;
using C = System.Console;
namespace Console.Server
{
    class Program:Beetle.ServerBase
    {
        static int Count;

        static void Main(string[] args)
        {
            C.WriteLine("License:");
            C.Write(Beetle.LICENSE.GetLICENSE());
            Beetle.TcpUtils.Setup("beetle");//初始化组件
            Program server = new Program();
            server.Open(9321);
            C.WriteLine("Server started @9321");
            System.Threading.Thread.Sleep(-1);
           
        }
        protected override void OnConnected(object sender, Beetle.ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            C.WriteLine("{0} connected!", e.Channel.EndPoint);
        }
        protected override void OnDisposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            C.WriteLine("{0} disposed!", e.Channel.EndPoint);
        }
        protected override void OnError(object sender, Beetle.ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            C.WriteLine("{0} Error {1}!", e.Channel.EndPoint,e.Exception.Message);
        }
        protected override void OnReceive(object sender, Beetle.ChannelReceiveEventArgs e)
        {
            System.Threading.Interlocked.Increment(ref Count);
            string value = e.Channel.Coding.GetString(e.Data.Array, e.Data.Offset, e.Data.Count);
            C.WriteLine(value);
            Beetle.StringMessage msg = new Beetle.StringMessage();
            msg.Value = value;
            e.Channel.Send(msg);
        }

    }
}
