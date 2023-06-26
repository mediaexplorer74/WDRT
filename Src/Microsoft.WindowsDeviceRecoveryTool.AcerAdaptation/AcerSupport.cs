using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.AcerAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.AcerAdaptation
{
	// Token: 0x02000005 RID: 5
	[Export(typeof(IDeviceSupport))]
	internal class AcerSupport : IDeviceSupport
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002668 File Offset: 0x00000868
		[ImportingConstructor]
		public AcerSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(AcerSupport.ManufacturerInfo, new ModelInfo[]
			{
				AcerModels.LiquidM220,
				AcerModels.JadePrimo,
				AcerModels.LiquidM330
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000026A5 File Offset: 0x000008A5
		public Guid Id
		{
			get
			{
				return AcerSupport.SupportGuid;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026AC File Offset: 0x000008AC
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026BC File Offset: 0x000008BC
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
				Tracer<AcerSupport>.WriteInformation("No Acer device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
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

		// Token: 0x0400000F RID: 15
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x04000010 RID: 16
		private static readonly Guid SupportGuid = new Guid("5DD6D8CE-DAB9-4E27-8846-669B343E89BE");

		// Token: 0x04000011 RID: 17
		private static readonly ManufacturerInfo ManufacturerInfo = new ManufacturerInfo(Resources.Name_Manufacturer, Resources.AcerLogo);

		// Token: 0x04000012 RID: 18
		private readonly ManufacturerModelsCatalog catalog;
	}
}
