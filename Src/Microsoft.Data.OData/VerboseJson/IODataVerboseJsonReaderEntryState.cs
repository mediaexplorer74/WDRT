using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x0200020D RID: 525
	internal interface IODataVerboseJsonReaderEntryState
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600100E RID: 4110
		ODataEntry Entry { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600100F RID: 4111
		IEdmEntityType EntityType { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001010 RID: 4112
		// (set) Token: 0x06001011 RID: 4113
		bool MetadataPropertyFound { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001012 RID: 4114
		// (set) Token: 0x06001013 RID: 4115
		ODataNavigationLink FirstNavigationLink { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001014 RID: 4116
		// (set) Token: 0x06001015 RID: 4117
		IEdmNavigationProperty FirstNavigationProperty { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001016 RID: 4118
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }
	}
}
