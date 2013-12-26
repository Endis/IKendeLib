package beetle.netpackage;

import java.io.DataInputStream;
import java.io.IOException;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
public class DataReader implements IDataReader {

	public DataReader(DataInputStream stream)
	{
		mStream = stream;
	}
	
	private DataInputStream mStream;
	
	@Override
	public DataInputStream GetStream() {
		// TODO Auto-generated method stub
		return mStream;
	}

	@Override
	public int Read(byte[] data) throws IOException {
		// TODO Auto-generated method stub
		return mStream.read(data);
	}

	@Override
	public int Read(byte[] data, int off, int len) throws IOException {
		// TODO Auto-generated method stub
		return mStream.read(data, off, len);
	}

	@Override
	public boolean ReadBoolean() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readBoolean();
	}

	@Override
	public byte ReadByte() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readByte();
	}

	@Override
	public char ReadChar() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readChar();
	}

	@Override
	public double ReadDouble() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readDouble();
	}

	@Override
	public float ReadFloat() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readFloat();
	}

	@Override
	public int ReadInt() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readInt();
	}

	@Override
	public short ReadShort() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readShort();
	}

	@Override
	public long ReadLong() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readLong();
	}

	@Override
	public String ReadUTF() throws IOException {
		// TODO Auto-generated method stub
		return mStream.readUTF();
	}

	@Override
	public void Read(IMessage msg) throws Exception {
		// TODO Auto-generated method stub
		msg.Load(this);
	}

}
