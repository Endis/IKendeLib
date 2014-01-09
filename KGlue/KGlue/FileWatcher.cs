using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.IO;

namespace KGlue
{
    class FileWatcher
    {
        private FileSystemWatcher mWather;

        private AdapterDomain mDomain;

        private DateTime mLastChangeTime;

        private bool mIsChange = false;

        public FileWatcher(AdapterDomain domain)
        {
            mDomain = domain;
            mWather = new FileSystemWatcher(domain.FullPath);
            mWather.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);
            mWather.Deleted += new FileSystemEventHandler(fileSystemWatcher_Changed);
            mWather.Created += new FileSystemEventHandler(fileSystemWatcher_Changed);
            
            mWather.EnableRaisingEvents = true;
            mWather.IncludeSubdirectories = false;

        }

        public void Update()
        {
            if (mIsChange && (DateTime.Now - mLastChangeTime).TotalSeconds > 10)
            {
                mIsChange = false;
                if (mDomain.Status == DomainStatus.Started)
                {
                    mDomain.Stop();
                    System.Threading.Thread.Sleep(2000);

                }
                mDomain.Start();
                
            }
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!mDomain.IsStarting)
            {
                mLastChangeTime = DateTime.Now;
               
                mIsChange = true;
            }
        }
    }
}
