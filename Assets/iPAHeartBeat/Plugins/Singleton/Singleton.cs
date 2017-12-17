/*
 * File: Singleton.cs
 * ==================
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

using System;
//
using UnityEngine;
//
using iPAHeartBeat.Core.Utility;
//
//
namespace iPAHeartBeat.Core.Singleton {
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
		static Type type;
		protected static bool applicationIsQuitting = false;
		protected static object locked = new object();
		static T _me;

		public static T Me {
			get { return Get(); }
			protected internal set { Set(value); }
		}

		static T FindSingleton() {
			T t = null;
			T[] objects = FindObjectsOfType<T>();
			if(objects.Length > 0) {
				t = objects[0];
				if(objects.Length > 1) {
					Debug.LogWarning("There is more than one instance for [Singleton] of type '" + type +
					"'. Keeping the first named " + objects[0].name + ". Destroying the others.");
					for(var i = 1; i < objects.Length; i++) {
						Destroy(objects[i].gameObject);
					}
				}
			}
			return t;
		}
		static void AutoCreateMe() {
			var gameObject = new GameObject();
			gameObject.name = typeof(T).ToString();
			Me = gameObject.AddComponent<T>();
			DontDestroyOnLoad(gameObject);
		}
		static void AutoCreateFromPrefab() {
			var attribute = Attribute.GetCustomAttribute(type, typeof(SingletonPrefab)) as SingletonPrefab;
			if(attribute == null) {
				throw new System.Exception("There is no Prefab Atrribute for Singleton of type '" + type + "'.");
			}
			string prefabName = attribute.name;
			if(String.IsNullOrEmpty(prefabName)) {
				throw new System.Exception("Prefab name is empty for Singleton of type '" + type + "'.");
			}

			var gameObject = Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject;
			if(gameObject == null) {
				throw new System.Exception("Could not find Prefab '" + prefabName + "' on Resources for Singleton of type \"" + type + "\".");
			}

			gameObject.name = prefabName;
			T t1 = gameObject.GetComponent<T>();
			if(null != t1) {
				Debug.LogWarning("There wasn't a component of type '" + type + "' on prefab '" + prefabName + "'. Adding new one.");
				Me = gameObject.AddComponent<T>();
			}
		}

		protected static void Set(T value) {
			if(value == null) {
				if(_me && _me.gameObject) {
					Destroy(_me.gameObject);
				}
				_me = null;
				//_instantiated = false;

			} else {
				_me = value;
				//_instantiated = true;
				var attribute = Attribute.GetCustomAttribute(type, typeof(PersistentSignleton)) as PersistentSignleton;
				if(attribute != null && attribute.persistent) {
					DontDestroyOnLoad(_me.gameObject);
				}
			}
		}
		protected static T Get() {
			return Get(SingletonTypes.Precreated);
		}
		protected static T Get(SingletonTypes sType) {
			if(applicationIsQuitting) {
				MyDebug.Warning("[Singleton] Me of '" + typeof(T) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return null;
			}

			lock(locked) {
				type = typeof(T);
				if(_me == null) {
					T t;
					switch(sType) {
					case SingletonTypes.AutoCraete:
						t = FindSingleton();
						if(null != t) {
							_me = t;
						} else {
							AutoCreateMe();
						}
						break;

					case SingletonTypes.AutoCreateWithPrefab:
						t = FindSingleton();
						if(null != t) {
							_me = t;
						} else {
							AutoCreateFromPrefab();
						}
						break;

					case SingletonTypes.Precreated:
						throw new System.NullReferenceException("[Singleton] " + type.ToString() + " not present in scene");
						break;
					}
				}
			}
			return _me;
		}

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public virtual void OnDestroy() {
			applicationIsQuitting = true;
		}
	}

	public enum SingletonTypes {
		Precreated, AutoCraete, AutoCreateWithPrefab
	}
}