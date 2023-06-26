using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000017 RID: 23
	public class ServiceBreak
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000574D File Offset: 0x0000394D
		public ServiceBreak(DateTime start, DateTime end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005767 File Offset: 0x00003967
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000576F File Offset: 0x0000396F
		public DateTime Start { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005778 File Offset: 0x00003978
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005780 File Offset: 0x00003980
		public DateTime End { get; private set; }
	}
}
