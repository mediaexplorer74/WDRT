using System;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.OemAdaptation.Primitives;
using Microsoft.WindowsDeviceRecoveryTool.VAIOAdaptation.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.VAIOAdaptation
{
	// Token: 0x02000002 RID: 2
	internal static class VAIOModels
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ModelInfo CreateNeoModelInfo()
		{
			DetectionInfo detectionInfo = new DetectionInfo(new DeviceDetectionInformation[]
			{
				new DeviceDetectionInformation(new VidPidPair("045E", "F0CA"), false)
			});
			IdentificationInfo identificationInfo = new IdentificationInfo(new string[] { "VPB051" });
			VariantInfo variantInfo = new VariantInfo(Resources.FriendlyName_VPB051, identificationInfo, VAIOMsrQuery.VPB0511S);
			return new ModelInfo(Resources.FriendlyName_VPB051, Resources.VAIO_Phone_Biz_VPB0511S, detectionInfo, new VariantInfo[] { variantInfo });
		}

		// Token: 0x04000001 RID: 1
		public static readonly ModelInfo VPB0511S = VAIOModels.CreateNeoModelInfo();
	}
}
