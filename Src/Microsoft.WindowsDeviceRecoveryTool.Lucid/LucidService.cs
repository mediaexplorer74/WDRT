using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Nokia.Lucid;
using Nokia.Lucid.DeviceDetection;
using Nokia.Lucid.DeviceInformation;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.Lucid
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(ILucidService))]
	public sealed class LucidService : ILucidService
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020AC File Offset: 0x000002AC
		public Task<string> TakeFirstDevicePathForInterfaceGuidAsync(string usbDeviceInterfaceDevicePath, Guid interfaceGuid, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Guid guid = new DeviceInfoSet
			{
				DeviceTypeMap = LucidService.UsbDeviceInterfaceTypeMap,
				Filter = (DeviceIdentifier identifier) => true
			}.GetDevice(usbDeviceInterfaceDevicePath).ReadContainerId();
			VidPidPair vidPidPair = VidPidPair.Parse(usbDeviceInterfaceDevicePath);
			DeviceWatcher deviceWatcher = LucidService.GetDeviceWatcher(vidPidPair.Vid, vidPidPair.Pid, interfaceGuid, guid);
			DeviceInfoSet deviceInfoSet = LucidService.GetDeviceInfoSet(vidPidPair.Vid, vidPidPair.Pid, interfaceGuid, guid);
			return LucidService.TakeDevicePathAsync(deviceWatcher, deviceInfoSet, cancellationToken);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public Task<string> TakeFirstDevicePathForInterfaceGuidAsync(string usbDeviceInterfaceDevicePath, Guid interfaceGuid, int interfaceNumber, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Guid guid = new DeviceInfoSet
			{
				DeviceTypeMap = LucidService.UsbDeviceInterfaceTypeMap,
				Filter = (DeviceIdentifier identifier) => true
			}.GetDevice(usbDeviceInterfaceDevicePath).ReadContainerId();
			VidPidPair vidPidPair = VidPidPair.Parse(usbDeviceInterfaceDevicePath);
			DeviceWatcher deviceWatcher = LucidService.GetDeviceWatcher(vidPidPair.Vid, vidPidPair.Pid, interfaceNumber, interfaceGuid, guid);
			DeviceInfoSet deviceInfoSet = LucidService.GetDeviceInfoSet(vidPidPair.Vid, vidPidPair.Pid, interfaceNumber, interfaceGuid, guid);
			return LucidService.TakeDevicePathAsync(deviceWatcher, deviceInfoSet, cancellationToken);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002200 File Offset: 0x00000400
		public Task<string> TakeFirstDevicePathForDeviceAndInterfaceGuidsAsync(string usbDeviceInterfaceDevicePath, Guid deviceInterfaceGuid, Guid deviceSetupClassGuid, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Guid guid = new DeviceInfoSet
			{
				DeviceTypeMap = LucidService.UsbDeviceInterfaceTypeMap,
				Filter = (DeviceIdentifier identifier) => true
			}.GetDevice(usbDeviceInterfaceDevicePath).ReadContainerId();
			VidPidPair vidPidPair = VidPidPair.Parse(usbDeviceInterfaceDevicePath);
			DeviceWatcher deviceWatcher = LucidService.GetDeviceWatcher(vidPidPair.Vid, vidPidPair.Pid, deviceInterfaceGuid, deviceSetupClassGuid, guid);
			DeviceInfoSet deviceInfoSet = LucidService.GetDeviceInfoSet(vidPidPair.Vid, vidPidPair.Pid, deviceInterfaceGuid, deviceSetupClassGuid, guid);
			return LucidService.TakeDevicePathAsync(deviceWatcher, deviceInfoSet, cancellationToken);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022AC File Offset: 0x000004AC
		public DeviceInfo GetDeviceInfoForInterfaceGuid(string interfaceDevicePath, Guid interfaceGuid)
		{
			DeviceTypeMap deviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid);
			return new DeviceInfoSet
			{
				DeviceTypeMap = deviceTypeMap,
				Filter = (DeviceIdentifier identifier) => true
			}.GetDevice(interfaceDevicePath);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002313 File Offset: 0x00000513
		private static DeviceWatcher GetDeviceWatcher(string vid, string pid, int mi, Guid interfaceGuid, Guid containerId)
		{
			return new DeviceWatcher
			{
				DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
				Filter = LucidService.GetFilter(vid, pid, mi, interfaceGuid, containerId)
			};
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002337 File Offset: 0x00000537
		private static DeviceInfoSet GetDeviceInfoSet(string vid, string pid, int mi, Guid interfaceGuid, Guid containerId)
		{
			return new DeviceInfoSet
			{
				DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
				Filter = LucidService.GetFilter(vid, pid, mi, interfaceGuid, containerId)
			};
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000235B File Offset: 0x0000055B
		private static DeviceWatcher GetDeviceWatcher(string vid, string pid, Guid interfaceGuid, Guid containerId)
		{
			return new DeviceWatcher
			{
				DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
				Filter = LucidService.GetFilter(vid, pid, interfaceGuid, containerId)
			};
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000237D File Offset: 0x0000057D
		private static DeviceInfoSet GetDeviceInfoSet(string vid, string pid, Guid interfaceGuid, Guid containerId)
		{
			return new DeviceInfoSet
			{
				DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
				Filter = LucidService.GetFilter(vid, pid, interfaceGuid, containerId)
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000239F File Offset: 0x0000059F
		private static DeviceWatcher GetDeviceWatcher(string vid, string pid, Guid interfaceGuid, Guid deviceSetupClassGuid, Guid containerId)
		{
			return new DeviceWatcher
			{
				DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
				Filter = LucidService.GetFilter(vid, pid, interfaceGuid, deviceSetupClassGuid, containerId)
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C3 File Offset: 0x000005C3
		private static DeviceInfoSet GetDeviceInfoSet(string vid, string pid, Guid interfaceGuid, Guid deviceSetupClassGuid, Guid containerId)
		{
			return new DeviceInfoSet
			{
				DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
				Filter = LucidService.GetFilter(vid, pid, interfaceGuid, deviceSetupClassGuid, containerId)
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023E7 File Offset: 0x000005E7
		private static DeviceTypeMap GetDeviceTypeMap(Guid interfaceGuid)
		{
			return new DeviceTypeMap(interfaceGuid, DeviceType.Interface);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F0 File Offset: 0x000005F0
		private static Expression<Func<DeviceIdentifier, bool>> GetFilter(string vid, string pid, Guid interfaceGuid, Guid containerId)
		{
			Func<DeviceIdentifier, bool> filterFunc = delegate(DeviceIdentifier identifier)
			{
				DeviceInfoSet deviceInfoSet = new DeviceInfoSet
				{
					DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
					Filter = (DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid(vid) && deviceIdentifier.Pid(pid)
				};
				bool flag;
				try
				{
					flag = deviceInfoSet.GetDevice(identifier.Value).ReadContainerId() == containerId;
				}
				catch (Exception)
				{
					flag = false;
				}
				return flag;
			};
			return (DeviceIdentifier identifier) => filterFunc(identifier);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002484 File Offset: 0x00000684
		private static Expression<Func<DeviceIdentifier, bool>> GetFilter(string vid, string pid, int mi, Guid interfaceGuid, Guid containerId)
		{
			Func<DeviceIdentifier, bool> filterFunc = delegate(DeviceIdentifier identifier)
			{
				DeviceInfoSet deviceInfoSet = new DeviceInfoSet
				{
					DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
					Filter = (DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid(vid) && deviceIdentifier.Pid(pid) && deviceIdentifier.MI(mi)
				};
				bool flag;
				try
				{
					flag = deviceInfoSet.GetDevice(identifier.Value).ReadContainerId() == containerId;
				}
				catch (Exception)
				{
					flag = false;
				}
				return flag;
			};
			return (DeviceIdentifier identifier) => filterFunc(identifier);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002520 File Offset: 0x00000720
		private static Expression<Func<DeviceIdentifier, bool>> GetFilter(string vid, string pid, Guid interfaceGuid, Guid deviceSetupClassGuid, Guid containerId)
		{
			Func<DeviceIdentifier, bool> filterFunc = delegate(DeviceIdentifier identifier)
			{
				DeviceInfoSet deviceInfoSet = new DeviceInfoSet
				{
					DeviceTypeMap = LucidService.GetDeviceTypeMap(interfaceGuid),
					Filter = (DeviceIdentifier deviceIdentifier) => deviceIdentifier.Vid(vid) && deviceIdentifier.Pid(pid)
				};
				bool flag;
				try
				{
					DeviceInfo device = deviceInfoSet.GetDevice(identifier.Value);
					flag = device.ReadContainerId() == containerId && device.ReadClassGuid() == deviceSetupClassGuid;
				}
				catch (Exception)
				{
					flag = false;
				}
				return flag;
			};
			return (DeviceIdentifier identifier) => filterFunc(identifier);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025BC File Offset: 0x000007BC
		private static async Task<string> TakeDevicePathAsync(DeviceWatcher deviceWatcher, DeviceInfoSet deviceInfoSet, CancellationToken cancellationToken)
		{
			bool completed = false;
			TaskCompletionSource<string> taskSource = new TaskCompletionSource<string>();
			IDisposable deviceWatcherDisposable = null;
			Action<string> onMatchingDevicePath = delegate(string path)
			{
				if (completed)
				{
					return;
				}
				completed = true;
				try
				{
					taskSource.TrySetResult(path);
				}
				catch (OperationCanceledException)
				{
					taskSource.SetCanceled();
				}
				catch (Exception ex)
				{
					taskSource.SetException(ex);
				}
				deviceWatcherDisposable.Dispose();
			};
			EventHandler<DeviceChangedEventArgs> eventHandler = delegate(object sender, DeviceChangedEventArgs args)
			{
				if (args.Action == DeviceChangeAction.Attach)
				{
					onMatchingDevicePath(args.Path);
				}
			};
			EventHandler<DeviceChangedEventArgs> onDeviceChanged = SynchronizationHelper.ExecuteInCurrentContext<DeviceChangedEventArgs>(eventHandler);
			deviceWatcher.DeviceChanged += onDeviceChanged;
			deviceWatcherDisposable = deviceWatcher.Start();
			string text;
			using (cancellationToken.Register(delegate
			{
				taskSource.TrySetCanceled();
			}))
			{
				foreach (DeviceInfo deviceInfo in deviceInfoSet.EnumeratePresentDevices())
				{
					onMatchingDevicePath(deviceInfo.Path);
				}
				try
				{
					text = await taskSource.Task;
				}
				finally
				{
					deviceWatcher.DeviceChanged -= onDeviceChanged;
				}
			}
			return text;
		}

		// Token: 0x04000002 RID: 2
		private static readonly DeviceTypeMap UsbDeviceInterfaceTypeMap = new DeviceTypeMap(new Dictionary<Guid, DeviceType> { 
		{
			WellKnownGuids.UsbDeviceInterfaceGuid,
			DeviceType.PhysicalDevice
		} });
	}
}
