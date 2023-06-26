using System;
using Nokia.Lucid.DeviceDetection;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000BC RID: 188
	public sealed class UsbDeviceChangeEvent
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x0001BB80 File Offset: 0x00019D80
		public UsbDeviceChangeEvent(DeviceChangedEventArgs data, bool isEnumerated = false)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			this.Data = data;
			this.IsEnumerated = isEnumerated;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001BBB9 File Offset: 0x00019DB9
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0001BBC1 File Offset: 0x00019DC1
		public DeviceChangedEventArgs Data { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001BBCA File Offset: 0x00019DCA
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0001BBD2 File Offset: 0x00019DD2
		public bool IsEnumerated { get; private set; }
	}
}
