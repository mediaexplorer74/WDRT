using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000594 RID: 1428
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class HStringMarshaler
	{
		// Token: 0x060042F2 RID: 17138 RVA: 0x000FAEE0 File Offset: 0x000F90E0
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(string managed)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (managed == null)
			{
				throw new ArgumentNullException();
			}
			IntPtr intPtr;
			int num = UnsafeNativeMethods.WindowsCreateString(managed, managed.Length, &intPtr);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return intPtr;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000FAF2C File Offset: 0x000F912C
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNativeReference(string managed, [Out] HSTRING_HEADER* hstringHeader)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (managed == null)
			{
				throw new ArgumentNullException();
			}
			char* ptr = managed;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			IntPtr intPtr;
			int num = UnsafeNativeMethods.WindowsCreateStringReference(ptr, managed.Length, hstringHeader, &intPtr);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return intPtr;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000FAF87 File Offset: 0x000F9187
		[SecurityCritical]
		internal static string ConvertToManaged(IntPtr hstring)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			return WindowsRuntimeMarshal.HStringToString(hstring);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000FAFA6 File Offset: 0x000F91A6
		[SecurityCritical]
		internal static void ClearNative(IntPtr hstring)
		{
			if (hstring != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(hstring);
			}
		}
	}
}
