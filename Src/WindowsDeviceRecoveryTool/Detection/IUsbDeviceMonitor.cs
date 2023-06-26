using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000BE RID: 190
	public interface IUsbDeviceMonitor : IDisposable
	{
		// Token: 0x060005DA RID: 1498
		Task<UsbDeviceChangeEvent> TakeDeviceChangeEventAsync(CancellationToken cancellationToken);
	}
}
