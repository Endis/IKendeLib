using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
namespace Beetle.NetPackage
{
    public interface INetClientHandler
    {
        void ClientReceive(NetClient client, object message);

        void ClientError(NetClient client, Exception e);

        void ClientDisposed(NetClient client);

        void Connected(NetClient client);
    }
}
