using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs
{
	// Token: 0x02000021 RID: 33
	public class UsbDeviceEventArgs : EventArgs
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00006304 File Offset: 0x00004504
		public UsbDeviceEventArgs(UsbDevice usbDevice)
		{
			bool flag = usbDevice == null;
			if (flag)
			{
				throw new ArgumentNullException("usbDevice");
			}
			this.UsbDevice = usbDevice;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006335 File Offset: 0x00004535
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000633D File Offset: 0x0000453D
		public UsbDevice UsbDevice { get; private set; }
	}
}
