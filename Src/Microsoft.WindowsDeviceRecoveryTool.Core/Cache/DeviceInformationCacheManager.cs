using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Core.Cache
{
	// Token: 0x02000008 RID: 8
	[Export(typeof(IDeviceInformationCacheManager))]
	public class DeviceInformationCacheManager : IDeviceInformationCacheManager
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000022E8 File Offset: 0x000004E8
		public IDisposable EnableCacheForDevicePath(string devicePath)
		{
			DeviceInformationCacheManager.DevicePathBasedCacheObject newCacheObject = new DeviceInformationCacheManager.DevicePathBasedCacheObject(devicePath);
			this.cacheObjects.Add(newCacheObject);
			return new DeviceInformationCacheManager.Disposable(delegate
			{
				this.cacheObjects.Remove(newCacheObject);
			});
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002330 File Offset: 0x00000530
		public IDevicePathBasedCacheObject GetCacheObjectForDevicePath(string devicePath)
		{
			DeviceInformationCacheManager.DevicePathBasedCacheObject devicePathBasedCacheObject = this.cacheObjects.FirstOrDefault((DeviceInformationCacheManager.DevicePathBasedCacheObject cache) => cache.DevicePath == devicePath);
			if (devicePathBasedCacheObject != null)
			{
				return devicePathBasedCacheObject;
			}
			return new DeviceInformationCacheManager.EmptyDevicePathBasedCacheObject();
		}

		// Token: 0x0400000B RID: 11
		public static readonly DeviceInformationCacheManager None = new DeviceInformationCacheManager();

		// Token: 0x0400000C RID: 12
		private readonly List<DeviceInformationCacheManager.DevicePathBasedCacheObject> cacheObjects = new List<DeviceInformationCacheManager.DevicePathBasedCacheObject>();

		// Token: 0x0200000C RID: 12
		private class Disposable : IDisposable
		{
			// Token: 0x0600002F RID: 47 RVA: 0x000023E7 File Offset: 0x000005E7
			public Disposable(Action onDispose)
			{
				this.onDispose = onDispose;
			}

			// Token: 0x06000030 RID: 48 RVA: 0x000023F6 File Offset: 0x000005F6
			public void Dispose()
			{
				if (this.isDisposed)
				{
					return;
				}
				this.onDispose();
				this.isDisposed = true;
			}

			// Token: 0x0400000D RID: 13
			private readonly Action onDispose;

			// Token: 0x0400000E RID: 14
			private bool isDisposed;
		}

		// Token: 0x0200000D RID: 13
		private class EmptyDevicePathBasedCacheObject : IDevicePathBasedCacheObject
		{
			// Token: 0x06000031 RID: 49 RVA: 0x00002413 File Offset: 0x00000613
			public bool TryGetReadInformationTaskForReader<T>(Type readerType, out Task<T> readInformationTask)
			{
				readInformationTask = null;
				return false;
			}

			// Token: 0x06000032 RID: 50 RVA: 0x00002419 File Offset: 0x00000619
			public void AddReadInformationTaskForReader<T>(Type readerType, Task<T> readInformationTask)
			{
			}
		}

		// Token: 0x0200000E RID: 14
		private sealed class DevicePathBasedCacheObject : IDevicePathBasedCacheObject
		{
			// Token: 0x06000034 RID: 52 RVA: 0x0000241B File Offset: 0x0000061B
			internal DevicePathBasedCacheObject(string devicePath)
			{
				this.devicePath = devicePath;
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00002435 File Offset: 0x00000635
			public string DevicePath
			{
				get
				{
					return this.devicePath;
				}
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002440 File Offset: 0x00000640
			public bool TryGetReadInformationTaskForReader<T>(Type readerType, out Task<T> readInformationTask)
			{
				DeviceInformationCacheManager.DevicePathBasedCacheObject.CacheItem cacheItem = this.cache.FirstOrDefault((DeviceInformationCacheManager.DevicePathBasedCacheObject.CacheItem item) => item.ReaderType == readerType);
				if (cacheItem == null)
				{
					readInformationTask = null;
					return false;
				}
				readInformationTask = (Task<T>)cacheItem.ReadInformationTask;
				return true;
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002488 File Offset: 0x00000688
			public void AddReadInformationTaskForReader<T>(Type readerType, Task<T> readInformationTask)
			{
				this.cache.Add(new DeviceInformationCacheManager.DevicePathBasedCacheObject.CacheItem
				{
					ReaderType = readerType,
					ReadInformationTask = readInformationTask
				});
			}

			// Token: 0x0400000F RID: 15
			private readonly string devicePath;

			// Token: 0x04000010 RID: 16
			private readonly List<DeviceInformationCacheManager.DevicePathBasedCacheObject.CacheItem> cache = new List<DeviceInformationCacheManager.DevicePathBasedCacheObject.CacheItem>();

			// Token: 0x02000012 RID: 18
			private sealed class CacheItem
			{
				// Token: 0x1700000C RID: 12
				// (get) Token: 0x0600003E RID: 62 RVA: 0x00002652 File Offset: 0x00000852
				// (set) Token: 0x0600003F RID: 63 RVA: 0x0000265A File Offset: 0x0000085A
				public object ReadInformationTask { get; set; }

				// Token: 0x1700000D RID: 13
				// (get) Token: 0x06000040 RID: 64 RVA: 0x00002663 File Offset: 0x00000863
				// (set) Token: 0x06000041 RID: 65 RVA: 0x0000266B File Offset: 0x0000086B
				public Type ReaderType { get; set; }
			}
		}
	}
}
