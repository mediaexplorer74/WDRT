using System;
using System.ComponentModel;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x0200003A RID: 58
	public class WorkerHelper : IDisposable
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public WorkerHelper(DoWorkEventHandler workEventHandler)
		{
			this.backgroundWorker = new BackgroundWorker();
			this.backgroundWorker.DoWork += workEventHandler;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000D114 File Offset: 0x0000B314
		public bool IsBusy
		{
			get
			{
				return this.backgroundWorker.IsBusy;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D131 File Offset: 0x0000B331
		public void RunWorkerAsync()
		{
			this.backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D140 File Offset: 0x0000B340
		public void Dispose()
		{
			bool flag = this.backgroundWorker != null;
			if (flag)
			{
				this.backgroundWorker.Dispose();
			}
		}

		// Token: 0x04000185 RID: 389
		private readonly BackgroundWorker backgroundWorker;
	}
}
