#include "Stdafx.h"

#include "Package.h"
#include "Endian.h"

namespace Beetle
{
	namespace NetPackage
	{
		Package::CheckSize::CheckSize()
		{
			m_Length = -1;
			m_LengthData = gcnew array<Byte>(4);
		}

		void Package::CheckSize::Import(Byte value)
		{
			LengthData[Index] = value;
			if (Index == 3)
			{
				Length = BitConverter::ToInt32(LengthData, 0);
				if (!LittleEndian)
				{
					Length = Endian::SwapInt32(Length);
				}
			}
			else
			{
				Index++;
			}
		}

		void Package::CheckSize::Reset()
		{
			m_Length = -1;
			m_Index = 0;
		}
	}
}