package beetle.netpackage;

import java.util.HashMap;

public class ProtoPackage extends Package {

	private static HashMap<String, ProtoMessageHandler> mMessageTbl = new HashMap<String, ProtoMessageHandler>();

	@Override
	public IMessage SendCast(Object msg) throws Exception
    {
    	if(msg ==null || !msg.getClass().getSuperclass().equals(com.google.protobuf.GeneratedMessage.class))
    		throw new Exception("object not is proto message!");
    	ProtoMessageAdapter adapter = new ProtoMessageAdapter();
    	adapter.Message =(com.google.protobuf.GeneratedMessage)msg;
    	return adapter;
    }
	
	@Override
    public Object ReceiveCast(IMessage msg) throws Exception
    {
    	ProtoMessageAdapter adapter =(ProtoMessageAdapter)msg;
    	return adapter.Message;
    }
	
	@Override
	protected void WriteMessageType(IDataWriter writer, IMessage message)
			throws Exception {
		

	}

	@Override
	protected IMessage GetMessage(IDataReader reader) throws Exception {
		// TODO Auto-generated method stub
		ProtoMessageAdapter adapter = new ProtoMessageAdapter();
		return adapter;
	}
	
	public static ProtoMessageHandler GetMsgHandler(String name)
	{
		return mMessageTbl.get(name);
	}
	
	public static void Register(Class<?> protobufclass) {
		try {
			ProtoMessageHandler mb;
			Class<?>[] protoObjs = protobufclass.getClasses();
			for (Class<?> item : protoObjs) {
				if(item==null)
					continue;
				if (!item.isInterface() && item.getSuperclass().equals(
						com.google.protobuf.GeneratedMessage.class)) {
					mb = new ProtoMessageHandler();
					mb.SetType(item);
					mMessageTbl.put(item.getSimpleName(), mb);
				}
			}
		} catch (Exception e) {
			String err = e.getMessage();
			
		}

	}

}
