using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000007 RID: 7
	public class EmergencyPackageInfo
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A8B File Offset: 0x00000C8B
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A93 File Offset: 0x00000C93
		public string ConfigFilePath { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public string HexFlasherFilePath { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002AAD File Offset: 0x00000CAD
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public string EdpImageFilePath { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002ABE File Offset: 0x00000CBE
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public string MbnImageFilePath { get; set; }

		// Token: 0x0600004E RID: 78 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public bool IsAlphaCollins()
		{
			return !string.IsNullOrWhiteSpace(this.MbnImageFilePath) && !string.IsNullOrWhiteSpace(this.HexFlasherFilePath);
		}
	}
}
