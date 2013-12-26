package Beetle.NetPackage
{
	/**
	 * Copyright © henryfan 2013
	 * Created by henryfan on 13-7-30.
	 * homepage:www.ikende.com
	 * email:henryfan@msn.com
	 */
	import flash.utils.ByteArray;
	
	public interface IMessage
	{
		function Load(reader:IDataReader):void;
		
		function Save(writer:IDataWriter):void;
	}
}