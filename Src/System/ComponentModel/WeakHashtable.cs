using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005C5 RID: 1477
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x06003735 RID: 14133 RVA: 0x000EF910 File Offset: 0x000EDB10
		internal WeakHashtable()
			: base(WeakHashtable._comparer)
		{
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000EF91D File Offset: 0x000EDB1D
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000EF925 File Offset: 0x000EDB25
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000EF92E File Offset: 0x000EDB2E
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000EF944 File Offset: 0x000EDB44
		private void ScavengeKeys()
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (this._lastHashCount == 0)
			{
				this._lastHashCount = count;
				return;
			}
			long totalMemory = GC.GetTotalMemory(false);
			if (this._lastGlobalMem == 0L)
			{
				this._lastGlobalMem = totalMemory;
				return;
			}
			float num = (float)(totalMemory - this._lastGlobalMem) / (float)this._lastGlobalMem;
			float num2 = (float)(count - this._lastHashCount) / (float)this._lastHashCount;
			if (num < 0f && num2 >= 0f)
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Keys)
				{
					WeakReference weakReference = obj as WeakReference;
					if (weakReference != null && !weakReference.IsAlive)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(weakReference);
					}
				}
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						this.Remove(obj2);
					}
				}
			}
			this._lastGlobalMem = totalMemory;
			this._lastHashCount = count;
		}

		// Token: 0x04002ACC RID: 10956
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x04002ACD RID: 10957
		private long _lastGlobalMem;

		// Token: 0x04002ACE RID: 10958
		private int _lastHashCount;

		// Token: 0x020008A8 RID: 2216
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x060045D3 RID: 17875 RVA: 0x001237B0 File Offset: 0x001219B0
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return x == y;
				}
				return false;
			}

			// Token: 0x060045D4 RID: 17876 RVA: 0x00123814 File Offset: 0x00121A14
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x020008A9 RID: 2217
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x060045D6 RID: 17878 RVA: 0x00123824 File Offset: 0x00121A24
			internal EqualityWeakReference(object o)
				: base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x060045D7 RID: 17879 RVA: 0x00123839 File Offset: 0x00121A39
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x060045D8 RID: 17880 RVA: 0x00123868 File Offset: 0x00121A68
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x040037E0 RID: 14304
			private int _hashCode;
		}
	}
}
