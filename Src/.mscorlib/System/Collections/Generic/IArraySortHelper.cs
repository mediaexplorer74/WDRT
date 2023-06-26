using System;

namespace System.Collections.Generic
{
	// Token: 0x020004DB RID: 1243
	internal interface IArraySortHelper<TKey>
	{
		// Token: 0x06003B4C RID: 15180
		void Sort(TKey[] keys, int index, int length, IComparer<TKey> comparer);

		// Token: 0x06003B4D RID: 15181
		int BinarySearch(TKey[] keys, int index, int length, TKey value, IComparer<TKey> comparer);
	}
}
