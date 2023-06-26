using System;

namespace System.Net
{
	// Token: 0x020000ED RID: 237
	[Flags]
	internal enum FtpMethodFlags
	{
		// Token: 0x04000D8B RID: 3467
		None = 0,
		// Token: 0x04000D8C RID: 3468
		IsDownload = 1,
		// Token: 0x04000D8D RID: 3469
		IsUpload = 2,
		// Token: 0x04000D8E RID: 3470
		TakesParameter = 4,
		// Token: 0x04000D8F RID: 3471
		MayTakeParameter = 8,
		// Token: 0x04000D90 RID: 3472
		DoesNotTakeParameter = 16,
		// Token: 0x04000D91 RID: 3473
		ParameterIsDirectory = 32,
		// Token: 0x04000D92 RID: 3474
		ShouldParseForResponseUri = 64,
		// Token: 0x04000D93 RID: 3475
		HasHttpCommand = 128,
		// Token: 0x04000D94 RID: 3476
		MustChangeWorkingDirectoryToPath = 256
	}
}
