﻿using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200005A RID: 90
	internal enum RequestTargetKind
	{
		// Token: 0x0400008D RID: 141
		Nothing,
		// Token: 0x0400008E RID: 142
		ServiceDirectory,
		// Token: 0x0400008F RID: 143
		Resource,
		// Token: 0x04000090 RID: 144
		ComplexObject,
		// Token: 0x04000091 RID: 145
		Primitive,
		// Token: 0x04000092 RID: 146
		PrimitiveValue,
		// Token: 0x04000093 RID: 147
		Metadata,
		// Token: 0x04000094 RID: 148
		VoidOperation,
		// Token: 0x04000095 RID: 149
		Batch,
		// Token: 0x04000096 RID: 150
		OpenProperty,
		// Token: 0x04000097 RID: 151
		OpenPropertyValue,
		// Token: 0x04000098 RID: 152
		MediaResource,
		// Token: 0x04000099 RID: 153
		Collection
	}
}
