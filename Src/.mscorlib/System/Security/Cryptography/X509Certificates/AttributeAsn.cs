using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C0 RID: 704
	internal struct AttributeAsn
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x0008883A File Offset: 0x00086A3A
		internal static AttributeAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return AttributeAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00088848 File Offset: 0x00086A48
		internal static AttributeAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			AttributeAsn attributeAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				AttributeAsn attributeAsn;
				AttributeAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out attributeAsn);
				asnValueReader.ThrowIfNotEmpty();
				attributeAsn2 = attributeAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return attributeAsn2;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00088898 File Offset: 0x00086A98
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out AttributeAsn decoded)
		{
			AttributeAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000888A8 File Offset: 0x00086AA8
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AttributeAsn decoded)
		{
			try
			{
				AttributeAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000888E0 File Offset: 0x00086AE0
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AttributeAsn decoded)
		{
			decoded = default(AttributeAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.AttrType = asnValueReader.ReadObjectIdentifier();
			AsnValueReader asnValueReader2 = asnValueReader.ReadSetOf();
			List<ReadOnlyMemory<byte>> list = new List<ReadOnlyMemory<byte>>();
			while (asnValueReader2.HasData)
			{
				ReadOnlySpan<byte> readOnlySpan = asnValueReader2.ReadEncodedValue();
				int num;
				ReadOnlyMemory<byte> readOnlyMemory = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
				list.Add(readOnlyMemory);
			}
			decoded.AttrValues = list.ToArray();
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000DFC RID: 3580
		internal byte[] AttrType;

		// Token: 0x04000DFD RID: 3581
		internal ReadOnlyMemory<byte>[] AttrValues;
	}
}
