using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000003 RID: 3
	public class ApplicationUpdate
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000270E File Offset: 0x0000090E
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002716 File Offset: 0x00000916
		public int AppId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000271F File Offset: 0x0000091F
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002727 File Offset: 0x00000927
		public string Description { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002730 File Offset: 0x00000930
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002738 File Offset: 0x00000938
		public string Version { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002741 File Offset: 0x00000941
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002749 File Offset: 0x00000949
		public string PackageUri { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002752 File Offset: 0x00000952
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000275A File Offset: 0x0000095A
		public long Size { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002764 File Offset: 0x00000964
		public string PackageFileName
		{
			get
			{
				string text;
				try
				{
					Uri uri = new Uri(this.PackageUri);
					text = uri.Segments[uri.Segments.Length - 1];
				}
				catch
				{
					text = null;
				}
				return text;
			}
		}
	}
}
