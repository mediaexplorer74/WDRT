using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000590 RID: 1424
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class AnsiBSTRMarshaler
	{
		// Token: 0x060042EA RID: 17130 RVA: 0x000FADF4 File Offset: 0x000F8FF4
		[SecurityCritical]
		internal static IntPtr ConvertToNative(int flags, string strManaged)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			int length = strManaged.Length;
			StubHelpers.CheckStringLength(length);
			byte[] array = null;
			int num = 0;
			if (length > 0)
			{
				array = AnsiCharMarshaler.DoAnsiConversion(strManaged, (flags & 255) != 0, flags >> 8 != 0, out num);
			}
			return Win32Native.SysAllocStringByteLen(array, (uint)num);
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x000FAE3F File Offset: 0x000F903F
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr bstr)
		{
			if (IntPtr.Zero == bstr)
			{
				return null;
			}
			return new string((sbyte*)(void*)bstr);
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000FAE5B File Offset: 0x000F905B
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Win32Native.SysFreeString(pNative);
			}
		}
	}
}
