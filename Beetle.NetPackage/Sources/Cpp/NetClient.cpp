#include "Stdafx.h"
#include "INetClientHandler.h"
#include "NetClient.h"
#include "Package.h"
#include "BufferStream.h"

using namespace System::Threading;

namespace Beetle
{
	namespace NetPackage
	{
		NetClient::NetClient( String ^host, int port, Package ^package, INetClientHandler ^handler )
		{
			m_Connected = false;
			m_LittleEndian =true;
			array<System::Net::IPAddress ^> ^ips = System::Net::Dns::GetHostAddresses(host);
			m_Host = ips[0];
			m_Port = port;
			Handler = handler;
			m_Package = package;
			m_Package->Receive = gcnew Action<System::IO::Stream ^>(this, &NetClient::OnDataReceive);
			m_ReceiveSAEA = gcnew SocketAsyncEventArgs();
			m_ReceiveSAEA->Completed += gcnew EventHandler<SocketAsyncEventArgs ^>(this, &NetClient::ReceiveCompleted);
			m_ReceiveSAEA->SetBuffer(gcnew array<Byte>(1024 * 4), 0, 1024 * 4);
		}

		NetClient::~NetClient()
		{
			ReleaseSocket();
		}

		bool NetClient::LittleEndian::get(void) 
		{ 
			return m_LittleEndian; 
		}
		void NetClient::LittleEndian::set(bool val) 
		{
			m_LittleEndian = val;
			m_Package->LittleEndian = LittleEndian;
		}

		bool NetClient::Connect()
		{
			if (Connected)
			{
				return true;
			}
			try
			{
				m_Package->Reset();
				m_Socket = gcnew Socket(AddressFamily::InterNetwork, SocketType::Stream, ProtocolType::Tcp);
				m_Socket->Connect(m_Host, m_Port);
				BeginReceive();
				m_Connected = true;
				Handler->Connected(this);
				return true;
			}
			catch (Exception ^e_)
			{
				m_Connected = false;
				OnError(e_);
				return false;
			}
		}

		void NetClient::OnError( Exception ^e )
		{
			try
			{
				Handler->ClientError(this, e);
			}
			catch (Exception ^e)
			{
				throw gcnew Exception(e->Message);
			}
		}

		void NetClient::BeginReceive()
		{
			try
			{
				if (!m_Socket->ReceiveAsync(m_ReceiveSAEA))
				{
					ReceiveCompleted(this, m_ReceiveSAEA);
				}
			}
			catch (SocketException ^se)
			{
				ReleaseSocket();
				OnError(se);
			}
			catch (ObjectDisposedException ^ode)
			{
				ReleaseSocket();
				OnError(ode);
			}
			catch (Exception ^e_)
			{
				OnError(e_);
			}
		}

		void NetClient::ReceiveCompleted( Object ^sender, System::Net::Sockets::SocketAsyncEventArgs ^e )
		{
			if (e->SocketError == SocketError::Success && e->BytesTransferred > 0)
			{
				try
				{
					m_Package->Import(e->Buffer, 0, e->BytesTransferred);
					BeginReceive();
				}
				catch (Exception ^e_)
				{
					ReleaseSocket();
					OnError(e_);
				}
			}
			else
			{
				ReleaseSocket();
			}
		}

		void NetClient::OnDataReceive( System::IO::Stream ^stream )
		{
			try
			{
				stream->Position = 0;
				BufferStream ^reader = gcnew BufferStream(stream, LittleEndian);
				IMessage ^msg = m_Package->FromStream(reader);
				if (msg == nullptr)
				{
					throw gcnew Exception(L"message type not found!");
				}
				Handler->ClientReceive(this, m_Package->DoReceiveCast(msg));
			}
			catch (Exception ^e_)
			{
				OnError(e_);
			}
			finally
			{
				m_Package->Reset();
			}
		}

		bool NetClient::Send( Object ^message )
		{
			bool flag = false;
			Monitor::Enter(this);
			if (Connect())
			{
				try
				{
					IMessage ^msg = m_Package->DoSendCast(message);
					array<Byte> ^data = m_Package->GetMessageData(msg);
					int count = data->Length;
					int index = 0;
					int scount;
					while (count > 0)
					{
						scount = m_Socket->Send(data, index, count, SocketFlags::None);
						count -= scount;
						index += scount;
					}
					flag = true;
				}
				catch (SocketException ^se)
				{
					ReleaseSocket();
					OnError(se);
				}
				catch (ObjectDisposedException ^ode)
				{
					ReleaseSocket();
					OnError(ode);
				}
				catch (Exception ^e_)
				{
					OnError(e_);
				}
			}
			Monitor::Exit(this);
			return flag;
		}

		void NetClient::ReleaseSocket()
		{
			m_Connected = false;
			Monitor::Enter(this);
			if (m_Socket != nullptr)
			{
				Handler->ClientDisposed(this);
				try
				{
					if (m_Socket != nullptr)
					{
						m_Socket->Shutdown(SocketShutdown::Both);
					}
				}
				catch (Exception ^e)
				{
					throw gcnew Exception(e->Message);
				}
				try
				{
					if (m_Socket != nullptr)
					{
						m_Socket->Close();
					}
				}
				catch (Exception ^e)
				{
					throw gcnew Exception(e->Message);
				}
			}
			m_Socket = nullptr;
			Monitor::Exit(this);
		}

	}
}