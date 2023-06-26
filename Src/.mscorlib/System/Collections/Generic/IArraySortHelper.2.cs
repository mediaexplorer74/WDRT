using System;

namespace System.Collections.Generic
{
	// Token: 0x020004DF RID: 1247
	internal interface IArraySortHelper<TKey, TValue>
	{
		// Token: 0x06003B6C RID: 15212
		void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer);
	}
}
