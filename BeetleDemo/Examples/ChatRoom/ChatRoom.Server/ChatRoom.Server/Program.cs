using System;
using System.Collections.Generic;
using System.Text;
using Beetle;
using System.Runtime.Remoting;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
namespace ChatRoom.Server
{
    class Program
    {
        static TcpServer mServer = new TcpServer();

        static Program mHandler;

        static void Main(string[] args)
        {
            Console.WriteLine(Beetle.LICENSE.GetLICENSE());
            mHandler = new Program();
            Beetle.Controllers.Controller.Register(mHandler);       
            Beetle.TcpUtils.Setup("beetle");
            try
            {
                mServer.ChannelConnected += OnConnect;
                mServer.ChannelDisposed += OnDisposed;
                mServer.Open(9001);
                Console.WriteLine("server start");
            }
            catch (Exception e_)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e_.Message);
            }
            Console.Read();
        }

        static void OnConnect(object sender, ChannelEventArgs e)
        {
            TcpServer.SetKeepAliveValues(e.Channel.Socket, 20000, 5000);
            e.Channel.ChannelError += OnError;
            e.Channel.SetPackage<Logic.HeadSizePage>();
            e.Channel.Package.ReceiveMessage = OnReceive;
            e.Channel.BeginReceive();
            Console.WriteLine("{0} Connected!", e.Channel.EndPoint);
        }

        static void OnReceive(Beetle.PacketRecieveMessagerArgs e)
        {
            Beetle.Controllers.Controller.Invoke(e.Channel, e.Message);
        }

        static void OnError(object sender, ChannelErrorEventArgs e)
        {
            Console.WriteLine("error:{0}", e.Exception.Message);
        }

        static void OnDisposed(object sender, ChannelEventArgs e)
        {
            Console.WriteLine("{0}{1} disposed!", e.Channel.Name, e.Channel.EndPoint);
            Logic.UnRegister ur = new Logic.UnRegister();
            ur.User.Name = e.Channel.Name;
            ur.User.IP = e.Channel.EndPoint.ToString();
            foreach (IChannel item in mServer.GetOnlines())
            {
                if (item != e.Channel)
                    item.Send(ur);
            }
        }

        public void _Register(IChannel channel, Logic.Register e)
        {
            channel.Name = e.Name;
            Logic.RegisterResponse response = new Logic.RegisterResponse();
            channel.Send(response);
            Logic.OnRegister onreg = new Logic.OnRegister();
            onreg.User = new Logic.UserInfo { Name = e.Name, IP = channel.EndPoint.ToString() };
            foreach (IChannel item in mServer.GetOnlines())
            {
                if (item != channel)
                    item.Send(onreg);
            }
            Console.WriteLine("{0} login from {1}", e.Name, channel.EndPoint);
        }

        public void _Say(IChannel channel, Logic.Say e)
        {
            e.User.Name = channel.Name;
            e.User.IP = channel.EndPoint.ToString();
            foreach (IChannel item in mServer.GetOnlines())
            {
                if (item != channel)
                    item.Send(e);
            }
            Console.WriteLine("{0} say", e.User.Name);
        }

        public void _List(IChannel channel, Logic.ListUsers e)
        {
            Logic.ListUsersResponse response = new Logic.ListUsersResponse();
            foreach (IChannel item in mServer.GetOnlines())
            {
                if (item != channel)
                {
                    response.Users.Add(new Logic.UserInfo { Name = item.Name, IP = item.EndPoint.ToString() });
                }
            }
            channel.Send(response);
        }

    }
}
