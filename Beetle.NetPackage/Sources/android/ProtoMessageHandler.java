package beetle.netpackage;

import java.io.InputStream;
import java.lang.reflect.Method;

public class ProtoMessageHandler {

	public ProtoMessageHandler() {

	}

	private Method mPassMethod;

	private Class<?> mType;

	public void SetType(Class<?> type) throws Exception {
		mType = type;	
		mPassMethod = type.getMethod("parseFrom", InputStream.class);
	}

	public Object GetObject(InputStream stream) throws Exception {
		return mPassMethod.invoke(null, stream);
	}

}
