using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026C RID: 620
	public sealed class AtomResourceCollectionMetadata
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0004C939 File Offset: 0x0004AB39
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0004C941 File Offset: 0x0004AB41
		public AtomTextConstruct Title { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0004C94A File Offset: 0x0004AB4A
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0004C952 File Offset: 0x0004AB52
		public string Accept { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004C95B File Offset: 0x0004AB5B
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0004C963 File Offset: 0x0004AB63
		public AtomCategoriesMetadata Categories { get; set; }
	}
}
