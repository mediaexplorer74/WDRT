using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023D RID: 573
	[SecurityCritical]
	internal sealed class SafeCspHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x000727FC File Offset: 0x000709FC
		private SafeCspHandle()
			: base(true)
		{
		}

		// Token: 0x0600209E RID: 8350
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x0600209F RID: 8351 RVA: 0x00072805 File Offset: 0x00070A05
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspHandle.CryptReleaseContext(this.handle, 0);
		}
	}
}
