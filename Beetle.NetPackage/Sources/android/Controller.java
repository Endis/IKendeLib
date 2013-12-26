package beetle.netpackage;

import java.util.HashMap;

public class Controller {

	private static HashMap<Class<?>, ControllerAction> mActions = new HashMap<Class<?>, ControllerAction>();

	public static void Invoke(Object actionobj, NetClient client, Object message) {
		synchronized (mActions) {

			Class<?> key = actionobj.getClass();
			ControllerAction action = mActions.get(key);
			if (action == null) {
				action = new ControllerAction(key);
				mActions.put(key, action);
			}
			action.Invoke(actionobj, client, message);
		}
	}
}
