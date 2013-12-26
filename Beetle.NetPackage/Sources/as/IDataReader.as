package Beetle.NetPackage
{
	import flash.utils.ByteArray;

	public interface IDataReader
	{
		function GetByteArray():ByteArray;
		
		function ReadBoolean():Boolean;
		
		function ReadByte():int;
		
		function ReadBytes(bytes:ByteArray, offset:uint = 0, length:uint = 0):void;
		
		function ReadDouble():Number;
		
		function ReadFloat():Number;
		
		function ReadInt():int;
		
		function ReadShort():int;
		
		function ReadUnsignedByte():uint;
		
		function ReadUnsignedInt():uint;
		
		function ReadUnsignedShort():uint;
		
		function ReadUTF():String;
		
		function Load(msg:IMessage):void;
	}
}