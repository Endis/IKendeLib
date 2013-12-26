#include "Stdafx.h"

#include "ByteArrayReader.h"
#include "Endian.h"

namespace Beetle
{
	namespace NetPackage
	{
		ByteArrayReader::ByteArrayReader( System::IO::Stream ^stream, Boolean littleEndian )
		{
			m_IsLittleEndian = littleEndian;
			m_Reader = gcnew System::IO::BinaryReader(stream);
		}

		Boolean ByteArrayReader::LittleEndian::get()
		{
			return m_IsLittleEndian;
		}

		void ByteArrayReader::LittleEndian::set(Boolean value)
		{
			m_IsLittleEndian = value;
		}

		Byte ByteArrayReader::Read()
		{
			return m_Reader->ReadByte();
		}

		Boolean ByteArrayReader::ReadBool()
		{
			return m_Reader->ReadBoolean();
		}

		Int16 ByteArrayReader::ReadInt16()
		{
			Int16 value = m_Reader->ReadInt16();
			if (!LittleEndian)
				value = Endian::SwapInt16(value);
			return value;
		}

		Int32 ByteArrayReader::ReadInt32()
		{
			Int32 value = m_Reader->ReadInt32();
			if (!LittleEndian)
				value = Endian::SwapInt32(value);
			return value;
		}

		Int64 ByteArrayReader::ReadInt64()
		{
			Int64 value = m_Reader->ReadInt64();
			if (!LittleEndian)
				value = Endian::SwapInt64(value);
			return value;
		}

		UInt16 ByteArrayReader::ReadUInt16()
		{
			UInt16 value = m_Reader->ReadUInt16();
			if (!LittleEndian)
				value = Endian::SwapUInt16(value);
			return value;

		}

		UInt32 ByteArrayReader::ReadUInt32()
		{
			UInt32 value = m_Reader->ReadUInt32();
			if (!LittleEndian)
				value = Endian::SwapUInt32(value);
			return value;
		}

		UInt64 ByteArrayReader::ReadUInt64()
		{
			UInt64 value = m_Reader->ReadUInt64();
			if (!LittleEndian)
				value = Endian::SwapUInt64(value);
			return value;
		}

		Char ByteArrayReader::ReadChar()
		{
			return static_cast<Char>(this->ReadInt16());
		}

		float ByteArrayReader::ReadFloat()
		{
			Int32 num = this->ReadInt32();
			return *(float*)(&num);
		}

		double ByteArrayReader::ReadDouble()
		{
			Int64 num = this->ReadInt64();
			return *(double*)(&num);
		}

		String ^ByteArrayReader::ReadUTF()
		{
			UInt16 value = ReadUInt16();
			if (value > 0)
			{
				array<Byte> ^result = ReadBytes(value);
				return Encoding::UTF8->GetString(result);
			}
			return nullptr;
		}

		array<Byte> ^ByteArrayReader::ReadBytes(int count)
		{
			array<Byte> ^result = m_Reader->ReadBytes(count);
			return result;
		}
	}
}
