﻿using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001ED RID: 493
	internal struct SecurityContextSwitcher : IDisposable
	{
		// Token: 0x06001DAC RID: 7596 RVA: 0x00067964 File Offset: 0x00065B64
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006796C File Offset: 0x00065B6C
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

		// Token: 0x06001DAE RID: 7598 RVA: 0x000679B0 File Offset: 0x00065BB0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		public void Undo()
		{
			if (this.currSC == null)
			{
				return;
			}
			if (this.currEC != null)
			{
				this.currEC.SecurityContext = this.prevSC.DangerousGetRawSecurityContext();
			}
			this.currSC = null;
			bool flag = true;
			try
			{
				if (this.wic != null)
				{
					flag &= this.wic.UndoNoThrow();
				}
			}
			catch
			{
				flag &= this.cssw.UndoNoThrow();
				Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"));
			}
			flag &= this.cssw.UndoNoThrow();
			if (!flag)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"));
			}
		}

		// Token: 0x04000A5D RID: 2653
		internal SecurityContext.Reader prevSC;

		// Token: 0x04000A5E RID: 2654
		internal SecurityContext currSC;

		// Token: 0x04000A5F RID: 2655
		internal ExecutionContext currEC;

		// Token: 0x04000A60 RID: 2656
		internal CompressedStackSwitcher cssw;

		// Token: 0x04000A61 RID: 2657
		internal WindowsImpersonationContext wic;
	}
}
