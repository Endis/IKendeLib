using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.NetPackage
{
    public class Controller
    {
        private static Dictionary<Type, ControllerAction> mActions = new Dictionary<Type, ControllerAction>();

        public static void Invoke(Object actionobj, NetClient client, Object message)
        {
            lock (mActions)
            {

                Type key = actionobj.GetType();
                ControllerAction action = null;
                if (!mActions.TryGetValue(key, out action))
                {
                    action = new ControllerAction(key);
                    mActions[key] = action;
                }
                action.Invoke(actionobj, client, message);
            }
        }
    }
}
