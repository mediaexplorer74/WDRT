using System;
using System.IO;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200000F RID: 15
	public class FileCrcInfo
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002F15 File Offset: 0x00001115
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002F1D File Offset: 0x0000111D
		public string FileName { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002F26 File Offset: 0x00001126
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002F2E File Offset: 0x0000112E
		public bool Optional { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002F37 File Offset: 0x00001137
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002F3F File Offset: 0x0000113F
		public string Directory { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002F48 File Offset: 0x00001148
		public string FilePath
		{
			get
			{
				return Path.Combine(this.Directory, this.FileName);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F6B File Offset: 0x0000116B
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002F73 File Offset: 0x00001173
		public string Crc { get; set; }
	}
}
