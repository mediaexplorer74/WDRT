using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000BA RID: 186
	internal sealed class DeviceInfo
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001BB19 File Offset: 0x00019D19
		public DeviceInfo(string deviceIdentifier)
		{
			this.DeviceIdentifier = deviceIdentifier;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001BB2B File Offset: 0x00019D2B
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0001BB33 File Offset: 0x00019D33
		public string DeviceIdentifier { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001BB3C File Offset: 0x00019D3C
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0001BB44 File Offset: 0x00019D44
		public bool IsDeviceSupported { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001BB4D File Offset: 0x00019D4D
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0001BB55 File Offset: 0x00019D55
		public Guid SupportId { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001BB5E File Offset: 0x00019D5E
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0001BB66 File Offset: 0x00019D66
		public string DeviceSalesName { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001BB6F File Offset: 0x00019D6F
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0001BB77 File Offset: 0x00019D77
		public byte[] DeviceBitmapBytes { get; set; }
	}
}
