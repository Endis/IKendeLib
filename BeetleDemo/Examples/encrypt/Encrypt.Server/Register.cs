using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encrypt
{

    public class Register : Beetle.IMessage
    {
        public string Name;
        public string EMail;
        public DateTime ResponseTime;
        public void Load(Beetle.IDataReader reader)
        {
            Name = reader.ReadUTF();
            EMail = reader.ReadUTF();
            ResponseTime = reader.ReadDateTime();
        }
        public void Save(Beetle.IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteUTF(EMail);
            writer.Write(ResponseTime);
        }
    }
}
