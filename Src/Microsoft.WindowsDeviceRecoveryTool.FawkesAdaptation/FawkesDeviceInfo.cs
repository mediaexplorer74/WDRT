using System;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation
{
	// Token: 0x02000004 RID: 4
	internal sealed class FawkesDeviceInfo
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public FawkesDeviceInfo(string firmwareVersion, string hardwareId, string deviceFriendlyName)
		{
			if (firmwareVersion == null)
			{
				throw new ArgumentNullException("firmwareVersion");
			}
			this.FirmwareVersion = firmwareVersion;
			this.HardwareId = hardwareId;
			this.DeviceFriendlyName = deviceFriendlyName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000215B File Offset: 0x0000035B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002163 File Offset: 0x00000363
		public string FirmwareVersion { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002174 File Offset: 0x00000374
		public string HardwareId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000217D File Offset: 0x0000037D
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002185 File Offset: 0x00000385
		public string DeviceFriendlyName { get; set; }
	}
}
