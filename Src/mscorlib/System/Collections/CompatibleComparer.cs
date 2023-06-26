using System;

namespace System.Collections
{
	// Token: 0x02000492 RID: 1170
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x0600384F RID: 14415 RVA: 0x000D953C File Offset: 0x000D773C
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000D9554 File Offset: 0x000D7754
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000D95A8 File Offset: 0x000D77A8
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000D95B5 File Offset: 0x000D77B5
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000D95E0 File Offset: 0x000D77E0
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000D95E8 File Offset: 0x000D77E8
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x040018E3 RID: 6371
		private IComparer _comparer;

		// Token: 0x040018E4 RID: 6372
		private IHashCodeProvider _hcp;
	}
}
