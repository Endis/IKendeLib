package
{
	import Beetle.NetPackage.IMessage;
	import Beetle.NetPackage.Package;
	
	import flash.utils.ByteArray;
	
	public class NPPackage extends Package
	{
		public function NPPackage()
		{
			super();
		}
		protected override function  WriteMessageType(writer:ByteArray, message:IMessage):void
		{
			writer.writeUTF(flash.utils.getQualifiedClassName(message));	
		}
		
		protected override  function GetMessage(reader:ByteArray):IMessage
		{
			var name:String = reader.readUTF();
			if(name=="Register")
				return new Register();
			return null;
		}
	}
}