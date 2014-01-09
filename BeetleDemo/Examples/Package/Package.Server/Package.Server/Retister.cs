using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    class Register : Beetle.IMessage
    {
        public string Name;
        public string EMail;
        public DateTime ResponseTime;
        public void Load(Beetle.IDataReader reader)
        {
            Name = reader.ReadString();
            EMail = reader.ReadString();
            ResponseTime = reader.ReadDateTime();
        }
        public void Save(Beetle.IDataWriter writer)
        {
            writer.Write(Name);
            writer.Write(EMail);
            writer.Write(ResponseTime);
        }
    }
}
