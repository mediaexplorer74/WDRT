using System;

namespace System.Threading
{
	// Token: 0x02000538 RID: 1336
	internal static class TimeoutHelper
	{
		// Token: 0x06003EDB RID: 16091 RVA: 0x000EB1D3 File Offset: 0x000E93D3
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x000EB1DC File Offset: 0x000E93DC
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
