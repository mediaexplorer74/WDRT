using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x02000094 RID: 148
	public class DeviceConnectionStatusReadMessage
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0001B219 File Offset: 0x00019419
		public DeviceConnectionStatusReadMessage(bool status)
		{
			this.Status = status;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001B22B File Offset: 0x0001942B
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0001B233 File Offset: 0x00019433
		public bool Status { get; private set; }
	}
}
