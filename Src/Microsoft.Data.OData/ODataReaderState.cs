using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024B RID: 587
	public enum ODataReaderState
	{
		// Token: 0x040006B9 RID: 1721
		Start,
		// Token: 0x040006BA RID: 1722
		FeedStart,
		// Token: 0x040006BB RID: 1723
		FeedEnd,
		// Token: 0x040006BC RID: 1724
		EntryStart,
		// Token: 0x040006BD RID: 1725
		EntryEnd,
		// Token: 0x040006BE RID: 1726
		NavigationLinkStart,
		// Token: 0x040006BF RID: 1727
		NavigationLinkEnd,
		// Token: 0x040006C0 RID: 1728
		EntityReferenceLink,
		// Token: 0x040006C1 RID: 1729
		Exception,
		// Token: 0x040006C2 RID: 1730
		Completed
	}
}
