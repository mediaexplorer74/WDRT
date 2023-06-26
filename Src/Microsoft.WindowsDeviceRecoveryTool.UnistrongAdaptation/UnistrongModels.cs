using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.UnistrongAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.UnistrongAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class UnistrongModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreateNeoModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false),
				new DeviceDetectionInformation(new VidPidPair("05C6", "9093"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "T536", "EE7736A2" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_T536, identificationInfo, UnistrongMsrQuery.T536);
			return new ModelInfo(Resources.FriendlyName_T536, Resources.DevicePicture_7739, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo T536 = UnistrongModels.CreateNeoModelInfo();
	}
}
