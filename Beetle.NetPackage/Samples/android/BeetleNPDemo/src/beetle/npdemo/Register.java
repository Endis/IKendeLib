package beetle.npdemo;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;

import beetle.netpackage.IDataReader;
import beetle.netpackage.IDataWriter;
import beetle.netpackage.IMessage;

public class Register implements IMessage {

	
	public String Name;
	
	public String EMail;
	
	public String City;
	
	public String Country;
	
	public Date RegTime;
	
	
	@Override
	public void Load(IDataReader stream) throws Exception {
		// TODO Auto-generated method stub
		Name = stream.ReadUTF();
		EMail = stream.ReadUTF();
		City = stream.ReadUTF();
		Country = stream.ReadUTF();
		String format = "yyyy-MM-dd HH:mm:ss";
		SimpleDateFormat sdf = new SimpleDateFormat(format);
		RegTime = sdf.parse(stream.ReadUTF());
	   
		
	}

	@Override
	public void Save(IDataWriter stream) throws Exception {
		// TODO Auto-generated method stub
		stream.WriteUTF(Name);
		stream.WriteUTF(EMail);
		stream.WriteUTF(City);
		stream.WriteUTF(Country);
		String format = "yyyy-MM-dd HH:mm:ss";
		SimpleDateFormat sdf = new SimpleDateFormat(format);
		stream.WriteUTF(sdf.format(RegTime));
	}

}
