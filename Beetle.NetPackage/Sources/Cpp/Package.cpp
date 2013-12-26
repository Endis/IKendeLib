#include "Stdafx.h"

#include "Package.h"
#include "IMessage.h"
#include "BufferStream.h"
#include "Endian.h"

namespace Beetle
{
	namespace NetPackage
	{
		Package::Package()
		{
			 m_Tables = gcnew System::Collections::Hashtable(8);
			 m_Stream = gcnew System::IO::MemoryStream(1024 * 4);
			 m_IsLoading = false;
			 m_CheckSize = nullptr;
		}

		IMessage ^Package::FromStream(BufferStream ^reader)
		{
			try
			{
				IMessage ^msg = GetMessage(reader);
				msg->Load(reader);
				return msg;
			}
			catch (Exception ^e)
			{
				throw gcnew Exception(L"read message error!", e);
			}
		}

		array<Byte> ^Package::GetMessageData(IMessage ^msg)
		{
			System::IO::MemoryStream ^stream = gcnew System::IO::MemoryStream();
			BufferStream ^writer = gcnew BufferStream(stream, LittleEndian);
			WriteMessageType(writer, msg);
			msg->Save(writer);
			array<Byte> ^result = gcnew array<Byte>(static_cast<int>(stream->Length) + 4);
			System::IO::MemoryStream ^resultStream = gcnew System::IO::MemoryStream(result);
			writer = gcnew BufferStream(resultStream, LittleEndian);
			writer->Write(static_cast<int>(stream->Length));
			stream->Position = 0;
			stream->Read(result, 4, static_cast<int>(stream->Length));
			return result;
		}

		void Package::Import(array<Byte> ^data, int start, int count)
		{
			if (m_CheckSize == nullptr)
			{
				m_CheckSize = gcnew CheckSize();
				m_CheckSize->LittleEndian = LittleEndian;
			}
			while (count > 0)
			{
				if (!m_IsLoading)
				{
					m_CheckSize->Reset();
					m_Stream->SetLength(0);
					m_Stream->Position = 0;
					m_IsLoading = true;
				}
				if (m_CheckSize->Length == -1)
				{
					while (count > 0 && m_CheckSize->Length == -1)
					{
						m_CheckSize->Import(data[start]);
						start++;
						count--;
					}
				}
				else
				{
					if (OnImport(data, start, count))
					{
						m_IsLoading = false;
						if (Receive != nullptr)
						{
							m_Stream->Position = 0;
							Receive(m_Stream);
						}
					}
				}
			}
		}

		bool Package::OnImport(array<Byte> ^data, int %start, int %count)
		{
			if (count >= m_CheckSize->Length)
			{
				m_Stream->Write(data, start, m_CheckSize->Length);
				start += m_CheckSize->Length;
				count -= m_CheckSize->Length;
				return true;
			}
			else
			{
				m_Stream->Write(data, start, count);
				start += count;
				m_CheckSize->Length -= count;
				count = 0;
				return false;
			}
		}

		Object ^ Package::operator[]( String ^key )
		{
			return m_Tables[key];
		}

		IMessage ^Package::DoSendCast(Object ^message)
		{
			return safe_cast<IMessage ^>(message);
		}

		Object ^ Package::DoReceiveCast( IMessage ^msg )
		{
			return msg;
		}

		void Package::Reset()
		{
			m_CheckSize = nullptr;
			m_IsLoading = false;
		}

		Package::~Package()
		{
			m_Tables->Clear();
		}

	}
}