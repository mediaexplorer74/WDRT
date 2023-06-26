using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.HPAdaptation.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;

namespace Microsoft.WindowsDeviceRecoveryTool.HPAdaptation
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IDeviceSupport))]
	internal class HPSupport : IDeviceSupport
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000022B7 File Offset: 0x000004B7
		[ImportingConstructor]
		public HPSupport(IMtpDeviceInfoProvider mtpDeviceInfoProvider)
		{
			this.mtpDeviceInfoProvider = mtpDeviceInfoProvider;
			this.catalog = new ManufacturerModelsCatalog(HPSupport.McjManufacturerInfo, new ModelInfo[]
			{
				HPModels.Elitex3,
				HPModels.Elitex3_Telstra,
				HPModels.Elitex3_Verizon
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000022F4 File Offset: 0x000004F4
		public Guid Id
		{
			get
			{
				return HPSupport.SupportGuid;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022FB File Offset: 0x000004FB
		public DeviceDetectionInformation[] GetDeviceDetectionInformation()
		{
			return this.catalog.GetDeviceDetectionInformations();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002308 File Offset: 0x00000508
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
				Tracer<HPSupport>.WriteInformation("No HP device detected. Path: {0}", new object[] { detectionData.UsbDeviceInterfaceDevicePath });
			}
			else
			{
				string description = (await this.mtpDeviceInfoProvider.ReadInformationAsync(usbDeviceInterfaceDevicePath, cancellationToken)).Description;
				int num = description.ToLower().IndexOf("telstra");
				int num2 = description.ToLower().IndexOf("verizon");
				if (num < 0 && num2 < 0)
				{
					HPModels.Elitex3.Variants[0].IdentificationInfo.DeviceReturnedValues[0] = HPModels.Elitex3.Variants[0].IdentificationInfo.DeviceReturnedValues[0].Replace("Standard", "").Trim();
				}
				else if (HPModels.Elitex3.Variants[0].IdentificationInfo.DeviceReturnedValues[0].IndexOf("Standard", StringComparison.OrdinalIgnoreCase) < 0)
				{
					string[] deviceReturnedValues = HPModels.Elitex3.Variants[0].IdentificationInfo.DeviceReturnedValues;
					int num3 = 0;
					deviceReturnedValues[num3] += " Standard";
				}
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

		// Token: 0x04000007 RID: 7
		private static readonly Guid SupportGuid = new Guid("2B5046B7-716B-42C2-ACBA-E5D5F1624334");

		// Token: 0x04000008 RID: 8
		private static readonly ManufacturerInfo McjManufacturerInfo = new ManufacturerInfo(Resources.FriendlyName_Manufacturer, Resources.HP_logo);

		// Token: 0x04000009 RID: 9
		private readonly IMtpDeviceInfoProvider mtpDeviceInfoProvider;

		// Token: 0x0400000A RID: 10
		private readonly ManufacturerModelsCatalog catalog;
	}
}
