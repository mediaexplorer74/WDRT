using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Represents an object that handles the low-level work of queuing tasks onto threads.</summary>
	// Token: 0x02000575 RID: 1397
	[DebuggerDisplay("Id={Id}")]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class TaskScheduler
	{
		/// <summary>Queues a <see cref="T:System.Threading.Tasks.Task" /> to the scheduler.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be queued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x06004219 RID: 16921
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected internal abstract void QueueTask(Task task);

		/// <summary>Determines whether the provided <see cref="T:System.Threading.Tasks.Task" /> can be executed synchronously in this call, and if it can, executes it.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be executed.</param>
		/// <param name="taskWasPreviouslyQueued">A Boolean denoting whether or not task has previously been queued. If this parameter is True, then the task may have been previously queued (scheduled); if False, then the task is known not to have been queued, and this call is being made in order to execute the task inline without queuing it.</param>
		/// <returns>A Boolean value indicating whether the task was executed inline.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> was already executed.</exception>
		// Token: 0x0600421A RID: 16922
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		/// <summary>For debugger support only, generates an enumerable of <see cref="T:System.Threading.Tasks.Task" /> instances currently queued to the scheduler waiting to be executed.</summary>
		/// <returns>An enumerable that allows a debugger to traverse the tasks currently queued to this scheduler.</returns>
		/// <exception cref="T:System.NotSupportedException">This scheduler is unable to generate a list of queued tasks at this time.</exception>
		// Token: 0x0600421B RID: 16923
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected abstract IEnumerable<Task> GetScheduledTasks();

		/// <summary>Indicates the maximum concurrency level this <see cref="T:System.Threading.Tasks.TaskScheduler" /> is able to support.</summary>
		/// <returns>Returns an integer that represents the maximum concurrency level. The default scheduler returns <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x000F78A6 File Offset: 0x000F5AA6
		[__DynamicallyInvokable]
		public virtual int MaximumConcurrencyLevel
		{
			[__DynamicallyInvokable]
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000F78B0 File Offset: 0x000F5AB0
		[SecuritySafeCritical]
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			StackGuard currentStackGuard;
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
			{
				return false;
			}
			bool flag = false;
			try
			{
				task.FireTaskScheduledIfNeeded(this);
				flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			}
			finally
			{
				currentStackGuard.EndInliningScope();
			}
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_InconsistentStateAfterTryExecuteTaskInline"));
			}
			return flag;
		}

		/// <summary>Attempts to dequeue a <see cref="T:System.Threading.Tasks.Task" /> that was previously queued to this scheduler.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be dequeued.</param>
		/// <returns>A Boolean denoting whether the <paramref name="task" /> argument was successfully dequeued.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x0600421E RID: 16926 RVA: 0x000F7950 File Offset: 0x000F5B50
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000F7953 File Offset: 0x000F5B53
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000F7955 File Offset: 0x000F5B55
		internal virtual bool RequiresAtomicStartTransition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000F7958 File Offset: 0x000F5B58
		[SecurityCritical]
		internal void InternalQueueTask(Task task)
		{
			task.FireTaskScheduledIfNeeded(this);
			this.QueueTask(task);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		// Token: 0x06004222 RID: 16930 RVA: 0x000F7969 File Offset: 0x000F5B69
		[__DynamicallyInvokable]
		protected TaskScheduler()
		{
			if (Debugger.IsAttached)
			{
				this.AddToActiveTaskSchedulers();
			}
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000F7980 File Offset: 0x000F5B80
		private void AddToActiveTaskSchedulers()
		{
			ConditionalWeakTable<TaskScheduler, object> conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			if (conditionalWeakTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), null);
				conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			}
			conditionalWeakTable.Add(this, null);
		}

		/// <summary>Gets the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance that is provided by the .NET Framework.</summary>
		/// <returns>Returns the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance.</returns>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000F79B5 File Offset: 0x000F5BB5
		[__DynamicallyInvokable]
		public static TaskScheduler Default
		{
			[__DynamicallyInvokable]
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</summary>
		/// <returns>Returns the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</returns>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000F79BC File Offset: 0x000F5BBC
		[__DynamicallyInvokable]
		public static TaskScheduler Current
		{
			[__DynamicallyInvokable]
			get
			{
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000F79DC File Offset: 0x000F5BDC
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />, as determined by <see cref="P:System.Threading.SynchronizationContext.Current" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current SynchronizationContext may not be used as a TaskScheduler.</exception>
		// Token: 0x06004227 RID: 16935 RVA: 0x000F7A05 File Offset: 0x000F5C05
		[__DynamicallyInvokable]
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		/// <summary>Gets the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <returns>Returns the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x000F7A0C File Offset: 0x000F5C0C
		[__DynamicallyInvokable]
		public int Id
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		/// <summary>Attempts to execute the provided <see cref="T:System.Threading.Tasks.Task" /> on this scheduler.</summary>
		/// <param name="task">A <see cref="T:System.Threading.Tasks.Task" /> object to be executed.</param>
		/// <returns>A Boolean that is true if <paramref name="task" /> was successfully executed, false if it was not. A common reason for execution failure is that the task had previously been executed or is in the process of being executed by another thread.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> is not associated with this scheduler.</exception>
		// Token: 0x06004229 RID: 16937 RVA: 0x000F7A49 File Offset: 0x000F5C49
		[SecurityCritical]
		[__DynamicallyInvokable]
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_ExecuteTask_WrongTaskScheduler"));
			}
			return task.ExecuteEntry(true);
		}

		/// <summary>Occurs when a faulted task's unobserved exception is about to trigger exception escalation policy, which, by default, would terminate the process.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600422A RID: 16938 RVA: 0x000F7A6C File Offset: 0x000F5C6C
		// (remove) Token: 0x0600422B RID: 16939 RVA: 0x000F7AC4 File Offset: 0x000F5CC4
		[__DynamicallyInvokable]
		public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			add
			{
				if (value != null)
				{
					RuntimeHelpers.PrepareContractedDelegate(value);
					object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
					lock (unobservedTaskExceptionLockObject)
					{
						TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Combine(TaskScheduler._unobservedTaskException, value);
					}
				}
			}
			[SecurityCritical]
			[__DynamicallyInvokable]
			remove
			{
				object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
				lock (unobservedTaskExceptionLockObject)
				{
					TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Remove(TaskScheduler._unobservedTaskException, value);
				}
			}
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000F7B14 File Offset: 0x000F5D14
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
			lock (unobservedTaskExceptionLockObject)
			{
				EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
				if (unobservedTaskException != null)
				{
					unobservedTaskException(sender, ueea);
				}
			}
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000F7B60 File Offset: 0x000F5D60
		[SecurityCritical]
		internal Task[] GetScheduledTasksForDebugger()
		{
			IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
			if (scheduledTasks == null)
			{
				return null;
			}
			Task[] array = scheduledTasks as Task[];
			if (array == null)
			{
				array = new List<Task>(scheduledTasks).ToArray();
			}
			foreach (Task task in array)
			{
				int id = task.Id;
			}
			return array;
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000F7BB0 File Offset: 0x000F5DB0
		[SecurityCritical]
		internal static TaskScheduler[] GetTaskSchedulersForDebugger()
		{
			if (TaskScheduler.s_activeTaskSchedulers == null)
			{
				return new TaskScheduler[] { TaskScheduler.s_defaultTaskScheduler };
			}
			ICollection<TaskScheduler> keys = TaskScheduler.s_activeTaskSchedulers.Keys;
			if (!keys.Contains(TaskScheduler.s_defaultTaskScheduler))
			{
				keys.Add(TaskScheduler.s_defaultTaskScheduler);
			}
			TaskScheduler[] array = new TaskScheduler[keys.Count];
			keys.CopyTo(array, 0);
			foreach (TaskScheduler taskScheduler in array)
			{
				int id = taskScheduler.Id;
			}
			return array;
		}

		// Token: 0x04001B75 RID: 7029
		private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;

		// Token: 0x04001B76 RID: 7030
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04001B77 RID: 7031
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04001B78 RID: 7032
		private volatile int m_taskSchedulerId;

		// Token: 0x04001B79 RID: 7033
		private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

		// Token: 0x04001B7A RID: 7034
		private static readonly object _unobservedTaskExceptionLockObject = new object();

		// Token: 0x02000C1D RID: 3101
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
			// Token: 0x06007017 RID: 28695 RVA: 0x00183DDA File Offset: 0x00181FDA
			public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
			{
				this.m_taskScheduler = scheduler;
			}

			// Token: 0x1700132C RID: 4908
			// (get) Token: 0x06007018 RID: 28696 RVA: 0x00183DE9 File Offset: 0x00181FE9
			public int Id
			{
				get
				{
					return this.m_taskScheduler.Id;
				}
			}

			// Token: 0x1700132D RID: 4909
			// (get) Token: 0x06007019 RID: 28697 RVA: 0x00183DF6 File Offset: 0x00181FF6
			public IEnumerable<Task> ScheduledTasks
			{
				[SecurityCritical]
				get
				{
					return this.m_taskScheduler.GetScheduledTasks();
				}
			}

			// Token: 0x040036D3 RID: 14035
			private readonly TaskScheduler m_taskScheduler;
		}
	}
}
