using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F1 RID: 497
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLocalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012F4 RID: 4852 RVA: 0x000640F0 File Offset: 0x000622F0
		private SafeLocalFree()
			: base(true)
		{
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000640F9 File Offset: 0x000622F9
		private SafeLocalFree(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00064104 File Offset: 0x00062304
		public static SafeLocalFree LocalAlloc(int cb)
		{
			SafeLocalFree safeLocalFree = UnsafeNclNativeMethods.SafeNetHandles.LocalAlloc(0, (UIntPtr)((ulong)((long)cb)));
			if (safeLocalFree.IsInvalid)
			{
				safeLocalFree.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			return safeLocalFree;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00064134 File Offset: 0x00062334
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04001526 RID: 5414
		private const int LMEM_FIXED = 0;

		// Token: 0x04001527 RID: 5415
		private const int NULL = 0;

		// Token: 0x04001528 RID: 5416
		public static SafeLocalFree Zero = new SafeLocalFree(false);
	}
}
