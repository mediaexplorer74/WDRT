using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000AF RID: 175
	public class SelectedPathMessage
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x0001B78D File Offset: 0x0001998D
		public SelectedPathMessage(string selectedPath)
		{
			this.SelectedPath = selectedPath;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001B79F File Offset: 0x0001999F
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001B7A7 File Offset: 0x000199A7
		public string SelectedPath { get; private set; }
	}
}
