using System;

namespace SoftwareRepository.Streaming
{
	// Token: 0x0200000F RID: 15
	public class DownloadProgress<T> : IProgress<T>
	{
		// Token: 0x0600005C RID: 92 RVA: 0x0000355C File Offset: 0x0000175C
		public DownloadProgress(Action<T> action)
		{
			bool flag = action == null;
			if (flag)
			{
				throw new ArgumentNullException("action");
			}
			this.action = action;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000358C File Offset: 0x0000178C
		public void Report(T value)
		{
			this.action(value);
		}

		// Token: 0x0400003D RID: 61
		private readonly Action<T> action;
	}
}
