using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Core
{
	// Token: 0x02000002 RID: 2
	public sealed class DeviceDetectionInformation
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public DeviceDetectionInformation(VidPidPair vidPidPair, bool detectionDeferred)
		{
			this.VidPidPair = vidPidPair;
			this.DetectionDeferred = detectionDeferred;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002066 File Offset: 0x00000266
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		public VidPidPair VidPidPair { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002077 File Offset: 0x00000277
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000207F File Offset: 0x0000027F
		public bool DetectionDeferred { get; private set; }
	}
}
