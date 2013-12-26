using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.NetPackage
{
    public interface IDataReader
    {
        System.IO.Stream Stream
        {
            get;
        }

        byte Read();

        bool ReadBool();

        short ReadShort();

        int ReadInt();

        long ReadLong();

        ushort ReadUShort();

        uint ReadUInt();

        ulong ReadULong();

        char ReadChar();

        float ReadFloat();

        double ReadDouble();

        string ReadUTF();

        byte[] ReadBytes(int count);

    }
}
