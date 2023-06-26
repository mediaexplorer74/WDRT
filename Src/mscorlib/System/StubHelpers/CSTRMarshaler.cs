using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x0200058B RID: 1419
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class CSTRMarshaler
	{
		// Token: 0x060042DC RID: 17116 RVA: 0x000FA9B0 File Offset: 0x000F8BB0
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			StubHelpers.CheckStringLength(strManaged.Length);
			byte* ptr = (byte*)(void*)pNativeBuffer;
			int num;
			if (ptr != null || Marshal.SystemMaxDBCSCharSize == 1)
			{
				num = (strManaged.Length + 1) * Marshal.SystemMaxDBCSCharSize;
				if (ptr == null)
				{
					ptr = (byte*)(void*)Marshal.AllocCoTaskMem(num + 1);
				}
				num = strManaged.ConvertToAnsi(ptr, num + 1, (flags & 255) != 0, flags >> 8 != 0);
			}
			else
			{
				byte[] array = AnsiCharMarshaler.DoAnsiConversion(strManaged, (flags & 255) != 0, flags >> 8 != 0, out num);
				ptr = (byte*)(void*)Marshal.AllocCoTaskMem(num + 2);
				Buffer.Memcpy(ptr, 0, array, 0, num);
			}
			ptr[num] = 0;
			ptr[num + 1] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x000FAA67 File Offset: 0x000F8C67
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr cstr)
		{
			if (IntPtr.Zero == cstr)
			{
				return null;
			}
			return new string((sbyte*)(void*)cstr);
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x000FAA83 File Offset: 0x000F8C83
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			Win32Native.CoTaskMemFree(pNative);
		}
	}
}
