using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle;
using System.Security.Cryptography;
using System.IO;

namespace Encrypt
{
    public class EncryptPackage : Beetle.Protocol.SizePackage
    {

        public EncryptPackage()
        {
        }
        public EncryptPackage(IChannel channel)
            : base(channel)
        {

        }
        private byte[] mKey = Convert.FromBase64String("d6uffncpM1qEZp56sZFNCeezbKgTBShW");

        private byte[] mIV = Convert.FromBase64String("S1ik9kRvN5w=");

        protected override IMessage ReadMessageByType(IDataReader reader, ReadObjectInfo typeTag)
        {
            typeTag.TypeofString  = reader.ReadUTF();
            switch (typeTag.TypeofString)
            {
                case "Register":
                    return new Register();
            }
            return null;
        }

        protected override void WriteMessageType(IMessage msg, IDataWriter writer)
        {
            writer.WriteUTF(msg.GetType().Name);
        }

        protected override void SaveBody(IDataWriter writer, IMessage msg)
        {

            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = mKey;
                tdsAlg.IV = mIV;
                ICryptoTransform encryptor = tdsAlg.CreateEncryptor(tdsAlg.Key, tdsAlg.IV);
                using (CryptoStream csEncrypt = new CryptoStream((Stream)writer, encryptor, CryptoStreamMode.Write))
                {
                    DataWriter dw = new DataWriter(csEncrypt, writer.LittleEndian);
                    base.SaveBody(dw, msg);
                    csEncrypt.FlushFinalBlock();

                }

            }
        }

        protected override void LoadBody(IDataReader reader, IMessage message)
        {
            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = mKey;
                tdsAlg.IV = mIV;
                ICryptoTransform decryptor = tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV);
                using (CryptoStream csDecrypt = new CryptoStream((Stream)reader, decryptor, CryptoStreamMode.Read))
                {
                    DataReader dr = new DataReader(csDecrypt, reader.LittleEndian);
                    base.LoadBody(dr, message);
                }

            }
        }
    }
}
