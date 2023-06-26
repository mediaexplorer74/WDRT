using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000545 RID: 1349
	internal class CancellationCallbackInfo
	{
		// Token: 0x06003F81 RID: 16257 RVA: 0x000ED874 File Offset: 0x000EBA74
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetSyncContext = targetSyncContext;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000ED8A4 File Offset: 0x000EBAA4
		[SecuritySafeCritical]
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000ED8EC File Offset: 0x000EBAEC
		[SecurityCritical]
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x04001AB6 RID: 6838
		internal readonly Action<object> Callback;

		// Token: 0x04001AB7 RID: 6839
		internal readonly object StateForCallback;

		// Token: 0x04001AB8 RID: 6840
		internal readonly SynchronizationContext TargetSyncContext;

		// Token: 0x04001AB9 RID: 6841
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x04001ABA RID: 6842
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x04001ABB RID: 6843
		[SecurityCritical]
		private static ContextCallback s_executionContextCallback;
	}
}
