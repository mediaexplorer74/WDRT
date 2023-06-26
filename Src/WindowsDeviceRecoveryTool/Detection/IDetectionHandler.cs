using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000BB RID: 187
	internal interface IDetectionHandler : IDisposable
	{
		// Token: 0x060005CD RID: 1485
		Task<DeviceInfoEventArgs> TakeDeviceInfoEventAsync(CancellationToken cancellationToken);

		// Token: 0x060005CE RID: 1486
		Task UpdateDeviceInfoAsync(DeviceInfo deviceInfo, CancellationToken cancellationToken);
	}
}
