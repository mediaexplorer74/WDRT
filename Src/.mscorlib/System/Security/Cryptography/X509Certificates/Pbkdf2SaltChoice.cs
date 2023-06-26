using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002CA RID: 714
	internal struct Pbkdf2SaltChoice
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x00089588 File Offset: 0x00087788
		internal static Pbkdf2SaltChoice Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			Pbkdf2SaltChoice pbkdf2SaltChoice2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				Pbkdf2SaltChoice pbkdf2SaltChoice;
				Pbkdf2SaltChoice.DecodeCore(ref asnValueReader, encoded, out pbkdf2SaltChoice);
				asnValueReader.ThrowIfNotEmpty();
				pbkdf2SaltChoice2 = pbkdf2SaltChoice;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbkdf2SaltChoice2;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000895D8 File Offset: 0x000877D8
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2SaltChoice decoded)
		{
			try
			{
				Pbkdf2SaltChoice.DecodeCore(ref reader, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0008960C File Offset: 0x0008780C
		private static void DecodeCore(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2SaltChoice decoded)
		{
			decoded = default(Pbkdf2SaltChoice);
			Asn1Tag asn1Tag = reader.PeekTag();
			ReadOnlySpan<byte> span = rebind.Span;
			if (asn1Tag.HasSameClassAndValue(Asn1Tag.PrimitiveOctetString))
			{
				ReadOnlySpan<byte> readOnlySpan;
				if (reader.TryReadPrimitiveOctetString(out readOnlySpan))
				{
					int num;
					decoded.Specified = new ReadOnlyMemory<byte>?(span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
					return;
				}
				decoded.Specified = new ReadOnlyMemory<byte>?(reader.ReadOctetString());
				return;
			}
			else
			{
				if (asn1Tag.HasSameClassAndValue(Asn1Tag.Sequence))
				{
					AlgorithmIdentifierAsn algorithmIdentifierAsn;
					AlgorithmIdentifierAsn.Decode(ref reader, rebind, out algorithmIdentifierAsn);
					decoded.OtherSource = new AlgorithmIdentifierAsn?(algorithmIdentifierAsn);
					return;
				}
				throw new CryptographicException();
			}
		}

		// Token: 0x04000E17 RID: 3607
		internal ReadOnlyMemory<byte>? Specified;

		// Token: 0x04000E18 RID: 3608
		internal AlgorithmIdentifierAsn? OtherSource;
	}
}
