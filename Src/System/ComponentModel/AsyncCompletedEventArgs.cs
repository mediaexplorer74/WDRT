using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodName<see langword="Completed" /> event.</summary>
	// Token: 0x02000510 RID: 1296
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AsyncCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class.</summary>
		// Token: 0x06003113 RID: 12563 RVA: 0x000DE62B File Offset: 0x000DC82B
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AsyncCompletedEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class.</summary>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		/// <param name="userState">The optional user-supplied state object passed to the <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" /> method.</param>
		// Token: 0x06003114 RID: 12564 RVA: 0x000DE633 File Offset: 0x000DC833
		[global::__DynamicallyInvokable]
		public AsyncCompletedEventArgs(Exception error, bool cancelled, object userState)
		{
			this.error = error;
			this.cancelled = cancelled;
			this.userState = userState;
		}

		/// <summary>Gets a value indicating whether an asynchronous operation has been canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the background operation has been canceled; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000DE650 File Offset: 0x000DC850
		[SRDescription("Async_AsyncEventArgs_Cancelled")]
		[global::__DynamicallyInvokable]
		public bool Cancelled
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.cancelled;
			}
		}

		/// <summary>Gets a value indicating which error occurred during an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> instance, if an error occurred during an asynchronous operation; otherwise <see langword="null" />.</returns>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000DE658 File Offset: 0x000DC858
		[SRDescription("Async_AsyncEventArgs_Error")]
		[global::__DynamicallyInvokable]
		public Exception Error
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.error;
			}
		}

		/// <summary>Gets the unique identifier for the asynchronous task.</summary>
		/// <returns>An object reference that uniquely identifies the asynchronous task; otherwise, <see langword="null" /> if no value has been set.</returns>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000DE660 File Offset: 0x000DC860
		[SRDescription("Async_AsyncEventArgs_UserState")]
		[global::__DynamicallyInvokable]
		public object UserState
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.userState;
			}
		}

		/// <summary>Raises a user-supplied exception if an asynchronous operation failed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> property is <see langword="true" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> property has been set by the asynchronous operation. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />.</exception>
		// Token: 0x06003118 RID: 12568 RVA: 0x000DE668 File Offset: 0x000DC868
		[global::__DynamicallyInvokable]
		protected void RaiseExceptionIfNecessary()
		{
			if (this.Error != null)
			{
				throw new TargetInvocationException(SR.GetString("Async_ExceptionOccurred"), this.Error);
			}
			if (this.Cancelled)
			{
				throw new InvalidOperationException(SR.GetString("Async_OperationCancelled"));
			}
		}

		// Token: 0x040028F2 RID: 10482
		private readonly Exception error;

		// Token: 0x040028F3 RID: 10483
		private readonly bool cancelled;

		// Token: 0x040028F4 RID: 10484
		private readonly object userState;
	}
}
