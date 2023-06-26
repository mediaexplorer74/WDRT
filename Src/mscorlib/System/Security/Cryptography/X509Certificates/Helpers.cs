using System;
using System.Diagnostics;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B7 RID: 695
	internal static class Helpers
	{
		// Token: 0x06002508 RID: 9480 RVA: 0x00087134 File Offset: 0x00085334
		internal static bool SequenceEqual(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00087164 File Offset: 0x00085364
		internal static ReadOnlyMemory<byte> DecodeOctetStringAsMemory(ReadOnlyMemory<byte> encodedOctetString)
		{
			ReadOnlyMemory<byte> readOnlyMemory;
			try
			{
				ReadOnlySpan<byte> span = encodedOctetString.Span;
				ReadOnlySpan<byte> readOnlySpan;
				int num;
				if (AsnDecoder.TryReadPrimitiveOctetString(span, AsnEncodingRules.BER, out readOnlySpan, out num, null))
				{
					if (num != span.Length)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					int num2;
					if (span.Overlaps(readOnlySpan, out num2))
					{
						return encodedOctetString.Slice(num2, readOnlySpan.Length);
					}
					Assert.Fail("input.Overlaps(primitive)", "input.Overlaps(primitive) failed after TryReadPrimitiveOctetString succeeded");
				}
				byte[] array = AsnDecoder.ReadOctetString(span, AsnEncodingRules.BER, out num, null);
				if (num != span.Length)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				readOnlyMemory = array;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return readOnlyMemory;
		}
	}
}
