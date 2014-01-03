using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class TrackerServer:Beetle.IServerHandler
    {
        public TrackerServer()
        {
            if (TrackerServerSection.Instance != null)
            {
                foreach (AppTrackerConfig conf in TrackerServerSection.Instance.Trackers)
                {
                    Type type = Type.GetType(conf.Type);
                    if (type != null)
                    {
                        mAppHandlers[conf.Name] = (IAppTrackerHandler)Activator.CreateInstance(type);
                        Utils.GetLog<TrackerServer>().InfoFormat("Load {0} AppTrackerHandler {1}", conf.Name, type);
                    }
                    else
                    {
                        Utils.GetLog<TrackerServer>().ErrorFormat("{0} AppTrackerHandler Notfound!", conf.Type);
                    }
                }
            }
        }

        private static Dictionary<Type, string> mTypeNames = new Dictionary<Type, string>();

        private static string GetTypeName(Type type)
        {
            lock (mTypeNames)
            {
                string result = null;
                if (mTypeNames.TryGetValue(type, out result))
                    return result;
                result = type.FullName + "," + type.Assembly.GetName().Name;
                mTypeNames[type] = result;
                return result;
            }
        }

        public void AddApp<T>(string name) where T:IAppTrackerHandler,new()
        {
            mAppHandlers[name] = new T();
        }

        private Dictionary<string, IAppTrackerHandler> mAppHandlers = new Dictionary<string, IAppTrackerHandler>();

        public IAppTrackerHandler GetTackerHandler(string appname)
        {
            IAppTrackerHandler handler = null;
            mAppHandlers.TryGetValue(appname, out handler);
            return handler;
        }

        public void ChannelCreated(ServerBase server, ChannelEventArgs e)
        {
            Utils.GetLog<TrackerServer>().InfoFormat("{0} Connected TrackerServer", e.Channel.EndPoint);
        }

        public void ChannelDisconnect(ServerBase server, ChannelDisposedEventArgs e)
        {
            Utils.GetLog<TrackerServer>().InfoFormat("{0} Discontected TrackerServer", e.Channel.EndPoint);
        }

        public void ChannelError(ServerBase server, ChannelErrorEventArgs e)
        {
            Utils.Error<TrackerServer>(e.Exception, "TrackerServer Channel {1} Error {0}", e.Exception.Message,e.Channel.EndPoint);
        }

        public void ChannelReceiveMessage(ServerBase server, PacketRecieveMessagerArgs e)
        {
            try
            {
                HttpExtend.HttpHeader hader = (HttpExtend.HttpHeader)e.Message;
                Properties ps = new Properties();
                ps.FromHeaders(hader.Properties);
                switch (hader.RequestType)
                {
                    case Protocol.COMMAND_GET:
                        OnGet(e.Channel, hader.Url, ps);
                        break;
                    case Protocol.COMMAND_GETINFO:
                        OnGetInfo(e.Channel, hader.Url, ps);
                        break;
                    case Protocol.COMMAND_REGISTER:
                        OnRegister(e.Channel, hader.Url, ps);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e_)
            {
                HttpExtend.HttpHeader header = Protocol.GetResponse(new Properties());
                header.Action = "500 " + e_.Message;
                e.Channel.Send(header);
            }
        }


        private void OnRegister(Beetle.IChannel channel, string appname, IProperties properties)
        {
            IAppTrackerHandler appHandler = GetTackerHandler(appname);
           
            if (appHandler == null)
            {
                HttpExtend.HttpHeader result = new HttpExtend.HttpHeader();
                result.Action = "500 " + string.Format("{0} Tracker Handler Notfound", appname);
                channel.Send(result);
            }
            else
            {
               
               IProperties ips = appHandler.Register(properties);
               HttpExtend.HttpHeader result = Protocol.GetResponse(ips);
               result.Action = "200";
               channel.Send(result);
            }
            
        }

        private void OnGetInfo(Beetle.IChannel channel, string appname, IProperties properties)
        {
            IAppTrackerHandler appHandler = GetTackerHandler(appname);

            if (appHandler == null)
            {
                HttpExtend.HttpHeader result = new HttpExtend.HttpHeader();
                result.Action = "500 " + string.Format("{0} Tracker Handler Notfound", appname);
                channel.Send(result);
            }
            else
            {
                object info = appHandler.GetInfo(properties);
                byte[] data = Encoding.UTF8.GetBytes( appHandler.Formater.ToStringValue(info));
                HttpExtend.HttpHeader result = new HttpExtend.HttpHeader();
                result.Action = "200";
                result.Length = data.Length;
                result[Protocol.HEADER_INFOTYPE] = GetTypeName(info.GetType());
                channel.Send(result);
                HttpExtend.BytesReader reader = new HttpExtend.BytesReader(data, 0, data.Length);
                while (reader.Read())
                {
                    HttpExtend.HttpBody body = HttpExtend.HttpPacket.InstanceBodyData();
                    reader.ReadTo(body);
                    channel.Send(body);
                }
            }
        }

        private void OnGet(Beetle.IChannel channel, string appname, IProperties properties)
        {
            IAppTrackerHandler appHandler = GetTackerHandler(appname);

            if (appHandler == null)
            {
                HttpExtend.HttpHeader result = new HttpExtend.HttpHeader();
                result.Action = "500 " + string.Format("{0} Tracker Handler Notfound", appname);
                channel.Send(result);
            }
            else
            {
                AppHost apphost = appHandler.GetHost(properties);
                if (apphost == null)
                {
                    HttpExtend.HttpHeader result = new HttpExtend.HttpHeader();
                    result.Action = "500 App Host Notfound!";
                    channel.Send(result);
                }
                else
                {
                    HttpExtend.HttpHeader result = new HttpExtend.HttpHeader();
                    result.Action = "200";
                    result["Host"] = apphost.Host;
                    result["Port"] = apphost.Port.ToString();
                    channel.Send(result);
                }
            }
        }


        public void Disposed(ServerBase server)
        {
            Utils.GetLog<TrackerServer>().Info("TrackerServer Disposed!");
        }

        public void Opened(ServerBase server)
        {
            Utils.GetLog<TrackerServer>().InfoFormat("TrackerServer Start @{0}", server.Server.Socket.LocalEndPoint);
        }

        public void ServerError(ServerBase server, TcpServerErrorArgs e)
        {
            Utils.Error<TrackerServer>(e.Error, "TrackerServer Error {0}", e.Error.Message);
        }

        public void SocketConnect(ServerBase server, ChannelCreatingArgs e)
        {
           
        }
    }
}
