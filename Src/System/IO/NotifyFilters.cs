using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	// Token: 0x020003FA RID: 1018
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The name of the file.</summary>
		// Token: 0x04002098 RID: 8344
		FileName = 1,
		/// <summary>The name of the directory.</summary>
		// Token: 0x04002099 RID: 8345
		DirectoryName = 2,
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x0400209A RID: 8346
		Attributes = 4,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x0400209B RID: 8347
		Size = 8,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x0400209C RID: 8348
		LastWrite = 16,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x0400209D RID: 8349
		LastAccess = 32,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x0400209E RID: 8350
		CreationTime = 64,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x0400209F RID: 8351
		Security = 256
	}
}
