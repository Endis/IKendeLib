using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Beetle.NetPackage
{
    public class ControllerAction
    {
        private Dictionary<Type, MethodInfo> mMethods = new Dictionary<Type, MethodInfo>();

        public ControllerAction(Type actionType)
        {
            loadAction(actionType);
        }

        private void loadAction(Type actionType)
        {
            foreach (MethodInfo method in actionType.GetMethods(BindingFlags.Instance| BindingFlags.Public))
            {
                ParameterInfo[] pis = method.GetParameters();
                if (pis.Length == 2 && pis[0].ParameterType == typeof(NetClient))
                {
                    mMethods[pis[1].ParameterType] = method;
                }
            }
        }

        public void Invoke(Object action, NetClient client, Object message)
        {
            MethodInfo method = null;
            if (mMethods.TryGetValue(message.GetType(), out method))
            {
                try
                {
                    method.Invoke(action, new object[] {client,message });
                }
                catch (Exception e)
                {

                }
            }

        }
    }
}
