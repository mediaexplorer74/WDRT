using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023E RID: 574
	[SecurityCritical]
	internal sealed class SafeCspHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A0 RID: 8352 RVA: 0x00072813 File Offset: 0x00070A13
		private SafeCspHashHandle()
			: base(true)
		{
		}

		// Token: 0x060020A1 RID: 8353
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyHash(IntPtr hKey);

		// Token: 0x060020A2 RID: 8354 RVA: 0x0007281C File Offset: 0x00070A1C
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspHashHandle.CryptDestroyHash(this.handle);
		}
	}
}
