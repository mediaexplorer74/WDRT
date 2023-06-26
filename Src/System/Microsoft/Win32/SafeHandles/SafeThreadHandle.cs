using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000033 RID: 51
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x000108CD File Offset: 0x0000EACD
		internal SafeThreadHandle()
			: base(true)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000108D6 File Offset: 0x0000EAD6
		internal void InitialSetHandle(IntPtr h)
		{
			base.SetHandle(h);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000108DF File Offset: 0x0000EADF
		protected override bool ReleaseHandle()
		{
			return SafeNativeMethods.CloseHandle(this.handle);
		}
	}
}
