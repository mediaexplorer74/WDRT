using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Core
{
	// Token: 0x02000004 RID: 4
	public interface IDeviceInformationProvider<T>
	{
		// Token: 0x06000011 RID: 17
		Task<T> ReadInformationAsync(string devicePath, CancellationToken token);
	}
}
