package
{
	import Beetle.NetPackage.IDataReader;
	import Beetle.NetPackage.IDataWriter;
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
		
		public function Load(reader:IDataReader):void
		{
			Name= reader.ReadUTF();
			EMail= reader.ReadUTF();
			City= reader.ReadUTF();
			Country = reader.ReadUTF();
			var dv:String= reader.ReadUTF();
			RegTime=DateFormatter.parseDateString(dv);
			
			
		}	
		public function Save(writer:IDataWriter):void
		{
			writer.WriteUTF(Name);
			writer.WriteUTF(EMail);
			writer.WriteUTF(City);
			writer.WriteUTF(Country);
			var d:DateFormatter =new DateFormatter();
			d.formatString="YYYY-MM-DD HH:NN:SS";
			writer.WriteUTF(d.format(RegTime));
			
		
		}
	}
}