using System;
//
using UnityEngine;
//
using iPAHeartBeat.Core.Singleton;
//
//
namespace iPAHeartBeat.Core.Utility {

	public class MyDebug : SingletonAuto<MyDebug> {
		string _logDataOnScreen = string.Empty;
		string _logDataOnFile = string.Empty;
#if MYDEBUG
		GUIStyle _style;
		Rect _startRect;
		bool _allowDrag, _showLogOnScreen;
		Vector2 _scrollPosition;
		int _guiX, _guiY, _guiwidth, _guiheight, _padding, _hbuttons, _vbuttons;
		string _buttonText = "";
#endif
		public bool isLogDataOnScreen = true;
		public bool isLogDataOnFile = false;
		public bool isLogDataOnConsole = true;
		void Awake() {
#if MYDEBUG
			_guiX = _guiY = _padding = 8;

			_hbuttons = UnityEngine.Camera.main.aspect > 1f ? 5 : 2;
			_vbuttons = UnityEngine.Camera.main.aspect > 1f ? 10 : 15;

			_guiwidth = (Screen.width - ((_hbuttons + 2) * _padding)) / _hbuttons;
			_guiheight = (Screen.height - ((_vbuttons + 2) * _padding)) / _vbuttons;

			_startRect = new Rect(_guiX, _guiheight + (_padding * 2), Screen.width - (_padding * 2), (Screen.height / 2) - _guiheight - (_padding * 2));
#endif
		}
#if MYDEBUG1
		void OnGUI() {
			if(_style == null) {
				_style = new GUIStyle(GUI.skin.textField);
				_style.richText = true;
				_style.alignment = TextAnchor.UpperLeft;
			}

			_guiX = _padding;
			_guiY = _padding;

			_buttonText = _showLogOnScreen ? "Hide Log" : "Show Log";
			if(GUI.Button(new Rect(_guiX, _guiY, _guiwidth, _guiheight), _buttonText)) {
				_showLogOnScreen = !_showLogOnScreen;
			}
			if(_showLogOnScreen) {
				_startRect = GUI.Window(0, _startRect, DoMyWindow, "");
			}
		}

		void DoMyWindow(int windowID) {
			GUILayout.BeginArea(new Rect(_padding, _padding, _startRect.width - (_padding * 2), _startRect.height - (_padding * 2)));
			_scrollPosition = GUILayout.BeginScrollView(_scrollPosition,
				GUILayout.Width(_startRect.width - (_padding * 2)), GUILayout.Height(_startRect.height - (_padding * 2)));
			GUILayout.Label(_logDataOnScreen, _style);
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			if(_allowDrag) {
				GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
			}
		}
#endif

		public static void Log(object data) {
			string s = string.Format("l: @ {0}: {1}", DateTime.Now.ToLongTimeString(), data);
			LogOnScreenOrFile(s, "#ffffffff");
#if MYDEBUG
			if(Me.isLogDataOnConsole) Debug.Log(s);
#endif
		}

		public static void Info(object data) {
			string s = string.Format("i: @ {0}: {1}", DateTime.Now.ToLongTimeString(), data);
			LogOnScreenOrFile(s, "#539641ff");
#if MYDEBUG
			if(Me.isLogDataOnConsole) Debug.Log(s);
#endif
		}
		public static void Warning(object data) {
			string s = string.Format("w: @ {0}: {1}", DateTime.Now.ToLongTimeString(), data);
			LogOnScreenOrFile(s, "#ffa500ff");
#if MYDEBUG
			if(Me.isLogDataOnConsole) Debug.LogWarning(s);
#endif
		}
		public static void Error(object data) {
			string s = string.Format("e: @ {0}: {1}", DateTime.Now.ToLongTimeString(), data);
			LogOnScreenOrFile(s, "#ff0000ff");
#if MYDEBUG
			if(Me.isLogDataOnConsole) Debug.LogError(s);
#endif
		}

		static void LogOnScreenOrFile(string data, string hexColor) {
			if(Me.isLogDataOnScreen) Me._logDataOnScreen += string.Format("<color={0}>{1}</color>\n", hexColor, data);
			if(Me.isLogDataOnFile) Me._logDataOnFile += string.Format("{0}\n", data);
		}
	}
}