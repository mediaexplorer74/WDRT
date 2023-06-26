using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x0200058C RID: 1420
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UTF8Marshaler
	{
		// Token: 0x060042DF RID: 17119 RVA: 0x000FAA8C File Offset: 0x000F8C8C
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
			if (ptr != null)
			{
				num = (strManaged.Length + 1) * 3;
				num = strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			else
			{
				num = Encoding.UTF8.GetByteCount(strManaged);
				ptr = (byte*)(void*)Marshal.AllocCoTaskMem(num + 1);
				strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000FAB08 File Offset: 0x000F8D08
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr cstr)
		{
			if (IntPtr.Zero == cstr)
			{
				return null;
			}
			int num = StubHelpers.strlen((sbyte*)(void*)cstr);
			return string.CreateStringFromEncoding((byte*)(void*)cstr, num, Encoding.UTF8);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000FAB41 File Offset: 0x000F8D41
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (pNative != IntPtr.Zero)
			{
				Win32Native.CoTaskMemFree(pNative);
			}
		}

		// Token: 0x04001BDE RID: 7134
		private const int MAX_UTF8_CHAR_SIZE = 3;
	}
}
