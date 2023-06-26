using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x0200058F RID: 1423
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class VBByValStrMarshaler
	{
		// Token: 0x060042E7 RID: 17127 RVA: 0x000FAD3C File Offset: 0x000F8F3C
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(string strManaged, bool fBestFit, bool fThrowOnUnmappableChar, ref int cch)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			cch = strManaged.Length;
			StubHelpers.CheckStringLength(cch);
			int num = 4 + (cch + 1) * Marshal.SystemMaxDBCSCharSize;
			byte* ptr = (byte*)(void*)Marshal.AllocCoTaskMem(num);
			int* ptr2 = (int*)ptr;
			ptr += 4;
			if (cch == 0)
			{
				*ptr = 0;
				*ptr2 = 0;
			}
			else
			{
				int num2;
				byte[] array = AnsiCharMarshaler.DoAnsiConversion(strManaged, fBestFit, fThrowOnUnmappableChar, out num2);
				Buffer.Memcpy(ptr, 0, array, 0, num2);
				ptr[num2] = 0;
				*ptr2 = num2;
			}
			return new IntPtr((void*)ptr);
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x000FADB1 File Offset: 0x000F8FB1
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr pNative, int cch)
		{
			if (IntPtr.Zero == pNative)
			{
				return null;
			}
			return new string((sbyte*)(void*)pNative, 0, cch);
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x000FADCF File Offset: 0x000F8FCF
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Win32Native.CoTaskMemFree((IntPtr)((long)pNative - 4L));
			}
		}
	}
}
