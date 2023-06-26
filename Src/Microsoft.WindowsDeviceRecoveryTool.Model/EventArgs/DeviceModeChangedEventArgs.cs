using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs
{
	// Token: 0x0200001D RID: 29
	public class DeviceModeChangedEventArgs : EventArgs
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x000060E8 File Offset: 0x000042E8
		public DeviceModeChangedEventArgs(ConnectedDevice connectedDevice, ConnectedDeviceMode oldMode, ConnectedDeviceMode newMode)
		{
			bool flag = connectedDevice == null;
			if (flag)
			{
				throw new ArgumentNullException("connectedDevice");
			}
			this.ConnectedDevice = connectedDevice;
			this.OldMode = oldMode;
			this.NewMode = newMode;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006129 File Offset: 0x00004329
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00006131 File Offset: 0x00004331
		public ConnectedDevice ConnectedDevice { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000613A File Offset: 0x0000433A
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006142 File Offset: 0x00004342
		public ConnectedDeviceMode OldMode { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000614B File Offset: 0x0000434B
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00006153 File Offset: 0x00004353
		public ConnectedDeviceMode NewMode { get; private set; }
	}
}
