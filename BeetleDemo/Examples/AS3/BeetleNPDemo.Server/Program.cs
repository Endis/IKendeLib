using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleNPDemo.Server
{
    class Program:Beetle.ServerBase<NPPacakge>
    {
        static Program mServer;

        static void Main(string[] args)
        {
            Beetle.TcpUtils.Setup("beetle");
            Console.WriteLine(Beetle.LICENSE.GetLICENSE());
            mServer = new Program();
            mServer.LittleEndian = false;
            mServer.Open(9088);
           
            Console.WriteLine("start @9088");
            System.Threading.Thread.Sleep(-1);
        }

        static DateTime GetTime(long value)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(value);
        }
        static long GetTime(DateTime dt)
        {
            double value = (dt - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            long result =(long)value;
            return result;
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
            base.OnMessageReceive(e);
            if (e.Message is Register)
            {
                Register reg = (Register)e.Message;
               
                Console.WriteLine("{0} Register", e.Channel.EndPoint);
                Console.WriteLine("Name:{0}", reg.Name);
                Console.WriteLine("EMail:{0}", reg.EMail);
                Console.WriteLine("City:{0}", reg.City);
                Console.WriteLine("Country:{0}", reg.Country);
                Console.WriteLine("RegTime:{0}", reg.RegTime);
                 reg.RegTime =DateTime.Now ;
                e.Channel.Send(reg);
            }
        }
    }
}
