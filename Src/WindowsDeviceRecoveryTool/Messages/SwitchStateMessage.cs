using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B6 RID: 182
	public class SwitchStateMessage
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00016287 File Offset: 0x00014487
		public SwitchStateMessage()
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001B8CD File Offset: 0x00019ACD
		public SwitchStateMessage(string state)
		{
			this.State = state;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001B8DF File Offset: 0x00019ADF
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001B8E7 File Offset: 0x00019AE7
		public string State { get; set; }
	}
}
