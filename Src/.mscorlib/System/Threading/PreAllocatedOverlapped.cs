using System;
using System.Security;

namespace System.Threading
{
	/// <summary>Represents pre-allocated state for native overlapped I/O operations.</summary>
	// Token: 0x02000505 RID: 1285
	public sealed class PreAllocatedOverlapped : IDisposable, IDeferredDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.PreAllocatedOverlapped" /> class and specifies a delegate to invoke when each asynchronous I/O operation is complete, a user-provided object that provides context, and managed objects that serve as buffers.</summary>
		/// <param name="callback">A delegate that represents the callback method to invoke when each asynchronous I/O operation completes.</param>
		/// <param name="state">A user-supplied object that distinguishes the <see cref="T:System.Threading.NativeOverlapped" /> instance produced from this object from other <see cref="T:System.Threading.NativeOverlapped" /> instances. Its value can be <see langword="null" />.</param>
		/// <param name="pinData">An object or array of objects that represent the input or output buffer for the operations. Each object represents a buffer, such as an array of bytes. Its value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> was disposed.</exception>
		// Token: 0x06003CC7 RID: 15559 RVA: 0x000E6513 File Offset: 0x000E4713
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._overlapped = new ThreadPoolBoundHandleOverlapped(callback, state, pinData, this);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000E6538 File Offset: 0x000E4738
		internal bool AddRef()
		{
			return this._lifetime.AddRef(this);
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000E6546 File Offset: 0x000E4746
		[SecurityCritical]
		internal void Release()
		{
			this._lifetime.Release(this);
		}

		/// <summary>Frees the resources associated with this <see cref="T:System.Threading.PreAllocatedOverlapped" /> instance.</summary>
		// Token: 0x06003CCA RID: 15562 RVA: 0x000E6554 File Offset: 0x000E4754
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		/// <summary>Frees unmanaged resources before the current instance is reclaimed by garbage collection.</summary>
		// Token: 0x06003CCB RID: 15563 RVA: 0x000E6568 File Offset: 0x000E4768
		~PreAllocatedOverlapped()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose();
			}
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000E659C File Offset: 0x000E479C
		[SecurityCritical]
		unsafe void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (this._overlapped != null)
			{
				if (disposed)
				{
					Overlapped.Free(this._overlapped._nativeOverlapped);
					return;
				}
				this._overlapped._boundHandle = null;
				this._overlapped._completed = false;
				*this._overlapped._nativeOverlapped = default(NativeOverlapped);
			}
		}

		// Token: 0x040019C4 RID: 6596
		[SecurityCritical]
		internal readonly ThreadPoolBoundHandleOverlapped _overlapped;

		// Token: 0x040019C5 RID: 6597
		private DeferredDisposableLifetime<PreAllocatedOverlapped> _lifetime;
	}
}
