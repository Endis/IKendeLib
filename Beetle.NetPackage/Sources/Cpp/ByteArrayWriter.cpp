#include "Stdafx.h"
#include "ByteArrayWriter.h"
#include "Endian.h"

namespace Beetle
{
	namespace NetPackage
	{
		ByteArrayWriter::ByteArrayWriter( System::IO::Stream ^stream, Boolean littleEndian )
		{
			m_IsLittleEndian = littleEndian;
			m_Writer = gcnew System::IO::BinaryWriter(stream);
		}

		Boolean ByteArrayWriter::LittleEndian::get()
		{
			return m_IsLittleEndian;
		}

		void ByteArrayWriter::LittleEndian::set(Boolean value)
		{
			m_IsLittleEndian = value;
		}

		void ByteArrayWriter::Write(Byte value)
		{
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(Boolean value)
		{
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(Int16 value)
		{
			if (!LittleEndian)
				value = Endian::SwapInt16(value);
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(Int32 value)
		{
			if (!LittleEndian)
				value = Endian::SwapInt32(value);
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(Int64 value)
		{
			if (!LittleEndian)
				value = Endian::SwapInt64(value);
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(Char value)
		{
			Write(static_cast<Int16>(value));
		}

		void ByteArrayWriter::Write(UInt16 value)
		{
			if (!LittleEndian)
				value = Endian::SwapUInt16(value);
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(UInt32 value)
		{
			if (!LittleEndian)
				value = Endian::SwapUInt32(value);
			m_Writer->Write(value);
		}

		void ByteArrayWriter::Write(UInt64 value)
		{
			if (!LittleEndian)
				value = Endian::SwapUInt64(value);
			m_Writer->Write(value);
		}

		void ByteArrayWriter::WriteUTF(String ^value)
		{
			if (String::IsNullOrEmpty(value))
			{
				Write((UInt16)0);
			}
			else
			{
				array<Byte> ^data = Encoding::UTF8->GetBytes(value);
				Write(static_cast<UInt16>(data->Length));
				Write(data, 0, data->Length);
			}
		}

		void ByteArrayWriter::Write(array<Byte> ^data)
		{
			m_Writer->Write(data);
		}

		void ByteArrayWriter::Write(array<Byte> ^data, int offset, int count)
		{
			m_Writer->Write(data, offset, count);
		}
	}
}
