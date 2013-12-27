using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public interface IInfoFormater
    {
        object FromString(Type type,string value);
        string ToStringValue(object obj);
    }
}
