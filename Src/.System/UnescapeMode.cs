using System;

namespace System
{
	// Token: 0x0200004D RID: 77
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x040004A5 RID: 1189
		CopyOnly = 0,
		// Token: 0x040004A6 RID: 1190
		Escape = 1,
		// Token: 0x040004A7 RID: 1191
		Unescape = 2,
		// Token: 0x040004A8 RID: 1192
		EscapeUnescape = 3,
		// Token: 0x040004A9 RID: 1193
		V1ToStringFlag = 4,
		// Token: 0x040004AA RID: 1194
		UnescapeAll = 8,
		// Token: 0x040004AB RID: 1195
		UnescapeAllOrThrow = 24
	}
}
