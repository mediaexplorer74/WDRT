using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000518 RID: 1304
	internal static class ThreadPoolGlobals
	{
		// Token: 0x04001A08 RID: 6664
		public static uint tpQuantum = 30U;

		// Token: 0x04001A09 RID: 6665
		public static int processorCount = Environment.ProcessorCount;

		// Token: 0x04001A0A RID: 6666
		public static bool tpHosted = ThreadPool.IsThreadPoolHosted();

		// Token: 0x04001A0B RID: 6667
		public static volatile bool vmTpInitialized;

		// Token: 0x04001A0C RID: 6668
		public static bool enableWorkerTracking;

		// Token: 0x04001A0D RID: 6669
		[SecurityCritical]
		public static ThreadPoolWorkQueue workQueue = new ThreadPoolWorkQueue();
	}
}
