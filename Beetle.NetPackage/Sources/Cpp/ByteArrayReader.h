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
		public ref class ByteArrayReader
		{
		public:
			ByteArrayReader(System::IO::Stream ^stream, Boolean littleEndian);

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

		private:
			System::IO::BinaryReader ^m_Reader;
			Boolean m_IsLittleEndian;
		};
	}
}