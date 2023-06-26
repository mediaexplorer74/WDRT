using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D8 RID: 728
	internal static class BinaryPrimitives
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x0008AD7A File Offset: 0x00088F7A
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> bytes, out ushort value)
		{
			if (bytes.Length < 2)
			{
				value = 0;
				return false;
			}
			value = (ushort)((int)bytes[1] | ((int)bytes[0] << 8));
			return true;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0008ADA2 File Offset: 0x00088FA2
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> bytes)
		{
			return (short)((int)bytes[1] | ((int)bytes[0] << 8));
		}
	}
}
