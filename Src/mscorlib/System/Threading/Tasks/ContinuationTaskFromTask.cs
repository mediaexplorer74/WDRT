using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000568 RID: 1384
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x0600417D RID: 16765 RVA: 0x000F59F8 File Offset: 0x000F3BF8
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
			: base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000F5A34 File Offset: 0x000F3C34
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001B5A RID: 7002
		private Task m_antecedent;
	}
}
