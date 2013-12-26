package beetle.netpackage;

/**
 * Copyright © henryfan 2013
 * Created by henryfan on 13-7-30.
 * homepage:www.ikende.com
 * email:henryfan@msn.com
 */
public interface IMessage {
    void Load(IDataReader stream) throws Exception;
    void Save(IDataWriter stream) throws Exception;
}
