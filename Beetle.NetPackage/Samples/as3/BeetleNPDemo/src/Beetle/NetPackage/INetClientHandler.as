package Beetle.NetPackage
{
	/**
	 * Copyright © henryfan 2013
	 * Created by henryfan on 13-7-30.
	 * homepage:www.ikende.com
	 * email:henryfan@msn.com
	 */

	public interface INetClientHandler
	{
		function ClientReceive(client:NetClient,msg:Object):void;
		
		function ClientError(client:NetClient,err:Error):void;
		
		function ClientDisposed(client:NetClient):void;
		
		function ClientConnected(client:NetClient):void;
	}
}