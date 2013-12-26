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
		ref class BufferStream;

		public interface class IMessage
		{
			virtual void Load(BufferStream ^reader) = 0;
			virtual void Save(BufferStream ^writer) = 0;
		};
	}
}
