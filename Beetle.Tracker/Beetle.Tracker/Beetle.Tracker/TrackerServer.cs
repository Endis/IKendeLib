using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle.HttpExtend;

namespace Beetle.Tracker
{
    public class TrackerServer : Beetle.IServerHandler
    {

        const string RESULT_STATE_200 = "200";

        const string RESULT_STATE_500 = "500";

        const string ERROR_MSG_TRACKER_NOTFOUND = "{0} Tracker Handler Notfound";

        public TrackerServer()
        {
            if (TrackerServerSection.Instance != null)
            {
                foreach (AppTrackerConfig item in TrackerServerSection.Instance.Trackers)
                {
                    Type type = Type.GetType(item.Type);
                    if (type == null)
                    {
                        Utils.Error<TrackerServer>("get {0} not found!", item.Type);
                    }
                    else
                    {
                        mHandlers[item.Name] = (IAppTrackerHandler)Activator.CreateInstance(type);
                    }
                }

            }
        }

        private Dictionary<string, IAppTrackerHandler> mHandlers = new Dictionary<string, IAppTrackerHandler>();

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

        private IAppTrackerHandler GetAppHandler(string name)
        {
            IAppTrackerHandler result = null;
            mHandlers.TryGetValue(name, out result);
            return result;
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
            Utils.Error<TrackerServer>(e.Exception, "TrackerServer Channel {1} Error {0}", e.Exception.Message, e.Channel.EndPoint);
        }

        public void ChannelReceiveMessage(ServerBase server, PacketRecieveMessagerArgs e)
        {
            try
            {
                HttpHeader hader = (HttpHeader)e.Message;
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
                Utils.Error<TrackerServer>("<{0}> message process error {1}", e.Message, e_.Message);
                HttpHeader header = Protocol.GetResponse(new Properties());
                header.Action = "500 " + e_.Message;
                e.Channel.Send(header);
            }
        }

        private HttpHeader GetResponse(IProperties ips)
        {
            return Protocol.GetResponse(ips);
        }

        private void OnRegister(Beetle.IChannel channel, string appName, IProperties properties)
        {
            IAppTrackerHandler appHandler;
            HttpHeader result;
            IProperties ips;
            appHandler = GetAppHandler(appName);
            if (appHandler == null)
            {
                result = new HttpHeader();
                result.Action = RESULT_STATE_500 + string.Format(ERROR_MSG_TRACKER_NOTFOUND, appName);
                channel.Send(result);
            }
            else
            {
                ips = appHandler.Register(properties);
                result = this.GetResponse(ips);
                result.Action = RESULT_STATE_200;
                channel.Send(result);
            }

        }

        private void OnGetInfo(Beetle.IChannel channel, string appname, IProperties properties)
        {
            HttpHeader result;
            TrackerInfo info;
            byte[] data;
            BytesReader reader;
            HttpBody body;
            IAppTrackerHandler appHandler = GetAppHandler(appname);
            if (appHandler == null)
            {
                result = new HttpHeader();
                result.Action = "500 " + string.Format("{0} Tracker Handler Notfound", appname);
                channel.Send(result);
            }
            else
            {
                info = appHandler.GetInfo(properties);
                data = Encoding.UTF8.GetBytes(info.Data);
                result = new HttpHeader();
                result.Action = "200";
                result.Length = data.Length;
                result[Protocol.HEADER_INFOTYPE] = info.TypeName;
                channel.Send(result);
                reader = new BytesReader(data, 0, data.Length);
                while (reader.Read())
                {
                    body = HttpPacket.InstanceBodyData();
                    reader.ReadTo(body);
                    channel.Send(body);
                }
            }
        }

        private void OnGet(Beetle.IChannel channel, string appname, IProperties properties)
        {
            IAppTrackerHandler appHandler = GetAppHandler(appname);
            if (appHandler == null)
            {
                HttpHeader result = new HttpHeader();
                result.Action = "500 " + string.Format("{0} Tracker Handler Notfound", appname);
                channel.Send(result);
            }
            else
            {
                AppHost apphost = appHandler.GetHost(properties);
                if (apphost == null)
                {
                    HttpHeader result = new HttpHeader();
                    result.Action = "500 App Host Notfound!";
                    channel.Send(result);
                }
                else
                {
                    HttpHeader result = new HttpHeader();
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
