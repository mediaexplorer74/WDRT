using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs
{
	// Token: 0x0200001C RID: 28
	public class DeviceConnectedEventArgs : EventArgs
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x000060A4 File Offset: 0x000042A4
		public DeviceConnectedEventArgs(ConnectedDevice connectedDevice)
		{
			bool flag = connectedDevice == null;
			if (flag)
			{
				throw new ArgumentNullException("connectedDevice");
			}
			this.ConnectedDevice = connectedDevice;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000060D5 File Offset: 0x000042D5
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000060DD File Offset: 0x000042DD
		public ConnectedDevice ConnectedDevice { get; private set; }
	}
}
