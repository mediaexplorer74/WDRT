using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002BF RID: 703
	internal struct AlgorithmIdentifierAsn
	{
		// Token: 0x0600252B RID: 9515 RVA: 0x000886A2 File Offset: 0x000868A2
		internal static AlgorithmIdentifierAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return AlgorithmIdentifierAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000886B0 File Offset: 0x000868B0
		internal static AlgorithmIdentifierAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			AlgorithmIdentifierAsn algorithmIdentifierAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				AlgorithmIdentifierAsn algorithmIdentifierAsn;
				AlgorithmIdentifierAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out algorithmIdentifierAsn);
				asnValueReader.ThrowIfNotEmpty();
				algorithmIdentifierAsn2 = algorithmIdentifierAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return algorithmIdentifierAsn2;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00088700 File Offset: 0x00086900
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			AlgorithmIdentifierAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00088710 File Offset: 0x00086910
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			try
			{
				AlgorithmIdentifierAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00088748 File Offset: 0x00086948
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			decoded = default(AlgorithmIdentifierAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.Algorithm = asnValueReader.ReadObjectIdentifier();
			if (asnValueReader.HasData)
			{
				ReadOnlySpan<byte> readOnlySpan = asnValueReader.ReadEncodedValue();
				int num;
				decoded.Parameters = new ReadOnlyMemory<byte>?(span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000887CA File Offset: 0x000869CA
		internal bool HasNullEquivalentParameters()
		{
			return AlgorithmIdentifierAsn.RepresentsNull(this.Parameters);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000887D8 File Offset: 0x000869D8
		internal static bool RepresentsNull(ReadOnlyMemory<byte>? parameters)
		{
			if (parameters == null)
			{
				return true;
			}
			ReadOnlySpan<byte> span = parameters.Value.Span;
			return span.Length == 2 && span[0] == 5 && span[1] == 0;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00088824 File Offset: 0x00086A24
		// Note: this type is marked as 'beforefieldinit'.
		static AlgorithmIdentifierAsn()
		{
			byte[] array = new byte[2];
			array[0] = 5;
			AlgorithmIdentifierAsn.ExplicitDerNull = array;
		}

		// Token: 0x04000DF9 RID: 3577
		internal byte[] Algorithm;

		// Token: 0x04000DFA RID: 3578
		internal ReadOnlyMemory<byte>? Parameters;

		// Token: 0x04000DFB RID: 3579
		internal static readonly ReadOnlyMemory<byte> ExplicitDerNull;
	}
}
