using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x0200009F RID: 159
	public class DeviceInfoReadMessage
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0001B3E5 File Offset: 0x000195E5
		public DeviceInfoReadMessage(bool result)
		{
			this.Result = result;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001B3F7 File Offset: 0x000195F7
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001B3FF File Offset: 0x000195FF
		public bool Result { get; private set; }
	}
}
