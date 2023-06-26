using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200000B RID: 11
	public class HtcDeviceInfo
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002BB2 File Offset: 0x00000DB2
		public HtcDeviceInfo(string mid, string cid, string path)
		{
			this.Mid = mid;
			this.Cid = cid;
			this.Path = path;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public HtcDeviceInfo(string path)
		{
			this.Path = path;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002BE6 File Offset: 0x00000DE6
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002BEE File Offset: 0x00000DEE
		public string Mid { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002BF7 File Offset: 0x00000DF7
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002BFF File Offset: 0x00000DFF
		public string Cid { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002C08 File Offset: 0x00000E08
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002C10 File Offset: 0x00000E10
		public string Path { get; set; }
	}
}
