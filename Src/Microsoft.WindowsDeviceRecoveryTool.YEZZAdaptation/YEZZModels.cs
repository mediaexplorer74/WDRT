using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.YEZZAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.YEZZAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class YEZZModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreateNeoModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "Billy 4.7" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_Billy, identificationInfo, YEZZMsrQuery.Billy47);
			return new ModelInfo(Resources.FriendlyName_Billy, Resources.yezz_billy_4_7_hero_image, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo Billy_4_7 = YEZZModels.CreateNeoModelInfo();
	}
}
