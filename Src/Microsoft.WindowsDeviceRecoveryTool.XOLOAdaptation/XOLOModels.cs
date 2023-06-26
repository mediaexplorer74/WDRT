using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.XOLOAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.XOLOAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class XOLOModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreateNeoModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "Win-Q900S" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_Win_Q900S, identificationInfo, XOLOMsrQuery.Win_Q900s);
			return new ModelInfo(Resources.FriendlyName_Win_Q900S, Resources.Xolo_Win_Q900s_f2, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo Win_Q900s = XOLOModels.CreateNeoModelInfo();
	}
}
