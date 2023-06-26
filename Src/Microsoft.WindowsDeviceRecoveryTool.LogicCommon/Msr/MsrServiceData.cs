using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr
{
	// Token: 0x0200002A RID: 42
	internal class MsrServiceData
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00009EFB File Offset: 0x000080FB
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00009F03 File Offset: 0x00008103
		public string ApiUrl { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00009F0C File Offset: 0x0000810C
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00009F14 File Offset: 0x00008114
		public string UploadApiUrl { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00009F1D File Offset: 0x0000811D
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00009F25 File Offset: 0x00008125
		public string UserAgent { get; set; }

		// Token: 0x060002C6 RID: 710 RVA: 0x00009F30 File Offset: 0x00008130
		public static MsrServiceData CreateServiceData()
		{
			return new MsrServiceData
			{
				ApiUrl = "https://api.swrepository.com/rest-api/",
				UploadApiUrl = "https://api.swrepository.com/rest-api/report/1/uploadlocation",
				UserAgent = "Microsoft-Windows Device Recovery Tool"
			};
		}
	}
}
