using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides concurrency management for classes that support asynchronous method calls. This class cannot be inherited.</summary>
	// Token: 0x02000513 RID: 1299
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public static class AsyncOperationManager
	{
		/// <summary>Returns an <see cref="T:System.ComponentModel.AsyncOperation" /> for tracking the duration of a particular asynchronous operation.</summary>
		/// <param name="userSuppliedState">An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation.</param>
		/// <returns>An <see cref="T:System.ComponentModel.AsyncOperation" /> that you can use to track the duration of an asynchronous method invocation.</returns>
		// Token: 0x06003128 RID: 12584 RVA: 0x000DE7DA File Offset: 0x000DC9DA
		[global::__DynamicallyInvokable]
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return AsyncOperation.CreateOperation(userSuppliedState, AsyncOperationManager.SynchronizationContext);
		}

		/// <summary>Gets or sets the synchronization context for the asynchronous operation.</summary>
		/// <returns>The synchronization context for the asynchronous operation.</returns>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000DE7E7 File Offset: 0x000DC9E7
		// (set) Token: 0x0600312A RID: 12586 RVA: 0x000DE7FF File Offset: 0x000DC9FF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[global::__DynamicallyInvokable]
		public static SynchronizationContext SynchronizationContext
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			[global::__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}
	}
}
