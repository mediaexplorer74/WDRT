using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Principal;

namespace System.Threading
{
	// Token: 0x020004F4 RID: 1268
	internal struct ExecutionContextSwitcher
	{
		// Token: 0x06003C02 RID: 15362 RVA: 0x000E4980 File Offset: 0x000E2B80
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch (Exception ex)
			{
				if (!AppContextSwitches.UseLegacyExecutionContextBehaviorUponUndoFailure)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"), ex);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x000E49C4 File Offset: 0x000E2BC4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void Undo()
		{
			if (this.thread == null)
			{
				return;
			}
			Thread thread = this.thread;
			if (this.hecsw != null)
			{
				HostExecutionContextSwitcher.Undo(this.hecsw);
			}
			ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
			thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
			if (this.scsw.currSC != null)
			{
				this.scsw.Undo();
			}
			if (this.wiIsValid)
			{
				SecurityContext.RestoreCurrentWI(this.outerEC, executionContextReader, this.wi, this.cachedAlwaysFlowImpersonationPolicy);
			}
			this.thread = null;
			ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
		}

		// Token: 0x0400198A RID: 6538
		internal ExecutionContext.Reader outerEC;

		// Token: 0x0400198B RID: 6539
		internal bool outerECBelongsToScope;

		// Token: 0x0400198C RID: 6540
		internal SecurityContextSwitcher scsw;

		// Token: 0x0400198D RID: 6541
		internal object hecsw;

		// Token: 0x0400198E RID: 6542
		internal WindowsIdentity wi;

		// Token: 0x0400198F RID: 6543
		internal bool cachedAlwaysFlowImpersonationPolicy;

		// Token: 0x04001990 RID: 6544
		internal bool wiIsValid;

		// Token: 0x04001991 RID: 6545
		internal Thread thread;
	}
}
