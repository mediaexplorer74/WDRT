using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A5 RID: 1445
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class HResultExceptionMarshaler
	{
		// Token: 0x06004340 RID: 17216 RVA: 0x000FB9FE File Offset: 0x000F9BFE
		internal static int ConvertToNative(Exception ex)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (ex == null)
			{
				return 0;
			}
			return ex._HResult;
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x000FBA24 File Offset: 0x000F9C24
		[SecuritySafeCritical]
		internal static Exception ConvertToManaged(int hr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			Exception ex = null;
			if (hr < 0)
			{
				ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, IntPtr.Zero, null, true);
			}
			return ex;
		}
	}
}
