using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x02000095 RID: 149
	public class DeviceBatteryStatusReadMessage
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0001B23C File Offset: 0x0001943C
		public DeviceBatteryStatusReadMessage(BatteryStatus status)
		{
			this.Status = status;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001B24E File Offset: 0x0001944E
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001B256 File Offset: 0x00019456
		public BatteryStatus Status { get; private set; }
	}
}
