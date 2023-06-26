using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C2 RID: 706
	internal struct DigestInfoAsn
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x00088ABD File Offset: 0x00086CBD
		internal static DigestInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return DigestInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00088ACC File Offset: 0x00086CCC
		internal static DigestInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			DigestInfoAsn digestInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				DigestInfoAsn digestInfoAsn;
				DigestInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out digestInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				digestInfoAsn2 = digestInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return digestInfoAsn2;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00088B1C File Offset: 0x00086D1C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			DigestInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00088B2C File Offset: 0x00086D2C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			try
			{
				DigestInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00088B64 File Offset: 0x00086D64
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			decoded = default(DigestInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.DigestAlgorithm);
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.Digest = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.Digest = asnValueReader.ReadOctetString();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E00 RID: 3584
		internal AlgorithmIdentifierAsn DigestAlgorithm;

		// Token: 0x04000E01 RID: 3585
		internal ReadOnlyMemory<byte> Digest;
	}
}
