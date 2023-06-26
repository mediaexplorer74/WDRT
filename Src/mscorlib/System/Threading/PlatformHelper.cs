using System;

namespace System.Threading
{
	// Token: 0x02000537 RID: 1335
	internal static class PlatformHelper
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000EB184 File Offset: 0x000E9384
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				int num = PlatformHelper.s_processorCount;
				if (num == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
				{
					num = (PlatformHelper.s_processorCount = Environment.ProcessorCount);
					PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
				}
				return num;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x000EB1C9 File Offset: 0x000E93C9
		internal static bool IsSingleProcessor
		{
			get
			{
				return PlatformHelper.ProcessorCount == 1;
			}
		}

		// Token: 0x04001A6E RID: 6766
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x04001A6F RID: 6767
		private static volatile int s_processorCount;

		// Token: 0x04001A70 RID: 6768
		private static volatile int s_lastProcessorCountRefreshTicks;
	}
}
