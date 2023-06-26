using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.TrinityAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.TrinityAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class TrinityModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreateNeoModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "NEO" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_NEO, identificationInfo, TrinityMsrQuery.Neo);
			return new ModelInfo(Resources.FriendlyName_NEO, Resources.Neo_Device_front, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo Neo = TrinityModels.CreateNeoModelInfo();
	}
}
