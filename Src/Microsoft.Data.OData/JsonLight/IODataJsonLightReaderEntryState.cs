using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000181 RID: 385
	internal interface IODataJsonLightReaderEntryState
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000ACF RID: 2767
		ODataEntry Entry { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AD0 RID: 2768
		IEdmEntityType EntityType { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AD1 RID: 2769
		// (set) Token: 0x06000AD2 RID: 2770
		ODataEntityMetadataBuilder MetadataBuilder { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AD3 RID: 2771
		// (set) Token: 0x06000AD4 RID: 2772
		bool AnyPropertyFound { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AD5 RID: 2773
		// (set) Token: 0x06000AD6 RID: 2774
		ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AD7 RID: 2775
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AD8 RID: 2776
		SelectedPropertiesNode SelectedProperties { get; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AD9 RID: 2777
		List<string> NavigationPropertiesRead { get; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000ADA RID: 2778
		// (set) Token: 0x06000ADB RID: 2779
		bool ProcessingMissingProjectedNavigationLinks { get; set; }
	}
}
