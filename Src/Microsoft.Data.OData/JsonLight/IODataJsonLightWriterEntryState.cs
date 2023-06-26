using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000168 RID: 360
	internal interface IODataJsonLightWriterEntryState
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A05 RID: 2565
		ODataEntry Entry { get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A06 RID: 2566
		IEdmEntityType EntityType { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A07 RID: 2567
		IEdmEntityType EntityTypeFromMetadata { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A08 RID: 2568
		ODataFeedAndEntrySerializationInfo SerializationInfo { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A09 RID: 2569
		// (set) Token: 0x06000A0A RID: 2570
		bool EditLinkWritten { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A0B RID: 2571
		// (set) Token: 0x06000A0C RID: 2572
		bool ReadLinkWritten { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A0D RID: 2573
		// (set) Token: 0x06000A0E RID: 2574
		bool MediaEditLinkWritten { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A0F RID: 2575
		// (set) Token: 0x06000A10 RID: 2576
		bool MediaReadLinkWritten { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A11 RID: 2577
		// (set) Token: 0x06000A12 RID: 2578
		bool MediaContentTypeWritten { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A13 RID: 2579
		// (set) Token: 0x06000A14 RID: 2580
		bool MediaETagWritten { get; set; }

		// Token: 0x06000A15 RID: 2581
		ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse);
	}
}
