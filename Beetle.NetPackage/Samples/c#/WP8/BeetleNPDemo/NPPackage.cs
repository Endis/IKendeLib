using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeetleNPDemo
{
    class NPPackage : Beetle.NetPackage.Package
    {

        protected override void WriteMessageType(Beetle.NetPackage.IDataWriter writer, Beetle.NetPackage.IMessage message)
        {
            writer.WriteUTF(message.GetType().Name);
        }

        protected override Beetle.NetPackage.IMessage GetMessage(Beetle.NetPackage.IDataReader reader)
        {
            string Name = reader.ReadUTF();
            switch (Name)
            {
                case "Register":
                    return new Register();
            }
            return null;
        }
    }
    public class Register : Beetle.NetPackage.IMessage
    {
        public String Name;

        public String EMail;

        public String City;

        public String Country;

        public DateTime RegTime;

        public void Load(Beetle.NetPackage.IDataReader reader)
        {
            Name = reader.ReadUTF();
            EMail = reader.ReadUTF();
            City = reader.ReadUTF();
            Country = reader.ReadUTF();
            string date = reader.ReadUTF();
            Console.WriteLine(date);
            RegTime = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public void Save(Beetle.NetPackage.IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteUTF(EMail);
            writer.WriteUTF(City);
            writer.WriteUTF(Country);
            writer.WriteUTF(RegTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
