package
{
	import Beetle.NetPackage.IMessage;
	
	import flash.utils.ByteArray;
	
	import mx.controls.DateField;
	import mx.formatters.DateFormatter;
	
	
	
	public class Register implements IMessage
	{
		
		public var Name:String;
		
		public var EMail:String;
		
		public var City:String;
		
		public var Country:String;
		
		public var RegTime:Date;
		
		public function Register()
		{
		}
		
		public function Load(reader:ByteArray):void
		{
			Name= reader.readUTF();
			EMail= reader.readUTF();
			City= reader.readUTF();
			Country = reader.readUTF();
			var dv:String= reader.readUTF();
			RegTime=DateFormatter.parseDateString(dv);
			
			
		}
		
		public function Save(writer:ByteArray):void
		{
			writer.writeUTF(Name);
			writer.writeUTF(EMail);
			writer.writeUTF(City);
			writer.writeUTF(Country);
			var d:DateFormatter =new DateFormatter();
			d.formatString="YYYY-MM-DD HH:NN:SS";
			writer.writeUTF(d.format(RegTime));
			
		
		}
	}
}