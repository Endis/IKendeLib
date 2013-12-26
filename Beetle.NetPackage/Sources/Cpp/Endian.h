#pragma once

namespace Beetle
{
	namespace NetPackage
	{
		ref class Endian abstract sealed
		{
		public:
			static Int16 SwapInt16(Int16 v)
			{
				return (Int16)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
			}

			static UInt16 SwapUInt16(UInt16 v)
			{
				return (UInt32)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
			}

			static Int32 SwapInt32(Int32 v)
			{
				return (Int32)(((SwapInt16((Int16)v) & 0xffff) << 0x10) |
					(SwapInt16((Int16)(v >> 0x10)) & 0xffff));
			}

			static UInt32 SwapUInt32(UInt32 v)
			{
				return (UInt32)(((SwapUInt16((UInt16)v) & 0xffff) << 0x10) |
					(SwapUInt16((UInt16)(v >> 0x10)) & 0xffff));
			}

			static Int64 SwapInt64(Int64 v)
			{
				return (Int64)(((Int64)(SwapInt32((Int32)v) & 0xffffffffL) << 0x20) |
					(SwapInt32((Int32)(v >> 0x20)) & 0xffffffffL));
			}

			static UInt64 SwapUInt64(UInt64 v)
			{
				return (UInt64)(((UInt64)(SwapUInt32((UInt32)v) & 0xffffffffL) << 0x20) |
					(SwapUInt32((UInt32)(v >> 0x20)) & 0xffffffffL));
			}
		};
	}
}