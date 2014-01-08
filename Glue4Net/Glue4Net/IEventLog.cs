using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glue4Net
{
    public interface IEventLog
    {
        void Track(string value);

        void Track(string formater, params object[] data);

        void Debug(string value);

        void Debug(string formater, params object[] data);

        void Info(string value);

        void Info(string formater, params object[] data);

        void Error(string value);

        void Error(string formater, params object[] data);


    }
}
