using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Logic
{
    public class ListUsers:MsgBase
    { 
    }

    public class ListUsersResponse:MsgBase
    {
        public ListUsersResponse()
        {
            Users = new List<UserInfo>();
        }

        public override void Load(Beetle.IDataReader reader)
        {
            base.Load(reader);
            Users = reader.ReadMessages<UserInfo>();
        }

        public override void Save(Beetle.IDataWriter writer)
        {
            base.Save(writer);
            writer.Write(Users);
        }

        public IList<UserInfo> Users; 
    }
}
