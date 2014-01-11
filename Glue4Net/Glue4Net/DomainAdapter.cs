using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glue4Net
{
    public class DomainAdapter
    {
        
        public DomainAdapter(string appPath, string appName,DomainArgs args)
        {
            Status = DomainStatus.Stop;
            mArgs = args;
            if (appPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) != appPath.Length - 1)
            {
                appPath += System.IO.Path.DirectorySeparatorChar;
            }
            AppPath = appPath;
            CachePath = Path.Combine(AppPath, "_tempdll" + Path.DirectorySeparatorChar);
            AppName = appName;
            if (args !=null && args.UpdateWatch)
            {
                mWatcher = new FileWatcher(appPath, args.WatchFilter);
                mWatcher.Change += OnChange;
            }

        }

        private System.Collections.Hashtable mProperties = new System.Collections.Hashtable();

        public object this[string key]
        {
            get
            {
                return mProperties[key];
            }
            set
            {
                mProperties[key] = value;
            }
        }

        private DomainArgs mArgs;

        private FileWatcher mWatcher;

        private AssemblyLoader mLoader;

        private AppDomain mAppDomain;

        protected void OnChange(FileWatcher e)
        {
            try
            {
                UnLoad();
                Load();
                if (Log != null)
                {
                    Log.Error("Update {0} restart appdomain success!", AppName);
                }

            }
            catch (Exception e_)
            {
                if (Log != null)
                {
                    Log.Error("Update {0} restart appdomain error {1}!", AppName, e_.Message);
                }
            }
        }

        public DomainStatus Status
        {
            get;
            set;
        }

        public string AppName
        {
            get;
            set;
        }

        public string CachePath
        {
            get;
            set;
        }

        public string AppPath
        { get; set; }

        public IEventLog Log
        {
            get;
            set;
        }

        public void Load()
        {
            try
            {
                Type loadertype = typeof(AssemblyLoader);
                AppDomainSetup setup = new AppDomainSetup();
                setup.ApplicationName = AppName;
                setup.ApplicationBase = AppPath;
                setup.CachePath = CachePath;
                setup.ShadowCopyFiles = "true";
                setup.ShadowCopyDirectories = AppPath;
                setup.ConfigurationFile = AppPath + "app.config";
                mAppDomain = AppDomain.CreateDomain(
                   AppPath, null, setup);
                mLoader = (AssemblyLoader)mAppDomain.CreateInstanceAndUnwrap(
                    loadertype.Assembly.GetName().Name,
                    loadertype.FullName);
                if (mArgs != null)
                    mLoader.CompilerFiles = mArgs.Compiler;
                mLoader.SetLog(Log);
                mLoader.AppName = AppName;
                mLoader.LoadAssembly(AppPath);
                mLoader.Load();
                Log.Info("load {0} appdomain success!", AppName);
                Status = DomainStatus.Start;
            }
            catch (Exception e_)
            {
                if (Log != null)
                {
                    Log.Error("load {0} appdomain error {1}!", AppName,e_.Message);
                }
            }
        }

        public void UnLoad()
        {
        
            if (mLoader != null)
            {
                try
                {
                    mLoader.UnLoad();
                    System.Threading.Thread.Sleep(1000);
                    AppDomain.Unload(mAppDomain);
                    Status = DomainStatus.Stop;
                }
                catch (Exception e_)
                {
                    if (Log != null)
                    {
                        Log.Error("unload {0} appdomain error {1}!", AppName,e_.Message);
                    }
                }
                mLoader = null;
            }
            
        }

        public object CreateProxyObject(string name)
        {
            if (mLoader != null)
                return mLoader.CreateProxyObject(name);
            return null;
        }

        public object GetValue(string key)
        {
            if (mLoader != null)
                return mLoader.GetValue(key);
            return null;
        }

        public void SetValue(string key, object value)
        {
            if (mLoader != null)
                mLoader.SetValue(key, value);
        }
    }
}
