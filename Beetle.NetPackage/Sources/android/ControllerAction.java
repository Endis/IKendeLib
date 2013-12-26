package beetle.netpackage;

import java.lang.reflect.Method;
import java.util.HashMap;

public class ControllerAction {

	private HashMap<Class<?>, Method> mMethods = new HashMap<Class<?>, Method>();

	public ControllerAction(Class<?> actionType) {
		loadAction(actionType);
	}

	private void loadAction(Class<?> actionType) {
		for (Method method : actionType.getMethods()) {
			Class<?>[] pts = method.getParameterTypes();
			if (pts.length == 2 && pts[0].equals(NetClient.class)) {
				mMethods.put(pts[1], method);
			}
		}
	}

	public void Invoke(Object action, NetClient client, Object message) {
		Method method = mMethods.get(message.getClass());
		if (method != null) {
			try {
				method.invoke(action, client, message);
			} catch (Exception e) {

			}
		}
	}
}
