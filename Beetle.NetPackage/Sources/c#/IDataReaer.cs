using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
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

        T Reads<T>() where T : IMessage, new();

        IList<T> ReadMessages<T>() where T : IMessage, new();

    }
}
