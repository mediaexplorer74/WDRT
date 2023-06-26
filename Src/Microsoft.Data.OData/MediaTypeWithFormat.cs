using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B4 RID: 436
	internal sealed class MediaTypeWithFormat
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0002F53E File Offset: 0x0002D73E
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0002F546 File Offset: 0x0002D746
		public MediaType MediaType { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0002F54F File Offset: 0x0002D74F
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0002F557 File Offset: 0x0002D757
		public ODataFormat Format { get; set; }
	}
}
