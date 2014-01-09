using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Smark.Core;
namespace Beetle.Controllers
{
    public class FilterContext : IDisposable
    {
        static FilterContext()
        {
            for (int i = 0; i < 128; i++)
            {
                mContexts.Push(new FilterContext());
            }
        }

        private FilterContext()
        {
        }

        public static FilterContext GetContext()
        {
            lock (mContexts)
            {
                if (mContexts.Count > 0)
                    return mContexts.Pop();
                return new FilterContext();
            }
        }

        private static Stack<FilterContext> mContexts = new Stack<FilterContext>(128);

        private IList<FilterAttribute> Filters = null;

        private MethodInvoke InvokeInfo = null;

        private int mFilterIndex = -1;

        public object Result { get; set; }

        public object Message { get; set; }

        public IChannel Channel { get; set; }

        internal void Init(IChannel channel, object message, MethodInvoke info)
        {
            mFilterIndex = -1;
            Channel = channel;
            Message = message;
            Filters = info.Filters;
            InvokeInfo = info;
        }

        public void Execute()
        {

            if (Filters != null && Filters.Count > 0)
            {
                mFilterIndex++;
                if (mFilterIndex >= Filters.Count)
                {
                    Result = InvokeInfo.Execute(Channel, Message);
                }
                else
                {
                    Filters[mFilterIndex].Execute(this);
                }


            }
            else
            {
                Result = InvokeInfo.Execute(Channel, Message);
            }
        }

        public void Dispose()
        {
            lock (mContexts)
            {
                mContexts.Push(this);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public abstract class FilterAttribute : Attribute
    {
        public abstract void Execute(FilterContext context);
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SkipFilterAttribute : Attribute
    {
        public SkipFilterAttribute(params Type[] types)
        {
            Types = types;
        }
        public Type[] Types
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class UseThreadPool : Attribute
    {

    }

    class MethodInvoke
    {
        public object Target;

        public bool UseThreadPool = false;

        public IList<FilterAttribute> Filters = new List<FilterAttribute>();

        public MethodHandler Handler;

        public object Execute(IChannel channel, object message)
        {
            return Handler.Execute(Target, new object[] { channel, message });
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Controller : Attribute
    {

        public delegate void EventControllerError(ControllerErrorArgs e);

        public static EventControllerError ControllerError
        {
            get;
            set;
        }

        public class ControllerErrorArgs
        {
            public IChannel Channel { get; set; }

            public object Message { get; set; }

            public Exception Error { get; set; }

        }

        private static Dictionary<Type, MethodInvoke> mInvokeTable = new Dictionary<Type, MethodInvoke>(256);

        private static MethodInvoke GetInvokeInfo(Type type)
        {
            MethodInvoke result = null;
            mInvokeTable.TryGetValue(type, out result);
            return result;
        }

        public static void Register(Assembly assembly)
        {
            foreach (Type item in assembly.GetTypes())
            {
                Register(item);
            }
        }

        public static void Register(Type type)
        {
            Controller[] controller = Functions.GetTypeAttributes<Controller>(type, false);
            if (controller.Length > 0 && !type.IsAbstract)
            {
                MakeInvoke(type, Activator.CreateInstance(type), Functions.GetTypeAttributes<FilterAttribute>(type, false));
            }
        }

        public static void Register(object handler)
        {
            Type type = handler.GetType();
            MakeInvoke(type, handler, Functions.GetTypeAttributes<FilterAttribute>(type, false));
        }

        private static List<Type> GetSkipFilter(MethodInfo method)
        {
            List<Type> result = new List<Type>(8);
            foreach (SkipFilterAttribute skip in Functions.GetMethodAttributes<SkipFilterAttribute>(method, false))
            {
                if (skip.Types != null)
                {
                    foreach (Type item in skip.Types)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        private static void MakeInvoke(Type type, object target, FilterAttribute[] filters)
        {

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                ParameterInfo[] pis = method.GetParameters();
                if (pis.Length == 2 && pis[0].ParameterType == typeof(IChannel))
                {
                    List<Type> types = GetSkipFilter(method);
                    MethodInvoke info = new MethodInvoke();
                    info.Handler = new MethodHandler(method);
                    info.Target = target;
                    info.UseThreadPool = Functions.GetMethodAttributes<UseThreadPool>(method, false).Length > 0;
                    foreach (FilterAttribute fa in filters)
                    {
                        if (!types.Contains(fa.GetType()))
                            info.Filters.Add(fa);
                    }
                    foreach (FilterAttribute fa in Functions.GetMethodAttributes<FilterAttribute>(method, false))
                    {
                        if (!types.Contains(fa.GetType()))
                            info.Filters.Add(fa);
                    }
                    mInvokeTable[pis[1].ParameterType] = info;
                }
            }


        }

        public static bool Invoke(IChannel channel, object msg)
        {
            MethodInvoke ii = GetInvokeInfo(msg.GetType());
            if (ii != null)
            {
                if (ii.UseThreadPool)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(OnInvoke, new object[] { channel, msg, ii });
                }
                else
                {
                    OnInvoke(new object[] { channel, msg, ii });
                }
                return true;
            }
            return false;

        }

        private static void OnInvoke(object state)
        {
            IChannel channel = null;
            object[] datas = (object[])state;
            try
            {

                channel = (IChannel)datas[0];
                MethodInvoke ii = (MethodInvoke)datas[2];
                using (FilterContext fc = FilterContext.GetContext())
                {
                    fc.Init(channel, datas[1], ii);
                    fc.Execute();
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (ControllerError != null)
                        ControllerError(new ControllerErrorArgs { Channel = channel, Error = e, Message = datas[1] });

                }
                catch
                {
                }
            }

        }

    }
}
