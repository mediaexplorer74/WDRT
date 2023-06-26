using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000051 RID: 81
	public sealed class ReadingFeedArgs
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public ReadingFeedArgs(ODataFeed feed)
		{
			Util.CheckArgumentNull<ODataFeed>(feed, "feed");
			this.Feed = feed;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000CED3 File Offset: 0x0000B0D3
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000CEDB File Offset: 0x0000B0DB
		public ODataFeed Feed { get; private set; }
	}
}
