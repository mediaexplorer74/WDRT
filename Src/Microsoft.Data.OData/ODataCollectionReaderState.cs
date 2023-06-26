using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023D RID: 573
	public enum ODataCollectionReaderState
	{
		// Token: 0x0400069E RID: 1694
		Start,
		// Token: 0x0400069F RID: 1695
		CollectionStart,
		// Token: 0x040006A0 RID: 1696
		Value,
		// Token: 0x040006A1 RID: 1697
		CollectionEnd,
		// Token: 0x040006A2 RID: 1698
		Exception,
		// Token: 0x040006A3 RID: 1699
		Completed
	}
}
