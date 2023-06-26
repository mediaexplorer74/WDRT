using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Core.Cache;
using Nokia.Lucid.DeviceDetection;

namespace Microsoft.WindowsDeviceRecoveryTool.Detection
{
	// Token: 0x020000B9 RID: 185
	internal sealed class DetectionHandler : IDetectionHandler, IDisposable
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x0001B948 File Offset: 0x00019B48
		public DetectionHandler(IUsbDeviceMonitor usbDeviceMonitor, IEnumerable<IDeviceSupport> supports, IDeviceInformationCacheManager deviceInformationCacheManager)
		{
			bool flag = usbDeviceMonitor == null;
			if (flag)
			{
				throw new ArgumentNullException("usbDeviceMonitor");
			}
			bool flag2 = supports == null;
			if (flag2)
			{
				throw new ArgumentNullException("supports");
			}
			bool flag3 = deviceInformationCacheManager == null;
			if (flag3)
			{
				throw new ArgumentNullException("deviceInformationCacheManager");
			}
			this.usbDeviceMonitor = usbDeviceMonitor;
			this.supports = supports;
			this.deviceInformationCacheManager = deviceInformationCacheManager;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		public async Task<DeviceInfoEventArgs> TakeDeviceInfoEventAsync(CancellationToken cancellationToken)
		{
			DetectionHandler.<>c__DisplayClass6_0 CS$<>8__locals1;
			DetectionHandler.ChangedDevice detachedDevice;
			DetectionHandler.ChangedDevice attachedDevice;
			for (;;)
			{
				CS$<>8__locals1 = new DetectionHandler.<>c__DisplayClass6_0();
				Task<DetectionHandler.ChangedDevice> detectionTask = this.CreateDetectionTask(cancellationToken);
				this.ongoingTasks.Add(detectionTask);
				Task<DetectionHandler.ChangedDevice> task = await Task.WhenAny<DetectionHandler.ChangedDevice>(this.ongoingTasks);
				Task<DetectionHandler.ChangedDevice> finishedTask = task;
				task = null;
				CS$<>8__locals1.changedDevice = null;
				try
				{
					DetectionHandler.ChangedDevice changedDevice = await finishedTask;
					CS$<>8__locals1.changedDevice = changedDevice;
					changedDevice = null;
				}
				catch (OperationCanceledException)
				{
				}
				catch (Exception)
				{
					continue;
				}
				finally
				{
					this.ongoingTasks.Remove(finishedTask);
				}
				if (cancellationToken.IsCancellationRequested)
				{
					break;
				}
				if (!CS$<>8__locals1.changedDevice.IsAttached)
				{
					detachedDevice = this.attachedDevices.FirstOrDefault((DetectionHandler.ChangedDevice d) => string.Equals(d.Identifier, CS$<>8__locals1.changedDevice.Identifier, StringComparison.OrdinalIgnoreCase));
					if (detachedDevice != null)
					{
						goto Block_4;
					}
				}
				else
				{
					attachedDevice = this.attachedDevices.FirstOrDefault((DetectionHandler.ChangedDevice d) => string.Equals(d.Identifier, CS$<>8__locals1.changedDevice.Identifier, StringComparison.OrdinalIgnoreCase));
					if (attachedDevice != null && attachedDevice.UpdatedDeviceInfo != null)
					{
						goto Block_6;
					}
					CS$<>8__locals1.vidPid = VidPidPair.Parse(CS$<>8__locals1.changedDevice.Identifier);
					var detectionInfos = (from s in this.supports.SelectMany((IDeviceSupport s) => from vp in s.GetDeviceDetectionInformation()
							select new
							{
								Support = s,
								DetectionInfo = vp
							})
						where s.DetectionInfo.VidPidPair == CS$<>8__locals1.vidPid
						select s).ToArray();
					var defferedDetection = (from info in detectionInfos
						where info.DetectionInfo.DetectionDeferred
						select (info)).ToArray();
					var nonDefferedDetection = (from info in detectionInfos
						where !info.DetectionInfo.DetectionDeferred
						select (info)).ToArray();
					if (nonDefferedDetection.Length != 0)
					{
						goto Block_12;
					}
					this.attachedDevices.Add(CS$<>8__locals1.changedDevice);
					IEnumerable<IDeviceSupport> defferedSupports = defferedDetection.Select(d => d.Support);
					Task<DetectionHandler.ChangedDevice> identificationTask = this.UpdateDeviceDetectionDataAsync(defferedSupports, CS$<>8__locals1.changedDevice, cancellationToken);
					this.ongoingTasks.Add(identificationTask);
					CS$<>8__locals1 = null;
					detectionTask = null;
					finishedTask = null;
					attachedDevice = null;
					detectionInfos = null;
					defferedDetection = null;
					nonDefferedDetection = null;
					defferedSupports = null;
					identificationTask = null;
				}
			}
			try
			{
				await Task.WhenAll<DetectionHandler.ChangedDevice>(this.ongoingTasks);
			}
			catch (Exception)
			{
			}
			throw new OperationCanceledException(cancellationToken);
			Block_4:
			this.attachedDevices.Remove(detachedDevice);
			return new DeviceInfoEventArgs(new DeviceInfo(detachedDevice.Identifier), DeviceInfoAction.Detached, false);
			Block_6:
			return new DeviceInfoEventArgs(attachedDevice.UpdatedDeviceInfo, DeviceInfoAction.Attached, CS$<>8__locals1.changedDevice.IsEnumerated);
			Block_12:
			this.attachedDevices.Add(CS$<>8__locals1.changedDevice);
			return new DeviceInfoEventArgs(new DeviceInfo(CS$<>8__locals1.changedDevice.Identifier), DeviceInfoAction.Attached, CS$<>8__locals1.changedDevice.IsEnumerated);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001BA10 File Offset: 0x00019C10
		public async Task UpdateDeviceInfoAsync(DeviceInfo deviceInfo, CancellationToken cancellationToken)
		{
			DetectionHandler.ChangedDevice attachedDevice = this.attachedDevices.FirstOrDefault((DetectionHandler.ChangedDevice d) => string.Equals(d.Identifier, deviceInfo.DeviceIdentifier, StringComparison.OrdinalIgnoreCase));
			bool flag = attachedDevice.UpdatedDeviceInfo == null;
			if (flag)
			{
				await this.UpdateDeviceDetectionDataAsync(this.supports, attachedDevice, cancellationToken);
			}
			if (attachedDevice.UpdatedDeviceInfo != null)
			{
				deviceInfo.DeviceBitmapBytes = attachedDevice.UpdatedDeviceInfo.DeviceBitmapBytes;
				deviceInfo.DeviceSalesName = attachedDevice.UpdatedDeviceInfo.DeviceSalesName;
				deviceInfo.IsDeviceSupported = attachedDevice.UpdatedDeviceInfo.IsDeviceSupported;
				deviceInfo.SupportId = attachedDevice.UpdatedDeviceInfo.SupportId;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001BA62 File Offset: 0x00019C62
		public void Dispose()
		{
			this.usbDeviceMonitor.Dispose();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001BA74 File Offset: 0x00019C74
		private async Task<DetectionHandler.ChangedDevice> CreateDetectionTask(CancellationToken cancellationToken)
		{
			UsbDeviceChangeEvent usbDeviceChangeEvent = await this.usbDeviceMonitor.TakeDeviceChangeEventAsync(cancellationToken);
			UsbDeviceChangeEvent result = usbDeviceChangeEvent;
			usbDeviceChangeEvent = null;
			return new DetectionHandler.ChangedDevice(result.Data.Path, result.Data.Action == DeviceChangeAction.Attach, result.IsEnumerated);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		private async Task<DetectionHandler.ChangedDevice> UpdateDeviceDetectionDataAsync(IEnumerable<IDeviceSupport> supportsToCheck, DetectionHandler.ChangedDevice changedDevice, CancellationToken cancellationToken)
		{
			DetectionHandler.<>c__DisplayClass10_0 CS$<>8__locals1 = new DetectionHandler.<>c__DisplayClass10_0();
			CS$<>8__locals1.deviceIdentifier = changedDevice.Identifier;
			DeviceDetectionData deviceDetectionData = new DeviceDetectionData(CS$<>8__locals1.deviceIdentifier);
			using (this.deviceInformationCacheManager.EnableCacheForDevicePath(CS$<>8__locals1.deviceIdentifier))
			{
				foreach (IDeviceSupport support in supportsToCheck)
				{
					await support.UpdateDeviceDetectionDataAsync(deviceDetectionData, cancellationToken);
					if (deviceDetectionData.IsDeviceSupported)
					{
						DeviceInfo info = new DeviceInfo(CS$<>8__locals1.deviceIdentifier)
						{
							DeviceBitmapBytes = deviceDetectionData.DeviceBitmapBytes,
							DeviceSalesName = deviceDetectionData.DeviceSalesName,
							IsDeviceSupported = deviceDetectionData.IsDeviceSupported,
							SupportId = support.Id
						};
						IEnumerable<DetectionHandler.ChangedDevice> enumerable = this.attachedDevices;
						Func<DetectionHandler.ChangedDevice, bool> func;
						if ((func = CS$<>8__locals1.<>9__0) == null)
						{
							DetectionHandler.<>c__DisplayClass10_0 CS$<>8__locals2 = CS$<>8__locals1;
							Func<DetectionHandler.ChangedDevice, bool> func2 = (DetectionHandler.ChangedDevice at) => string.Equals(at.Identifier, CS$<>8__locals1.deviceIdentifier, StringComparison.OrdinalIgnoreCase);
							CS$<>8__locals2.<>9__0 = func2;
							func = func2;
						}
						DetectionHandler.ChangedDevice device = enumerable.FirstOrDefault(func);
						if (device != null)
						{
							device.UpdatedDeviceInfo = info;
						}
						break;
					}
					support = null;
				}
				IEnumerator<IDeviceSupport> enumerator = null;
			}
			IDisposable disposable = null;
			return changedDevice;
		}

		// Token: 0x0400027E RID: 638
		private readonly IUsbDeviceMonitor usbDeviceMonitor;

		// Token: 0x0400027F RID: 639
		private readonly IEnumerable<IDeviceSupport> supports;

		// Token: 0x04000280 RID: 640
		private readonly IDeviceInformationCacheManager deviceInformationCacheManager;

		// Token: 0x04000281 RID: 641
		private readonly List<Task<DetectionHandler.ChangedDevice>> ongoingTasks = new List<Task<DetectionHandler.ChangedDevice>>();

		// Token: 0x04000282 RID: 642
		private readonly List<DetectionHandler.ChangedDevice> attachedDevices = new List<DetectionHandler.ChangedDevice>();

		// Token: 0x02000143 RID: 323
		private sealed class ChangedDevice
		{
			// Token: 0x0600082A RID: 2090 RVA: 0x00023324 File Offset: 0x00021524
			public ChangedDevice(string identifier, bool isAttached, bool isEnumerated)
			{
				this.Identifier = identifier;
				this.IsAttached = isAttached;
				this.IsEnumerated = isEnumerated;
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x0600082B RID: 2091 RVA: 0x00023346 File Offset: 0x00021546
			// (set) Token: 0x0600082C RID: 2092 RVA: 0x0002334E File Offset: 0x0002154E
			public string Identifier { get; private set; }

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x00023357 File Offset: 0x00021557
			// (set) Token: 0x0600082E RID: 2094 RVA: 0x0002335F File Offset: 0x0002155F
			public bool IsAttached { get; private set; }

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x0600082F RID: 2095 RVA: 0x00023368 File Offset: 0x00021568
			// (set) Token: 0x06000830 RID: 2096 RVA: 0x00023370 File Offset: 0x00021570
			public bool IsEnumerated { get; private set; }

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000831 RID: 2097 RVA: 0x00023379 File Offset: 0x00021579
			// (set) Token: 0x06000832 RID: 2098 RVA: 0x00023381 File Offset: 0x00021581
			public DeviceInfo UpdatedDeviceInfo { get; set; }
		}
	}
}
