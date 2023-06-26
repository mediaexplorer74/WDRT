using System;

namespace Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic
{
	// Token: 0x020000E5 RID: 229
	internal static class ApplicationBuildSettings
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0002157C File Offset: 0x0001F77C
		public static DateTime ExpirationDate
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00021594 File Offset: 0x0001F794
		public static bool SkipApplicationUpdate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000215A8 File Offset: 0x0001F7A8
		public static int ApplicationId
		{
			get
			{
				return 3901;
			}
		}
	}
}
