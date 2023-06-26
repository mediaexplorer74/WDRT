using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027F RID: 639
	public sealed class AtomLinkMetadata : ODataAnnotatable
	{
		// Token: 0x0600153C RID: 5436 RVA: 0x0004E5B7 File Offset: 0x0004C7B7
		public AtomLinkMetadata()
		{
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0004E5C0 File Offset: 0x0004C7C0
		internal AtomLinkMetadata(AtomLinkMetadata other)
		{
			if (other == null)
			{
				return;
			}
			this.Relation = other.Relation;
			this.Href = other.Href;
			this.HrefLang = other.HrefLang;
			this.Title = other.Title;
			this.MediaType = other.MediaType;
			this.Length = other.Length;
			this.hrefFromEpm = other.hrefFromEpm;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0004E62B File Offset: 0x0004C82B
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0004E633 File Offset: 0x0004C833
		public Uri Href { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0004E63C File Offset: 0x0004C83C
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x0004E644 File Offset: 0x0004C844
		public string Relation { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0004E64D File Offset: 0x0004C84D
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x0004E655 File Offset: 0x0004C855
		public string MediaType { get; set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0004E65E File Offset: 0x0004C85E
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0004E666 File Offset: 0x0004C866
		public string HrefLang { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0004E66F File Offset: 0x0004C86F
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0004E677 File Offset: 0x0004C877
		public string Title { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0004E680 File Offset: 0x0004C880
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0004E688 File Offset: 0x0004C888
		public int? Length { get; set; }

		// Token: 0x040007BC RID: 1980
		private string hrefFromEpm;
	}
}
