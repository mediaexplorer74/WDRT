using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043A RID: 1082
	internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
	{
		// Token: 0x060035F7 RID: 13815
		public abstract int Compare(ItemType other);

		// Token: 0x060035F8 RID: 13816
		public abstract int Compare(KeyType key);
	}
}
