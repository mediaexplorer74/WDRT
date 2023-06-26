using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task" /> objects.</summary>
	// Token: 0x02000574 RID: 1396
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskFactory
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000F696D File Offset: 0x000F4B6D
		private TaskScheduler DefaultScheduler
		{
			get
			{
				if (this.m_defaultScheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x000F6983 File Offset: 0x000F4B83
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the default configuration.</summary>
		// Token: 0x060041C0 RID: 16832 RVA: 0x000F69B0 File Offset: 0x000F4BB0
		[__DynamicallyInvokable]
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		// Token: 0x060041C1 RID: 16833 RVA: 0x000F69CF File Offset: 0x000F4BCF
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken)
			: this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any tasks created with this TaskFactory. A null value indicates that the current TaskScheduler should be used.</param>
		// Token: 0x060041C2 RID: 16834 RVA: 0x000F69DC File Offset: 0x000F4BDC
		[__DynamicallyInvokable]
		public TaskFactory(TaskScheduler scheduler)
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />.  
		///  -or-  
		///  The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x060041C3 RID: 16835 RVA: 0x000F69FC File Offset: 0x000F4BFC
		[__DynamicallyInvokable]
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
			: this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <param name="scheduler">The default <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any Tasks created with this TaskFactory. A null value indicates that TaskScheduler.Current should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />.  
		///  -or-  
		///  The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x060041C4 RID: 16836 RVA: 0x000F6A1B File Offset: 0x000F4C1B
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x000F6A4C File Offset: 0x000F4C4C
		internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		/// <summary>Gets the default cancellation token for this task factory.</summary>
		/// <returns>The default task cancellation token for this task factory.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x000F6A5F File Offset: 0x000F4C5F
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		/// <summary>Gets the default task scheduler for this task factory.</summary>
		/// <returns>The default task scheduler for this task factory.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060041C7 RID: 16839 RVA: 0x000F6A67 File Offset: 0x000F4C67
		[__DynamicallyInvokable]
		public TaskScheduler Scheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultScheduler;
			}
		}

		/// <summary>Gets the default task creation options for this task factory.</summary>
		/// <returns>The default task creation options for this task factory.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x000F6A6F File Offset: 0x000F4C6F
		[__DynamicallyInvokable]
		public TaskCreationOptions CreationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		/// <summary>Gets the default task continuation options for this task factory.</summary>
		/// <returns>The default task continuation options for this task factory.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x000F6A77 File Offset: 0x000F4C77
		[__DynamicallyInvokable]
		public TaskContinuationOptions ContinuationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x060041CA RID: 16842 RVA: 0x000F6A80 File Offset: 0x000F4C80
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x060041CB RID: 16843 RVA: 0x000F6AB4 File Offset: 0x000F4CB4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x060041CC RID: 16844 RVA: 0x000F6AE4 File Offset: 0x000F4CE4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041CD RID: 16845 RVA: 0x000F6B14 File Offset: 0x000F4D14
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000F6B38 File Offset: 0x000F4D38
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, internalOptions, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is <see langword="null" />.</exception>
		// Token: 0x060041CF RID: 16847 RVA: 0x000F6B5C File Offset: 0x000F4D5C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x060041D0 RID: 16848 RVA: 0x000F6B90 File Offset: 0x000F4D90
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x060041D1 RID: 16849 RVA: 0x000F6BC0 File Offset: 0x000F4DC0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041D2 RID: 16850 RVA: 0x000F6BF0 File Offset: 0x000F4DF0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		// Token: 0x060041D3 RID: 16851 RVA: 0x000F6C18 File Offset: 0x000F4E18
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x060041D4 RID: 16852 RVA: 0x000F6C4C File Offset: 0x000F4E4C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041D5 RID: 16853 RVA: 0x000F6C7C File Offset: 0x000F4E7C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041D6 RID: 16854 RVA: 0x000F6CAC File Offset: 0x000F4EAC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x060041D7 RID: 16855 RVA: 0x000F6CD0 File Offset: 0x000F4ED0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x060041D8 RID: 16856 RVA: 0x000F6D04 File Offset: 0x000F4F04
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041D9 RID: 16857 RVA: 0x000F6D34 File Offset: 0x000F4F34
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041DA RID: 16858 RVA: 0x000F6D64 File Offset: 0x000F4F64
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041DB RID: 16859 RVA: 0x000F6D8C File Offset: 0x000F4F8C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041DC RID: 16860 RVA: 0x000F6DB4 File Offset: 0x000F4FB4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the task that executes the end method.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041DD RID: 16861 RVA: 0x000F6DD4 File Offset: 0x000F4FD4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000F6DF0 File Offset: 0x000F4FF0
		private Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, null, endMethod, creationOptions, scheduler, ref stackMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041DF RID: 16863 RVA: 0x000F6DFF File Offset: 0x000F4FFF
		[__DynamicallyInvokable]
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
		{
			return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x060041E0 RID: 16864 RVA: 0x000F6E10 File Offset: 0x000F5010
		[__DynamicallyInvokable]
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, null, endMethod, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041E1 RID: 16865 RVA: 0x000F6E1D File Offset: 0x000F501D
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
		{
			return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041E2 RID: 16866 RVA: 0x000F6E30 File Offset: 0x000F5030
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, null, endMethod, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041E3 RID: 16867 RVA: 0x000F6E3F File Offset: 0x000F503F
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041E4 RID: 16868 RVA: 0x000F6E54 File Offset: 0x000F5054
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, null, endMethod, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041E5 RID: 16869 RVA: 0x000F6E65 File Offset: 0x000F5065
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041E6 RID: 16870 RVA: 0x000F6E7C File Offset: 0x000F507C
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, null, endMethod, arg1, arg2, arg3, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041E7 RID: 16871 RVA: 0x000F6E90 File Offset: 0x000F5090
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041E8 RID: 16872 RVA: 0x000F6EB8 File Offset: 0x000F50B8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the task that executes the end method.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041E9 RID: 16873 RVA: 0x000F6ED8 File Offset: 0x000F50D8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041EA RID: 16874 RVA: 0x000F6EF4 File Offset: 0x000F50F4
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041EB RID: 16875 RVA: 0x000F6F05 File Offset: 0x000F5105
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041EC RID: 16876 RVA: 0x000F6F12 File Offset: 0x000F5112
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041ED RID: 16877 RVA: 0x000F6F25 File Offset: 0x000F5125
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041EE RID: 16878 RVA: 0x000F6F34 File Offset: 0x000F5134
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041EF RID: 16879 RVA: 0x000F6F49 File Offset: 0x000F5149
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060041F0 RID: 16880 RVA: 0x000F6F5A File Offset: 0x000F515A
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060041F1 RID: 16881 RVA: 0x000F6F71 File Offset: 0x000F5171
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000F6F84 File Offset: 0x000F5184
		internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
		{
			if (hasBeginMethod)
			{
				if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("Task_FromAsync_LongRunning"));
				}
				if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("Task_FromAsync_PreferFairness"));
				}
			}
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000F6FDC File Offset: 0x000F51DC
		internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000F7020 File Offset: 0x000F5220
		internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise<T> completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041F5 RID: 16885 RVA: 0x000F7064 File Offset: 0x000F5264
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041F6 RID: 16886 RVA: 0x000F70A0 File Offset: 0x000F52A0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041F7 RID: 16887 RVA: 0x000F70D4 File Offset: 0x000F52D4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041F8 RID: 16888 RVA: 0x000F7108 File Offset: 0x000F5308
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041F9 RID: 16889 RVA: 0x000F7134 File Offset: 0x000F5334
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041FA RID: 16890 RVA: 0x000F7170 File Offset: 0x000F5370
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041FB RID: 16891 RVA: 0x000F71A4 File Offset: 0x000F53A4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041FC RID: 16892 RVA: 0x000F71D8 File Offset: 0x000F53D8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041FD RID: 16893 RVA: 0x000F7204 File Offset: 0x000F5404
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041FE RID: 16894 RVA: 0x000F7240 File Offset: 0x000F5440
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060041FF RID: 16895 RVA: 0x000F7274 File Offset: 0x000F5474
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x06004200 RID: 16896 RVA: 0x000F72A8 File Offset: 0x000F54A8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x06004201 RID: 16897 RVA: 0x000F72D4 File Offset: 0x000F54D4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x06004202 RID: 16898 RVA: 0x000F7310 File Offset: 0x000F5510
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x06004203 RID: 16899 RVA: 0x000F7344 File Offset: 0x000F5544
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06004204 RID: 16900 RVA: 0x000F7378 File Offset: 0x000F5578
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000F73A4 File Offset: 0x000F55A4
		internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
		{
			TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
			bool flag = false;
			int count = tasks.Count;
			for (int i = 0; i < count; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				if (!flag)
				{
					if (completeOnInvokePromise.IsCompleted)
					{
						flag = true;
					}
					else if (task.IsCompleted)
					{
						completeOnInvokePromise.Invoke(task);
						flag = true;
					}
					else
					{
						task.AddCompletionAction(completeOnInvokePromise);
						if (completeOnInvokePromise.IsCompleted)
						{
							task.RemoveContinuation(completeOnInvokePromise);
						}
					}
				}
			}
			return completeOnInvokePromise;
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a <see langword="null" /> value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004206 RID: 16902 RVA: 0x000F742C File Offset: 0x000F562C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a <see langword="null" /> value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty .</exception>
		// Token: 0x06004207 RID: 16903 RVA: 0x000F7468 File Offset: 0x000F5668
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004208 RID: 16904 RVA: 0x000F749C File Offset: 0x000F569C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004209 RID: 16905 RVA: 0x000F74D0 File Offset: 0x000F56D0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600420A RID: 16906 RVA: 0x000F74FC File Offset: 0x000F56FC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600420B RID: 16907 RVA: 0x000F7538 File Offset: 0x000F5738
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600420C RID: 16908 RVA: 0x000F756C File Offset: 0x000F576C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600420D RID: 16909 RVA: 0x000F75A0 File Offset: 0x000F57A0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600420E RID: 16910 RVA: 0x000F75CC File Offset: 0x000F57CC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600420F RID: 16911 RVA: 0x000F7608 File Offset: 0x000F5808
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004210 RID: 16912 RVA: 0x000F763C File Offset: 0x000F583C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004211 RID: 16913 RVA: 0x000F7670 File Offset: 0x000F5870
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004212 RID: 16914 RVA: 0x000F769C File Offset: 0x000F589C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004213 RID: 16915 RVA: 0x000F76D8 File Offset: 0x000F58D8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004214 RID: 16916 RVA: 0x000F770C File Offset: 0x000F590C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06004215 RID: 16917 RVA: 0x000F7740 File Offset: 0x000F5940
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000F776C File Offset: 0x000F596C
		internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			Task[] array = new Task[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
			}
			return array;
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000F77D8 File Offset: 0x000F59D8
		internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			Task<TResult>[] array = new Task<TResult>[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
			}
			return array;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000F7844 File Offset: 0x000F5A44
		internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_ESandLR"));
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_MultiTaskContinuation_FireOptions"));
			}
		}

		// Token: 0x04001B71 RID: 7025
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001B72 RID: 7026
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001B73 RID: 7027
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001B74 RID: 7028
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000C1A RID: 3098
		private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
		{
			// Token: 0x0600700F RID: 28687 RVA: 0x00183B6D File Offset: 0x00181D6D
			internal CompleteOnCountdownPromise(Task[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06007010 RID: 28688 RVA: 0x00183BB0 File Offset: 0x00181DB0
			public void Invoke(Task completingTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x1700132A RID: 4906
			// (get) Token: 0x06007011 RID: 28689 RVA: 0x00183C20 File Offset: 0x00181E20
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
				}
			}

			// Token: 0x040036CD RID: 14029
			private readonly Task[] _tasks;

			// Token: 0x040036CE RID: 14030
			private int _count;
		}

		// Token: 0x02000C1B RID: 3099
		private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
		{
			// Token: 0x06007012 RID: 28690 RVA: 0x00183C37 File Offset: 0x00181E37
			internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAll<>", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x00183C78 File Offset: 0x00181E78
			public void Invoke(Task completingTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x1700132B RID: 4907
			// (get) Token: 0x06007014 RID: 28692 RVA: 0x00183CE8 File Offset: 0x00181EE8
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					if (base.ShouldNotifyDebuggerOfWaitCompletion)
					{
						Task[] tasks = this._tasks;
						return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(tasks);
					}
					return false;
				}
			}

			// Token: 0x040036CF RID: 14031
			private readonly Task<T>[] _tasks;

			// Token: 0x040036D0 RID: 14032
			private int _count;
		}

		// Token: 0x02000C1C RID: 3100
		internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
		{
			// Token: 0x06007015 RID: 28693 RVA: 0x00183D0C File Offset: 0x00181F0C
			public CompleteOnInvokePromise(IList<Task> tasks)
			{
				this._tasks = tasks;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAny", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06007016 RID: 28694 RVA: 0x00183D44 File Offset: 0x00181F44
			public void Invoke(Task completingTask)
			{
				if (Interlocked.CompareExchange(ref this.m_firstTaskAlreadyCompleted, 1, 0) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Choice);
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					bool flag = base.TrySetResult(completingTask);
					IList<Task> tasks = this._tasks;
					int count = tasks.Count;
					for (int i = 0; i < count; i++)
					{
						Task task = tasks[i];
						if (task != null && !task.IsCompleted)
						{
							task.RemoveContinuation(this);
						}
					}
					this._tasks = null;
				}
			}

			// Token: 0x040036D1 RID: 14033
			private IList<Task> _tasks;

			// Token: 0x040036D2 RID: 14034
			private int m_firstTaskAlreadyCompleted;
		}
	}
}
