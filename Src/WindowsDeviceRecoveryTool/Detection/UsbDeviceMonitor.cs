using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Nokia.Lucid;
using Nokia.Lucid.DeviceDetection;
using Nokia.Lucid.DeviceInformation;
using Nokia.Lucid.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000BD RID: 189
	public sealed class UsbDeviceMonitor : IUsbDeviceMonitor, IDisposable
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x0001BBDB File Offset: 0x00019DDB
		private UsbDeviceMonitor(IDisposable notificationTicket)
		{
			this.events = new BlockingCollection<UsbDeviceChangeEvent>(25);
			this.notificationTicket = notificationTicket;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001BBFC File Offset: 0x00019DFC
		public static UsbDeviceMonitor StartNew(DeviceTypeMap deviceTypeMap, Expression<Func<DeviceIdentifier, bool>> deviceIdentifierFilter)
		{
			bool flag = deviceIdentifierFilter == null;
			if (flag)
			{
				throw new ArgumentNullException("deviceIdentifierFilter");
			}
			SynchronizationContext context = SynchronizationContext.Current;
			DeviceWatcher deviceWatcher = new DeviceWatcher
			{
				DeviceTypeMap = deviceTypeMap,
				Filter = deviceIdentifierFilter
			};
			IDisposable disposable = deviceWatcher.Start();
			UsbDeviceMonitor deviceMonitor = new UsbDeviceMonitor(disposable);
			deviceWatcher.DeviceChanged += delegate(object s, DeviceChangedEventArgs a)
			{
				deviceMonitor.DeviceWatcherOnDeviceChanged(context, s, new UsbDeviceChangeEvent(a, false));
			};
			DeviceInfoSet deviceInfoSet = new DeviceInfoSet
			{
				DeviceTypeMap = deviceTypeMap,
				Filter = deviceIdentifierFilter
			};
			foreach (DeviceInfo deviceInfo in deviceInfoSet.EnumeratePresentDevices())
			{
				DeviceChangedEventArgs deviceChangedEventArgs = new DeviceChangedEventArgs(DeviceChangeAction.Attach, deviceInfo.Path, deviceInfo.DeviceType);
				deviceMonitor.DeviceWatcherOnDeviceChanged(context, deviceWatcher, new UsbDeviceChangeEvent(deviceChangedEventArgs, true));
			}
			return deviceMonitor;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001BD04 File Offset: 0x00019F04
		public async Task<UsbDeviceChangeEvent> TakeDeviceChangeEventAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			UsbDeviceChangeEvent @event;
			bool flag = this.events.TryTake(out @event);
			UsbDeviceChangeEvent usbDeviceChangeEvent;
			if (flag)
			{
				usbDeviceChangeEvent = @event;
			}
			else
			{
				TaskCompletionSource<UsbDeviceChangeEvent> completionSource = new TaskCompletionSource<UsbDeviceChangeEvent>();
				using (cancellationToken.Register(delegate
				{
					completionSource.TrySetCanceled();
				}))
				{
					this.pendingTask = completionSource;
					UsbDeviceChangeEvent usbDeviceChangeEvent2 = await this.pendingTask.Task;
					@event = usbDeviceChangeEvent2;
					usbDeviceChangeEvent2 = null;
					usbDeviceChangeEvent = @event;
				}
			}
			return usbDeviceChangeEvent;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001BD4F File Offset: 0x00019F4F
		public void Dispose()
		{
			this.notificationTicket.Dispose();
			this.events.CompleteAdding();
			this.events.Dispose();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001BD78 File Offset: 0x00019F78
		private static void TraceDeviceChanged(DeviceChangedEventArgs args)
		{
			bool flag = args == null;
			if (!flag)
			{
				bool flag2 = args.Action == DeviceChangeAction.Attach;
				if (flag2)
				{
					Tracer<UsbDeviceMonitor>.WriteInformation("Device attached: {0}, Path: {1}", new object[]
					{
						args.DeviceType.ToString(),
						args.Path
					});
				}
				else
				{
					Tracer<UsbDeviceMonitor>.WriteInformation("Device detached: {0}, Path: {1}", new object[]
					{
						args.DeviceType.ToString(),
						args.Path
					});
				}
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001BE08 File Offset: 0x0001A008
		private void DeviceWatcherOnDeviceChanged(SynchronizationContext synchronizationContext, object sender, UsbDeviceChangeEvent args)
		{
			bool flag = args == null;
			if (flag)
			{
				throw new ArgumentNullException("args");
			}
			UsbDeviceMonitor.TraceDeviceChanged(args.Data);
			EventHandler<UsbDeviceChangeEvent> baseHandler = delegate(object s, UsbDeviceChangeEvent a)
			{
				bool flag3 = this.pendingTask != null && this.pendingTask.TrySetResult(a);
				if (flag3)
				{
					this.pendingTask = null;
				}
				else
				{
					bool flag4 = !this.events.Any((UsbDeviceChangeEvent e) => string.Equals(e.Data.Path, a.Data.Path, StringComparison.InvariantCultureIgnoreCase) && e.Data.Action == a.Data.Action && e.Data.DeviceType == a.Data.DeviceType);
					if (flag4)
					{
						this.events.TryAdd(a);
					}
				}
			};
			bool flag2 = synchronizationContext == null || synchronizationContext.GetType() == typeof(SynchronizationContext);
			if (flag2)
			{
				baseHandler(sender, args);
			}
			else
			{
				synchronizationContext.Post(delegate(object state)
				{
					baseHandler(sender, args);
				}, null);
			}
		}

		// Token: 0x0400028A RID: 650
		private readonly BlockingCollection<UsbDeviceChangeEvent> events;

		// Token: 0x0400028B RID: 651
		private readonly IDisposable notificationTicket;

		// Token: 0x0400028C RID: 652
		private TaskCompletionSource<UsbDeviceChangeEvent> pendingTask;
	}
}
