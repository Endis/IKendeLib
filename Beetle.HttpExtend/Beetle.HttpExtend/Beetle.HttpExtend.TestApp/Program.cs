using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.HttpExtend.TestApp
{
    class Program : Beetle.ServerBase<HttpExtend.HttpPacket>
    {

        private static Program mServer = new Program();

        private static int Closeds = 0;

        private static int Connections = 0;

        private static int SendMessages;

        static void Main(string[] args)
        {
            Beetle.TcpUtils.Setup("beetle");
            Console.WriteLine(Beetle.LICENSE.GetLICENSE());
            mServer.Listens = 1000;
            mServer.Open(8089);
            Console.WriteLine("Http Extend Listen @8089");
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("{0}/{1} send messages:{2}", Closeds, Connections, SendMessages);
            }

        }

        protected override void OnConnected(object sender, ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            System.Threading.Interlocked.Increment(ref Connections);
            e.Channel.EnabledSendCompeletedEvent = true;
            e.Channel.SendMessageCompleted = (o, i) =>
            {
                if (e.Channel != null && e.Channel["BODY_COMPLETED"] != null)
                {
                    bool completed = (bool)e.Channel["BODY_COMPLETED"];
                    if (completed)
                    {
                        if (e.Channel["keep-alive"] == null)
                            e.Channel.Dispose();
                        System.Threading.Interlocked.Increment(ref Closeds);
                        System.Threading.Interlocked.Add(ref SendMessages, i.Messages.Count);
                    }
                }
                else
                {

                }
            };
        }

        protected override void OnError(object sender, ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            Console.WriteLine("{0} error {1}!", e.Channel.EndPoint, e.Exception.Message);
        }

        protected override void OnMessageReceive(PacketRecieveMessagerArgs e)
        {
            base.OnMessageReceive(e);
            if (e.Message is HttpExtend.HttpHeader)
            {
                HttpExtend.HttpHeader header = e.Message as HttpExtend.HttpHeader;
                if (header.Connection != null)
                    e.Channel["keep-alive"] = true;
                header = new HttpHeader();
                header.Action = "HTTP/1.1 200 OK";
                header["Cache-Control"] = "private";
                header.ContentType = "text/html; charset=utf-8";
                header.Server = "Beetle/HttpExtend";
                header.Connection = "keep-alive";
                // FileReader reader = new FileReader("h:\\KendeSoft.htm");
                // header.Length = reader.FileInfo.Length;
                HttpBody body = HttpPacket.InstanceBodyData();
                body.Data.Encoding("<p>beetle http extend server!</p><p>Beetle是基于c#编写的高性能稳定的TCP通讯组件，它可以轻易支持成千上万长连接基础上进行密集的通讯交互. 组件提供了出色的性能支持和可靠的稳定性足以保证应用7x24无间断运行。为了更好地利用.Net的网络IO来处理数据，组件提供智能合并消息机制,组件调度器会根据当前负载情况对发向客户的多个消息进行合并处处理，从而减少IO操作达到更高的处理效能；通过测试在大量用户信息广播的情况轻易可以处理上百万的消息转发</p>", Encoding.UTF8);
                body.Eof = true;
                header.Length = body.Data.Count;
                e.Channel.Send(header);
                e.Channel.Send(body);
                //while (reader.Read())
                //{
                //    HttpBody body = HttpPacket.InstanceBodyData();
                //    reader.ReadTo(body);
                //    e.Channel.Send(body);
                //}


            }
        }

        protected override void OnDisposed(object sender, ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            // Console.WriteLine("{0} disposed!", e.Channel.EndPoint);
        }
    }

}
