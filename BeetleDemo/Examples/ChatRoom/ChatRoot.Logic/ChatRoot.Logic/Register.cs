using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Logic
{
    public class Register:MsgBase
    {
        public string Name;
        public override void Load(Beetle.IDataReader reader)
        {
            base.Load(reader);
            Name = reader.ReadString();           
        }
        public override void Save(Beetle.IDataWriter writer)
        {
            base.Save(writer);
            writer.Write(Name);
           
        }
        
    }
    public class RegisterResponse : MsgBase
    {

    }
    public class OnRegister : MsgBase
    {
        public OnRegister()
        {
            User = new UserInfo();
        }
        public UserInfo User;
        public override void Load(Beetle.IDataReader reader)
        {
            base.Load(reader);
            User = reader.ReadMessage<UserInfo>();
        }
        public override void Save(Beetle.IDataWriter writer)
        {
            base.Save(writer);
            writer.Write(User);
        }
    }
    public class UnRegister :OnRegister
    {

    }
   
  
}
