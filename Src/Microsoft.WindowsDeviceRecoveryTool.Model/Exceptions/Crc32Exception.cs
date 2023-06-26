using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class Crc32Exception : Exception
	{
		// Token: 0x0600020C RID: 524 RVA: 0x000063CD File Offset: 0x000045CD
		public Crc32Exception(string filePath)
			: base(string.Format("Invalid Crc32 for file: {0}.", filePath))
		{
			this.FilePath = filePath;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000063EA File Offset: 0x000045EA
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000063F2 File Offset: 0x000045F2
		public string FilePath { get; private set; }
	}
}
