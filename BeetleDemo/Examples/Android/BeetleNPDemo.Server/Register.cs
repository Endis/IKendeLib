using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace BeetleNPDemo.Server
{
    public class Register:Beetle.IMessage
    {
        public String Name;

        public String EMail;

        public String City;

        public String Country;

        public DateTime RegTime;
        
        public void Load(Beetle.IDataReader reader)
        {
            Name = reader.ReadUTF();
            EMail = reader.ReadUTF();
            City = reader.ReadUTF();
            Country = reader.ReadUTF();
            string date = reader.ReadUTF();
            Console.WriteLine(date);
            RegTime = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public void Save(Beetle.IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteUTF(EMail);
            writer.WriteUTF(City);
            writer.WriteUTF(Country);
            writer.WriteUTF(RegTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
