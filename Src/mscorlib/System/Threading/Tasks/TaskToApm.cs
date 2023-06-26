using System;
using System.IO;

namespace System.Threading.Tasks
{
	// Token: 0x02000587 RID: 1415
	internal static class TaskToApm
	{
		// Token: 0x060042BA RID: 17082 RVA: 0x000F9C4C File Offset: 0x000F7E4C
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x000F9C9C File Offset: 0x000F7E9C
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x000F9CDC File Offset: 0x000F7EDC
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x000F9D20 File Offset: 0x000F7F20
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000C2E RID: 3118
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06007049 RID: 28745 RVA: 0x0018446A File Offset: 0x0018266A
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this.m_state = state;
				this.m_completedSynchronously = completedSynchronously;
			}

			// Token: 0x1700133A RID: 4922
			// (get) Token: 0x0600704A RID: 28746 RVA: 0x00184487 File Offset: 0x00182687
			object IAsyncResult.AsyncState
			{
				get
				{
					return this.m_state;
				}
			}

			// Token: 0x1700133B RID: 4923
			// (get) Token: 0x0600704B RID: 28747 RVA: 0x0018448F File Offset: 0x0018268F
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this.m_completedSynchronously;
				}
			}

			// Token: 0x1700133C RID: 4924
			// (get) Token: 0x0600704C RID: 28748 RVA: 0x00184497 File Offset: 0x00182697
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x1700133D RID: 4925
			// (get) Token: 0x0600704D RID: 28749 RVA: 0x001844A4 File Offset: 0x001826A4
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x0400371D RID: 14109
			internal readonly Task Task;

			// Token: 0x0400371E RID: 14110
			private readonly object m_state;

			// Token: 0x0400371F RID: 14111
			private readonly bool m_completedSynchronously;
		}
	}
}
