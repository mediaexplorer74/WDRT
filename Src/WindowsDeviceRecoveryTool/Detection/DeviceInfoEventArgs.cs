using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000B8 RID: 184
	internal sealed class DeviceInfoEventArgs
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		public DeviceInfoEventArgs(DeviceInfo deviceInfo, DeviceInfoAction deviceInfoAction, bool isEnumerated = false)
		{
			this.DeviceInfo = deviceInfo;
			this.DeviceInfoAction = deviceInfoAction;
			this.IsEnumerated = isEnumerated;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001B912 File Offset: 0x00019B12
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001B91A File Offset: 0x00019B1A
		public DeviceInfo DeviceInfo { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001B923 File Offset: 0x00019B23
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001B92B File Offset: 0x00019B2B
		public DeviceInfoAction DeviceInfoAction { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001B934 File Offset: 0x00019B34
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001B93C File Offset: 0x00019B3C
		public bool IsEnumerated { get; private set; }
	}
}
