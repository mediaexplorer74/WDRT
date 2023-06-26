using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.AlcatelAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.AlcatelAdaptation
{
	// Token: 0x02000005 RID: 5
	[Export(typeof(IDeviceSupport))]
	internal class AlcatelSupport : IDeviceSupport
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000023A0 File Offset: 0x000005A0
		public Guid Id
		{
			get
			{
				return AlcatelSupport.SupportGuid;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023B7 File Offset: 0x000005B7
		[ImportingConstructor]
		public AlcatelSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(AlcatelSupport.ManufacturerInfo, new ModelInfo[]
			{
				AlcatelModels.IDOL4S,
				AlcatelModels.FierceXL,
				AlcatelModels.IDO4SPRO
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023F8 File Offset: 0x000005F8
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002418 File Offset: 0x00000618
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
				Tracer<AlcatelSupport>.WriteInformation("No Alatel device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
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

		// Token: 0x04000008 RID: 8
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x04000009 RID: 9
		private static readonly Guid SupportGuid = new Guid("C40DAE33-E5D2-4778-807A-903A343F610F");

		// Token: 0x0400000A RID: 10
		private static readonly ManufacturerInfo ManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.Alcatel_Logo);

		// Token: 0x0400000B RID: 11
		private readonly ManufacturerModelsCatalog catalog;
	}
}
