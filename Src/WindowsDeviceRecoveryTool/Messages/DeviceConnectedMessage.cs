using System;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A0 RID: 160
	public class DeviceConnectedMessage
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0001B408 File Offset: 0x00019608
		public DeviceConnectedMessage(Phone phone)
		{
			this.Phone = phone;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001B41A File Offset: 0x0001961A
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001B422 File Offset: 0x00019622
		public Phone Phone { get; private set; }
	}
}
