using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D9 RID: 729
	internal static class CryptoPool
	{
		// Token: 0x060025C4 RID: 9668 RVA: 0x0008ADB8 File Offset: 0x00088FB8
		public static byte[] Rent(int size)
		{
			return new byte[size];
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0008ADC0 File Offset: 0x00088FC0
		public static void Return(byte[] array, int clearSize)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(array, 0, clearSize));
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0008ADCF File Offset: 0x00088FCF
		public static void Return(byte[] array)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(array));
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0008ADDC File Offset: 0x00088FDC
		public static void Return(ArraySegment<byte> segment, int clearSize)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(segment).Slice(0, clearSize));
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0008ADFE File Offset: 0x00088FFE
		public static void Return(ArraySegment<byte> segment)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(segment));
		}
	}
}
