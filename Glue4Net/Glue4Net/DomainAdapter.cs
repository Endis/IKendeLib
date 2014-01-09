using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glue4Net
{
    public class DomainAdapter
    {

        public DomainAdapter(string appPath, string appName,bool updateWatch,params string[] filters)
        {
            if (appPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) != appPath.Length - 1)
            {
                appPath += System.IO.Path.DirectorySeparatorChar;
            }
            AppPath = appPath;
            CachePath = Path.Combine(AppPath, "_tempdll" + Path.DirectorySeparatorChar);
            AppName = appName;
            if (updateWatch)
            {
                mWatcher = new FileWatcher(appPath, filters);
                mWatcher.Change += OnChange;
            }
        }

        private FileWatcher mWatcher;

        private AssemblyLoader mLoader;

        private AppDomain mAppDomain;

        protected void OnChange(FileWatcher e)
        {
            UnLoad();
            Load();
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
                mLoader.Log = Log;
                mLoader.AppName = AppName;
                mLoader.LoadAssembly(AppPath);
                mLoader.Load();
                Log.Error("load {0} appdomain success!", AppName);
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
