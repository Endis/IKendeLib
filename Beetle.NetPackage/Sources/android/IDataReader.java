package beetle.netpackage;

import java.io.DataInputStream;
import java.io.IOException;
import java.util.AbstractCollection;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */

public interface IDataReader {
	DataInputStream GetStream();
	int Read(byte[] data)throws IOException;
	int Read(byte[] data,int off,int len)throws IOException;
	boolean ReadBoolean()throws IOException;
	byte ReadByte()throws IOException;
	char ReadChar()throws IOException;
	double ReadDouble() throws IOException ;
	float ReadFloat()throws IOException;
	int ReadInt()throws IOException;
	short ReadShort()throws IOException;
	long ReadLong()throws IOException;
	String ReadUTF()throws IOException;
	void Read(IMessage msg)throws Exception;
	<T extends IMessage> T ReadMessage(Class<T> type) throws Exception;
	<T extends IMessage> AbstractCollection<T> ReadMessages(Class<T> type) throws Exception;
}
