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
using Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IDeviceSupport))]
	internal class TrekStorSupport : IDeviceSupport
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000211C File Offset: 0x0000031C
		[ImportingConstructor]
		public TrekStorSupport(IMtpDeviceInfoProvider trekttorDeviceInfoProvider)
		{
			this.TrekStorDeviceInfoProvider = trekttorDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(TrekStorSupport.TrekStorManufacturerInfo, new ModelInfo[] { TrekStorModels.TrekStor_Winphone_5_0_LTE });
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002149 File Offset: 0x00000349
		public Guid Id
		{
			get
			{
				return TrekStorSupport.SupportGuid;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002150 File Offset: 0x00000350
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002160 File Offset: 0x00000360
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
				Tracer<TrekStorSupport>.WriteInformation("No TrekStor device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
			}
			else
			{
				string description = (await this.TrekStorDeviceInfoProvider.ReadInformationAsync(usbDeviceInterfaceDevicePath, cancellationToken)).Description;
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

		// Token: 0x04000003 RID: 3
		private static readonly Guid SupportGuid = new Guid("C3D0AA61-0D19-41C8-AD30-99D46A4CAB60");

		// Token: 0x04000004 RID: 4
		private static readonly ManufacturerInfo TrekStorManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.Trekstor_logo);

		// Token: 0x04000005 RID: 5
		private readonly IMtpDeviceInfoProvider TrekStorDeviceInfoProvider;

		// Token: 0x04000006 RID: 6
		private readonly ManufacturerModelsCatalog catalog;
	}
}
