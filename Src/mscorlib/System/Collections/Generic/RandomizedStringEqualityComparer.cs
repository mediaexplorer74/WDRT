using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C7 RID: 1223
	internal sealed class RandomizedStringEqualityComparer : IEqualityComparer<string>, IEqualityComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000E0EC4 File Offset: 0x000DF0C4
		public RandomizedStringEqualityComparer()
		{
			this._entropy = HashHelpers.GetEntropy();
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000E0ED7 File Offset: 0x000DF0D7
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is string && y is string)
			{
				return this.Equals((string)x, (string)y);
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000E0F11 File Offset: 0x000DF111
		public bool Equals(string x, string y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000E0F29 File Offset: 0x000DF129
		[SecuritySafeCritical]
		public int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return string.InternalMarvin32HashString(obj, obj.Length, this._entropy);
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000E0F44 File Offset: 0x000DF144
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

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000E0F7C File Offset: 0x000DF17C
		public override bool Equals(object obj)
		{
			RandomizedStringEqualityComparer randomizedStringEqualityComparer = obj as RandomizedStringEqualityComparer;
			return randomizedStringEqualityComparer != null && this._entropy == randomizedStringEqualityComparer._entropy;
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000E0FA3 File Offset: 0x000DF1A3
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode() ^ (int)(this._entropy & 2147483647L);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000E0FC4 File Offset: 0x000DF1C4
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new RandomizedStringEqualityComparer();
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000E0FCB File Offset: 0x000DF1CB
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return EqualityComparer<string>.Default;
		}

		// Token: 0x04001959 RID: 6489
		private long _entropy;
	}
}
