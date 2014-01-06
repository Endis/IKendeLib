using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace KGlue
{
    class AppAssembly : MarshalByRefObject
    {
        private AssemblyFactory mAssemblyFactory = new AssemblyFactory();



        public void AddReference(string referenceToDll)
        {
            if (!mRefAssembly.Contains(referenceToDll))
            {
                mRefAssembly.Add(referenceToDll);
            }
        }

        private string[] mArgs = new string[0];

        public void SetArgs(string[] args)
        {
            mArgs = args;
        }

        public bool Ping()
        {
            return true;
        }

        public AppAssembly()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnLoaderror;
            AddReference("Accessibility.dll");
            AddReference("System.Configuration.dll");
            AddReference("System.Configuration.Install.dll");
            AddReference("System.Data.dll");
            AddReference("System.Design.dll");
            AddReference("System.DirectoryServices.dll");
            AddReference("System.Drawing.Design.dll");
            AddReference("System.Drawing.dll");
            AddReference("System.EnterpriseServices.dll");
            AddReference("System.Management.dll");
            AddReference("System.Runtime.Remoting.dll");
            AddReference("System.Runtime.Serialization.Formatters.Soap.dll");
            AddReference("System.Security.dll");
            AddReference("System.ServiceProcess.dll");
            AddReference("System.Web.dll");
            AddReference("System.Web.RegularExpressions.dll");
            AddReference("System.Web.Services.dll");
            AddReference("System.Windows.Forms.Dll");
            AddReference("System.Xml.Linq.dll");
            AddReference("System.XML.dll");
            AddReference("System.Core.dll");

        }

        public string VirtualName
        {
            get;
            set;
        }

        private List<string> mRefAssembly = new List<string>();

        private IList<IAppAdapter> mAdapters = new List<IAppAdapter>();

        public void LoadAssembly(string path)
        {
            if (RunExe == null)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                foreach (FileInfo item in directory.GetFiles("*.dll"))
                {
                    try
                    {

                        string filename = Path.GetFileNameWithoutExtension(item.FullName);

                        Assembly assembly = Assembly.Load(filename);

                        LoadType(assembly.GetTypes());
                        AddReference(item.FullName);
                    }
                    catch (Exception e_)
                    {
                        OnCallLogEvent(ExecutingStatus.Warning, e_, string.Format("domain [{0}] load {1} assembly error.",VirtualName,item.Name));
                    }
                }
                LoadVB(path);
                LoadCS(path);
            }
        }

        private void LoadType(Type[] types)
        {
            foreach (Type type in types)
            {
                if (type.GetInterface("IAppAdapter") != null)
                {
                    mAdapters.Add((IAppAdapter)Activator.CreateInstance(type));
                }
            }
        }

        private void LoadVB(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.vb");
            if (files.Length > 0)
            {
                Assembly assembly = mAssemblyFactory.CreateAssembly(files, mRefAssembly);
                OnCallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] Compiler files:{1}.", VirtualName, string.Join(",", files)));
                LoadType(assembly.GetTypes());
            }
        }

        private void LoadCS(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.cs");
            if (files.Length > 0)
            {
                Assembly assembly = mAssemblyFactory.CreateAssembly(files, mRefAssembly);
                OnCallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] Compiler files:{1}.", VirtualName, string.Join(",", files)));
                LoadType(assembly.GetTypes());
            }
        }

        public Action<ExecutingStatus, Exception, string> CallLogEvent;

        private void OnCallLogEvent(ExecutingStatus status, Exception error, string message)
        {
            if (CallLogEvent != null)
            {
                CallLogEvent(status, error, message);
            }
        }

        public UnhandledExceptionEventHandler UnloadError;

        public void Start()
        {
            foreach (IAppAdapter item in mAdapters)
            {
                try
                {
                    item.Start(mArgs);
                    OnCallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] App:{1} start.", this.VirtualName, item.Name));
                }
                catch (Exception e__)
                {
                    OnCallLogEvent(ExecutingStatus.Error, e__, string.Format("domain [{0}] App:{1} start error.", this.VirtualName, item.Name));
                }
            }
        }

        public void Stop()
        {
            foreach (IAppAdapter item in mAdapters)
            {
                try
                {
                    item.Stop();
                    OnCallLogEvent(ExecutingStatus.Success, null, string.Format("domain [{0}] App:{1} stop.", this.VirtualName, item.Name));
                }
                catch (Exception e__)
                {
                    OnCallLogEvent(ExecutingStatus.Error, e__, string.Format("domain [{0}] App:{1} stop error.", this.VirtualName, item.Name));
                }

            }
        }

        private void OnUnLoaderror(object sender, UnhandledExceptionEventArgs e)
        {


            if (UnloadError != null)
                UnloadError(this, e);
        }

        public string RunExe
        {
            get;
            set;
        }

    }
}
