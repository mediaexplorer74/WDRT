using System;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000567 RID: 1383
	internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
	{
		// Token: 0x06004176 RID: 16758 RVA: 0x000F5750 File Offset: 0x000F3950
		public UnwrapPromise(Task outerTask, bool lookForOce)
			: base(null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
		{
			this._lookForOce = lookForOce;
			this._state = 0;
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.Unwrap", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (outerTask.IsCompleted)
			{
				this.ProcessCompletedOuterTask(outerTask);
				return;
			}
			outerTask.AddCompletionAction(this);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000F57BC File Offset: 0x000F39BC
		public void Invoke(Task completingTask)
		{
			StackGuard currentStackGuard = Task.CurrentStackGuard;
			if (currentStackGuard.TryBeginInliningScope())
			{
				try
				{
					this.InvokeCore(completingTask);
					return;
				}
				finally
				{
					currentStackGuard.EndInliningScope();
				}
			}
			this.InvokeCoreAsync(completingTask);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000F5800 File Offset: 0x000F3A00
		private void InvokeCore(Task completingTask)
		{
			byte state = this._state;
			if (state == 0)
			{
				this.ProcessCompletedOuterTask(completingTask);
				return;
			}
			if (state != 1)
			{
				return;
			}
			bool flag = this.TrySetFromTask(completingTask, false);
			this._state = 2;
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000F5834 File Offset: 0x000F3A34
		[SecuritySafeCritical]
		private void InvokeCoreAsync(Task completingTask)
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}, Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000F5864 File Offset: 0x000F3A64
		private void ProcessCompletedOuterTask(Task task)
		{
			this._state = 1;
			TaskStatus status = task.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status - TaskStatus.Canceled <= 1)
				{
					bool flag = this.TrySetFromTask(task, this._lookForOce);
					return;
				}
			}
			else
			{
				Task<Task<TResult>> task2 = task as Task<Task<TResult>>;
				this.ProcessInnerTask((task2 != null) ? task2.Result : ((Task<Task>)task).Result);
			}
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000F58BC File Offset: 0x000F3ABC
		private bool TrySetFromTask(Task task, bool lookForOce)
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
			}
			bool flag = false;
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				Task<TResult> task2 = task as Task<TResult>;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(base.Id);
				}
				flag = base.TrySetResult((task2 != null) ? task2.Result : default(TResult));
				break;
			}
			case TaskStatus.Canceled:
				flag = base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
				break;
			case TaskStatus.Faulted:
			{
				ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
				ExceptionDispatchInfo exceptionDispatchInfo;
				OperationCanceledException ex;
				if (lookForOce && exceptionDispatchInfos.Count > 0 && (exceptionDispatchInfo = exceptionDispatchInfos[0]) != null && (ex = exceptionDispatchInfo.SourceException as OperationCanceledException) != null)
				{
					flag = base.TrySetCanceled(ex.CancellationToken, exceptionDispatchInfo);
				}
				else
				{
					flag = base.TrySetException(exceptionDispatchInfos);
				}
				break;
			}
			}
			return flag;
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000F59B0 File Offset: 0x000F3BB0
		private void ProcessInnerTask(Task task)
		{
			if (task == null)
			{
				base.TrySetCanceled(default(CancellationToken));
				this._state = 2;
				return;
			}
			if (task.IsCompleted)
			{
				this.TrySetFromTask(task, false);
				this._state = 2;
				return;
			}
			task.AddCompletionAction(this);
		}

		// Token: 0x04001B55 RID: 6997
		private const byte STATE_WAITING_ON_OUTER_TASK = 0;

		// Token: 0x04001B56 RID: 6998
		private const byte STATE_WAITING_ON_INNER_TASK = 1;

		// Token: 0x04001B57 RID: 6999
		private const byte STATE_DONE = 2;

		// Token: 0x04001B58 RID: 7000
		private byte _state;

		// Token: 0x04001B59 RID: 7001
		private readonly bool _lookForOce;
	}
}
