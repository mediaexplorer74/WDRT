using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C3 RID: 707
	internal struct EncryptedContentInfoAsn
	{
		// Token: 0x06002542 RID: 9538 RVA: 0x00088BF0 File Offset: 0x00086DF0
		internal static EncryptedContentInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedContentInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x00088C00 File Offset: 0x00086E00
		internal static EncryptedContentInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedContentInfoAsn encryptedContentInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedContentInfoAsn encryptedContentInfoAsn;
				EncryptedContentInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedContentInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				encryptedContentInfoAsn2 = encryptedContentInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return encryptedContentInfoAsn2;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00088C50 File Offset: 0x00086E50
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			EncryptedContentInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00088C60 File Offset: 0x00086E60
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			try
			{
				EncryptedContentInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00088C98 File Offset: 0x00086E98
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			decoded = default(EncryptedContentInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.ContentType = asnValueReader.ReadObjectIdentifier();
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.ContentEncryptionAlgorithm);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
			{
				ReadOnlySpan<byte> readOnlySpan;
				if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan, new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0))))
				{
					int num;
					decoded.EncryptedContent = new ReadOnlyMemory<byte>?(span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
				}
				else
				{
					decoded.EncryptedContent = new ReadOnlyMemory<byte>?(asnValueReader.ReadOctetString(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0))));
				}
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E02 RID: 3586
		internal byte[] ContentType;

		// Token: 0x04000E03 RID: 3587
		internal AlgorithmIdentifierAsn ContentEncryptionAlgorithm;

		// Token: 0x04000E04 RID: 3588
		internal ReadOnlyMemory<byte>? EncryptedContent;
	}
}
