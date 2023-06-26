using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BC RID: 1212
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x06003A4F RID: 14927 RVA: 0x000DF67E File Offset: 0x000DD87E
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000DF68D File Offset: 0x000DD88D
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04001949 RID: 6473
		private readonly Comparison<T> _comparison;
	}
}
