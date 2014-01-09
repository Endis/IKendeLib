using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Logic
{
    public class UserInfo:Beetle.IMessage
    {
        public string Name;

        public string IP;
       
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserInfo))
                return false;
            UserInfo info = (UserInfo)obj;
            return info.IP == IP && info.Name == Name;
        }
        public override string ToString()
        {
            return Name;
        }

        public void Save(Beetle.IDataWriter writer)
        {
            writer.Write(Name);
            writer.Write(IP);
        }

        public void Load(Beetle.IDataReader reader)
        {
            Name = reader.ReadString();
            IP = reader.ReadString();
        }
    }
}
