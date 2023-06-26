using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B6 RID: 694
	internal static class CryptographicOperations
	{
		// Token: 0x06002507 RID: 9479 RVA: 0x0008712B File Offset: 0x0008532B
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ZeroMemory(Span<byte> buffer)
		{
			buffer.Clear();
		}
	}
}
