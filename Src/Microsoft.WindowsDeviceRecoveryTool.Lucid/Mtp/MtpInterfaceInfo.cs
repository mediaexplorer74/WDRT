using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp
{
	// Token: 0x02000007 RID: 7
	public sealed class MtpInterfaceInfo
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002687 File Offset: 0x00000887
		internal MtpInterfaceInfo(string description, string manufacturer)
		{
			this.Description = description;
			this.Manufacturer = manufacturer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000269D File Offset: 0x0000089D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000026A5 File Offset: 0x000008A5
		public string Description { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000026AE File Offset: 0x000008AE
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000026B6 File Offset: 0x000008B6
		public string Manufacturer { get; private set; }
	}
}
