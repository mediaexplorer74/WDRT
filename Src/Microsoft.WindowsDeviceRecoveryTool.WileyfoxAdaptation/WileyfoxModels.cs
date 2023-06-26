using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.WileyfoxAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.WileyfoxAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class WileyfoxModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreateWileyfox_Pro_ModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "Pro" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_Wileyfox_Pro, identificationInfo, WileyfoxMsrQuery.Wileyfox_Pro);
			return new ModelInfo(Resources.FriendlyName_Wileyfox_Pro, Resources.Wileyfox_Pro_mobileImage, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo Wileyfox_Pro = WileyfoxModels.CreateWileyfox_Pro_ModelInfo();
	}
}
