using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.NetPackage
{
    public class ProtoPakcage:Package
    {

        private static Dictionary<string, Type> mProtoTbl = new Dictionary<string, Type>();

        public override IMessage SendCast(object message)
        {
            ProtoMessageAdapter adapter = new ProtoMessageAdapter();
            adapter.Message = message;
            return adapter;
        }

        public static Type GetProtoType(string name)
        {
            Type result = null;
            mProtoTbl.TryGetValue(name, out result);
            return result;
        }

        public static void Register(System.Reflection.Assembly assembly)
        {
            foreach(Type type in assembly.GetTypes())
            {
                if (!type.IsAbstract && !type.IsInterface && type.GetCustomAttributes(typeof(ProtoBuf.ProtoContractAttribute), false).Length > 0)
                    mProtoTbl[type.Name] = type;
            }
        }

        public override object ReceiveCast(IMessage msg)
        {
            ProtoMessageAdapter adapter = (ProtoMessageAdapter)msg;
            return adapter.Message;
        }
        
        protected override void WriteMessageType(IDataWriter writer, IMessage message)
        {
            
        }

        protected override IMessage GetMessage(IDataReader reader)
        {
            ProtoMessageAdapter adapter = new ProtoMessageAdapter();
            return adapter;
        }
    }
}
