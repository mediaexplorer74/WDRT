using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001E8 RID: 488
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012DA RID: 4826 RVA: 0x00063DFA File Offset: 0x00061FFA
		private SafeCloseHandle()
		{
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00063E02 File Offset: 0x00062002
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00063E0A File Offset: 0x0006200A
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this._disposed) != 1 || UnsafeNclNativeMethods.SafeNetHandles.CloseHandle(this.handle);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00063E2F File Offset: 0x0006202F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Abort()
		{
			this.ReleaseHandle();
			base.SetHandleAsInvalid();
		}

		// Token: 0x0400151E RID: 5406
		private int _disposed;
	}
}
