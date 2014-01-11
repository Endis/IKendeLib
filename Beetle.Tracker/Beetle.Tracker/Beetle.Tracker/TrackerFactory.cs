using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Tracker
{
    public class TrackerFactory
    {
        public TrackerFactory()
        {
            mTrackerPath = AppDomain.CurrentDomain.BaseDirectory + @"Trackers\";
            Log = new Log4NetEventLog();
        }

        public Glue4Net.IEventLog Log
        {
            get;
            set;
        }

        private Dictionary<string, Glue4Net.DomainAdapter> mDomains = new Dictionary<string, Glue4Net.DomainAdapter>();

        private string mTrackerPath;

        public IAppTrackerHandler GetTrackHandler(string name)
        {
            Glue4Net.DomainAdapter adapter = null;
            if (mDomains.TryGetValue(name, out adapter))
            {
                return (IAppTrackerHandler)adapter["TRACK_HANDLER"];
            }
            return null;
        }

        private void DomainUnload(Glue4Net.DomainAdapter adapter)
        {
            if (adapter.Status == Glue4Net.DomainStatus.Start)
                adapter.UnLoad();
        }

        private void DomainLoad(Glue4Net.DomainAdapter adapter)
        {
            if (adapter.Status == Glue4Net.DomainStatus.Stop)
            {
                adapter.Load();
                if (adapter.Status == Glue4Net.DomainStatus.Start)
                    adapter["TRACK_HANDLER"] = adapter.CreateProxyObject("");
            }
        }

        public void Load()
        {
            if (Log != null)
                Log.Info("Tracker Factory Tracker Loding...");
            foreach (string item in System.IO.Directory.GetDirectories(mTrackerPath))
            {


                System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(item);
                if (!mDomains.ContainsKey(info.Name))
                {
                    Glue4Net.DomainArgs args = new Glue4Net.DomainArgs();
                    args.Compiler = true;
                    args.UpdateWatch = true;
                    args.WatchFilter = new string[] { "*.dll", "*.xml", "*.config", ".ini" };
                    Glue4Net.DomainAdapter domain = new Glue4Net.DomainAdapter(item, info.Name, args);
                    domain.Log = Log;
                    mDomains[info.Name] = domain;
                    if (Log != null)
                        Log.Info("Created Tracker {0} Path:{1}", info.Name, item);
                }
            }
        }

        public void Stop()
        {
            if (Log != null)
                Log.Info("Tracker Factory stop ...");
            foreach (Glue4Net.DomainAdapter item in mDomains.Values)
            {
                DomainUnload(item);
            }
        }

        public void Stop(string name)
        {
          
            foreach (Glue4Net.DomainAdapter item in mDomains.Values)
            {
                if (item.AppName == name)
                {
                    if (Log != null)
                        Log.Info("Tracker Factory stop {0} ...", name);
                    DomainUnload(item);
                }
            }
        }

        public void Restart()
        {
            if (Log != null)
                Log.Info("Tracker Factory restart...");
            foreach (Glue4Net.DomainAdapter item in mDomains.Values)
            {
                DomainUnload(item);
                DomainLoad(item);
            }
        }

        public void Restart(string name)
        {
            foreach (Glue4Net.DomainAdapter item in mDomains.Values)
            {
                if (item.AppName == name)
                {
                    if (Log != null)
                        Log.Info("Tracker Factory {0} restart...",name);
                    DomainUnload(item);
                    DomainLoad(item);
                }
            }
        }

        public void Start()
        {
            if (Log != null)
                Log.Info("Tracker Factory start...");
            foreach (Glue4Net.DomainAdapter item in mDomains.Values)
            {
                DomainLoad(item);
            }
        }

        public void Start(string name)
        {
            foreach (Glue4Net.DomainAdapter item in mDomains.Values)
            {
                if (item.Status == Glue4Net.DomainStatus.Stop && name == item.AppName)
                {
                    if (Log != null)
                        Log.Info("Tracker Factory {0} start...",name);
                    DomainLoad(item);
                }
            }
        }
    }
}
