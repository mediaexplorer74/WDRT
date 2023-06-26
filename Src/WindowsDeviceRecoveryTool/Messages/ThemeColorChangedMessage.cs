using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B1 RID: 177
	public class ThemeColorChangedMessage
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0001B7D3 File Offset: 0x000199D3
		public ThemeColorChangedMessage(string theme, string color)
		{
			this.Theme = theme;
			this.Color = color;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001B7ED File Offset: 0x000199ED
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001B7F5 File Offset: 0x000199F5
		public string Theme { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001B7FE File Offset: 0x000199FE
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001B806 File Offset: 0x00019A06
		public string Color { get; private set; }
	}
}
