using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027E RID: 638
	public sealed class AtomEntryMetadata : ODataAnnotatable
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x0004E4B8 File Offset: 0x0004C6B8
		public IEnumerable<AtomPersonMetadata> Authors { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0004E4C1 File Offset: 0x0004C6C1
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x0004E4C9 File Offset: 0x0004C6C9
		public AtomCategoryMetadata CategoryWithTypeName { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0004E4D2 File Offset: 0x0004C6D2
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x0004E4DA File Offset: 0x0004C6DA
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0004E4E3 File Offset: 0x0004C6E3
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x0004E4EB File Offset: 0x0004C6EB
		public IEnumerable<AtomPersonMetadata> Contributors { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x0004E4FC File Offset: 0x0004C6FC
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0004E505 File Offset: 0x0004C705
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x0004E50D File Offset: 0x0004C70D
		public AtomLinkMetadata EditLink { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0004E516 File Offset: 0x0004C716
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x0004E51E File Offset: 0x0004C71E
		public IEnumerable<AtomLinkMetadata> Links { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0004E527 File Offset: 0x0004C727
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x0004E52F File Offset: 0x0004C72F
		public DateTimeOffset? Published { get; set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0004E538 File Offset: 0x0004C738
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x0004E540 File Offset: 0x0004C740
		public AtomTextConstruct Rights { get; set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0004E549 File Offset: 0x0004C749
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x0004E551 File Offset: 0x0004C751
		public AtomFeedMetadata Source { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0004E55A File Offset: 0x0004C75A
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x0004E562 File Offset: 0x0004C762
		public AtomTextConstruct Summary { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0004E56B File Offset: 0x0004C76B
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x0004E573 File Offset: 0x0004C773
		public AtomTextConstruct Title { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0004E57C File Offset: 0x0004C77C
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x0004E584 File Offset: 0x0004C784
		public DateTimeOffset? Updated { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0004E58D File Offset: 0x0004C78D
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x0004E595 File Offset: 0x0004C795
		internal string PublishedString
		{
			get
			{
				return this.publishedString;
			}
			set
			{
				this.publishedString = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0004E59E File Offset: 0x0004C79E
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x0004E5A6 File Offset: 0x0004C7A6
		internal string UpdatedString
		{
			get
			{
				return this.updatedString;
			}
			set
			{
				this.updatedString = value;
			}
		}

		// Token: 0x040007AD RID: 1965
		private string publishedString;

		// Token: 0x040007AE RID: 1966
		private string updatedString;
	}
}
