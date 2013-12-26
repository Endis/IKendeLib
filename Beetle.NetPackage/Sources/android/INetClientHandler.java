package beetle.netpackage;

/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
public interface INetClientHandler {
    void  Receive(NetClient client, Object msg);
    void  Error(NetClient client, Exception e);
    void  Disposed(NetClient client);
    void  Connected(NetClient client);

}
