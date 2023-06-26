using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000502 RID: 1282
	internal class _IOCompletionCallback
	{
		// Token: 0x06003CA4 RID: 15524 RVA: 0x000E600E File Offset: 0x000E420E
		[SecurityCritical]
		internal _IOCompletionCallback(IOCompletionCallback ioCompletionCallback, ref StackCrawlMark stackMark)
		{
			this._ioCompletionCallback = ioCompletionCallback;
			this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000E602C File Offset: 0x000E422C
		[SecurityCritical]
		internal static void IOCompletionCallback_Context(object state)
		{
			_IOCompletionCallback iocompletionCallback = (_IOCompletionCallback)state;
			iocompletionCallback._ioCompletionCallback(iocompletionCallback._errorCode, iocompletionCallback._numBytes, iocompletionCallback._pOVERLAP);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000E6060 File Offset: 0x000E4260
		[SecurityCritical]
		internal unsafe static void PerformIOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP)
		{
			do
			{
				Overlapped overlapped = OverlappedData.GetOverlappedFromNative(pOVERLAP).m_overlapped;
				_IOCompletionCallback iocbHelper = overlapped.iocbHelper;
				if (iocbHelper == null || iocbHelper._executionContext == null || iocbHelper._executionContext.IsDefaultFTContext(true))
				{
					IOCompletionCallback userCallback = overlapped.UserCallback;
					userCallback(errorCode, numBytes, pOVERLAP);
				}
				else
				{
					iocbHelper._errorCode = errorCode;
					iocbHelper._numBytes = numBytes;
					iocbHelper._pOVERLAP = pOVERLAP;
					using (ExecutionContext executionContext = iocbHelper._executionContext.CreateCopy())
					{
						ExecutionContext.Run(executionContext, _IOCompletionCallback._ccb, iocbHelper, true);
					}
				}
				OverlappedData.CheckVMForIOPacket(out pOVERLAP, out errorCode, out numBytes);
			}
			while (pOVERLAP != null);
		}

		// Token: 0x040019B1 RID: 6577
		[SecurityCritical]
		private IOCompletionCallback _ioCompletionCallback;

		// Token: 0x040019B2 RID: 6578
		private ExecutionContext _executionContext;

		// Token: 0x040019B3 RID: 6579
		private uint _errorCode;

		// Token: 0x040019B4 RID: 6580
		private uint _numBytes;

		// Token: 0x040019B5 RID: 6581
		[SecurityCritical]
		private unsafe NativeOverlapped* _pOVERLAP;

		// Token: 0x040019B6 RID: 6582
		internal static ContextCallback _ccb = new ContextCallback(_IOCompletionCallback.IOCompletionCallback_Context);
	}
}
