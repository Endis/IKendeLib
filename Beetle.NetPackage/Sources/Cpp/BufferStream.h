#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Linq;
using namespace System::Text;
/**
* Original author:
* Copyright © henryfan 2013
* Created by henryfan on 2013-7-30.
* C++.NET Version:
* Edit by ForeverACMer on 2013-09-07.
* homepage:www.ikende.com
* email:henryfan@msn.com & helibin822@gmail.com
*/
namespace Beetle
{
	namespace NetPackage
	{
		ref class ByteArrayReader;
		ref class ByteArrayWriter;

		public ref class BufferStream
		{
		public:
			BufferStream(System::IO::Stream ^stream, Boolean littleEndian);

		private:
			ByteArrayReader ^m_ByteReader;
			ByteArrayWriter ^m_ByteWriter;
		public: 
			property Boolean LittleEndian 
			{ 
				Boolean get(); 
				void set(Boolean value); 
			}

			Byte Read();
			Boolean ReadBool();
			Int16 ReadInt16();
			Int32 ReadInt32();
			Int64 ReadInt64();
			UInt16 ReadUInt16();
			UInt32 ReadUInt32();
			UInt64 ReadUInt64();
			Char ReadChar();
			float ReadFloat();
			double ReadDouble();
			String ^ReadUTF();
			array<Byte> ^ReadBytes(int count);

			void Write(Byte value);
			void Write(Boolean value);
			void Write(Int16 value);
			void Write(Int32 value);
			void Write(Int64 value);
			void Write(Char value);
			void Write(UInt16 value);
			void Write(UInt32 value);
			void Write(UInt64 value);
			void WriteUTF(String ^value);
			void Write(array<Byte> ^data);
			void Write(array<Byte> ^data, int offset, int count);

		private:
			System::IO::BinaryReader ^m_Reader;
			System::IO::BinaryWriter ^m_Writer;
			Boolean m_IsLittleEndian;
		};
	}
}