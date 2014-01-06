using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.IO;

namespace KGlue
{
    class AdapterDomain : MarshalByRefObject
    {
        public event EventDomainExecuting LogEvent;

        private AppDomain mAppDomain;

        private DomainStatus mStatus = DomainStatus.Stoped;

        public DomainStatus Status
        {
            get
            {
                return mStatus;
            }
        }
        public string[] Args
        {
            get;
            set;
        }
        private AppAssembly mDomainAssembly;

        private string mFullPath;

        private IList<IAppAdapter> mAdapters = new List<IAppAdapter>();

        public AdapterDomain(string virtualPath, string virtualName, string runExe = null)
        {
            VirtualPath = virtualPath;
            VirtualName = virtualName;
            mFullPath = virtualPath;
            RunExe = runExe;
            CachePath = Path.Combine(FullPath, "_tempdll" + Path.DirectorySeparatorChar);
        }

        public string CachePath
        {
            get;
            set;
        }

        public string RunExe
        {
            get;
            set;
        }

        public string FullPath
        {
            get
            {
                return mFullPath;
            }
        }

        public string VirtualName
        {
            get;
            set;
        }

        public Exception LastException
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get;
            set;
        }
        public bool IsStarting = false;
        public bool Start()
        {
            lock (this)
            {
                if (mAppDomain == null)
                {
                    try
                    {
                        IsStarting = true;
                        System.IO.Directory.Delete(CachePath,true);
                        OnLoad();
                        CallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] creating.", this.VirtualName));
                        mDomainAssembly.LoadAssembly(FullPath);
                        CallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] load assembly.", this.VirtualName));
                        mDomainAssembly.Start();
                        CallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] start.", this.VirtualName));
                        mAppDomain.DomainUnload += OnUnload;
                        mStatus = DomainStatus.Started;
                        return true;
                    }
                    catch (Exception e_)
                    {
                        try
                        {
                            if (mAppDomain != null)
                            {

                                AppDomain.Unload(mAppDomain);

                            }
                        }
                        catch
                        {
                        }
                        mAppDomain = null;
                        LastException = e_;
                        CallLogEvent(ExecutingStatus.Error, e_, string.Format("domain [{0}] start error.", this.VirtualName));
                        return false;
                    }
                    finally
                    {
                        IsStarting = false;
                    }
                }
                return true;

            }

        }

        private void OnLoad()
        {
            Type loadertype = typeof(AppAssembly);

            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = VirtualName;
            setup.ApplicationBase = FullPath;
            setup.CachePath = CachePath;
            setup.ShadowCopyFiles = "true";
            setup.ShadowCopyDirectories = FullPath;
            setup.ConfigurationFile = FullPath + "app.config";
           
            mAppDomain = AppDomain.CreateDomain(
               VirtualPath, null, setup);

            mDomainAssembly = (AppAssembly)mAppDomain.CreateInstanceAndUnwrap(
                loadertype.Assembly.GetName().Name,
                loadertype.FullName);
            mDomainAssembly.UnloadError = OnUnhandledException;
            mDomainAssembly.RunExe = RunExe;
            mDomainAssembly.VirtualName = VirtualName;
            mDomainAssembly.CallLogEvent = CallLogEvent;
            mDomainAssembly.SetArgs(Args);
        }

        public bool Ping()
        {
            mDomainAssembly.CallLogEvent = null;
            mDomainAssembly.CallLogEvent = CallLogEvent;
            return mDomainAssembly.Ping();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CallLogEvent(ExecutingStatus.Error, e.ExceptionObject as Exception, string.Format("domain [{0}]  OnUnhandledException.", VirtualName));
            Stop();

        }

        private void OnUnload(object sender, EventArgs e)
        {
            
            mStatus = DomainStatus.Stoped;
            CallLogEvent(ExecutingStatus.Warning, null, string.Format("domain [{0}] unload.", this.VirtualName));
        }
        public void Stop()
        {

            lock (this)
            {
                try
                {
                    if (mAppDomain != null)
                    {
                        mDomainAssembly.Stop();
                        AppDomain.Unload(mAppDomain);
                        mAppDomain = null;
                    }
                   
                   
                }
                catch (Exception e_)
                {
                    CallLogEvent(ExecutingStatus.Error, e_, string.Format("domain [{0}] unload error.", this.VirtualName));
                }
            }

        }

        private void CallLogEvent(ExecutingStatus status, Exception error, string message)
        {
            DomainExecutingArgs e = new DomainExecutingArgs();
            e.App = this.VirtualName;
            e.Error = error;
            e.Status = status;
            e.Message = message;
            e.AppFullPath = this.FullPath;
            OnLogEvent(e);
        }

        protected virtual void OnLogEvent(DomainExecutingArgs e)
        {
            if (LogEvent != null)
                LogEvent(this, e);
        }

        public override bool Equals(object obj)
        {
            if (obj is AdapterDomain)
                return this.FullPath == ((AdapterDomain)obj).FullPath;
            else
                return this.FullPath == obj;
        }


    }
}
