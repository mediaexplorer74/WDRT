using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C4 RID: 708
	internal struct EncryptedDataAsn
	{
		// Token: 0x06002547 RID: 9543 RVA: 0x00088D84 File Offset: 0x00086F84
		internal static EncryptedDataAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedDataAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x00088D94 File Offset: 0x00086F94
		internal static EncryptedDataAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedDataAsn encryptedDataAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedDataAsn encryptedDataAsn;
				EncryptedDataAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedDataAsn);
				asnValueReader.ThrowIfNotEmpty();
				encryptedDataAsn2 = encryptedDataAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return encryptedDataAsn2;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00088DE4 File Offset: 0x00086FE4
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedDataAsn decoded)
		{
			EncryptedDataAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00088DF4 File Offset: 0x00086FF4
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedDataAsn decoded)
		{
			try
			{
				EncryptedDataAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00088E2C File Offset: 0x0008702C
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedDataAsn decoded)
		{
			decoded = default(EncryptedDataAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			if (!asnValueReader.TryReadInt32(out decoded.Version))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			EncryptedContentInfoAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptedContentInfo);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
			{
				AsnValueReader asnValueReader2 = asnValueReader.ReadSetOf(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 1)));
				List<AttributeAsn> list = new List<AttributeAsn>();
				while (asnValueReader2.HasData)
				{
					AttributeAsn attributeAsn;
					AttributeAsn.Decode(ref asnValueReader2, rebind, out attributeAsn);
					list.Add(attributeAsn);
				}
				decoded.UnprotectedAttributes = list.ToArray();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E05 RID: 3589
		internal int Version;

		// Token: 0x04000E06 RID: 3590
		internal EncryptedContentInfoAsn EncryptedContentInfo;

		// Token: 0x04000E07 RID: 3591
		internal AttributeAsn[] UnprotectedAttributes;
	}
}
