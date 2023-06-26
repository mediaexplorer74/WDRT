using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Core.Cache
{
	// Token: 0x02000009 RID: 9
	public static class DeviceInformationProviderExtensions
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000238C File Offset: 0x0000058C
		public static async Task<T> ReadInformationAsync<T>(this IDeviceInformationProvider<T> deviceInformationReader, string parentDevicePath, IDeviceInformationCacheManager cacheManager, CancellationToken cancellationToken)
		{
			IDevicePathBasedCacheObject cacheObjectForDevicePath = cacheManager.GetCacheObjectForDevicePath(parentDevicePath);
			Task<T> task;
			T t;
			if (cacheObjectForDevicePath.TryGetReadInformationTaskForReader<T>(deviceInformationReader.GetType(), out task))
			{
				t = await task;
			}
			else
			{
				task = deviceInformationReader.ReadInformationAsync(parentDevicePath, cancellationToken);
				cacheObjectForDevicePath.AddReadInformationTaskForReader<T>(deviceInformationReader.GetType(), task);
				t = await task;
			}
			return t;
		}
	}
}
