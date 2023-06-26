using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Core.Cache
{
	// Token: 0x0200000A RID: 10
	public interface IDeviceInformationCacheManager
	{
		// Token: 0x0600002B RID: 43
		IDisposable EnableCacheForDevicePath(string devicePath);

		// Token: 0x0600002C RID: 44
		IDevicePathBasedCacheObject GetCacheObjectForDevicePath(string devicePath);
	}
}
