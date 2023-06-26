using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000101 RID: 257
	internal interface IODataEntryMetadataContext
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006DC RID: 1756
		ODataEntry Entry { get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006DD RID: 1757
		IODataFeedAndEntryTypeContext TypeContext { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006DE RID: 1758
		string ActualEntityTypeName { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006DF RID: 1759
		ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006E0 RID: 1760
		IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006E1 RID: 1761
		IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006E2 RID: 1762
		IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006E3 RID: 1763
		IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations { get; }
	}
}
