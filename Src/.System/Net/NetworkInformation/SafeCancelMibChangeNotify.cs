using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002EF RID: 751
	[SuppressUnmanagedCodeSecurity]
	internal class SafeCancelMibChangeNotify : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x0007FC94 File Offset: 0x0007DE94
		public SafeCancelMibChangeNotify()
			: base(true)
		{
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0007FCA0 File Offset: 0x0007DEA0
		protected override bool ReleaseHandle()
		{
			uint num = UnsafeNetInfoNativeMethods.CancelMibChangeNotify2(this.handle);
			this.handle = IntPtr.Zero;
			return num == 0U;
		}
	}
}
