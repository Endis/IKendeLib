using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Logic
{
    public class HeadSizePage:Beetle.Protocol.SizePackage
    {
        public HeadSizePage(Beetle.IChannel channel)
            : base(channel)
        {
        }

        protected override void WriteMessageType(Beetle.IMessage msg, Beetle.IDataWriter writer)
        {
            if (msg is ListUsers)
            {
                writer.Write(1);
            }
            else if (msg is ListUsersResponse)
            {
                writer.Write(2);
            }
            else if (msg is Register)
            {
                writer.Write(3);
            }
            else if (msg is UnRegister)
            {
                writer.Write(4);
            }
            else if (msg is RegisterResponse)
            {
                writer.Write(5);
            }
            else if(msg is Say )
            {
                writer.Write(6);
            }
            else if (msg is OnRegister)
            {
                writer.Write(7);
            }
            else
            {
                writer.Write(-1);
            }
          
        }

        protected override Beetle.IMessage ReadMessageByType(Beetle.IDataReader reader, Beetle.ReadObjectInfo typeTag)
        {
            typeTag.TypeOfInt = reader.ReadInt();
            switch (typeTag.TypeOfInt)
            {
                case 1:
                    return new ListUsers();
                case 2:
                    return new ListUsersResponse();
                case 3:
                    return new Register();
                case 4:
                    return new UnRegister();
                case 5:
                    return new RegisterResponse();
                case 6:
                    return new Say();
                case 7:
                    return new OnRegister();
                default:
                    return null;

            }
        }
      
       
    }
}
