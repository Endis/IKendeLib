package beetle.netpackage;

import java.io.DataOutputStream;
import java.io.IOException;
import java.util.AbstractCollection;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
public interface IDataWriter {
	
	DataOutputStream GetStream();
	void Write(byte[] data,int off,int len) throws IOException;
	void Write(boolean value)throws IOException;
	void Write(byte value)throws IOException;
	void Write(char value)throws IOException;
	void Write(double value)throws IOException ;
	void Write(float value)throws IOException;
	void Write(int value)throws IOException;
	void Write(short value)throws IOException;
	void Write(long value)throws IOException;
	void WriteUTF(String value)throws IOException; 
	void Write(IMessage msg)throws Exception;
	<T extends IMessage> void Write(AbstractCollection<T> messages) throws Exception;
}
