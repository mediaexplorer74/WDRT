using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000280 RID: 640
	public sealed class AtomCategoryMetadata : ODataAnnotatable
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x0004E691 File Offset: 0x0004C891
		public AtomCategoryMetadata()
		{
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0004E699 File Offset: 0x0004C899
		internal AtomCategoryMetadata(AtomCategoryMetadata other)
		{
			if (other == null)
			{
				return;
			}
			this.Term = other.Term;
			this.Scheme = other.Scheme;
			this.Label = other.Label;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0004E6C9 File Offset: 0x0004C8C9
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0004E6D1 File Offset: 0x0004C8D1
		public string Term { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0004E6DA File Offset: 0x0004C8DA
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0004E6E2 File Offset: 0x0004C8E2
		public string Scheme { get; set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0004E6EB File Offset: 0x0004C8EB
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0004E6F3 File Offset: 0x0004C8F3
		public string Label { get; set; }
	}
}
