using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker.TestImpl
{
    public class TestFormater:Beetle.Tracker.IInfoFormater
    {
        public object FromString(Type type, string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, type);
        }

        public string ToStringValue(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
