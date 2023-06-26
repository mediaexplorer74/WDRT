using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.McjAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.McjAdaptation
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IDeviceSupport))]
	internal class McjSupport : IDeviceSupport
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002219 File Offset: 0x00000419
		[ImportingConstructor]
		public McjSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(McjSupport.McjManufacturerInfo, new ModelInfo[]
			{
				McjModels.MadosmaQ501,
				McjModels.MadosmaQ601
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002250 File Offset: 0x00000450
		public Guid Id
		{
			get
			{
				return McjSupport.SupportGuid;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002268 File Offset: 0x00000468
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002288 File Offset: 0x00000488
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
				Tracer<McjSupport>.WriteInformation("No MCJ device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
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

		// Token: 0x04000005 RID: 5
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x04000006 RID: 6
		private readonly ManufacturerModelsCatalog catalog;

		// Token: 0x04000007 RID: 7
		private static readonly Guid SupportGuid = new Guid("E1213532-3425-4DD8-A468-0E72A75DEF04");

		// Token: 0x04000008 RID: 8
		private static readonly ManufacturerInfo McjManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.MadosmaLogo);
	}
}
