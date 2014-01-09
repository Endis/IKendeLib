package beetle.npdemo;

import java.io.DataInputStream;
import java.io.DataOutputStream;

import beetle.netpackage.IMessage;
import beetle.netpackage.Package;

public class NPPackage extends Package {

	@Override
	protected void WriteMessageType(DataOutputStream writer, IMessage message)
			throws Exception {
		// TODO Auto-generated method stub
		writer.writeUTF(message.getClass().getSimpleName());
	}

	@Override
	protected IMessage GetMessage(DataInputStream reader) throws Exception {
		// TODO Auto-generated method stub
		String name= reader.readUTF();
		if(name.equals("Register"))
			return new Register();
		return null;
	}

}
