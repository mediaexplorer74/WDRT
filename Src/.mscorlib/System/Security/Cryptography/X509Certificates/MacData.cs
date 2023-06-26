using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C6 RID: 710
	internal struct MacData
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x00089018 File Offset: 0x00087218
		internal static MacData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return MacData.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00089028 File Offset: 0x00087228
		internal static MacData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			MacData macData2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				MacData macData;
				MacData.DecodeCore(ref asnValueReader, expectedTag, encoded, out macData);
				asnValueReader.ThrowIfNotEmpty();
				macData2 = macData;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return macData2;
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00089078 File Offset: 0x00087278
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			MacData.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00089088 File Offset: 0x00087288
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			try
			{
				MacData.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000890C0 File Offset: 0x000872C0
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			decoded = default(MacData);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			DigestInfoAsn.Decode(ref asnValueReader, rebind, out decoded.Mac);
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.MacSalt = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.MacSalt = asnValueReader.ReadOctetString();
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
			{
				if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
				{
					asnValueReader.ThrowIfNotEmpty();
				}
			}
			else
			{
				AsnValueReader asnValueReader2 = new AsnValueReader(MacData.s_DefaultIterationCount, AsnEncodingRules.DER);
				if (!asnValueReader2.TryReadInt32(out decoded.IterationCount))
				{
					asnValueReader2.ThrowIfNotEmpty();
				}
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E0A RID: 3594
		private static readonly byte[] s_DefaultIterationCount = new byte[] { 2, 1, 1 };

		// Token: 0x04000E0B RID: 3595
		internal DigestInfoAsn Mac;

		// Token: 0x04000E0C RID: 3596
		internal ReadOnlyMemory<byte> MacSalt;

		// Token: 0x04000E0D RID: 3597
		internal int IterationCount;
	}
}
