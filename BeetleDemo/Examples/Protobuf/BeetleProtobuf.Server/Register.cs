using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleProtobuf
{
    [ProtoBuf.ProtoContract]
    public class Register
    {
        [ProtoBuf.ProtoMember(1)]
        public string UserName
        {
            get;
            set;
        }
        [ProtoBuf.ProtoMember(2)]
        public string EMail
        {
            get;
            set;
        }
        [ProtoBuf.ProtoMember(3)]
        public DateTime RegTime
        {
            get;
            set;
        }
    }
}
