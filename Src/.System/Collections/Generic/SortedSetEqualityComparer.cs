using System;

namespace System.Collections.Generic
{
	// Token: 0x020003CC RID: 972
	internal class SortedSetEqualityComparer<T> : IEqualityComparer<SortedSet<T>>
	{
		// Token: 0x06002528 RID: 9512 RVA: 0x000AD4BF File Offset: 0x000AB6BF
		public SortedSetEqualityComparer()
			: this(null, null)
		{
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000AD4C9 File Offset: 0x000AB6C9
		public SortedSetEqualityComparer(IComparer<T> comparer)
			: this(comparer, null)
		{
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000AD4D3 File Offset: 0x000AB6D3
		public SortedSetEqualityComparer(IEqualityComparer<T> memberEqualityComparer)
			: this(null, memberEqualityComparer)
		{
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000AD4DD File Offset: 0x000AB6DD
		public SortedSetEqualityComparer(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
		{
			if (comparer == null)
			{
				this.comparer = Comparer<T>.Default;
			}
			else
			{
				this.comparer = comparer;
			}
			if (memberEqualityComparer == null)
			{
				this.e_comparer = EqualityComparer<T>.Default;
				return;
			}
			this.e_comparer = memberEqualityComparer;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000AD512 File Offset: 0x000AB712
		public bool Equals(SortedSet<T> x, SortedSet<T> y)
		{
			return SortedSet<T>.SortedSetEquals(x, y, this.comparer);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000AD524 File Offset: 0x000AB724
		public int GetHashCode(SortedSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T t in obj)
				{
					num ^= this.e_comparer.GetHashCode(t) & int.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000AD588 File Offset: 0x000AB788
		public override bool Equals(object obj)
		{
			SortedSetEqualityComparer<T> sortedSetEqualityComparer = obj as SortedSetEqualityComparer<T>;
			return sortedSetEqualityComparer != null && this.comparer == sortedSetEqualityComparer.comparer;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000AD5AF File Offset: 0x000AB7AF
		public override int GetHashCode()
		{
			return this.comparer.GetHashCode() ^ this.e_comparer.GetHashCode();
		}

		// Token: 0x04002031 RID: 8241
		private IComparer<T> comparer;

		// Token: 0x04002032 RID: 8242
		private IEqualityComparer<T> e_comparer;
	}
}
