using System;
using System.Collections.Generic;
using System.Text;
using Beetle;
namespace Messages
{
    public class HeadSizePackage:Beetle.Protocol.SizePackage
    {
        public HeadSizePackage(Beetle.IChannel channel) : base(channel) { }

        protected override IMessage ReadMessageByType(IDataReader reader, ReadObjectInfo typeTag)
        {
            typeTag.TypeofString = reader.ReadUTF();
            return (IMessage)Activator.CreateInstance(Type.GetType(typeTag.TypeofString));
        }

        protected override void WriteMessageType(Beetle.IMessage msg, Beetle.IDataWriter writer)
        {
            writer.WriteUTF(msg.GetType().FullName);
        }

    }
}
