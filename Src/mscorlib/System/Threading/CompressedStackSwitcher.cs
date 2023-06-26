using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004ED RID: 1261
	internal struct CompressedStackSwitcher : IDisposable
	{
		// Token: 0x06003BBB RID: 15291 RVA: 0x000E3F58 File Offset: 0x000E2158
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is CompressedStackSwitcher))
			{
				return false;
			}
			CompressedStackSwitcher compressedStackSwitcher = (CompressedStackSwitcher)obj;
			return this.curr_CS == compressedStackSwitcher.curr_CS && this.prev_CS == compressedStackSwitcher.prev_CS && this.prev_ADStack == compressedStackSwitcher.prev_ADStack;
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x000E3FA8 File Offset: 0x000E21A8
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000E3FBB File Offset: 0x000E21BB
		public static bool operator ==(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000E3FD0 File Offset: 0x000E21D0
		public static bool operator !=(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000E3FE8 File Offset: 0x000E21E8
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000E3FF0 File Offset: 0x000E21F0
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

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000E4034 File Offset: 0x000E2234
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Undo()
		{
			if (this.curr_CS == null && this.prev_CS == null)
			{
				return;
			}
			if (this.prev_ADStack != (IntPtr)0)
			{
				CompressedStack.RestoreAppDomainStack(this.prev_ADStack);
			}
			CompressedStack.SetCompressedStackThread(this.prev_CS);
			this.prev_CS = null;
			this.curr_CS = null;
			this.prev_ADStack = (IntPtr)0;
		}

		// Token: 0x0400197D RID: 6525
		internal CompressedStack curr_CS;

		// Token: 0x0400197E RID: 6526
		internal CompressedStack prev_CS;

		// Token: 0x0400197F RID: 6527
		internal IntPtr prev_ADStack;
	}
}
