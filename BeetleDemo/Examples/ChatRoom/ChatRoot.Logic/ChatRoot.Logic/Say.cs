using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Logic
{
    public class Say:MsgBase
    {
        public Say()
        {
            User = new UserInfo();
        }

        public override void Load(Beetle.IDataReader reader)
        {
            base.Load(reader);
            User = reader.ReadMessage<UserInfo>();
            Body = reader.ReadString();
        }

        public override void Save(Beetle.IDataWriter writer)
        {
            base.Save(writer);
            writer.Write(User);
            writer.Write(Body);
        }

        public UserInfo User;

        public string Body;
        
    }
}
