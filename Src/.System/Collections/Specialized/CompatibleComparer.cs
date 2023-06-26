using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020003AF RID: 943
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06002342 RID: 9026 RVA: 0x000A73E1 File Offset: 0x000A55E1
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000A73F8 File Offset: 0x000A55F8
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000A7468 File Offset: 0x000A5668
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

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000A7493 File Offset: 0x000A5693
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000A749B File Offset: 0x000A569B
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000A74A3 File Offset: 0x000A56A3
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000A74C6 File Offset: 0x000A56C6
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x04001FBE RID: 8126
		private IComparer _comparer;

		// Token: 0x04001FBF RID: 8127
		private static volatile IComparer defaultComparer;

		// Token: 0x04001FC0 RID: 8128
		private IHashCodeProvider _hcp;

		// Token: 0x04001FC1 RID: 8129
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}
