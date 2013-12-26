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
    /**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
    public class DataReader:IDataReader
    {
        public DataReader(System.IO.Stream stream, bool littleEndian)
        {
            LittleEndian = littleEndian;
            mReader = new System.IO.BinaryReader(stream);
            mStream = stream;
        }   

        private System.IO.Stream mStream;

        public System.IO.Stream Stream
        {
            get{
                return mStream;
            }
        }

        private byte[] mTmpData = new byte[8];

        private System.IO.BinaryReader mReader;

        public bool LittleEndian
        {
            get;
            set;
        }

        public byte Read()
        {
            return mReader.ReadByte();
        }

        public bool ReadBool()
        {
            return mReader.ReadBoolean();
        }

        public short ReadShort()
        {
            short value = mReader.ReadInt16();
            if (!LittleEndian)
                value = Endian.SwapInt16(value);
            return value;

        }

        public int ReadInt()
        {
            int value = mReader.ReadInt32();
            if (!LittleEndian)
                value = Endian.SwapInt32(value);
            return value;
        }

        public long ReadLong()
        {
            long value = mReader.ReadInt64();
            if (!LittleEndian)
                value = Endian.SwapInt64(value);
            return value;
        }

        public ushort ReadUShort()
        {
            ushort value = mReader.ReadUInt16();
            if (!LittleEndian)
                value = Endian.SwapUInt16(value);
            return value;

        }

        public uint ReadUInt()
        {
            uint value = mReader.ReadUInt32();
            if (!LittleEndian)
                value = Endian.SwapUInt32(value);
            return value;
        }

        public ulong ReadULong()
        {
            ulong value = mReader.ReadUInt64();
            if (!LittleEndian)
                value = Endian.SwapUInt64(value);
            return value;
        }

        public char ReadChar()
        {
            return (char)this.ReadShort();
        }

        public  float ReadFloat()
        {
            mReader.Read(mTmpData, 0, 4);
            if(!LittleEndian)
                Array.Reverse(mTmpData, 0, 4);
            return BitConverter.ToSingle(mTmpData, 0);
          
        }

        public  double ReadDouble()
        {
            mReader.Read(mTmpData, 0, 8);
            if (!LittleEndian)
                Array.Reverse(mTmpData, 0, 8);
            return BitConverter.ToDouble(mTmpData, 0);
        }

        public string ReadUTF()
        {
            ushort value = ReadUShort();
            if (value > 0)
            {
                byte[] result = ReadBytes(value);
                return Encoding.UTF8.GetString(result, 0, result.Length);
            }
            return null;
        }

        public byte[] ReadBytes(int count)
        {
            byte[] result = mReader.ReadBytes(count);
            return result;
        }
       
    }
}
