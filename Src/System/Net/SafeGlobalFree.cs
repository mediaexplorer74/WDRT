using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F2 RID: 498
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeGlobalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x00064158 File Offset: 0x00062358
		private SafeGlobalFree()
			: base(true)
		{
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00064161 File Offset: 0x00062361
		private SafeGlobalFree(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0006416A File Offset: 0x0006236A
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.GlobalFree(this.handle) == IntPtr.Zero;
		}
	}
}
