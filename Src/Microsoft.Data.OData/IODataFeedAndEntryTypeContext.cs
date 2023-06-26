using System;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x0200010B RID: 267
	internal interface IODataFeedAndEntryTypeContext
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600073C RID: 1852
		string EntitySetName { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600073D RID: 1853
		string EntitySetElementTypeName { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600073E RID: 1854
		string ExpectedEntityTypeName { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600073F RID: 1855
		bool IsMediaLinkEntry { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000740 RID: 1856
		UrlConvention UrlConvention { get; }
	}
}
