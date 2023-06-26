using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002CB RID: 715
	internal struct PfxAsn
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x000896BD File Offset: 0x000878BD
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PfxAsn decoded)
		{
			PfxAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000896CC File Offset: 0x000878CC
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PfxAsn decoded)
		{
			try
			{
				PfxAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00089704 File Offset: 0x00087904
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PfxAsn decoded)
		{
			decoded = default(PfxAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			if (!asnValueReader.TryReadInt32(out decoded.Version))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			ContentInfoAsn.Decode(ref asnValueReader, rebind, out decoded.AuthSafe);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
			{
				MacData macData;
				System.Security.Cryptography.X509Certificates.MacData.Decode(ref asnValueReader, rebind, out macData);
				decoded.MacData = new MacData?(macData);
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00089788 File Offset: 0x00087988
		internal ulong CountTotalIterations()
		{
			ulong num = 0UL;
			if (!Helpers.SequenceEqual(this.AuthSafe.ContentType, Oids.Pkcs7Data))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlyMemory<byte> readOnlyMemory = Helpers.DecodeOctetStringAsMemory(this.AuthSafe.Content);
			AsnValueReader asnValueReader = new AsnValueReader(readOnlyMemory.Span, AsnEncodingRules.BER);
			AsnValueReader asnValueReader2 = asnValueReader.ReadSequence();
			asnValueReader.ThrowIfNotEmpty();
			bool flag = false;
			checked
			{
				while (asnValueReader2.HasData)
				{
					ContentInfoAsn contentInfoAsn;
					ContentInfoAsn.Decode(ref asnValueReader2, readOnlyMemory, out contentInfoAsn);
					ArraySegment<byte>? arraySegment = null;
					try
					{
						ReadOnlyMemory<byte> readOnlyMemory2;
						if (!Helpers.SequenceEqual(contentInfoAsn.ContentType, Oids.Pkcs7Data))
						{
							if (!Helpers.SequenceEqual(contentInfoAsn.ContentType, Oids.Pkcs7Encrypted))
							{
								throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"));
							}
							if (flag)
							{
								throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"));
							}
							uint num2;
							ArraySegment<byte> arraySegment2 = PfxAsn.DecryptContentInfo(contentInfoAsn, out num2);
							readOnlyMemory2 = arraySegment2;
							arraySegment = new ArraySegment<byte>?(arraySegment2);
							flag = true;
							num += unchecked((ulong)num2);
						}
						else
						{
							readOnlyMemory2 = Helpers.DecodeOctetStringAsMemory(contentInfoAsn.Content);
						}
						AsnValueReader asnValueReader3 = new AsnValueReader(readOnlyMemory2.Span, AsnEncodingRules.BER);
						AsnValueReader asnValueReader4 = asnValueReader3.ReadSequence();
						asnValueReader3.ThrowIfNotEmpty();
						while (asnValueReader4.HasData)
						{
							SafeBagAsn safeBagAsn;
							SafeBagAsn.Decode(ref asnValueReader4, readOnlyMemory2, out safeBagAsn);
							if (Helpers.SequenceEqual(safeBagAsn.BagId, Oids.Pkcs12ShroudedKeyBag))
							{
								AsnValueReader asnValueReader5 = new AsnValueReader(safeBagAsn.BagValue.Span, AsnEncodingRules.BER);
								EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn;
								EncryptedPrivateKeyInfoAsn.Decode(ref asnValueReader5, safeBagAsn.BagValue, out encryptedPrivateKeyInfoAsn);
								num += unchecked((ulong)PfxAsn.IterationsFromParameters(ref encryptedPrivateKeyInfoAsn.EncryptionAlgorithm));
							}
						}
					}
					finally
					{
						if (arraySegment != null)
						{
							CryptoPool.Return(arraySegment.Value);
						}
					}
				}
				if (this.MacData != null)
				{
					if (this.MacData.Value.IterationCount < 0)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					num += unchecked((ulong)(checked((uint)this.MacData.Value.IterationCount)));
				}
				return num;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00089974 File Offset: 0x00087B74
		private static ArraySegment<byte> DecryptContentInfo(ContentInfoAsn contentInfo, out uint iterations)
		{
			char[] array = new char[0];
			byte[] array2 = new byte[0];
			char[] array3 = null;
			byte[] array4 = null;
			EncryptedDataAsn encryptedDataAsn = EncryptedDataAsn.Decode(contentInfo.Content, AsnEncodingRules.BER);
			if (encryptedDataAsn.Version != 0 && encryptedDataAsn.Version != 2)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (!Helpers.SequenceEqual(encryptedDataAsn.EncryptedContentInfo.ContentType, Oids.Pkcs7Data))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (encryptedDataAsn.EncryptedContentInfo.EncryptedContent == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			iterations = PfxAsn.IterationsFromParameters(ref encryptedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm);
			if (iterations > 600000U)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"));
			}
			int length = encryptedDataAsn.EncryptedContentInfo.EncryptedContent.Value.Length;
			byte[] array5 = new byte[length];
			int num = 0;
			try
			{
				num = PasswordBasedEncryption.Decrypt(ref encryptedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm, array, array2, encryptedDataAsn.EncryptedContentInfo.EncryptedContent.Value.Span, array5);
			}
			catch
			{
				num = PasswordBasedEncryption.Decrypt(ref encryptedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm, array3, array4, encryptedDataAsn.EncryptedContentInfo.EncryptedContent.Value.Span, array5);
			}
			finally
			{
				if (num == 0)
				{
					CryptographicOperations.ZeroMemory(array5);
				}
			}
			return new ArraySegment<byte>(array5, 0, num);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00089B10 File Offset: 0x00087D10
		private static uint IterationsFromParameters(ref AlgorithmIdentifierAsn algorithmIdentifier)
		{
			if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PasswordBasedEncryptionScheme2))
			{
				if (algorithmIdentifier.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PBES2Params pbes2Params = PBES2Params.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.BER);
				if (!Helpers.SequenceEqual(pbes2Params.KeyDerivationFunc.Algorithm, Oids.Pbkdf2))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (pbes2Params.KeyDerivationFunc.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				Pbkdf2Params pbkdf2Params = Pbkdf2Params.Decode(pbes2Params.KeyDerivationFunc.Parameters.Value, AsnEncodingRules.BER);
				if (pbkdf2Params.IterationCount < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (uint)pbkdf2Params.IterationCount;
			}
			else
			{
				if (!Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndDESCBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndRC2CBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndDESCBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndRC2CBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd3Key3Des) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd2Key3Des) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd128BitRC2) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd40BitRC2))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (algorithmIdentifier.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PBEParameter pbeparameter = PBEParameter.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.BER);
				if (pbeparameter.IterationCount < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (uint)pbeparameter.IterationCount;
			}
		}

		// Token: 0x04000E19 RID: 3609
		internal int Version;

		// Token: 0x04000E1A RID: 3610
		internal ContentInfoAsn AuthSafe;

		// Token: 0x04000E1B RID: 3611
		internal MacData? MacData;

		// Token: 0x04000E1C RID: 3612
		private const uint MaxIterationWork = 600000U;
	}
}
