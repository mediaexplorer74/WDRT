﻿using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.LgeAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Lucid;
using Nokia.Lucid.DeviceInformation;

namespace Microsoft.WindowsDeviceRecoveryTool.LgeAdaptation
{
	// Token: 0x02000003 RID: 3
	[Export(typeof(IDeviceSupport))]
	internal class LgeSupport : IDeviceSupport
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
		public Guid Id
		{
			get
			{
				return LgeSupport.SupportGuid;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		[ImportingConstructor]
		public LgeSupport(ILucidService lucidService)
		{
			this.lucidService = lucidService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(LgeSupport.LgeVidPid, false)
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public async Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken)
		{
			bool isDeviceSupported = detectionData.IsDeviceSupported;
			if (isDeviceSupported)
			{
				throw new InvalidOperationException("Device is already supported.");
			}
			VidPidPair vidPid = detectionData.VidPidPair;
			string devicePath = detectionData.UsbDeviceInterfaceDevicePath;
			bool flag = vidPid != LgeSupport.LgeVidPid;
			if (!flag)
			{
				DeviceInfo deviceInfo = this.lucidService.GetDeviceInfoForInterfaceGuid(devicePath, WellKnownGuids.UsbDeviceInterfaceGuid);
				detectionData.DeviceSalesName = deviceInfo.ReadBusReportedDeviceDescription();
				detectionData.DeviceBitmapBytes = Resources.Lancet.ToBytes();
				detectionData.IsDeviceSupported = true;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly ILucidService lucidService;

		// Token: 0x04000002 RID: 2
		private static readonly VidPidPair LgeVidPid = new VidPidPair("1004", "627E");

		// Token: 0x04000003 RID: 3
		private static readonly Guid SupportGuid = new Guid("77D0B92B-C020-4163-AB74-B251F5C32EEA");
	}
}
