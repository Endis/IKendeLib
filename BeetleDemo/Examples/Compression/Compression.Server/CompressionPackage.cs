using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle;
using System.IO.Compression;
using System.IO;

namespace Compression
{
    public class CompressionPackage : Beetle.Protocol.SizePackage
    {
        public CompressionPackage()
        {
        }
        public CompressionPackage(IChannel channel)
            : base(channel)
        {

        }
        protected override void WriteMessageType(IMessage msg, IDataWriter writer)
        {
            writer.WriteUTF(msg.GetType().Name);
        }
        protected override IMessage ReadMessageByType(IDataReader reader, ReadObjectInfo typeTag)
        {
            typeTag.TypeofString = reader.ReadUTF();
            switch (typeTag.TypeofString)
            {
                case "FileContent":
                    return new FileContent();
            }
            return null;
        }
        protected override void SaveBody(IDataWriter writer, IMessage msg)
        {
            using (GZipStream compStream = new GZipStream((Stream)writer, CompressionMode.Compress))
            {
                DataWriter dw = new DataWriter(compStream, writer.LittleEndian);
                base.SaveBody(dw, msg);
            }
        }
        protected override void LoadBody(IDataReader reader, IMessage message)
        {
            Console.WriteLine("Receive Data length:" + reader.Length);
            using (GZipStream compStream = new GZipStream((Stream)reader, CompressionMode.Decompress))
            {
                DataReader dw = new DataReader(compStream, reader.LittleEndian);
                base.LoadBody(dw, message);
            }
        }
    }
}
