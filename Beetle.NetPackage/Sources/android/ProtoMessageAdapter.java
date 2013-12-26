package beetle.netpackage;

public class ProtoMessageAdapter implements IMessage {

	public com.google.protobuf.GeneratedMessage Message;
	
	@Override
	public void Load(IDataReader stream) throws Exception {
		// TODO Auto-generated method stub
		String name= stream.ReadUTF();
		ProtoMessageHandler handler = ProtoPackage.GetMsgHandler(name);
		if(handler==null)
			throw new Exception(name+" message type notfound!");
		Message=(com.google.protobuf.GeneratedMessage) handler.GetObject(stream.GetStream());
	}

	@Override
	public void Save(IDataWriter stream) throws Exception {
		// TODO Auto-generated method stub
		stream.WriteUTF(Message.getClass().getSimpleName());
		Message.writeTo(stream.GetStream());
	}

}
