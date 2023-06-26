using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B2 RID: 178
	public class WaitMessage
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001B80F File Offset: 0x00019A0F
		public WaitMessage(int seconds)
		{
			this.WaitSeconds = seconds;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001B821 File Offset: 0x00019A21
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001B829 File Offset: 0x00019A29
		public int WaitSeconds { get; private set; }
	}
}
