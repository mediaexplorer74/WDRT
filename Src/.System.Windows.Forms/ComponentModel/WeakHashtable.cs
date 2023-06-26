using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020000F8 RID: 248
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000C331 File Offset: 0x0000A531
		internal WeakHashtable()
			: base(WeakHashtable._comparer)
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C33E File Offset: 0x0000A53E
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C346 File Offset: 0x0000A546
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000C34F File Offset: 0x0000A54F
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000C364 File Offset: 0x0000A564
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

		// Token: 0x04000425 RID: 1061
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x04000426 RID: 1062
		private long _lastGlobalMem;

		// Token: 0x04000427 RID: 1063
		private int _lastHashCount;

		// Token: 0x02000543 RID: 1347
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x0600555A RID: 21850 RVA: 0x00165F7C File Offset: 0x0016417C
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

			// Token: 0x0600555B RID: 21851 RVA: 0x00165FE0 File Offset: 0x001641E0
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x02000544 RID: 1348
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x0600555D RID: 21853 RVA: 0x00165FE8 File Offset: 0x001641E8
			internal EqualityWeakReference(object o)
				: base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x0600555E RID: 21854 RVA: 0x00165FFD File Offset: 0x001641FD
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x0600555F RID: 21855 RVA: 0x0016602C File Offset: 0x0016422C
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x040037FE RID: 14334
			private int _hashCode;
		}
	}
}
