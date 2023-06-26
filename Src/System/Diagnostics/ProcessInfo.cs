using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004EF RID: 1263
	internal class ProcessInfo
	{
		// Token: 0x0400284D RID: 10317
		public ArrayList threadInfoList = new ArrayList();

		// Token: 0x0400284E RID: 10318
		public int basePriority;

		// Token: 0x0400284F RID: 10319
		public string processName;

		// Token: 0x04002850 RID: 10320
		public int processId;

		// Token: 0x04002851 RID: 10321
		public int handleCount;

		// Token: 0x04002852 RID: 10322
		public long poolPagedBytes;

		// Token: 0x04002853 RID: 10323
		public long poolNonpagedBytes;

		// Token: 0x04002854 RID: 10324
		public long virtualBytes;

		// Token: 0x04002855 RID: 10325
		public long virtualBytesPeak;

		// Token: 0x04002856 RID: 10326
		public long workingSetPeak;

		// Token: 0x04002857 RID: 10327
		public long workingSet;

		// Token: 0x04002858 RID: 10328
		public long pageFileBytesPeak;

		// Token: 0x04002859 RID: 10329
		public long pageFileBytes;

		// Token: 0x0400285A RID: 10330
		public long privateBytes;

		// Token: 0x0400285B RID: 10331
		public int mainModuleId;

		// Token: 0x0400285C RID: 10332
		public int sessionId;
	}
}
