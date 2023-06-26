using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Core
{
	// Token: 0x02000005 RID: 5
	public interface IDeviceSupport
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18
		Guid Id { get; }

		// Token: 0x06000013 RID: 19
		DeviceDetectionInformation[] GetDeviceDetectionInformation();

		// Token: 0x06000014 RID: 20
		Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken);
	}
}
