using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000E6 RID: 230
	public sealed class AtomCategoriesMetadata
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00013CF0 File Offset: 0x00011EF0
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00013CF8 File Offset: 0x00011EF8
		public bool? Fixed { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00013D01 File Offset: 0x00011F01
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00013D09 File Offset: 0x00011F09
		public string Scheme { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00013D12 File Offset: 0x00011F12
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00013D1A File Offset: 0x00011F1A
		public Uri Href { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00013D23 File Offset: 0x00011F23
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00013D2B File Offset: 0x00011F2B
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }
	}
}
