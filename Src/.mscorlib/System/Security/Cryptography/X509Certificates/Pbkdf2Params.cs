using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C9 RID: 713
	internal struct Pbkdf2Params
	{
		// Token: 0x06002561 RID: 9569 RVA: 0x000893EC File Offset: 0x000875EC
		internal static Pbkdf2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return Pbkdf2Params.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000893FC File Offset: 0x000875FC
		internal static Pbkdf2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			Pbkdf2Params pbkdf2Params2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				Pbkdf2Params pbkdf2Params;
				Pbkdf2Params.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbkdf2Params);
				asnValueReader.ThrowIfNotEmpty();
				pbkdf2Params2 = pbkdf2Params;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbkdf2Params2;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0008944C File Offset: 0x0008764C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			Pbkdf2Params.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0008945C File Offset: 0x0008765C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			try
			{
				Pbkdf2Params.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00089494 File Offset: 0x00087694
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			decoded = default(Pbkdf2Params);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			Pbkdf2SaltChoice.Decode(ref asnValueReader, rebind, out decoded.Salt);
			if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
			{
				int num;
				if (asnValueReader.TryReadInt32(out num))
				{
					decoded.KeyLength = new int?(num);
				}
				else
				{
					asnValueReader.ThrowIfNotEmpty();
				}
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
			{
				AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.Prf);
			}
			else
			{
				AsnValueReader asnValueReader2 = new AsnValueReader(Pbkdf2Params.s_DefaultPrf, AsnEncodingRules.DER);
				AlgorithmIdentifierAsn.Decode(ref asnValueReader2, rebind, out decoded.Prf);
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E12 RID: 3602
		private static readonly byte[] s_DefaultPrf = new byte[]
		{
			48, 12, 6, 8, 42, 134, 72, 134, 247, 13,
			2, 7, 5, 0
		};

		// Token: 0x04000E13 RID: 3603
		internal Pbkdf2SaltChoice Salt;

		// Token: 0x04000E14 RID: 3604
		internal int IterationCount;

		// Token: 0x04000E15 RID: 3605
		internal int? KeyLength;

		// Token: 0x04000E16 RID: 3606
		internal AlgorithmIdentifierAsn Prf;
	}
}
