using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.NetPackage
{
    public class ProtoMessageAdapter:IMessage
    {

        public object Message
        {
            get;
            set;
        }

        public void Load(IDataReader reader)
        {
            string name = reader.ReadUTF();
            Type type = ProtoPakcage.GetProtoType(name);
            Message = ProtoBuf.Meta.RuntimeTypeModel.Default.Deserialize(reader.Stream, null, type);
        }

        public void Save(IDataWriter writer)
        {
            writer.WriteUTF(Message.GetType().Name);
            ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(writer.Stream, Message);
            
        }
    }
}
