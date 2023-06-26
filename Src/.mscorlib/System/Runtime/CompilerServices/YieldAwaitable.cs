using System;
using System.Diagnostics.Tracing;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides the context for waiting when asynchronously switching into a target environment.</summary>
	// Token: 0x020008F8 RID: 2296
	[__DynamicallyInvokable]
	public struct YieldAwaitable
	{
		/// <summary>Retrieves a <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> object  for this instance of the class.</summary>
		/// <returns>The object that is used to monitor the completion of an asynchronous operation.</returns>
		// Token: 0x06005E66 RID: 24166 RVA: 0x0014CC48 File Offset: 0x0014AE48
		[__DynamicallyInvokable]
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		/// <summary>Provides an awaiter for switching into a target environment.</summary>
		// Token: 0x02000C90 RID: 3216
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			/// <summary>Gets a value that indicates whether a yield is not required.</summary>
			/// <returns>Always <see langword="false" />, which indicates that a yield is always required for <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" />.</returns>
			// Token: 0x17001362 RID: 4962
			// (get) Token: 0x0600711E RID: 28958 RVA: 0x00186979 File Offset: 0x00184B79
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			/// <summary>Sets the continuation to invoke.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="continuation" /> is <see langword="null" />.</exception>
			// Token: 0x0600711F RID: 28959 RVA: 0x0018697C File Offset: 0x00184B7C
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			/// <summary>Posts the <paramref name="continuation" /> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is <see langword="null" />.</exception>
			// Token: 0x06007120 RID: 28960 RVA: 0x00186985 File Offset: 0x00184B85
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x06007121 RID: 28961 RVA: 0x00186990 File Offset: 0x00184B90
			[SecurityCritical]
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					continuation = YieldAwaitable.YieldAwaiter.OutputCorrelationEtwEvent(continuation);
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x06007122 RID: 28962 RVA: 0x00186A30 File Offset: 0x00184C30
			private static Action OutputCorrelationEtwEvent(Action continuation)
			{
				int continuationId = Task.NewId();
				Task internalCurrent = Task.InternalCurrent;
				TplEtwProvider.Log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, continuationId);
				return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
				{
					TplEtwProvider log = TplEtwProvider.Log;
					log.TaskWaitContinuationStarted(continuationId);
					Guid guid = default(Guid);
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out guid);
					}
					continuation();
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(guid);
					}
					log.TaskWaitContinuationComplete(continuationId);
				}, null);
			}

			// Token: 0x06007123 RID: 28963 RVA: 0x00186A99 File Offset: 0x00184C99
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			/// <summary>Ends the await operation.</summary>
			// Token: 0x06007124 RID: 28964 RVA: 0x00186AA6 File Offset: 0x00184CA6
			[__DynamicallyInvokable]
			public void GetResult()
			{
			}

			// Token: 0x0400384E RID: 14414
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x0400384F RID: 14415
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
