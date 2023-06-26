using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.FreetelAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.FreetelAdaptation
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IDeviceSupport))]
	internal class FreetelSupport : IDeviceSupport
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021BD File Offset: 0x000003BD
		[ImportingConstructor]
		public FreetelSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(FreetelSupport.FreetelManufacturerInfo, new ModelInfo[]
			{
				FreetelModels.Katana01Model,
				FreetelModels.Katana02Model
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021F2 File Offset: 0x000003F2
		public Guid Id
		{
			get
			{
				return FreetelSupport.SupportGuid;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021F9 File Offset: 0x000003F9
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002208 File Offset: 0x00000408
		public async Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken)
		{
			if (detectionData.IsDeviceSupported)
			{
				throw new InvalidOperationException("Device is already supported.");
			}
			cancellationToken.ThrowIfCancellationRequested();
			VidPidPair vidPidPair = detectionData.VidPidPair;
			string usbDeviceInterfaceDevicePath = detectionData.UsbDeviceInterfaceDevicePath;
			Func<DeviceDetectionInformation, bool> <>9__1;
			if (this.catalog.Models.FirstOrDefault(delegate(ModelInfo m)
			{
				IEnumerable<DeviceDetectionInformation> deviceDetectionInformations = m.DetectionInfo.DeviceDetectionInformations;
				Func<DeviceDetectionInformation, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (DeviceDetectionInformation di) => di.VidPidPair == vidPidPair);
				}
				return deviceDetectionInformations.Any(func);
			}) == null)
			{
				Tracer<FreetelSupport>.WriteInformation("No Freetel device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
			}
			else
			{
				string description = (await this.mtpDeviceInfoProvider.ReadInformationAsync(usbDeviceInterfaceDevicePath, cancellationToken)).Description;
				ModelInfo modelInfo;
				if (this.catalog.TryGetModelInfo(description, out modelInfo))
				{
					string name = modelInfo.Name;
					detectionData.DeviceBitmapBytes = modelInfo.Bitmap.ToBytes();
					detectionData.DeviceSalesName = name;
					detectionData.IsDeviceSupported = true;
				}
			}
		}

		// Token: 0x04000005 RID: 5
		private static readonly Guid SupportGuid = new Guid("37EB525A-9379-43C8-9B48-B5833CFEBD10");

		// Token: 0x04000006 RID: 6
		private static readonly ManufacturerInfo FreetelManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.Freetel_Logo);

		// Token: 0x04000007 RID: 7
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x04000008 RID: 8
		private readonly ManufacturerModelsCatalog catalog;
	}
}
