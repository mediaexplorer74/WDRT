using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001E7 RID: 487
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeAddrInfo : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x00063DD8 File Offset: 0x00061FD8
		private SafeFreeAddrInfo()
			: base(true)
		{
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00063DE1 File Offset: 0x00061FE1
		internal static int GetAddrInfo(string nodename, string servicename, ref AddressInfo hints, out SafeFreeAddrInfo outAddrInfo)
		{
			return UnsafeNclNativeMethods.SafeNetHandlesXPOrLater.GetAddrInfoW(nodename, servicename, ref hints, out outAddrInfo);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00063DEC File Offset: 0x00061FEC
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandlesXPOrLater.freeaddrinfo(this.handle);
			return true;
		}

		// Token: 0x0400151D RID: 5405
		private const string WS2_32 = "ws2_32.dll";
	}
}
