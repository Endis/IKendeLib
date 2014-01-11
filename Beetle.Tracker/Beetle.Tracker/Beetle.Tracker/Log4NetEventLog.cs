using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class Log4NetEventLog
    {

      

        public void Track(string value)
        {
            Utils.GetLog<TrackerServer>().Info(value);
        }

        public void Track(string formater, params object[] data)
        {
            Utils.GetLog<TrackerServer>().InfoFormat(formater, data);
        }

        public void Debug(string value)
        {
            Utils.GetLog<TrackerServer>().Debug(value);
        }

        public void Debug(string formater, params object[] data)
        {
            Utils.GetLog<TrackerServer>().DebugFormat(formater, data);
        }

        public void Info(string value)
        {
            Utils.GetLog<TrackerServer>().Info(value);
        }

        public void Info(string formater, params object[] data)
        {
            Utils.GetLog<TrackerServer>().InfoFormat(formater, data);
        }

        public void Error(string value)
        {
            Utils.GetLog<TrackerServer>().Error(value);
        }

        public void Error(string formater, params object[] data)
        {
            Utils.GetLog<TrackerServer>().ErrorFormat(formater, data);
        }
    }
}
