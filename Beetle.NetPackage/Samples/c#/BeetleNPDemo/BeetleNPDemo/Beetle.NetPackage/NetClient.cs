using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
namespace Beetle.NetPackage
{
    public class NetClient : IDisposable
    {

        public NetClient(string host, int port, Package package, INetClientHandler handler)
        {


            mEndPoint = new DnsEndPoint(host, port);
            Handler = handler;
            mPackage = package;
            mPackage.Receive = OnDataReceive;
            mReceiveSAEA = new SocketAsyncEventArgs();
            mReceiveSAEA.Completed += ReceiveCompleted;
            mReceiveSAEA.SetBuffer(new byte[1024 * 4], 0, 1024 * 4);

            mConnectSAEA = new SocketAsyncEventArgs();
            mConnectSAEA.RemoteEndPoint = mEndPoint;
            mConnectSAEA.Completed += OnConnected;

            mSendSAEA = new SocketAsyncEventArgs();
            mSendSAEA.Completed += SendCompleted;

        }

        private System.Net.Sockets.SocketAsyncEventArgs mConnectSAEA;

        private System.Net.Sockets.SocketAsyncEventArgs mSendSAEA;

        private System.Net.Sockets.SocketAsyncEventArgs mReceiveSAEA;

        private Package mPackage;

        public INetClientHandler Handler
        {
            get;
            set;
        }

        private System.Net.EndPoint mEndPoint;

        private System.Net.Sockets.Socket mSocket;

        private bool mConnected = false;

        private bool mLittleEndian = true;

        public bool Connected
        {
            get
            {
                return mConnected;
            }
        }

        public Exception LastError
        {
            get;
            private set;
        }

        private void OnConnected(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                mConnected = true;
                BeginReceive();
                Handler.Connected(this);
            }
            else
            {
                mConnected = false;
                ReleaseSocket();
            }
        }

        public bool Connect()
        {
            if (Connected)
                return true;
            try
            {
                mPackage.Reset();
                mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (!mSocket.ConnectAsync(mConnectSAEA))
                {
                    OnConnected(this, mConnectSAEA);
                }
                return mConnected;
            }
            catch (Exception e_)
            {
                mConnected = false;
                OnError(e_);
                return false;
            }

        }

        private void OnError(Exception e)
        {
            try
            {
                Handler.ClientError(this, e);
            }
            catch
            {

            }
        }

        private void BeginReceive()
        {
            try
            {
                if (!mSocket.ReceiveAsync(mReceiveSAEA))
                {
                    ReceiveCompleted(this, mReceiveSAEA);
                }
            }
            catch (SocketException se)
            {
                ReleaseSocket();
                OnError(se);
            }
            catch (ObjectDisposedException ode)
            {
                ReleaseSocket();
                OnError(ode);
            }
            catch (Exception e_)
            {
                OnError(e_);

            }
        }

        private void ReceiveCompleted(object sender, System.Net.Sockets.SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                try
                {
                    mPackage.Import(e.Buffer, 0, e.BytesTransferred);
                    BeginReceive();
                }
                catch (Exception e_)
                {
                    ReleaseSocket();
                    OnError(e_);
                }

            }
            else
            {
                ReleaseSocket();
            }

        }

        private void SendCompleted(object sender, System.Net.Sockets.SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                if (e.BytesTransferred < e.Count)
                {
                    e.SetBuffer(e.Offset + e.BytesTransferred, e.Count - e.BytesTransferred);
                    if (!mSocket.SendAsync(e))
                    {
                        SendCompleted(this, e);
                    }
                    

                }
            }
            else
            {
                ReleaseSocket();
            }
        }

        private void OnDataReceive(System.IO.Stream stream)
        {
            try
            {
                stream.Position = 0;
                BufferStream reader = new BufferStream(stream, LittleEndian);
                IMessage msg = mPackage.FromStream(reader);
                if (msg == null)
                    throw new Exception("message type not found!");
                Handler.ClientReceive(this, mPackage.ReceiveCast(msg));
            }
            catch (Exception e_)
            {
                OnError(e_);
            }
            finally
            {
                mPackage.Reset();
            }
        }

        public bool LittleEndian
        {
            get
            {
                return mLittleEndian;
            }
            set
            {
                mLittleEndian = value;
                mPackage.LittleEndian = LittleEndian;
            }
        }

        public bool Send(object message)
        {
            lock (this)
            {
                if (Connect())
                {

                    try
                    {
                        IMessage msg = mPackage.SendCast(message);
                        byte[] data = mPackage.GetMessageData(msg);
                        mSendSAEA.SetBuffer(data, 0, data.Length);
                        if (!mSocket.SendAsync(mSendSAEA))
                        {
                            SendCompleted(this, mSendSAEA);
                        }
                        return true;
                    }
                    catch (SocketException se)
                    {
                        ReleaseSocket();
                        OnError(se);
                    }
                    catch (ObjectDisposedException ode)
                    {
                        ReleaseSocket();
                        OnError(ode);
                    }
                    catch (Exception e_)
                    {
                        OnError(e_);

                    }
                }
                return false;
            }

        }

        private void ReleaseSocket()
        {
            mConnected = false;
            lock (this)
            {
                if (mSocket != null)
                {
                    Handler.ClientDisposed(this);
                    try
                    {
                        if (mSocket != null)
                        {
                            mSocket.Shutdown(SocketShutdown.Both);
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (mSocket != null)
                        {
                            mSocket.Close();
                        }
                    }
                    catch
                    {
                    }
                }
                mSocket = null;

            }

        }

        public void Dispose()
        {
            ReleaseSocket();
        }
    }
}
