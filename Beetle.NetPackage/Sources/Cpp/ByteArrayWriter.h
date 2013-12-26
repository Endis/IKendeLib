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
		public ref class ByteArrayWriter
		{
		public:
			ByteArrayWriter(System::IO::Stream ^stream, Boolean littleEndian);

		public: 
			property Boolean LittleEndian 
			{ 
				Boolean get(); 
				void set(Boolean value); 
			}

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
			System::IO::BinaryWriter ^m_Writer;
			Boolean m_IsLittleEndian;
		};
	}
}