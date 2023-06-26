using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class TrekStorModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreatePanasonic_WE81H_FZ_E1BModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false),
				new DeviceDetectionInformation(new VidPidPair("045E", "062A"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "WinPhone 5.0 LTE" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_TrekStor_WinPhone50LTE, identificationInfo, TrekStorMsrQuery.TrekStor_Winphone5_0_LTE);
			return new ModelInfo(Resources.FriendlyName_TrekStor_WinPhone50LTE, Resources.WinPhone_4_7_HD_wp47, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo TrekStor_Winphone_5_0_LTE = TrekStorModels.CreatePanasonic_WE81H_FZ_E1BModelInfo();
	}
}
