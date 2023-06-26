using System;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000AE RID: 174
	public class SelectedDeviceMessage
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x0001B76A File Offset: 0x0001996A
		public SelectedDeviceMessage(Phone phone)
		{
			this.SelectedPhone = phone;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001B77C File Offset: 0x0001997C
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001B784 File Offset: 0x00019984
		public Phone SelectedPhone { get; private set; }
	}
}
