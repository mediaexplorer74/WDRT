using System;

namespace System.Diagnostics
{
	// Token: 0x02000504 RID: 1284
	internal class ProcessData
	{
		// Token: 0x060030D7 RID: 12503 RVA: 0x000DDB5F File Offset: 0x000DBD5F
		public ProcessData(int pid, long startTime)
		{
			this.ProcessId = pid;
			this.StartupTime = startTime;
		}

		// Token: 0x040028BD RID: 10429
		public int ProcessId;

		// Token: 0x040028BE RID: 10430
		public long StartupTime;
	}
}
