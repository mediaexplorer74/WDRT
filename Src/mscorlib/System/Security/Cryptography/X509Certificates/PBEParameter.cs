using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C7 RID: 711
	internal struct PBEParameter
	{
		// Token: 0x06002557 RID: 9559 RVA: 0x000891C5 File Offset: 0x000873C5
		internal static PBEParameter Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return PBEParameter.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000891D4 File Offset: 0x000873D4
		internal static PBEParameter Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			PBEParameter pbeparameter2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				PBEParameter pbeparameter;
				PBEParameter.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbeparameter);
				asnValueReader.ThrowIfNotEmpty();
				pbeparameter2 = pbeparameter;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbeparameter2;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00089224 File Offset: 0x00087424
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			PBEParameter.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00089234 File Offset: 0x00087434
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			try
			{
				PBEParameter.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0008926C File Offset: 0x0008746C
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			decoded = default(PBEParameter);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.Salt = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.Salt = asnValueReader.ReadOctetString();
			}
			if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E0E RID: 3598
		internal ReadOnlyMemory<byte> Salt;

		// Token: 0x04000E0F RID: 3599
		internal int IterationCount;
	}
}
