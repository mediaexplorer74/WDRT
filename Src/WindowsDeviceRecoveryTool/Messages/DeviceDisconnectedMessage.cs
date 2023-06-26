using System;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A1 RID: 161
	public class DeviceDisconnectedMessage
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0001B42B File Offset: 0x0001962B
		public DeviceDisconnectedMessage(Phone phone)
		{
			this.Phone = phone;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001B43D File Offset: 0x0001963D
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001B445 File Offset: 0x00019645
		public Phone Phone { get; private set; }
	}
}
