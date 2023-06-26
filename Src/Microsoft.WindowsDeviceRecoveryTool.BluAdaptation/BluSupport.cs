using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.BluAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.BluAdaptation
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IDeviceSupport))]
	internal class BluSupport : IDeviceSupport
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000026E0 File Offset: 0x000008E0
		[ImportingConstructor]
		public BluSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(BluSupport.ManufacturerInfo, new ModelInfo[]
			{
				BluModels.WinJrLte,
				BluModels.WinHdLte,
				BluModels.WinJR410,
				BluModels.WinHd510
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002734 File Offset: 0x00000934
		public Guid Id
		{
			get
			{
				return BluSupport.SupportGuid;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000274C File Offset: 0x0000094C
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000276C File Offset: 0x0000096C
		public async Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken)
		{
			bool isDeviceSupported = detectionData.IsDeviceSupported;
			if (isDeviceSupported)
			{
				throw new InvalidOperationException("Device is already supported.");
			}
			cancellationToken.ThrowIfCancellationRequested();
			VidPidPair vidPidPair = detectionData.VidPidPair;
			string devicePath = detectionData.UsbDeviceInterfaceDevicePath;
			Func<DeviceDetectionInformation, bool> <>9__1;
			ModelInfo model = this.catalog.Models.FirstOrDefault(delegate(ModelInfo m)
			{
				IEnumerable<DeviceDetectionInformation> deviceDetectionInformations = m.DetectionInfo.DeviceDetectionInformations;
				Func<DeviceDetectionInformation, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (DeviceDetectionInformation di) => di.VidPidPair == vidPidPair);
				}
				return deviceDetectionInformations.Any(func);
			});
			bool flag = model == null;
			if (flag)
			{
				Tracer<BluSupport>.WriteInformation("No Blu device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
			}
			else
			{
				MtpInterfaceInfo mtpInterfaceInfo = await this.mtpDeviceInfoProvider.ReadInformationAsync(devicePath, cancellationToken);
				MtpInterfaceInfo deviceInfo = mtpInterfaceInfo;
				mtpInterfaceInfo = null;
				string mtpDeviceDescription = deviceInfo.Description;
				ModelInfo modelInfo;
				if (this.catalog.TryGetModelInfo(mtpDeviceDescription, out modelInfo))
				{
					string deviceFriendlyName = modelInfo.Name;
					byte[] bitmapBytes = modelInfo.Bitmap.ToBytes();
					detectionData.DeviceBitmapBytes = bitmapBytes;
					detectionData.DeviceSalesName = deviceFriendlyName;
					detectionData.IsDeviceSupported = true;
					deviceFriendlyName = null;
					bitmapBytes = null;
				}
			}
		}

		// Token: 0x0400000F RID: 15
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x04000010 RID: 16
		private static readonly Guid SupportGuid = new Guid("93AB08C4-B626-420D-BCD8-B4C3D45B1B04");

		// Token: 0x04000011 RID: 17
		private static readonly ManufacturerInfo ManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.blulogo);

		// Token: 0x04000012 RID: 18
		private readonly ManufacturerModelsCatalog catalog;
	}
}
