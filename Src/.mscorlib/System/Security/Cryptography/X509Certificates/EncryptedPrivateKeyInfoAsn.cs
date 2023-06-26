using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C5 RID: 709
	internal struct EncryptedPrivateKeyInfoAsn
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x00088EE4 File Offset: 0x000870E4
		internal static EncryptedPrivateKeyInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedPrivateKeyInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00088EF4 File Offset: 0x000870F4
		internal static EncryptedPrivateKeyInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn;
				EncryptedPrivateKeyInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedPrivateKeyInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				encryptedPrivateKeyInfoAsn2 = encryptedPrivateKeyInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return encryptedPrivateKeyInfoAsn2;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00088F44 File Offset: 0x00087144
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			EncryptedPrivateKeyInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00088F54 File Offset: 0x00087154
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			try
			{
				EncryptedPrivateKeyInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00088F8C File Offset: 0x0008718C
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			decoded = default(EncryptedPrivateKeyInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptionAlgorithm);
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.EncryptedData = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.EncryptedData = asnValueReader.ReadOctetString();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E08 RID: 3592
		internal AlgorithmIdentifierAsn EncryptionAlgorithm;

		// Token: 0x04000E09 RID: 3593
		internal ReadOnlyMemory<byte> EncryptedData;
	}
}
