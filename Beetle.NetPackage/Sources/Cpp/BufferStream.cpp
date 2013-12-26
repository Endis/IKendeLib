#include "Stdafx.h"
#include "BufferStream.h"
#include "ByteArrayReader.h"
#include "ByteArrayWriter.h"
#include "Endian.h"

namespace Beetle
{
	namespace NetPackage
	{
		BufferStream::BufferStream( System::IO::Stream ^stream, Boolean littleEndian )
		{
			m_ByteReader = gcnew ByteArrayReader(stream, littleEndian);
			m_ByteWriter = gcnew ByteArrayWriter(stream, littleEndian);
		}

		Boolean BufferStream::LittleEndian::get()
		{
			return m_ByteReader->LittleEndian & m_ByteWriter->LittleEndian;
		}

		void BufferStream::LittleEndian::set(Boolean value)
		{
			m_ByteReader->LittleEndian = value;
			m_ByteWriter->LittleEndian = value;
		}
	
		Byte BufferStream::Read()
		{
			return m_ByteReader->Read();
		}

		Boolean BufferStream::ReadBool()
		{
			return m_ByteReader->ReadBool();
		}

		Int16 BufferStream::ReadInt16()
		{
			return m_ByteReader->ReadInt16();
		}

		Int32 BufferStream::ReadInt32()
		{
			return m_ByteReader->ReadInt32();
		}

		Int64 BufferStream::ReadInt64()
		{
			return m_ByteReader->ReadInt64();
		}

		UInt16 BufferStream::ReadUInt16()
		{
			return m_ByteReader->ReadUInt16();
		}

		UInt32 BufferStream::ReadUInt32()
		{
			return m_ByteReader->ReadUInt32();
		}

		UInt64 BufferStream::ReadUInt64()
		{
			return m_ByteReader->ReadUInt64();
		}

		Char BufferStream::ReadChar()
		{
			return m_ByteReader->ReadChar();
		}

		float BufferStream::ReadFloat()
		{
			return m_ByteReader->ReadFloat();
		}

		double BufferStream::ReadDouble()
		{
			return m_ByteReader->ReadDouble();
		}

		String ^BufferStream::ReadUTF()
		{
			return m_ByteReader->ReadUTF();
		}

		array<Byte> ^BufferStream::ReadBytes(int count)
		{
			return m_ByteReader->ReadBytes(count);
		}

		void BufferStream::Write(Byte value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(Boolean value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(Int16 value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(Int32 value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(Int64 value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(Char value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(UInt16 value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(UInt32 value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::Write(UInt64 value)
		{
			m_ByteWriter->Write(value);
		}

		void BufferStream::WriteUTF(String ^value)
		{
			m_ByteWriter->WriteUTF(value);
		}

		void BufferStream::Write(array<Byte> ^data)
		{
			m_ByteWriter->Write(data);
		}

		void BufferStream::Write(array<Byte> ^data, int offset, int count)
		{
			m_ByteWriter->Write(data, offset, count);
		}
	}
}
