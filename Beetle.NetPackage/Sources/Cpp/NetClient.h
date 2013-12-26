#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Linq;
using namespace System::Text;
using namespace System::Net::Sockets;

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
		ref class Package;
		interface class INetClientHandler;

		public ref class NetClient : IDisposable
		{
		public:
			NetClient(String ^host, int port, Package ^package, INetClientHandler ^handler);
			~NetClient();

		public:
			bool Connect();
			bool Send(Object ^message);

		public:
			property INetClientHandler ^Handler
			{
				INetClientHandler ^get(void) { return m_Handler; }
				void set(INetClientHandler ^val) { m_Handler = val; }
			}
			property bool Connected
			{
				bool get(void) { return m_Connected; }
			}
			property Exception ^LastError
			{
			public:
				Exception ^get(void) { return m_LastError; }
			private:
				void set(Exception ^ex) { m_LastError = ex; }
			}
			property bool LittleEndian
			{
				bool get(void);
				void set(bool);
			}

		private:
			void OnError(Exception ^e);
			void BeginReceive();
			void ReceiveCompleted(Object ^sender, System::Net::Sockets::SocketAsyncEventArgs ^e);
			void OnDataReceive(System::IO::Stream ^stream);
			void ReleaseSocket();

		private:
			System::Net::Sockets::SocketAsyncEventArgs ^m_ReceiveSAEA;
			Package ^m_Package;
			System::Net::IPAddress ^m_Host;
			System::Net::Sockets::Socket ^m_Socket;
			int m_Port;
			bool m_Connected, m_LittleEndian;
			INetClientHandler ^m_Handler;
			Exception ^m_LastError;
		};
	}
}