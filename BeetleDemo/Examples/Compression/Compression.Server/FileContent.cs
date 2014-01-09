using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle;

namespace Compression
{
    public class FileContent : Beetle.IMessage
    {
        public string Data;


        public void Save(IDataWriter writer)
        {
            writer.WriteUTF(Data);
        }

        public void Load(IDataReader reader)
        {
            Data = reader.ReadUTF();
        }
    }
}
