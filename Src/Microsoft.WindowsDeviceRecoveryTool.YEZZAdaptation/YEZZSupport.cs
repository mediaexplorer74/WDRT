using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.YEZZAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.YEZZAdaptation
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IDeviceSupport))]
	internal class YEZZSupport : IDeviceSupport
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		[ImportingConstructor]
		public YEZZSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(YEZZSupport.YEZZManufacturerInfo, new ModelInfo[] { YEZZModels.Billy_4_7 });
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002131 File Offset: 0x00000331
		public Guid Id
		{
			get
			{
				return YEZZSupport.SupportGuid;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002148 File Offset: 0x00000348
		public async Task UpdateDeviceDetectionDataAsync(DeviceDetectionData detectionData, CancellationToken cancellationToken)
		{
			if (detectionData.IsDeviceSupported)
			{
				throw new InvalidOperationException("Device is already supported.");
			}
			cancellationToken.ThrowIfCancellationRequested();
			VidPidPair vidPidPair = detectionData.VidPidPair;
			string usbDeviceInterfaceDevicePath = detectionData.UsbDeviceInterfaceDevicePath;
			Func<DeviceDetectionInformation, bool> <>9__2;
			if (this.catalog.Models.FirstOrDefault(delegate(ModelInfo m)
			{
				IEnumerable<DeviceDetectionInformation> deviceDetectionInformations = m.DetectionInfo.DeviceDetectionInformations;
				Func<DeviceDetectionInformation, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (DeviceDetectionInformation di) => di.VidPidPair == vidPidPair);
				}
				return deviceDetectionInformations.Any(func);
			}) == null)
			{
				Tracer<YEZZSupport>.WriteInformation("No YEZZ device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
			}
			else
			{
				MtpInterfaceInfo mtpInterfaceInfo = await this.mtpDeviceInfoProvider.ReadInformationAsync(usbDeviceInterfaceDevicePath, cancellationToken);
				string mtpDeviceDescription = mtpInterfaceInfo.Description;
				ModelInfo modelInfo;
				if (this.catalog.TryGetModelInfo(mtpDeviceDescription, out modelInfo))
				{
					string name = modelInfo.Name;
					detectionData.DeviceBitmapBytes = modelInfo.Bitmap.ToBytes();
					detectionData.DeviceSalesName = name;
					detectionData.IsDeviceSupported = true;
				}
				else
				{
					Action<string> <>9__3;
					this.catalog.Models.FirstOrDefault<ModelInfo>().Variants.ToList<VariantInfo>().ForEach(delegate(VariantInfo v)
					{
						List<string> list = v.IdentificationInfo.DeviceReturnedValues.ToList<string>();
						Action<string> action;
						if ((action = <>9__3) == null)
						{
							action = (<>9__3 = delegate(string dv)
							{
								Tracer<YEZZSupport>.WriteInformation("No YEZZ device detected. mtpDeviceDescription: {0}, YEZZ IdentificationInfo: {1}", new object[] { mtpDeviceDescription, dv });
							});
						}
						list.ForEach(action);
					});
				}
			}
		}

		// Token: 0x04000003 RID: 3
		private static readonly Guid SupportGuid = new Guid("BA2D8739-0A4E-4AE7-8287-9D0E31E1F391");

		// Token: 0x04000004 RID: 4
		private static readonly ManufacturerInfo YEZZManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.yezz_logo);

		// Token: 0x04000005 RID: 5
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x04000006 RID: 6
		private readonly ManufacturerModelsCatalog catalog;
	}
}
