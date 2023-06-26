using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Core;

namespace Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives
{
	// Token: 0x02000003 RID: 3
	public sealed class DetectionInfo
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
		public DetectionInfo(IEnumerable<DeviceDetectionInformation> deviceDetectionInformations)
		{
			if (deviceDetectionInformations == null)
			{
				throw new ArgumentNullException("deviceDetectionInformations");
			}
			DeviceDetectionInformation[] array = deviceDetectionInformations.ToArray<DeviceDetectionInformation>();
			if (array.Length == 0)
			{
				throw new ArgumentException("deviceReturnedValues should have at least one element");
			}
			this.DeviceDetectionInformations = array;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002186 File Offset: 0x00000386
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000218E File Offset: 0x0000038E
		public DeviceDetectionInformation[] DeviceDetectionInformations { get; private set; }
	}
}
