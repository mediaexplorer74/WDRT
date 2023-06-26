using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200055D RID: 1373
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x0600415D RID: 16733 RVA: 0x000F54CD File Offset: 0x000F36CD
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000F54E3 File Offset: 0x000F36E3
		[SecurityCritical]
		public void ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000F54F6 File Offset: 0x000F36F6
		[SecurityCritical]
		public void MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x04001B27 RID: 6951
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04001B28 RID: 6952
		private readonly Task m_completingTask;
	}
}
