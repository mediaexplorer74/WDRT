using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides task schedulers that coordinate to execute tasks while ensuring that concurrent tasks may run concurrently and exclusive tasks never do.</summary>
	// Token: 0x0200057F RID: 1407
	[DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
	[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.DebugView))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ConcurrentExclusiveSchedulerPair
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x000F83BC File Offset: 0x000F65BC
		private static int DefaultMaxConcurrencyLevel
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x000F83C3 File Offset: 0x000F65C3
		private object ValueLock
		{
			get
			{
				return this.m_threadProcessingMapping;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class.</summary>
		// Token: 0x06004262 RID: 16994 RVA: 0x000F83CB File Offset: 0x000F65CB
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair()
			: this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class that targets the specified scheduler.</summary>
		/// <param name="taskScheduler">The target scheduler on which this pair should execute.</param>
		// Token: 0x06004263 RID: 16995 RVA: 0x000F83DE File Offset: 0x000F65DE
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler)
			: this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class that targets the specified scheduler with a maximum concurrency level.</summary>
		/// <param name="taskScheduler">The target scheduler on which this pair should execute.</param>
		/// <param name="maxConcurrencyLevel">The maximum number of tasks to run concurrently.</param>
		// Token: 0x06004264 RID: 16996 RVA: 0x000F83ED File Offset: 0x000F65ED
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel)
			: this(taskScheduler, maxConcurrencyLevel, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class that targets the specified scheduler with a maximum concurrency level and a maximum number of scheduled tasks that may be processed as a unit.</summary>
		/// <param name="taskScheduler">The target scheduler on which this pair should execute.</param>
		/// <param name="maxConcurrencyLevel">The maximum number of tasks to run concurrently.</param>
		/// <param name="maxItemsPerTask">The maximum number of tasks to process for each underlying scheduled task used by the pair.</param>
		// Token: 0x06004265 RID: 16997 RVA: 0x000F83F8 File Offset: 0x000F65F8
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
		{
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
			{
				throw new ArgumentOutOfRangeException("maxItemsPerTask");
			}
			this.m_underlyingTaskScheduler = taskScheduler;
			this.m_maxConcurrencyLevel = maxConcurrencyLevel;
			this.m_maxItemsPerTask = maxItemsPerTask;
			int maximumConcurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
			if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel < this.m_maxConcurrencyLevel)
			{
				this.m_maxConcurrencyLevel = maximumConcurrencyLevel;
			}
			if (this.m_maxConcurrencyLevel == -1)
			{
				this.m_maxConcurrencyLevel = int.MaxValue;
			}
			if (this.m_maxItemsPerTask == -1)
			{
				this.m_maxItemsPerTask = int.MaxValue;
			}
			this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
			this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
		}

		/// <summary>Informs the scheduler pair that it should not accept any more tasks.</summary>
		// Token: 0x06004266 RID: 16998 RVA: 0x000F84C4 File Offset: 0x000F66C4
		[__DynamicallyInvokable]
		public void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (!this.CompletionRequested)
				{
					this.RequestCompletion();
					this.CleanupStateIfCompletingAndQuiesced();
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.Tasks.Task" /> that will complete when the scheduler has completed processing.</summary>
		/// <returns>The asynchronous operation that will complete when the scheduler finishes processing.</returns>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000F8514 File Offset: 0x000F6714
		[__DynamicallyInvokable]
		public Task Completion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.EnsureCompletionStateInitialized().Task;
			}
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x000F8521 File Offset: 0x000F6721
		private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
		{
			return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, () => new ConcurrentExclusiveSchedulerPair.CompletionState());
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x000F854D File Offset: 0x000F674D
		private bool CompletionRequested
		{
			get
			{
				return this.m_completionState != null && Volatile.Read(ref this.m_completionState.m_completionRequested);
			}
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000F8569 File Offset: 0x000F6769
		private void RequestCompletion()
		{
			this.EnsureCompletionStateInitialized().m_completionRequested = true;
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000F8577 File Offset: 0x000F6777
		private void CleanupStateIfCompletingAndQuiesced()
		{
			if (this.ReadyToComplete)
			{
				this.CompleteTaskAsync();
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x000F8588 File Offset: 0x000F6788
		private bool ReadyToComplete
		{
			get
			{
				if (!this.CompletionRequested || this.m_processingCount != 0)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
				return (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0) || (this.m_concurrentTaskScheduler.m_tasks.IsEmpty && this.m_exclusiveTaskScheduler.m_tasks.IsEmpty);
			}
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000F85EC File Offset: 0x000F67EC
		private void CompleteTaskAsync()
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (!completionState.m_completionQueued)
			{
				completionState.m_completionQueued = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					ConcurrentExclusiveSchedulerPair.CompletionState completionState2 = (ConcurrentExclusiveSchedulerPair.CompletionState)state;
					List<Exception> exceptions = completionState2.m_exceptions;
					if (exceptions == null || exceptions.Count <= 0)
					{
						completionState2.TrySetResult(default(VoidTaskResult));
					}
					else
					{
						completionState2.TrySetException(exceptions);
					}
				}, completionState);
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000F8638 File Offset: 0x000F6838
		private void FaultWithTask(Task faultedTask)
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (completionState.m_exceptions == null)
			{
				completionState.m_exceptions = new List<Exception>();
			}
			completionState.m_exceptions.AddRange(faultedTask.Exception.InnerExceptions);
			this.RequestCompletion();
		}

		/// <summary>Gets a <see cref="T:System.Threading.Tasks.TaskScheduler" /> that can be used to schedule tasks to this pair that may run concurrently with other tasks on this pair.</summary>
		/// <returns>An object that can be used to schedule tasks concurrently.</returns>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x000F867B File Offset: 0x000F687B
		[__DynamicallyInvokable]
		public TaskScheduler ConcurrentScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_concurrentTaskScheduler;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.Tasks.TaskScheduler" /> that can be used to schedule tasks to this pair that must run exclusively with regards to other tasks on this pair.</summary>
		/// <returns>An object that can be used to schedule tasks that do not run concurrently with other tasks.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x000F8683 File Offset: 0x000F6883
		[__DynamicallyInvokable]
		public TaskScheduler ExclusiveScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_exclusiveTaskScheduler;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000F868B File Offset: 0x000F688B
		private int ConcurrentTaskCountForDebugger
		{
			get
			{
				return this.m_concurrentTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x000F869D File Offset: 0x000F689D
		private int ExclusiveTaskCountForDebugger
		{
			get
			{
				return this.m_exclusiveTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x000F86B0 File Offset: 0x000F68B0
		private void ProcessAsyncIfNecessary(bool fairly = false)
		{
			if (this.m_processingCount >= 0)
			{
				bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
				Task task = null;
				if (this.m_processingCount == 0 && flag)
				{
					this.m_processingCount = -1;
					try
					{
						task = new Task(delegate(object thisPair)
						{
							((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
						}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
						task.Start(this.m_underlyingTaskScheduler);
						goto IL_149;
					}
					catch
					{
						this.m_processingCount = 0;
						this.FaultWithTask(task);
						goto IL_149;
					}
				}
				int count = this.m_concurrentTaskScheduler.m_tasks.Count;
				if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
				{
					int num = 0;
					while (num < count && this.m_processingCount < this.m_maxConcurrencyLevel)
					{
						this.m_processingCount++;
						try
						{
							task = new Task(delegate(object thisPair)
							{
								((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
							}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
							task.Start(this.m_underlyingTaskScheduler);
						}
						catch
						{
							this.m_processingCount--;
							this.FaultWithTask(task);
						}
						num++;
					}
				}
				IL_149:
				this.CleanupStateIfCompletingAndQuiesced();
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000F8828 File Offset: 0x000F6A28
		private void ProcessExclusiveTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_exclusiveTaskScheduler.ExecuteTask(task);
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this.m_processingCount = 0;
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x000F88EC File Offset: 0x000F6AEC
		private void ProcessConcurrentTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_concurrentTaskScheduler.ExecuteTask(task);
					}
					if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
					{
						break;
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this.m_processingCount > 0)
					{
						this.m_processingCount--;
					}
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x000F89D8 File Offset: 0x000F6BD8
		private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
		{
			get
			{
				if (this.m_completionState != null && this.m_completionState.Task.IsCompleted)
				{
					return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				if (this.m_processingCount == -1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				}
				if (this.m_processingCount >= 1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				}
				if (this.CompletionRequested)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
				}
				return processingMode;
			}
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000F8A2A File Offset: 0x000F6C2A
		[Conditional("DEBUG")]
		internal static void ContractAssertMonitorStatus(object syncObj, bool held)
		{
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000F8A2C File Offset: 0x000F6C2C
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x04001B98 RID: 7064
		private readonly ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMapping = new ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode>();

		// Token: 0x04001B99 RID: 7065
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;

		// Token: 0x04001B9A RID: 7066
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;

		// Token: 0x04001B9B RID: 7067
		private readonly TaskScheduler m_underlyingTaskScheduler;

		// Token: 0x04001B9C RID: 7068
		private readonly int m_maxConcurrencyLevel;

		// Token: 0x04001B9D RID: 7069
		private readonly int m_maxItemsPerTask;

		// Token: 0x04001B9E RID: 7070
		private int m_processingCount;

		// Token: 0x04001B9F RID: 7071
		private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;

		// Token: 0x04001BA0 RID: 7072
		private const int UNLIMITED_PROCESSING = -1;

		// Token: 0x04001BA1 RID: 7073
		private const int EXCLUSIVE_PROCESSING_SENTINEL = -1;

		// Token: 0x04001BA2 RID: 7074
		private const int DEFAULT_MAXITEMSPERTASK = -1;

		// Token: 0x02000C20 RID: 3104
		private sealed class CompletionState : TaskCompletionSource<VoidTaskResult>
		{
			// Token: 0x040036DD RID: 14045
			internal bool m_completionRequested;

			// Token: 0x040036DE RID: 14046
			internal bool m_completionQueued;

			// Token: 0x040036DF RID: 14047
			internal List<Exception> m_exceptions;
		}

		// Token: 0x02000C21 RID: 3105
		[DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
		[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
		private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
		{
			// Token: 0x06007024 RID: 28708 RVA: 0x00183F8C File Offset: 0x0018218C
			internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
			{
				this.m_pair = pair;
				this.m_maxConcurrencyLevel = maxConcurrencyLevel;
				this.m_processingMode = processingMode;
				IProducerConsumerQueue<Task> producerConsumerQueue2;
				if (processingMode != ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask)
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new MultiProducerMultiConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				else
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new SingleProducerSingleConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				this.m_tasks = producerConsumerQueue2;
			}

			// Token: 0x17001330 RID: 4912
			// (get) Token: 0x06007025 RID: 28709 RVA: 0x00183FCE File Offset: 0x001821CE
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.m_maxConcurrencyLevel;
				}
			}

			// Token: 0x06007026 RID: 28710 RVA: 0x00183FD8 File Offset: 0x001821D8
			[SecurityCritical]
			protected internal override void QueueTask(Task task)
			{
				object valueLock = this.m_pair.ValueLock;
				lock (valueLock)
				{
					if (this.m_pair.CompletionRequested)
					{
						throw new InvalidOperationException(base.GetType().Name);
					}
					this.m_tasks.Enqueue(task);
					this.m_pair.ProcessAsyncIfNecessary(false);
				}
			}

			// Token: 0x06007027 RID: 28711 RVA: 0x00184050 File Offset: 0x00182250
			[SecuritySafeCritical]
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x06007028 RID: 28712 RVA: 0x0018405C File Offset: 0x0018225C
			[SecurityCritical]
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
				{
					return false;
				}
				bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
				if (flag && taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				if (!this.m_pair.m_threadProcessingMapping.TryGetValue(Thread.CurrentThread.ManagedThreadId, out processingMode) || processingMode != this.m_processingMode)
				{
					return false;
				}
				if (!flag || taskWasPreviouslyQueued)
				{
					return this.TryExecuteTaskInlineOnTargetScheduler(task);
				}
				return base.TryExecuteTask(task);
			}

			// Token: 0x06007029 RID: 28713 RVA: 0x001840E0 File Offset: 0x001822E0
			private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
			{
				Task<bool> task2 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
				bool result;
				try
				{
					task2.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
					result = task2.Result;
				}
				catch
				{
					AggregateException exception = task2.Exception;
					throw;
				}
				finally
				{
					task2.Dispose();
				}
				return result;
			}

			// Token: 0x0600702A RID: 28714 RVA: 0x00184148 File Offset: 0x00182348
			[SecuritySafeCritical]
			private static bool TryExecuteTaskShim(object state)
			{
				Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>)state;
				return tuple.Item1.TryExecuteTask(tuple.Item2);
			}

			// Token: 0x0600702B RID: 28715 RVA: 0x0018416D File Offset: 0x0018236D
			[SecurityCritical]
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.m_tasks;
			}

			// Token: 0x17001331 RID: 4913
			// (get) Token: 0x0600702C RID: 28716 RVA: 0x00184175 File Offset: 0x00182375
			private int CountForDebugger
			{
				get
				{
					return this.m_tasks.Count;
				}
			}

			// Token: 0x040036E0 RID: 14048
			private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);

			// Token: 0x040036E1 RID: 14049
			private readonly ConcurrentExclusiveSchedulerPair m_pair;

			// Token: 0x040036E2 RID: 14050
			private readonly int m_maxConcurrencyLevel;

			// Token: 0x040036E3 RID: 14051
			private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;

			// Token: 0x040036E4 RID: 14052
			internal readonly IProducerConsumerQueue<Task> m_tasks;

			// Token: 0x02000D08 RID: 3336
			private sealed class DebugView
			{
				// Token: 0x06007234 RID: 29236 RVA: 0x0018AD6D File Offset: 0x00188F6D
				public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
				{
					this.m_taskScheduler = scheduler;
				}

				// Token: 0x17001390 RID: 5008
				// (get) Token: 0x06007235 RID: 29237 RVA: 0x0018AD7C File Offset: 0x00188F7C
				public int MaximumConcurrencyLevel
				{
					get
					{
						return this.m_taskScheduler.m_maxConcurrencyLevel;
					}
				}

				// Token: 0x17001391 RID: 5009
				// (get) Token: 0x06007236 RID: 29238 RVA: 0x0018AD89 File Offset: 0x00188F89
				public IEnumerable<Task> ScheduledTasks
				{
					get
					{
						return this.m_taskScheduler.m_tasks;
					}
				}

				// Token: 0x17001392 RID: 5010
				// (get) Token: 0x06007237 RID: 29239 RVA: 0x0018AD96 File Offset: 0x00188F96
				public ConcurrentExclusiveSchedulerPair SchedulerPair
				{
					get
					{
						return this.m_taskScheduler.m_pair;
					}
				}

				// Token: 0x0400395A RID: 14682
				private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;
			}
		}

		// Token: 0x02000C22 RID: 3106
		private sealed class DebugView
		{
			// Token: 0x0600702E RID: 28718 RVA: 0x00184195 File Offset: 0x00182395
			public DebugView(ConcurrentExclusiveSchedulerPair pair)
			{
				this.m_pair = pair;
			}

			// Token: 0x17001332 RID: 4914
			// (get) Token: 0x0600702F RID: 28719 RVA: 0x001841A4 File Offset: 0x001823A4
			public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
			{
				get
				{
					return this.m_pair.ModeForDebugger;
				}
			}

			// Token: 0x17001333 RID: 4915
			// (get) Token: 0x06007030 RID: 28720 RVA: 0x001841B1 File Offset: 0x001823B1
			public IEnumerable<Task> ScheduledExclusive
			{
				get
				{
					return this.m_pair.m_exclusiveTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17001334 RID: 4916
			// (get) Token: 0x06007031 RID: 28721 RVA: 0x001841C3 File Offset: 0x001823C3
			public IEnumerable<Task> ScheduledConcurrent
			{
				get
				{
					return this.m_pair.m_concurrentTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17001335 RID: 4917
			// (get) Token: 0x06007032 RID: 28722 RVA: 0x001841D5 File Offset: 0x001823D5
			public int CurrentlyExecutingTaskCount
			{
				get
				{
					if (this.m_pair.m_processingCount != -1)
					{
						return this.m_pair.m_processingCount;
					}
					return 1;
				}
			}

			// Token: 0x17001336 RID: 4918
			// (get) Token: 0x06007033 RID: 28723 RVA: 0x001841F2 File Offset: 0x001823F2
			public TaskScheduler TargetScheduler
			{
				get
				{
					return this.m_pair.m_underlyingTaskScheduler;
				}
			}

			// Token: 0x040036E5 RID: 14053
			private readonly ConcurrentExclusiveSchedulerPair m_pair;
		}

		// Token: 0x02000C23 RID: 3107
		[Flags]
		private enum ProcessingMode : byte
		{
			// Token: 0x040036E7 RID: 14055
			NotCurrentlyProcessing = 0,
			// Token: 0x040036E8 RID: 14056
			ProcessingExclusiveTask = 1,
			// Token: 0x040036E9 RID: 14057
			ProcessingConcurrentTasks = 2,
			// Token: 0x040036EA RID: 14058
			Completing = 4,
			// Token: 0x040036EB RID: 14059
			Completed = 8
		}
	}
}
