using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027D RID: 637
	public sealed class AtomFeedMetadata : ODataAnnotatable
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0004E3BA File Offset: 0x0004C5BA
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0004E3C2 File Offset: 0x0004C5C2
		public IEnumerable<AtomPersonMetadata> Authors { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0004E3CB File Offset: 0x0004C5CB
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0004E3D3 File Offset: 0x0004C5D3
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0004E3E4 File Offset: 0x0004C5E4
		public IEnumerable<AtomPersonMetadata> Contributors { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0004E3ED File Offset: 0x0004C5ED
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0004E3F5 File Offset: 0x0004C5F5
		public AtomGeneratorMetadata Generator { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0004E3FE File Offset: 0x0004C5FE
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x0004E406 File Offset: 0x0004C606
		public Uri Icon { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0004E40F File Offset: 0x0004C60F
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0004E417 File Offset: 0x0004C617
		public IEnumerable<AtomLinkMetadata> Links { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004E420 File Offset: 0x0004C620
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0004E428 File Offset: 0x0004C628
		public Uri Logo { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0004E431 File Offset: 0x0004C631
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x0004E439 File Offset: 0x0004C639
		public AtomTextConstruct Rights { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0004E442 File Offset: 0x0004C642
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x0004E44A File Offset: 0x0004C64A
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0004E453 File Offset: 0x0004C653
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x0004E45B File Offset: 0x0004C65B
		public AtomLinkMetadata NextPageLink { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0004E464 File Offset: 0x0004C664
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x0004E46C File Offset: 0x0004C66C
		public string SourceId { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0004E475 File Offset: 0x0004C675
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x0004E47D File Offset: 0x0004C67D
		public AtomTextConstruct Subtitle { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0004E486 File Offset: 0x0004C686
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x0004E48E File Offset: 0x0004C68E
		public AtomTextConstruct Title { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004E497 File Offset: 0x0004C697
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0004E49F File Offset: 0x0004C69F
		public DateTimeOffset? Updated { get; set; }
	}
}
