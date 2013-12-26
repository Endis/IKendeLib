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
		interface class IMessage;
		ref class BufferStream;

		public ref class Package abstract : IDisposable
		{
		private:
			ref class CheckSize
			{
			public:
				CheckSize();
			public:
				property int Length
				{
					int get(void) { return m_Length; }
					void set(int len) { m_Length = len; }
				}
				property int Index
				{
					int get(void) { return m_Index; }
					void set(int index) { m_Index = index; }
				}
				property array<Byte> ^LengthData
				{
					array<Byte> ^get(void) { return m_LengthData; }
					void set(array<Byte> ^val) { m_LengthData = val; }
				}
				property bool LittleEndian
				{
					bool get(void) { return m_LittleEndian; }
					void set(bool value) { m_LittleEndian = value; }
				};
			public:
				void Import(Byte value);
				void Reset();
			private:
				int m_Length, m_Index;
				array<Byte> ^m_LengthData;
				bool m_LittleEndian;
			};
		public:
			Package();
			~Package();

		public:
			property bool LittleEndian
			{
				bool get(void) { return m_LittleEndian; }
				void set(bool value) { m_LittleEndian = value; }
			};
			property Action<System::IO::Stream ^> ^Receive
			{
				Action<System::IO::Stream ^> ^get(void) { return m_Receive; }
				void set(Action<System::IO::Stream ^> ^val) { m_Receive = val; }
			}
		public: 
			Object ^operator [](String ^key);

		public:
			void Reset();
			void Import(array<Byte> ^data, int start, int count);
			virtual IMessage ^FromStream(BufferStream ^reader);
			virtual array<Byte> ^GetMessageData(IMessage ^msg);
			virtual IMessage ^DoSendCast(Object ^message);
			virtual Object ^DoReceiveCast(IMessage ^msg);
		protected:
			virtual void WriteMessageType(BufferStream ^writer, IMessage ^message) = 0;
			virtual IMessage ^GetMessage(BufferStream ^reader) = 0;
		private:
			bool OnImport(array<Byte> ^data, int %start, int %count);

		private:
			System::Collections::Hashtable ^m_Tables;
			bool m_LittleEndian, m_IsLoading;
			Action<System::IO::Stream ^> ^m_Receive;
			CheckSize ^m_CheckSize;
			System::IO::MemoryStream ^m_Stream;
		};
	}
}
