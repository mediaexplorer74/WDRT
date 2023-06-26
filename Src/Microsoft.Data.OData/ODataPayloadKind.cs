using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000277 RID: 631
	public enum ODataPayloadKind
	{
		// Token: 0x04000761 RID: 1889
		Feed,
		// Token: 0x04000762 RID: 1890
		Entry,
		// Token: 0x04000763 RID: 1891
		Property,
		// Token: 0x04000764 RID: 1892
		EntityReferenceLink,
		// Token: 0x04000765 RID: 1893
		EntityReferenceLinks,
		// Token: 0x04000766 RID: 1894
		Value,
		// Token: 0x04000767 RID: 1895
		BinaryValue,
		// Token: 0x04000768 RID: 1896
		Collection,
		// Token: 0x04000769 RID: 1897
		ServiceDocument,
		// Token: 0x0400076A RID: 1898
		MetadataDocument,
		// Token: 0x0400076B RID: 1899
		Error,
		// Token: 0x0400076C RID: 1900
		Batch,
		// Token: 0x0400076D RID: 1901
		Parameter,
		// Token: 0x0400076E RID: 1902
		Unsupported = 2147483647
	}
}
