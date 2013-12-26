package Beetle.NetPackage
{
	/**
	 * Copyright © henryfan 2013
	 * Created by henryfan on 13-7-30.
	 * homepage:www.ikende.com
	 * email:henryfan@msn.com
	 */
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.events.SecurityErrorEvent;
	import flash.net.Socket;
	import flash.utils.ByteArray;
	import flash.utils.Endian;

	public class NetClient
	{
		public function NetClient(host:String, port:int, pack:Package, handler:INetClientHandler)
		{
			mHost= host;
			mPort= port;
			mPackage = pack;
			Handler = handler;
			mPackage.Receive= OnPackageReceive;
		}
		
		private var mHost:String;
		
		private var mPort:int;
		
		private var mConnected:Boolean= false;
		
		private var mPackage:Package;
		
		public var Handler:INetClientHandler;
		
	    private var mLittleEndian:Boolean = true;
		
		private var mSocket:Socket= null;
		
		private function OnError(e:Error):void
		{
			if(Handler!=null)
				Handler.ClientError(this,e);
		}
		
		public function Connect():Boolean
		{
			try{
			
				if(mConnected)
					return true;
				mConnected = false;
				if(mSocket!=null && mSocket.connected)
					mSocket.close();
				mSocket = new Socket();
				mSocket.addEventListener(Event.CLOSE, closeHandler);
				mSocket.addEventListener(Event.CONNECT, connectHandler);
				mSocket.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);
				mSocket.addEventListener(SecurityErrorEvent.SECURITY_ERROR, securityErrorHandler);
				mSocket.addEventListener(ProgressEvent.SOCKET_DATA, socketDataHandler);
				mSocket.connect(mHost,mPort);
				
			}
			catch(e:Error)
			{
				OnError(e);
			}
			return false;
		}
		
		public function set LittleEndian(value:Boolean):void
		{
			mLittleEndian = value;
			mPackage.LittleEndian = value;
		}
		
		public function get LittleEndian():Boolean
		{
			return mLittleEndian;
		}
		
		public function Send(msg:Object):Boolean
		{
			if(Connect())
			{
				var message:IMessage = mPackage.SendCast(msg);
				var data:ByteArray= mPackage.GetMessageData(message);
				mSocket.writeBytes(data,0,data.length);
				mSocket.flush();
				return true;
			}
		    return false;
		}
		
		private function OnPackageReceive(data:ByteArray):void
		{
			try
			{
			data.position=0;
			var msg:IMessage = mPackage.FromStream(data);
			Handler.ClientReceive(this,mPackage.ReceiveCaste(msg));
			}
			catch(e:Error)
			{
				Handler.ClientError(this,e);
			}
			
		}
		
		private function closeHandler(event:Event):void {
			mConnected = false;
			Handler.ClientDisposed(this);
		}
		
		private function connectHandler(event:Event):void {
			mConnected= true;
			Handler.ClientConnected(this);
		}
		
		private function ioErrorHandler(event:IOErrorEvent):void {
			mConnected = false;
			OnError(new Error(event.text));
			
		}
		
		private function securityErrorHandler(event:SecurityErrorEvent):void {
			mConnected = false;
			OnError(new Error(event.text));
		}
		
		private function socketDataHandler(event:ProgressEvent):void {
			var data:ByteArray= new ByteArray();
			mSocket.readBytes(data,0,mSocket.bytesAvailable);
			mPackage.Import(data,0,data.length);
		}
	}
}