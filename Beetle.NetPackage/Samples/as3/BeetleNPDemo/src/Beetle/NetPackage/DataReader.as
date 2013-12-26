package Beetle.NetPackage
{
	import flash.utils.ByteArray;
	import flash.utils.Endian;
	/**
	 * Copyright © henryfan 2013
	 * Created by henryfan on 13-7-30.
	 * homepage:www.ikende.com
	 * email:henryfan@msn.com
	 */
	public class DataReader implements IDataReader
	{
		public function DataReader(stream:ByteArray,littleEndian:Boolean)
		{
			mStream= stream;
			mStream.endian = littleEndian?Endian.LITTLE_ENDIAN:Endian.BIG_ENDIAN;
		}
		
		private var mStream:ByteArray;
		
		public function GetByteArray():ByteArray
		{
			return mStream;
		}
		
		public function ReadBoolean():Boolean
		{
			return mStream.readBoolean();
		}
		
		public function ReadByte():int
		{
			return mStream.readByte();
		}
		
		public function ReadBytes(bytes:ByteArray, offset:uint = 0, length:uint = 0):void
		{
			return mStream.readBytes(bytes,offset,length);
		}
		
		public function ReadDouble():Number
		{
			return mStream.readDouble();
		}
		
		public function  ReadFloat():Number
		{
			return mStream.readFloat();
		}
		
		public function  ReadInt():int
		{
			return mStream.readInt();
		}
		
		public function ReadShort():int
		{
			return mStream.readShort();
		}
		
		public function ReadUnsignedByte():uint
		{
			return mStream.readUnsignedByte();
		}
		
		public function ReadUnsignedInt():uint
		{
			return mStream.readUnsignedInt();
		}
		
		public function ReadUnsignedShort():uint
		{
			return mStream.readUnsignedShort();
		}
		
		public function ReadUTF():String
		{
			return mStream.readUTF();
		}
		
		public function Load(msg:IMessage):void
		{
			msg.Load(this);
		}
		
	}
}