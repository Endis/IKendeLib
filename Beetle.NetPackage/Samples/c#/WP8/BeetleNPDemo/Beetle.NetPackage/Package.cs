using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
namespace Beetle.NetPackage
{
    public abstract class Package:IDisposable
    {
        public Package()
        {
            LittleEndian = true;
        }

        public bool LittleEndian
        {
            get;
            set;
        }

        public Action<System.IO.Stream> Receive;

        private System.Collections.Generic.Dictionary<string, object> mTables = new System.Collections.Generic.Dictionary<string, object>(8);

        public object this[string key]
        {
            get
            {
                return mTables[key];
            }
            set
            {
                mTables[key] = value;
            }
        }

        private bool mLoading = false;

        private CheckSize mCheckSize = null;

        private System.IO.MemoryStream mStream = new System.IO.MemoryStream(1024*4);

        public  void Import(byte[] data, int start, int count)
        {
            if (mCheckSize == null)
            {
                mCheckSize = new CheckSize();
                mCheckSize.LittleEndian = LittleEndian;
            }
            while (count > 0)
            {
                if (!mLoading)
                {
                    mCheckSize.Reset();
                    mStream.SetLength(0);
                    mStream.Position = 0;
                    mLoading = true;
                }
                if (mCheckSize.Length == -1)
                {
                    while (count > 0 && mCheckSize.Length == -1)
                    {
                        mCheckSize.Import(data[start]);
                        start++;
                        count--;
                    }
                }
                else
                {
                    if (OnImport(data, ref start, ref count))
                    {
                        mLoading = false;
                        if (Receive != null)
                        {
                            mStream.Position = 0;
                            Receive(mStream);
                        }
                    }
                }
            }
        }   

        private bool OnImport(byte[] data, ref int start, ref int count)
        {
            if (count >= mCheckSize.Length)
            {
                mStream.Write(data, start, mCheckSize.Length);
                start += mCheckSize.Length;
                count -= mCheckSize.Length;
                return true;
            }
            else
            {
                mStream.Write(data, start, count);
                start += count;
                mCheckSize.Length -= count;
                count = 0;
                return false;
            }

        }

        class CheckSize
        {
            public int Length = -1;

            public bool LittleEndian
            {
                get;
                set;
            }

            private int mIndex;

            public byte[] LengthData = new byte[4];

            public void Import(byte value)
            {
                LengthData[mIndex] = value;
                if (mIndex == 3)
                {
                    Length = BitConverter.ToInt32(LengthData, 0);
                    if (!LittleEndian)
                        Length = Endian.SwapInt32(Length);
                }
                else
                {
                    mIndex++;
                }
            }

            public void Reset()
            {
                Length = -1;
                mIndex = 0;
            }
        }

        public virtual IMessage SendCast(object message)
        {
            return (IMessage)message;
        }

        public virtual object ReceiveCast(IMessage msg)
        {
            return msg;
        }

        protected abstract void WriteMessageType(IDataWriter writer, IMessage message);

        protected abstract IMessage GetMessage(IDataReader reader);

        public virtual IMessage FromStream(BufferStream reader)
        {
            try
            {
                IMessage msg = GetMessage(reader);
                msg.Load(reader);
                return msg;
            }
            catch (Exception e_)
            {
                throw new Exception("read message error!", e_);
            }
        }

        public virtual byte[] GetMessageData(IMessage msg)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            BufferStream writer = new BufferStream(stream, LittleEndian);
               WriteMessageType(writer, msg);
            msg.Save(writer);
            byte[] result = new byte[stream.Length+4];
            using (System.IO.MemoryStream resultStream = new System.IO.MemoryStream(result))
            {
                writer = new BufferStream(resultStream, LittleEndian);
                writer.Write((int)stream.Length);
                stream.Position = 0;
                stream.Read(result, 4, (int)stream.Length);
            }
            return result;
        }

        public void Reset()
        {
            mCheckSize = null;
            mLoading = false;
        }

        public void Dispose()
        {
            mTables.Clear();
        }
    }
}
