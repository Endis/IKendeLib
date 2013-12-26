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
    public class DataWriter:IDataWriter
    {
        public DataWriter(System.IO.Stream stream, bool littleEndian)
        {
            LittleEndian = littleEndian;
            mWriter = new System.IO.BinaryWriter(stream);
            mStream = stream;
        }

        private System.IO.BinaryWriter mWriter;

        private System.IO.Stream mStream;

        public System.IO.Stream Stream
        {
            get
            {
                return mStream;
            }
        }

        public bool LittleEndian
        {
            get;
            set;
        }

        public void Write(byte value)
        {
            mWriter.Write(value);
        }

        public void Write(bool value)
        {
            mWriter.Write(value);
        }

        public void Write(short value)
        {
            if (!LittleEndian)
                value = Endian.SwapInt16(value);
            mWriter.Write(value);
        }

        public void Write(int value)
        {
            if (!LittleEndian)
                value = Endian.SwapInt32(value);
            mWriter.Write(value);
        }

        public void Write(long value)
        {
            if (!LittleEndian)
                value = Endian.SwapInt64(value);
            mWriter.Write(value);
        }

        public void Write(char value)
        {
            Write((short)value);
        }

        public void Write(ushort value)
        {
            if (!LittleEndian)
                value = Endian.SwapUInt16(value);
            mWriter.Write(value);
        }

        public void Write(uint value)
        {
            if (!LittleEndian)
                value = Endian.SwapUInt32(value);
            mWriter.Write(value);
        }

        public void Write(ulong value)
        {
            if (!LittleEndian)
                value = Endian.SwapUInt64(value);
            mWriter.Write(value);
        }

        public void WriteUTF(string value)
        {
            if (string.IsNullOrEmpty(value))
                Write((ushort)0);
            byte[] data = Encoding.UTF8.GetBytes(value);
            Write((ushort)data.Length);
            Write(data, 0, data.Length);
        }

        public void Write(byte[] data)
        {
            mWriter.Write(data);
        }

        public void Write(byte[] data, int offset, int count)
        {
            mWriter.Write(data, offset, count);
        }

        public void Write(double value)
        {
            byte[] data = BitConverter.GetBytes(value);
            if (!LittleEndian)
                Array.Reverse(data);
            Write(data, 0, 8);
        }

        public void Write(float value)
        {
            byte[] data = BitConverter.GetBytes(value);
            if (!LittleEndian)
                Array.Reverse(data);
            Write(data, 0, 4);
        }
    }
}
