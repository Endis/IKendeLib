using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleNPDemo.Server
{
    class NPPacakge:Beetle.Protocol.SizePackage
    {
        public NPPacakge(Beetle.IChannel channel)
            : base(channel)
        {

        }
        protected override Beetle.IMessage ReadMessageByType(Beetle.IDataReader reader, Beetle.ReadObjectInfo typeTag)
        {
            typeTag.TypeofString = reader.ReadUTF();
            switch (typeTag.TypeofString)
            {
                case "Register":
                    return new Register();
            }
            return null;
        }
        protected override void WriteMessageType(Beetle.IMessage msg, Beetle.IDataWriter writer)
        {
            writer.WriteUTF(msg.GetType().Name);
        }
    }
}
