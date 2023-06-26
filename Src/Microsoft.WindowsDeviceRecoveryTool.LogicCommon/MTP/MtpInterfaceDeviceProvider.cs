using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Nokia.Lucid;
using Nokia.Lucid.DeviceDetection;
using Nokia.Lucid.DeviceInformation;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.MTP
{
	// Token: 0x02000025 RID: 37
	public sealed class MtpInterfaceDeviceProvider
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000902C File Offset: 0x0000722C
		public async Task<MtpInterfaceDevice> GetChildMtpInterfaceDeviceAsync(string vid, string pid, string parentPath, CancellationToken cancellationToken)
		{
			bool completed = false;
			Guid parentContainerId = MtpInterfaceDeviceProvider.GetParentContainerId(parentPath);
			DeviceWatcher deviceWatcher = MtpInterfaceDeviceProvider.GetDeviceWatcher(vid, pid, parentContainerId);
			DeviceInfoSet deviceInfoSet = MtpInterfaceDeviceProvider.GetDeviceInfoSet(vid, pid, parentContainerId);
			TaskCompletionSource<MtpInterfaceDevice> taskSource = new TaskCompletionSource<MtpInterfaceDevice>();
			IDisposable deviceWatcherDisposable = null;
			Action<string> onMatchingDevicePath = delegate(string path)
			{
				bool completed2 = completed;
				if (!completed2)
				{
					completed = true;
					Tracer<MtpInterfaceDeviceProvider>.WriteInformation("MTP interface found {0}", new object[] { path });
					taskSource.TrySetResult(new MtpInterfaceDevice(path));
					deviceWatcherDisposable.Dispose();
				}
			};
			EventHandler<DeviceChangedEventArgs> onDevice = delegate(object sender, DeviceChangedEventArgs args)
			{
				bool flag = args.Action == DeviceChangeAction.Attach;
				if (flag)
				{
					onMatchingDevicePath(args.Path);
				}
			};
			EventHandler<DeviceChangedEventArgs> onDeviceChanged = SynchronizationHelper.ExecuteInCurrentContext<DeviceChangedEventArgs>(onDevice);
			deviceWatcher.DeviceChanged += onDeviceChanged;
			deviceWatcherDisposable = deviceWatcher.Start();
			foreach (DeviceInfo device in deviceInfoSet.EnumeratePresentDevices())
			{
				onMatchingDevicePath(device.Path);
				device = null;
			}
			IEnumerator<DeviceInfo> enumerator = null;
			MtpInterfaceDevice mtpInterfaceDevice2;
			using (cancellationToken.Register(delegate
			{
				taskSource.TrySetCanceled();
			}))
			{
				try
				{
					MtpInterfaceDevice mtpInterfaceDevice = await taskSource.Task;
					mtpInterfaceDevice2 = mtpInterfaceDevice;
				}
				finally
				{
					deviceWatcher.DeviceChanged -= onDeviceChanged;
				}
			}
			return mtpInterfaceDevice2;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009090 File Offset: 0x00007290
		private static Guid GetParentContainerId(string path)
		{
			DeviceInfoSet deviceInfoSet = new DeviceInfoSet
			{
				DeviceTypeMap = new DeviceTypeMap(WindowsPhoneIdentifiers.GenericUsbDeviceInterfaceGuid, DeviceType.PhysicalDevice),
				Filter = (DeviceIdentifier deviceIdentifier) => true
			};
			return deviceInfoSet.GetDevice(path).ReadContainerId();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009108 File Offset: 0x00007308
		private static DeviceWatcher GetDeviceWatcher(string vid, string pid, Guid conatinerId)
		{
			return new DeviceWatcher
			{
				DeviceTypeMap = MtpInterfaceDeviceProvider.GetDeviceTypeMap(),
				Filter = MtpInterfaceDeviceProvider.GetFilter(vid, pid, conatinerId)
			};
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000913C File Offset: 0x0000733C
		private static DeviceInfoSet GetDeviceInfoSet(string vid, string pid, Guid conatinerId)
		{
			return new DeviceInfoSet
			{
				DeviceTypeMap = MtpInterfaceDeviceProvider.GetDeviceTypeMap(),
				Filter = MtpInterfaceDeviceProvider.GetFilter(vid, pid, conatinerId)
			};
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009170 File Offset: 0x00007370
		private static DeviceTypeMap GetDeviceTypeMap()
		{
			return new DeviceTypeMap(new Guid("6ac27878-a6fa-4155-ba85-f98f491d4f33"), DeviceType.Interface);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009194 File Offset: 0x00007394
		private static Expression<Func<DeviceIdentifier, bool>> GetFilter(string vid, string pid, Guid containerId)
		{
			Func<DeviceIdentifier, bool> filterFunc = delegate(DeviceIdentifier identifier)
			{
				DeviceInfoSet deviceInfoSet = new DeviceInfoSet
				{
					DeviceTypeMap = MtpInterfaceDeviceProvider.GetDeviceTypeMap(),
					Filter = (DeviceIdentifier deviceIdentifier) => true
				};
				bool flag;
				try
				{
					flag = identifier.Vid(vid) && identifier.Pid(pid) && deviceInfoSet.GetDevice(identifier.Value).ReadContainerId() == containerId;
				}
				catch (Exception)
				{
					flag = false;
				}
				return flag;
			};
			return (DeviceIdentifier identifier) => filterFunc(identifier);
		}

		// Token: 0x04000115 RID: 277
		private const string MtpInterfaceGuid = "6ac27878-a6fa-4155-ba85-f98f491d4f33";
	}
}
