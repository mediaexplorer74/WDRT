using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F0 RID: 752
	[SuppressUnmanagedCodeSecurity]
	internal class SafeFreeMibTable : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001A58 RID: 6744 RVA: 0x0007FCC8 File Offset: 0x0007DEC8
		public SafeFreeMibTable()
			: base(true)
		{
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0007FCD1 File Offset: 0x0007DED1
		protected override bool ReleaseHandle()
		{
			UnsafeNetInfoNativeMethods.FreeMibTable(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
