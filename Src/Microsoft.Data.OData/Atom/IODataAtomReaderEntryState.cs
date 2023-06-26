using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000218 RID: 536
	internal interface IODataAtomReaderEntryState
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600108B RID: 4235
		ODataEntry Entry { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600108C RID: 4236
		IEdmEntityType EntityType { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600108D RID: 4237
		// (set) Token: 0x0600108E RID: 4238
		bool EntryElementEmpty { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600108F RID: 4239
		// (set) Token: 0x06001090 RID: 4240
		bool HasReadLink { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001091 RID: 4241
		// (set) Token: 0x06001092 RID: 4242
		bool HasEditLink { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001093 RID: 4243
		// (set) Token: 0x06001094 RID: 4244
		bool HasEditMediaLink { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001095 RID: 4245
		// (set) Token: 0x06001096 RID: 4246
		bool HasId { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001097 RID: 4247
		// (set) Token: 0x06001098 RID: 4248
		bool HasContent { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001099 RID: 4249
		// (set) Token: 0x0600109A RID: 4250
		bool HasTypeNameCategory { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600109B RID: 4251
		// (set) Token: 0x0600109C RID: 4252
		bool HasProperties { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600109D RID: 4253
		// (set) Token: 0x0600109E RID: 4254
		bool? MediaLinkEntry { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600109F RID: 4255
		// (set) Token: 0x060010A0 RID: 4256
		ODataAtomReaderNavigationLinkDescriptor FirstNavigationLinkDescriptor { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060010A1 RID: 4257
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060010A2 RID: 4258
		ODataEntityPropertyMappingCache CachedEpm { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060010A3 RID: 4259
		AtomEntryMetadata AtomEntryMetadata { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060010A4 RID: 4260
		EpmCustomReaderValueCache EpmCustomReaderValueCache { get; }
	}
}
