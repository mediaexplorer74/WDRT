using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000AB RID: 171
	public class IsBackButtonMessage
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001B643 File Offset: 0x00019843
		public IsBackButtonMessage(bool isBackButtton)
		{
			this.IsBackButton = isBackButtton;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001B655 File Offset: 0x00019855
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001B65D File Offset: 0x0001985D
		public bool IsBackButton { get; private set; }
	}
}
