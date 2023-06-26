using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Core
{
	// Token: 0x02000003 RID: 3
	public class DeviceDetectionData
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		public DeviceDetectionData(string usbDeviceInterfaceDevicePath)
		{
			this.UsbDeviceInterfaceDevicePath = usbDeviceInterfaceDevicePath;
			this.VidPidPair = VidPidPair.Parse(usbDeviceInterfaceDevicePath);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020A3 File Offset: 0x000002A3
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020AB File Offset: 0x000002AB
		public VidPidPair VidPidPair { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020B4 File Offset: 0x000002B4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020BC File Offset: 0x000002BC
		public string UsbDeviceInterfaceDevicePath { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020C5 File Offset: 0x000002C5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020CD File Offset: 0x000002CD
		public bool IsDeviceSupported { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020D6 File Offset: 0x000002D6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020DE File Offset: 0x000002DE
		public string DeviceSalesName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020E7 File Offset: 0x000002E7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020EF File Offset: 0x000002EF
		public byte[] DeviceBitmapBytes { get; set; }
	}
}
