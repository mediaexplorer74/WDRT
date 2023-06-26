using System;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000202 RID: 514
	[SuppressUnmanagedCodeSecurity]
	internal class SafeLocalFreeChannelBinding : ChannelBinding
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00065D1C File Offset: 0x00063F1C
		public override int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00065D24 File Offset: 0x00063F24
		public static SafeLocalFreeChannelBinding LocalAlloc(int cb)
		{
			SafeLocalFreeChannelBinding safeLocalFreeChannelBinding = UnsafeNclNativeMethods.SafeNetHandles.LocalAllocChannelBinding(0, (UIntPtr)((ulong)((long)cb)));
			if (safeLocalFreeChannelBinding.IsInvalid)
			{
				safeLocalFreeChannelBinding.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			safeLocalFreeChannelBinding.size = cb;
			return safeLocalFreeChannelBinding;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00065D5B File Offset: 0x00063F5B
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04001546 RID: 5446
		private const int LMEM_FIXED = 0;

		// Token: 0x04001547 RID: 5447
		private int size;
	}
}
