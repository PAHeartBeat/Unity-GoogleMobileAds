/*
 * File: SingletonAuto.cs
 * ======================
 * 
 * Version				: 1.1
 * Author				: Kleber Lopes da Silva (@kleber_swf / www.linkedin.com/in/kleberswf)
 * E-Mail				: Not Available
 * Copyright			: Not Available
 * Company				: Not Available
 * Script Location		: Plugins/INICPlugins/Other
 * 
 * 
 * Created By			: Kleber Lopes da Silva
 * Created Date			: 2014.11.24
 * 
 * Last Modified by		: Ankur Ranpariya on 2014.11.28 (ankur30884@gmail.com / @PA_HeartBeat)
 * Last Modified		: 2014.11.28
 * 
 * Contributors 		:
 * Curtosey By			: Kleber Lopes da Silva (http://kleber-swf.com/singleton-monobehaviour-unity-projects/)
 * 
 * Purpose
 * ====================================================================================================================
 * 
 * 
 * 
 * 
 * ====================================================================================================================
 * LICENCE / WARRENTY
 * ==================
 * The MIT License (MIT) 
 * 
 * Copyright (c) 2014 Kleber Silva (kleber.swf@gmail.com)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell  copies of the Software, and to permit
 * persons to whom the Software is furnished to do so, subject to the following conditions: 
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of
 * the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
 * 
 * Change Log
 * ====================================================================================================================
 * v1.0
 * ====
 * 1. Initial version by "Kleber Lopes da Silva"
 * 
 * v1.1
 * ====
 * 1.	Change DestroyImmediate to Destroy for removing Extra copies of singleton instance, DestroyImmediate wiil remove
 * 		copy imeediately and effect in same frame. It should cause null excption error if destroyed copy is referanced.
 * 		whether Destroy will remove instance end of the frame so it will not cause Null exception in same frame, and next
 * 		frame can identify used referance are removed or not.
 * ====================================================================================================================
*/


using UnityEngine;

namespace iPAHeartBeat.Core.Singleton {
	/// <summary>
	/// <para version="1.1.0.0" />	 
	/// <para author="Kleber Lopes da Silva, Ranpariya Ankur" />
	/// <para support="" />
	/// <para>
	/// Description: 
	/// </para>
	/// </summary>
	public abstract class SingletonAuto<T> : Singleton<T> where T : MonoBehaviour {
		public new static T Me {
			get { return Get(SingletonTypes.AutoCraete); }
			set { Set(value); }
		}
	}
}