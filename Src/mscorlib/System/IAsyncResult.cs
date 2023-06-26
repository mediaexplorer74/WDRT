using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Represents the status of an asynchronous operation.</summary>
	// Token: 0x020000EC RID: 236
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IAsyncResult
	{
		/// <summary>Gets a value that indicates whether the asynchronous operation has completed.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation is complete; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000EF8 RID: 3832
		[__DynamicallyInvokable]
		bool IsCompleted
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000EF9 RID: 3833
		[__DynamicallyInvokable]
		WaitHandle AsyncWaitHandle
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a user-defined object that qualifies or contains information about an asynchronous operation.</summary>
		/// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000EFA RID: 3834
		[__DynamicallyInvokable]
		object AsyncState
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that indicates whether the asynchronous operation completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the asynchronous operation completed synchronously; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000EFB RID: 3835
		[__DynamicallyInvokable]
		bool CompletedSynchronously
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
