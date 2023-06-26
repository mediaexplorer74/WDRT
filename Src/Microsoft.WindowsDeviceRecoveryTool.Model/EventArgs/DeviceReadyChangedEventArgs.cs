using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs
{
	// Token: 0x0200001E RID: 30
	public class DeviceReadyChangedEventArgs : EventArgs
	{
		// Token: 0x060001BB RID: 443 RVA: 0x0000615C File Offset: 0x0000435C
		public DeviceReadyChangedEventArgs(ConnectedDevice device, bool deviceReady, ConnectedDeviceMode mode)
		{
			this.ConnectedDevice = device;
			this.DeviceReady = deviceReady;
			this.Mode = mode;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000617E File Offset: 0x0000437E
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00006186 File Offset: 0x00004386
		public ConnectedDevice ConnectedDevice { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000618F File Offset: 0x0000438F
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006197 File Offset: 0x00004397
		public bool DeviceReady { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000061A0 File Offset: 0x000043A0
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000061A8 File Offset: 0x000043A8
		public ConnectedDeviceMode Mode { get; private set; }
	}
}
