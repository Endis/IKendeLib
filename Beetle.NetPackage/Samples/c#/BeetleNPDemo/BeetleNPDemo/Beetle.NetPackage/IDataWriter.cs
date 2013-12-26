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
    public interface IDataWriter
    {
        System.IO.Stream Stream
        {
            get;
        }
        void Write(byte value);

        void Write(bool value);

        void Write(short value);

        void Write(int value);

        void Write(long value);

        void Write(char value);

        void Write(ushort value);

        void Write(uint value);

        void Write(ulong value);

        void WriteUTF(string value);

        void Write(byte[] data);

        void Write(byte[] data, int offset, int count);

        void Write(double value);

        void Write(float value);

    }
}
