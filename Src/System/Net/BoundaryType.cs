using System;

namespace System.Net
{
	// Token: 0x020000E5 RID: 229
	internal enum BoundaryType
	{
		// Token: 0x04000D2F RID: 3375
		ContentLength,
		// Token: 0x04000D30 RID: 3376
		Chunked,
		// Token: 0x04000D31 RID: 3377
		Multipart = 3,
		// Token: 0x04000D32 RID: 3378
		None,
		// Token: 0x04000D33 RID: 3379
		Invalid
	}
}
