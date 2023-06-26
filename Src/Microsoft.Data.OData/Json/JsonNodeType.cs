using System;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000251 RID: 593
	internal enum JsonNodeType
	{
		// Token: 0x040006F3 RID: 1779
		None,
		// Token: 0x040006F4 RID: 1780
		StartObject,
		// Token: 0x040006F5 RID: 1781
		EndObject,
		// Token: 0x040006F6 RID: 1782
		StartArray,
		// Token: 0x040006F7 RID: 1783
		EndArray,
		// Token: 0x040006F8 RID: 1784
		Property,
		// Token: 0x040006F9 RID: 1785
		PrimitiveValue,
		// Token: 0x040006FA RID: 1786
		EndOfInput
	}
}
