using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleNPDemo
{
    class NPPackage:Beetle.NetPackage.Package
    {

        protected override void WriteMessageType(Beetle.NetPackage.IDataWriter writer, Beetle.NetPackage.IMessage message)
        {
            writer.WriteUTF(message.GetType().Name);
        }

        protected override Beetle.NetPackage.IMessage GetMessage(Beetle.NetPackage.IDataReader reader)
        {
            string Name = reader.ReadUTF();
            switch (Name)
            {
                case "Register":
                    return new Register();
            }
            return null;
        }
    }
}
