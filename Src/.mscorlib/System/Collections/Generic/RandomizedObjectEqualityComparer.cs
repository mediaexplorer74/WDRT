using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C8 RID: 1224
	internal sealed class RandomizedObjectEqualityComparer : IEqualityComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06003ACA RID: 15050 RVA: 0x000E0FD2 File Offset: 0x000DF1D2
		public RandomizedObjectEqualityComparer()
		{
			this._entropy = HashHelpers.GetEntropy();
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000E0FE5 File Offset: 0x000DF1E5
		public bool Equals(object x, object y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000E1000 File Offset: 0x000DF200
		[SecuritySafeCritical]
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			string text = obj as string;
			if (text != null)
			{
				return string.InternalMarvin32HashString(text, text.Length, this._entropy);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000E1038 File Offset: 0x000DF238
		public override bool Equals(object obj)
		{
			RandomizedObjectEqualityComparer randomizedObjectEqualityComparer = obj as RandomizedObjectEqualityComparer;
			return randomizedObjectEqualityComparer != null && this._entropy == randomizedObjectEqualityComparer._entropy;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000E105F File Offset: 0x000DF25F
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode() ^ (int)(this._entropy & 2147483647L);
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000E1080 File Offset: 0x000DF280
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new RandomizedObjectEqualityComparer();
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000E1087 File Offset: 0x000DF287
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return null;
		}

		// Token: 0x0400195A RID: 6490
		private long _entropy;
	}
}
