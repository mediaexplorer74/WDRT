using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200058D RID: 1421
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UTF8BufferMarshaler
	{
		// Token: 0x060042E2 RID: 17122 RVA: 0x000FAB58 File Offset: 0x000F8D58
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(StringBuilder sb, IntPtr pNativeBuffer, int flags)
		{
			if (sb == null)
			{
				return IntPtr.Zero;
			}
			string text = sb.ToString();
			int num = Encoding.UTF8.GetByteCount(text);
			byte* ptr = (byte*)(void*)pNativeBuffer;
			num = text.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x000FABA4 File Offset: 0x000F8DA4
		[SecurityCritical]
		internal unsafe static void ConvertToManaged(StringBuilder sb, IntPtr pNative)
		{
			int num = StubHelpers.strlen((sbyte*)(void*)pNative);
			int num2 = Encoding.UTF8.GetCharCount((byte*)(void*)pNative, num);
			char[] array = new char[num2 + 1];
			array[num2] = '\0';
			char[] array2;
			char* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			num2 = Encoding.UTF8.GetChars((byte*)(void*)pNative, num, ptr, num2);
			sb.ReplaceBufferInternal(ptr, num2);
			array2 = null;
		}
	}
}
