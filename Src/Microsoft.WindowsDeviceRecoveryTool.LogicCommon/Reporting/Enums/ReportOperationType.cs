using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums
{
	// Token: 0x02000023 RID: 35
	public enum ReportOperationType
	{
		// Token: 0x04000108 RID: 264
		Flashing,
		// Token: 0x04000109 RID: 265
		Recovery,
		// Token: 0x0400010A RID: 266
		ReadDeviceInfo,
		// Token: 0x0400010B RID: 267
		ReadDeviceInfoWithThor,
		// Token: 0x0400010C RID: 268
		DownloadPackage,
		// Token: 0x0400010D RID: 269
		CheckPackage,
		// Token: 0x0400010E RID: 270
		EmergencyFlashing,
		// Token: 0x0400010F RID: 271
		RecoveryAfterEmergencyFlashing,
		// Token: 0x04000110 RID: 272
		ReadInfoAfterEmergencyFlashing,
		// Token: 0x04000111 RID: 273
		DownloadEmergencyPackage
	}
}
