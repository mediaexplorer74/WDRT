using System;
using Microsoft.WindowsDeviceRecoveryTool.States.Help;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A2 RID: 162
	public class HelpScreenChangedMessage
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0001B44E File Offset: 0x0001964E
		public HelpScreenChangedMessage(HelpTabs tab)
		{
			this.SelectedTab = tab;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001B460 File Offset: 0x00019660
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001B468 File Offset: 0x00019668
		public HelpTabs SelectedTab { get; private set; }
	}
}
