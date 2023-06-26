using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B0 RID: 176
	public class SettingsPreviousStateMessage
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0001B7B0 File Offset: 0x000199B0
		public SettingsPreviousStateMessage(string previousState = null)
		{
			this.PreviousState = previousState;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001B7C2 File Offset: 0x000199C2
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001B7CA File Offset: 0x000199CA
		public string PreviousState { get; private set; }
	}
}
