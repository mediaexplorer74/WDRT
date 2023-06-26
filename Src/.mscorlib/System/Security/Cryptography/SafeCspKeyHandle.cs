using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023F RID: 575
	[SecurityCritical]
	internal sealed class SafeCspKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x00072829 File Offset: 0x00070A29
		internal SafeCspKeyHandle()
			: base(true)
		{
		}

		// Token: 0x060020A4 RID: 8356
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyKey(IntPtr hKey);

		// Token: 0x060020A5 RID: 8357 RVA: 0x00072832 File Offset: 0x00070A32
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspKeyHandle.CryptDestroyKey(this.handle);
		}
	}
}
