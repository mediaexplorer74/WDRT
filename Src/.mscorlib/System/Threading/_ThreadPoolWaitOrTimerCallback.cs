using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000522 RID: 1314
	internal class _ThreadPoolWaitOrTimerCallback
	{
		// Token: 0x06003E04 RID: 15876 RVA: 0x000E8D87 File Offset: 0x000E6F87
		[SecurityCritical]
		internal _ThreadPoolWaitOrTimerCallback(WaitOrTimerCallback waitOrTimerCallback, object state, bool compressStack, ref StackCrawlMark stackMark)
		{
			this._waitOrTimerCallback = waitOrTimerCallback;
			this._state = state;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000E8DB5 File Offset: 0x000E6FB5
		[SecurityCritical]
		private static void WaitOrTimerCallback_Context_t(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, true);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000E8DBE File Offset: 0x000E6FBE
		[SecurityCritical]
		private static void WaitOrTimerCallback_Context_f(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, false);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000E8DC8 File Offset: 0x000E6FC8
		private static void WaitOrTimerCallback_Context(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000E8DF0 File Offset: 0x000E6FF0
		[SecurityCritical]
		internal static void PerformWaitOrTimerCallback(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			if (threadPoolWaitOrTimerCallback._executionContext == null)
			{
				WaitOrTimerCallback waitOrTimerCallback = threadPoolWaitOrTimerCallback._waitOrTimerCallback;
				waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
				return;
			}
			using (ExecutionContext executionContext = threadPoolWaitOrTimerCallback._executionContext.CreateCopy())
			{
				if (timedOut)
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbt, threadPoolWaitOrTimerCallback, true);
				}
				else
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbf, threadPoolWaitOrTimerCallback, true);
				}
			}
		}

		// Token: 0x04001A20 RID: 6688
		private WaitOrTimerCallback _waitOrTimerCallback;

		// Token: 0x04001A21 RID: 6689
		private ExecutionContext _executionContext;

		// Token: 0x04001A22 RID: 6690
		private object _state;

		// Token: 0x04001A23 RID: 6691
		[SecurityCritical]
		private static ContextCallback _ccbt = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_t);

		// Token: 0x04001A24 RID: 6692
		[SecurityCritical]
		private static ContextCallback _ccbf = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_f);
	}
}
