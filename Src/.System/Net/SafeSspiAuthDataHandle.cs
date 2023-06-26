using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001ED RID: 493
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeSspiAuthDataHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x00063EED File Offset: 0x000620ED
		public SafeSspiAuthDataHandle()
			: base(true)
		{
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00063EF6 File Offset: 0x000620F6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SspiHelper.SspiFreeAuthIdentity(this.handle) == SecurityStatus.OK;
		}
	}
}
