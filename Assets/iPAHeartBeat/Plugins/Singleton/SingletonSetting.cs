using System;

namespace iPAHeartBeat.Core.Singleton {
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class PersistentSignleton : Attribute {
		public readonly bool persistent;
		public PersistentSignleton() {
			this.persistent = false;
		}
		public PersistentSignleton(bool isPersistent) {
			this.persistent = isPersistent;
		}
	}
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class SingletonPrefab : PersistentSignleton {
		public readonly string name;
		public SingletonPrefab(string name) : base(true) {
			this.name = name;
		}
		public SingletonPrefab(string name, bool isPersistant) : base(isPersistant) {
			this.name = name;
		}
	}
}