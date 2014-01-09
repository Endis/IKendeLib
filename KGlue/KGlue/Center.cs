using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace KGlue
{
    public class Center
    {
        private System.Collections.ArrayList mDomains = new System.Collections.ArrayList();

        private System.Collections.ArrayList mFileWatches = new System.Collections.ArrayList();

        public event EventDomainExecuting LogEvent;

        public Center()
        {
            CreateWatchTimer();
        }

        public Center(string section)
        {
            CreateWatchTimer();
            AppSection config = (AppSection)System.Configuration.ConfigurationManager.GetSection(section);
            foreach(Item item in config.Items)
            {
                Add(item.Path,item.Name,item.Args.Split(' '));
            }
           
        }

        private System.Threading.Timer mTimer;

     

        private void CreateWatchTimer()
        {
            mTimer = new System.Threading.Timer(OnFileWatche,null,10000,5000);
         
        }

        public void Add(string virtualpath, string virtualname,string[] args)
        {
            if (virtualpath.IndexOf(System.IO.Path.DirectorySeparatorChar) < 0)
            {
                virtualpath = AppDomain.CurrentDomain.BaseDirectory + virtualpath;
            }
            if (virtualpath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) != virtualpath.Length - 1)
            {
                virtualpath += System.IO.Path.DirectorySeparatorChar;
            }
            if (System.IO.Directory.Exists(virtualpath))
            {
                AdapterDomain pd = new AdapterDomain(virtualpath, virtualname, null);
                pd.Args = args;
                pd.LogEvent += OnLogEvent;
                mDomains.Add(pd);
                if (!System.IO.Directory.Exists(pd.CachePath))
                {
                    System.IO.Directory.CreateDirectory(pd.CachePath);
                }
                mFileWatches.Add(new FileWatcher(pd));
            }
            else
            {
                OnLogEvent(this, new DomainExecutingArgs { Status = ExecutingStatus.Warning, Message = string.Format("{0} notfound.", virtualpath) });
            }
        }

        public System.Collections.ArrayList Domains
        {
            get
            {
                return mDomains;
            }
        }

        public void Start()
        {
            foreach (AdapterDomain item in mDomains)
            {
                item.Start();
            }

        }

        public void Start(string virtualpath)
        {
            int index = mDomains.IndexOf(virtualpath);
            if (index >= 0)
                ((AdapterDomain)mDomains[index]).Start();
        }

        public void Stop()
        {
            foreach (AdapterDomain item in mDomains)
            {
                item.Stop();
            }

        }

        public void Stop(string virtualpath)
        {
            int index = mDomains.IndexOf(virtualpath);
            if (index >= 0)
                ((AdapterDomain)mDomains[index]).Stop();
        }

        private AdapterDomain GetDomain(string virtualpath)
        {
            int index = mDomains.IndexOf(virtualpath);
            if (index >= 0)
                return ((AdapterDomain)mDomains[index]);
            return null;
        }

        private void OnLogEvent(object sender, DomainExecutingArgs e)
        {
            if (LogEvent != null)
                LogEvent(this, e);
        }

        private long mPingCount = 0;

        private void OnPingDomian(object state)
        {
            foreach (AdapterDomain item in mDomains)
            {
                try
                {
                    if (item.Status == DomainStatus.Started)
                        item.Ping();
                }
                catch (Exception e_)
                {
                    OnLogEvent(this, new DomainExecutingArgs { Status = ExecutingStatus.Error, Error =e_, Message = string.Format("domain [{0}] ping error.", item.VirtualName) });
                }
            }
        }
        private void OnFileWatche(object state)
        {
            System.Threading.Interlocked.Increment(ref mPingCount);
            if (mPingCount % 2 == 0)
            {
                OnPingDomian(state);
            }
            for (int i = 0; i < mFileWatches.Count; i++)
            {
                ((FileWatcher)mFileWatches[i]).Update();
            }
        }
    }
}
