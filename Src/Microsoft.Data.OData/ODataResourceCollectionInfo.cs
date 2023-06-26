using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000278 RID: 632
	public sealed class ODataResourceCollectionInfo : ODataAnnotatable
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0004E285 File Offset: 0x0004C485
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x0004E28D File Offset: 0x0004C48D
		public Uri Url { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0004E296 File Offset: 0x0004C496
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x0004E29E File Offset: 0x0004C49E
		public string Name { get; set; }
	}
}
