using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000506 RID: 1286
	[SecurityCritical]
	internal sealed class ThreadPoolBoundHandleOverlapped : Overlapped
	{
		// Token: 0x06003CCD RID: 15565 RVA: 0x000E65F0 File Offset: 0x000E47F0
		public unsafe ThreadPoolBoundHandleOverlapped(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			this._userCallback = callback;
			this._userState = state;
			this._preAllocated = preAllocated;
			this._nativeOverlapped = base.Pack(ThreadPoolBoundHandleOverlapped.s_completionCallback, pinData);
			this._nativeOverlapped->OffsetLow = 0;
			this._nativeOverlapped->OffsetHigh = 0;
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000E6644 File Offset: 0x000E4844
		private unsafe static void CompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			ThreadPoolBoundHandleOverlapped threadPoolBoundHandleOverlapped = (ThreadPoolBoundHandleOverlapped)Overlapped.Unpack(nativeOverlapped);
			if (threadPoolBoundHandleOverlapped._completed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NativeOverlappedReused"));
			}
			threadPoolBoundHandleOverlapped._completed = true;
			if (threadPoolBoundHandleOverlapped._boundHandle == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Argument_NativeOverlappedAlreadyFree"));
			}
			threadPoolBoundHandleOverlapped._userCallback(errorCode, numBytes, nativeOverlapped);
		}

		// Token: 0x040019C6 RID: 6598
		private static readonly IOCompletionCallback s_completionCallback = new IOCompletionCallback(ThreadPoolBoundHandleOverlapped.CompletionCallback);

		// Token: 0x040019C7 RID: 6599
		private readonly IOCompletionCallback _userCallback;

		// Token: 0x040019C8 RID: 6600
		internal readonly object _userState;

		// Token: 0x040019C9 RID: 6601
		internal PreAllocatedOverlapped _preAllocated;

		// Token: 0x040019CA RID: 6602
		internal unsafe NativeOverlapped* _nativeOverlapped;

		// Token: 0x040019CB RID: 6603
		internal ThreadPoolBoundHandle _boundHandle;

		// Token: 0x040019CC RID: 6604
		internal bool _completed;
	}
}
