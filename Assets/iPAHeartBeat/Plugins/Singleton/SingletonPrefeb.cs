/*
 * File: SingletonPrefeb.cs
 * =========================
 * 
 * Version				: 1.0
 * Author				: Kleber Lopes da Silva
 * E-Mail				: Not Available
 * Copyright			: Not Available
 * Company				: Not Available
 * Script Location		: Plugins/INICPlugins/Other
 * 
 * 
 * Last Modified by		: Ankur Ranpariya on 2014.11.26
 * Contributors 		:
 * Curtosey By			: Kleber Lopes da Silva (http://kleber-swf.com/singleton-monobehaviour-unity-projects/)
 * 
 * Purpose
 * ====================================================================================================================
 * 
 * 
 * 
 * Change Log
 * ====================================================================================================================
 * v1.0
 * ====
 * 1. Initial version
 * 
 * v1.1
 * ====
 * 1.
 * 2.
 * ====================================================================================================================
*/
using UnityEngine;

namespace iPAHeartBeat.Core.Singleton {
	public abstract class SingletonPrefeb<T> : Singleton<T> where T : MonoBehaviour {
		public new static T Me {
			get { return Get(SingletonTypes.AutoCreateWithPrefab); }
			set { Set(value); }
		}
	}


}