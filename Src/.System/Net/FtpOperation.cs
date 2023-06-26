using System;

namespace System.Net
{
	// Token: 0x020000EC RID: 236
	internal enum FtpOperation
	{
		// Token: 0x04000D7C RID: 3452
		DownloadFile,
		// Token: 0x04000D7D RID: 3453
		ListDirectory,
		// Token: 0x04000D7E RID: 3454
		ListDirectoryDetails,
		// Token: 0x04000D7F RID: 3455
		UploadFile,
		// Token: 0x04000D80 RID: 3456
		UploadFileUnique,
		// Token: 0x04000D81 RID: 3457
		AppendFile,
		// Token: 0x04000D82 RID: 3458
		DeleteFile,
		// Token: 0x04000D83 RID: 3459
		GetDateTimestamp,
		// Token: 0x04000D84 RID: 3460
		GetFileSize,
		// Token: 0x04000D85 RID: 3461
		Rename,
		// Token: 0x04000D86 RID: 3462
		MakeDirectory,
		// Token: 0x04000D87 RID: 3463
		RemoveDirectory,
		// Token: 0x04000D88 RID: 3464
		PrintWorkingDirectory,
		// Token: 0x04000D89 RID: 3465
		Other
	}
}
