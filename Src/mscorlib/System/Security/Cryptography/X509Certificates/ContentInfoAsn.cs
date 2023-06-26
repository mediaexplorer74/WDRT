using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C1 RID: 705
	internal struct ContentInfoAsn
	{
		// Token: 0x06002538 RID: 9528 RVA: 0x00088982 File Offset: 0x00086B82
		internal static ContentInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return ContentInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00088990 File Offset: 0x00086B90
		internal static ContentInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			ContentInfoAsn contentInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				ContentInfoAsn contentInfoAsn;
				ContentInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out contentInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				contentInfoAsn2 = contentInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return contentInfoAsn2;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000889E0 File Offset: 0x00086BE0
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			ContentInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000889F0 File Offset: 0x00086BF0
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			try
			{
				ContentInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00088A28 File Offset: 0x00086C28
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			decoded = default(ContentInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.ContentType = asnValueReader.ReadObjectIdentifier();
			AsnValueReader asnValueReader2 = asnValueReader.ReadSequence(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0)));
			ReadOnlySpan<byte> readOnlySpan = asnValueReader2.ReadEncodedValue();
			int num;
			decoded.Content = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			asnValueReader2.ThrowIfNotEmpty();
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000DFE RID: 3582
		internal byte[] ContentType;

		// Token: 0x04000DFF RID: 3583
		internal ReadOnlyMemory<byte> Content;
	}
}
