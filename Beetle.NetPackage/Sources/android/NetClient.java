package beetle.netpackage;

/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.UnknownHostException;

import android.R.bool;

public class NetClient implements IDataReceive {
	public NetClient(String host, int port, Package pack,
			INetClientHandler handler) {
		mHost = host;
		mPort = port;
		mPackage = pack;
		mPackage.ReceiveHandler = this;
		ClientHandler = handler;
	}

	private String mHost;

	private int mPort;

	private Socket mSokcet;

	private Package mPackage;

	private byte[] mReceiveBuffer = new byte[1024 * 4];

	public INetClientHandler ClientHandler;

	private boolean mConnected = false;

	private boolean mConnecting = false;

	private OutputStream mSocketOutput;

	private InputStream mSocketInput;

	private void DisConnect() {
		mConnected = false;
		try {
			ClientHandler.Disposed(this);
			if (mSokcet != null)
				mSokcet.close();
		} catch (Exception e) {

		}

	}

	public Boolean GetConnected()
	{
		return mConnected;
	}
	
	public String getHost() {
		return mHost;
	}

	public void setHost(String value) {
		mHost = value;
	}

	public int getPort() {
		return mPort;
	}

	public void setPort(int value) {
		mPort = value;
	}

	public boolean Connect() {
		if (mConnected)
			return mConnected;
		ClientThread ct = new ClientThread();
		Thread thread = new Thread(ct);
		thread.start();

		return mConnected;
	}

	private void OnConnected() {
		try {
			ClientHandler.Connected(this);
		} catch (Exception e_) {

		}
	}

	private void OnError(Exception e) {
		try {
			ClientHandler.Error(this, e);
		} catch (Exception e_) {

		}
	}

	class ReceiveThread implements Runnable {
		@Override
		public void run() {

			while (mConnected) {
				try {
					int reads = mSocketInput.read(mReceiveBuffer, 0,
							mReceiveBuffer.length);
					if (reads == -1)
					{
						DisConnect();
						break;
					}
					mPackage.Import(mReceiveBuffer, 0, reads);

				} catch (IOException ioe) {
					DisConnect();
					OnError(ioe);
				} catch (Exception e) {
					OnError(e);
				}

			}
		}

	}

	class ClientThread implements Runnable {

		@Override
		public void run() {

			if (!mConnecting) {
				mConnecting = true;
				try {
					InetAddress hostIP = InetAddress.getByName(mHost);
					if (mSokcet != null)
						mSokcet.close();
					mPackage.Reset();
					mSokcet = new Socket();
					mSokcet.connect(new InetSocketAddress(hostIP, mPort));

					mSocketInput = mSokcet.getInputStream();
					mConnected = true;
					OnConnected();
					ReceiveThread rt = new ReceiveThread();
					Thread thread = new Thread(rt);
					thread.start();

				} catch (UnknownHostException e1) {
					DisConnect();
					OnError(e1);
				} catch (IOException e1) {
					DisConnect();
					OnError(e1);
				}
				mConnecting = false;
			}
		}

	}

	public void Receive(ByteArrayInputStream stream) {
		try {
			IMessage msg = mPackage.FromStream(new DataInputStream(stream));
			ClientHandler.Receive(this, mPackage.ReceiveCast(msg));
		} catch (Exception e) {
			OnError(e);

		}

	}

	public Boolean Send(Object msg) {
		try {
			Connect();
			if (mConnected) {
				synchronized (this) {
					IMessage sendmsg = mPackage.SendCast(msg);
					byte[] data = mPackage.GetMessageData(sendmsg);
					mSocketOutput = mSokcet.getOutputStream();
					mSocketOutput.write(data);
					mSocketOutput.flush();
					return true;
				}
			}
		} catch (IOException ioe) {
			DisConnect();
			OnError(ioe);
		} catch (Exception e_) {
			OnError(e_);
		}
		return false;
	}

}
