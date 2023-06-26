using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a builder for asynchronous methods that do not return a value.</summary>
	// Token: 0x020008EC RID: 2284
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncVoidMethodBuilder
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.CompilerServices.AsyncVoidMethodBuilder" /> class.</summary>
		/// <returns>A new instance of the builder.</returns>
		// Token: 0x06005E22 RID: 24098 RVA: 0x0014BD58 File Offset: 0x00149F58
		[__DynamicallyInvokable]
		public static AsyncVoidMethodBuilder Create()
		{
			SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
			if (currentNoFlow != null)
			{
				currentNoFlow.OperationStarted();
			}
			return new AsyncVoidMethodBuilder
			{
				m_synchronizationContext = currentNoFlow
			};
		}

		/// <summary>Begins running the builder with the associated state machine.</summary>
		/// <param name="stateMachine">The state machine instance, passed by reference.</param>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is <see langword="null" />.</exception>
		// Token: 0x06005E23 RID: 24099 RVA: 0x0014BD88 File Offset: 0x00149F88
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[__DynamicallyInvokable]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.EstablishCopyOnWriteScope(ref executionContextSwitcher);
				stateMachine.MoveNext();
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		/// <summary>Associates the builder with the specified state machine.</summary>
		/// <param name="stateMachine">The state machine instance to associate with the builder.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The state machine was previously set.</exception>
		// Token: 0x06005E24 RID: 24100 RVA: 0x0014BDE8 File Offset: 0x00149FE8
		[__DynamicallyInvokable]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x06005E25 RID: 24101 RVA: 0x0014BDF8 File Offset: 0x00149FF8
		[__DynamicallyInvokable]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
					}
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, null);
				}
				awaiter.OnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes. This method can be called from partially trusted code.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x06005E26 RID: 24102 RVA: 0x0014BEA8 File Offset: 0x0014A0A8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
					}
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, null);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		/// <summary>Marks the method builder as successfully completed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The builder is not initialized.</exception>
		// Token: 0x06005E27 RID: 24103 RVA: 0x0014BF58 File Offset: 0x0014A158
		[__DynamicallyInvokable]
		public void SetResult()
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Completed);
			}
			if (this.m_synchronizationContext != null)
			{
				this.NotifySynchronizationContextOfCompletion();
			}
		}

		/// <summary>Binds an exception to the method builder.</summary>
		/// <param name="exception">The exception to bind.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exception" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not initialized.</exception>
		// Token: 0x06005E28 RID: 24104 RVA: 0x0014BF84 File Offset: 0x0014A184
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Error);
			}
			if (this.m_synchronizationContext != null)
			{
				try
				{
					AsyncMethodBuilderCore.ThrowAsync(exception, this.m_synchronizationContext);
					return;
				}
				finally
				{
					this.NotifySynchronizationContextOfCompletion();
				}
			}
			AsyncMethodBuilderCore.ThrowAsync(exception, null);
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0014BFEC File Offset: 0x0014A1EC
		private void NotifySynchronizationContextOfCompletion()
		{
			try
			{
				this.m_synchronizationContext.OperationCompleted();
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x0014C020 File Offset: 0x0014A220
		private Task Task
		{
			get
			{
				if (this.m_task == null)
				{
					this.m_task = new Task();
				}
				return this.m_task;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x0014C03B File Offset: 0x0014A23B
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x04002A4E RID: 10830
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04002A4F RID: 10831
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x04002A50 RID: 10832
		private Task m_task;
	}
}
