using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200051F RID: 1311
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x06003DFA RID: 15866 RVA: 0x000E8C91 File Offset: 0x000E6E91
		[SecurityCritical]
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
