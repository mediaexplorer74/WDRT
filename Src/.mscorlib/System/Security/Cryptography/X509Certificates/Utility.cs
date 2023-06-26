using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D7 RID: 727
	internal static class Utility
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x0008ACA4 File Offset: 0x00088EA4
		public static Span<T> GetSpanForArray<T>(T[] array, int offset)
		{
			return Utility.GetSpanForArray<T>(array, offset, array.Length - offset);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0008ACB2 File Offset: 0x00088EB2
		public static Span<T> GetSpanForArray<T>(T[] array, int offset, int count)
		{
			return new Span<T>(array, offset, count);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0008ACBC File Offset: 0x00088EBC
		public static int EncodingGetByteCount(Encoding encoding, ReadOnlySpan<char> input)
		{
			if (input.IsNull)
			{
				return encoding.GetByteCount(new char[0]);
			}
			ArraySegment<char> arraySegment = input.DangerousGetArraySegment();
			return encoding.GetByteCount(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0008AD04 File Offset: 0x00088F04
		public static int EncodingGetBytes(Encoding encoding, char[] input, Span<byte> destination)
		{
			ArraySegment<byte> arraySegment = destination.DangerousGetArraySegment();
			return encoding.GetBytes(input, 0, input.Length, arraySegment.Array, arraySegment.Offset);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0008AD34 File Offset: 0x00088F34
		public static int EncodingGetBytes(Encoding encoding, ReadOnlySpan<char> input, Span<byte> destination)
		{
			ArraySegment<byte> arraySegment = destination.DangerousGetArraySegment();
			ArraySegment<char> arraySegment2 = input.DangerousGetArraySegment();
			return encoding.GetBytes(arraySegment2.Array, arraySegment2.Offset, arraySegment2.Count, arraySegment.Array, arraySegment.Offset);
		}
	}
}
