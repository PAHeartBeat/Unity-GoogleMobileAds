using UnityEngine;
public class MyDebug {
	public static void Log(object data) {
		#if ISDEBUG
		Debug.Log("LOG: " + data);
		#endif
	}
	public static void LogError(object data) {
		#if ISDEBUG
		Debug.LogError("LOG-ERROR: " + data);
		#endif
	}
	public static void LogWarning(object data) {
		#if ISDEBUG
		Debug.LogWarning("LOG-WARNING: " + data);
		#endif
	}
}
