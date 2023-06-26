using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000034 RID: 52
	internal static class TimeStampUtility
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public static long CreateTimeStamp(DateTime date)
		{
			return (long)(date - TimeStampUtility.UnixTimeStampRefTime).TotalMilliseconds;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public static long CreateTimeStamp()
		{
			return TimeStampUtility.CreateTimeStamp(DateTime.UtcNow);
		}

		// Token: 0x0400014C RID: 332
		private static readonly DateTime UnixTimeStampRefTime = new DateTime(1970, 1, 1, 0, 0, 0);
	}
}
