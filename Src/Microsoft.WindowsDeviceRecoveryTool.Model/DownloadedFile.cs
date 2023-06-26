using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200000C RID: 12
	public class DownloadedFile
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002C19 File Offset: 0x00000E19
		public DownloadedFile(string fileName, long fileSize)
		{
			this.FileName = fileName;
			this.TotalSize = fileSize;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002C33 File Offset: 0x00000E33
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002C3B File Offset: 0x00000E3B
		public string FileName { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002C44 File Offset: 0x00000E44
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002C4C File Offset: 0x00000E4C
		public long TotalSize { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002C55 File Offset: 0x00000E55
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002C5D File Offset: 0x00000E5D
		public long TotalDownloaded { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002C66 File Offset: 0x00000E66
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002C6E File Offset: 0x00000E6E
		public long? PreviouslyDownloaded { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002C77 File Offset: 0x00000E77
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002C7F File Offset: 0x00000E7F
		public long CurrentlyDownloaded { get; set; }
	}
}
