using System;

namespace System.Diagnostics
{
	// Token: 0x020004F0 RID: 1264
	internal class ThreadInfo
	{
		// Token: 0x0400285D RID: 10333
		public int threadId;

		// Token: 0x0400285E RID: 10334
		public int processId;

		// Token: 0x0400285F RID: 10335
		public int basePriority;

		// Token: 0x04002860 RID: 10336
		public int currentPriority;

		// Token: 0x04002861 RID: 10337
		public IntPtr startAddress;

		// Token: 0x04002862 RID: 10338
		public ThreadState threadState;

		// Token: 0x04002863 RID: 10339
		public ThreadWaitReason threadWaitReason;
	}
}
