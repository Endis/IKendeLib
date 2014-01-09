package beetle.netpackage;
import java.io.DataInputStream;
import java.io.DataOutputStream;

/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
public interface IMessage {
    void Load(DataInputStream stream) throws Exception;
    void Save(DataOutputStream stream) throws Exception;
}
