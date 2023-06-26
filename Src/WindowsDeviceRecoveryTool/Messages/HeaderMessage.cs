using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000AA RID: 170
	public class HeaderMessage
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0001B607 File Offset: 0x00019807
		public HeaderMessage(string header, string subheader = "")
		{
			this.Header = header;
			this.Subheader = subheader;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001B621 File Offset: 0x00019821
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001B629 File Offset: 0x00019829
		public string Header { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001B632 File Offset: 0x00019832
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0001B63A File Offset: 0x0001983A
		public string Subheader { get; private set; }
	}
}
