using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200058A RID: 1418
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class AnsiCharMarshaler
	{
		// Token: 0x060042D9 RID: 17113 RVA: 0x000FA908 File Offset: 0x000F8B08
		[SecurityCritical]
		internal unsafe static byte[] DoAnsiConversion(string str, bool fBestFit, bool fThrowOnUnmappableChar, out int cbLength)
		{
			byte[] array = new byte[(str.Length + 1) * Marshal.SystemMaxDBCSCharSize];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			cbLength = str.ConvertToAnsi(ptr, array.Length, fBestFit, fThrowOnUnmappableChar);
			array2 = null;
			return array;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000FA954 File Offset: 0x000F8B54
		[SecurityCritical]
		internal unsafe static byte ConvertToNative(char managedChar, bool fBestFit, bool fThrowOnUnmappableChar)
		{
			int num = 2 * Marshal.SystemMaxDBCSCharSize;
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = managedChar.ToString().ConvertToAnsi(ptr, num, fBestFit, fThrowOnUnmappableChar);
			return *ptr;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x000FA984 File Offset: 0x000F8B84
		internal static char ConvertToManaged(byte nativeChar)
		{
			byte[] array = new byte[] { nativeChar };
			string @string = Encoding.Default.GetString(array);
			return @string[0];
		}
	}
}
