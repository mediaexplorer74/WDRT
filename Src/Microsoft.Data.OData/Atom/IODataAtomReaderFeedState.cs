using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000219 RID: 537
	internal interface IODataAtomReaderFeedState
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060010A5 RID: 4261
		ODataFeed Feed { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060010A6 RID: 4262
		// (set) Token: 0x060010A7 RID: 4263
		bool FeedElementEmpty { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060010A8 RID: 4264
		AtomFeedMetadata AtomFeedMetadata { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060010A9 RID: 4265
		// (set) Token: 0x060010AA RID: 4266
		bool HasCount { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060010AB RID: 4267
		// (set) Token: 0x060010AC RID: 4268
		bool HasNextPageLink { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060010AD RID: 4269
		// (set) Token: 0x060010AE RID: 4270
		bool HasReadLink { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060010AF RID: 4271
		// (set) Token: 0x060010B0 RID: 4272
		bool HasDeltaLink { get; set; }
	}
}
