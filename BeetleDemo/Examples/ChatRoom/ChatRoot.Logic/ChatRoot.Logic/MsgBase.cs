using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Logic
{
    public abstract class MsgBase:Beetle.IMessage
    {
        public MsgBase()
        {
            ID = GetID();
        }

        public string ID
        {
            get;
            set;
        }

        static int mSeed = 1;

        static string GetID()
        {
            lock (typeof(MsgBase))
            {
                mSeed++;
                if (mSeed >= int.MaxValue)
                    mSeed = 1;
                return mSeed.ToString("0000000000");
            }
        }

        public virtual void Save(Beetle.IDataWriter writer)
        {
            writer.Write(ID);
        }

        public virtual void Load(Beetle.IDataReader reader)
        {
            ID = reader.ReadString();
        }
     
    }
}
