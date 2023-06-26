using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x02000512 RID: 1298
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class AsyncOperation
	{
		// Token: 0x0600311D RID: 12573 RVA: 0x000DE6A0 File Offset: 0x000DC8A0
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this.userSuppliedState = userSuppliedState;
			this.syncContext = syncContext;
			this.alreadyCompleted = false;
			this.syncContext.OperationStarted();
		}

		/// <summary>Finalizes the asynchronous operation.</summary>
		// Token: 0x0600311E RID: 12574 RVA: 0x000DE6C8 File Offset: 0x000DC8C8
		~AsyncOperation()
		{
			if (!this.alreadyCompleted && this.syncContext != null)
			{
				this.syncContext.OperationCompleted();
			}
		}

		/// <summary>Gets or sets an object used to uniquely identify an asynchronous operation.</summary>
		/// <returns>The state object passed to the asynchronous method invocation.</returns>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000DE70C File Offset: 0x000DC90C
		[global::__DynamicallyInvokable]
		public object UserSuppliedState
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.userSuppliedState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</summary>
		/// <returns>The <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</returns>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000DE714 File Offset: 0x000DC914
		[global::__DynamicallyInvokable]
		public SynchronizationContext SynchronizationContext
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.syncContext;
			}
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends.</param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06003121 RID: 12577 RVA: 0x000DE71C File Offset: 0x000DC91C
		[global::__DynamicallyInvokable]
		public void Post(SendOrPostCallback d, object arg)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			this.syncContext.Post(d, arg);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends.</param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06003122 RID: 12578 RVA: 0x000DE738 File Offset: 0x000DC938
		[global::__DynamicallyInvokable]
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.Post(d, arg);
			this.OperationCompletedCore();
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task.</exception>
		// Token: 0x06003123 RID: 12579 RVA: 0x000DE748 File Offset: 0x000DC948
		[global::__DynamicallyInvokable]
		public void OperationCompleted()
		{
			this.VerifyNotCompleted();
			this.OperationCompletedCore();
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000DE758 File Offset: 0x000DC958
		private void OperationCompletedCore()
		{
			try
			{
				this.syncContext.OperationCompleted();
			}
			finally
			{
				this.alreadyCompleted = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000DE790 File Offset: 0x000DC990
		private void VerifyNotCompleted()
		{
			if (this.alreadyCompleted)
			{
				throw new InvalidOperationException(SR.GetString("Async_OperationAlreadyCompleted"));
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000DE7AA File Offset: 0x000DC9AA
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException(SR.GetString("Async_NullDelegate"), "d");
			}
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000DE7C4 File Offset: 0x000DC9C4
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x040028F5 RID: 10485
		private SynchronizationContext syncContext;

		// Token: 0x040028F6 RID: 10486
		private object userSuppliedState;

		// Token: 0x040028F7 RID: 10487
		private bool alreadyCompleted;
	}
}
