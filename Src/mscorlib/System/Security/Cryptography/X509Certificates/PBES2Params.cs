using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C8 RID: 712
	internal struct PBES2Params
	{
		// Token: 0x0600255C RID: 9564 RVA: 0x00089300 File Offset: 0x00087500
		internal static PBES2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return PBES2Params.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00089310 File Offset: 0x00087510
		internal static PBES2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			PBES2Params pbes2Params2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				PBES2Params pbes2Params;
				PBES2Params.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbes2Params);
				asnValueReader.ThrowIfNotEmpty();
				pbes2Params2 = pbes2Params;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbes2Params2;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00089360 File Offset: 0x00087560
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			PBES2Params.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00089370 File Offset: 0x00087570
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			try
			{
				PBES2Params.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000893A8 File Offset: 0x000875A8
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			decoded = default(PBES2Params);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.KeyDerivationFunc);
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptionScheme);
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E10 RID: 3600
		internal AlgorithmIdentifierAsn KeyDerivationFunc;

		// Token: 0x04000E11 RID: 3601
		internal AlgorithmIdentifierAsn EncryptionScheme;
	}
}
