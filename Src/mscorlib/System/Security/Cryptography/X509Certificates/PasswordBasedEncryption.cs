using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002BB RID: 699
	internal static class PasswordBasedEncryption
	{
		// Token: 0x06002516 RID: 9494 RVA: 0x00087633 File Offset: 0x00085833
		private static CryptographicException AlgorithmKdfRequiresChars()
		{
			return new CryptographicException("The KDF requires a char-based password input.");
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00087640 File Offset: 0x00085840
		[SecuritySafeCritical]
		internal unsafe static int Decrypt(ref AlgorithmIdentifierAsn algorithmIdentifier, ReadOnlySpan<char> password, ReadOnlySpan<byte> passwordBytes, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			bool flag = false;
			HashAlgorithmName hashAlgorithmName;
			SymmetricAlgorithm symmetricAlgorithm;
			if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndDESCBC))
			{
				hashAlgorithmName = HashAlgorithmName.MD5;
				symmetricAlgorithm = DES.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndRC2CBC))
			{
				hashAlgorithmName = HashAlgorithmName.MD5;
				symmetricAlgorithm = RC2.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndDESCBC))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
				symmetricAlgorithm = DES.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndRC2CBC))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
				symmetricAlgorithm = RC2.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd3Key3Des))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
				symmetricAlgorithm = TripleDES.Create();
				flag = true;
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd2Key3Des))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
				symmetricAlgorithm = TripleDES.Create();
				symmetricAlgorithm.KeySize = 128;
				flag = true;
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd128BitRC2))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
				symmetricAlgorithm = RC2.Create();
				symmetricAlgorithm.KeySize = 128;
				flag = true;
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd40BitRC2))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
				symmetricAlgorithm = RC2.Create();
				symmetricAlgorithm.KeySize = 40;
				flag = true;
			}
			else
			{
				if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PasswordBasedEncryptionScheme2))
				{
					return PasswordBasedEncryption.Pbes2Decrypt(algorithmIdentifier.Parameters, password, passwordBytes, encryptedData, destination);
				}
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
			int num;
			using (symmetricAlgorithm)
			{
				if (flag)
				{
					if (password.Length == 0 && passwordBytes.Length > 0)
					{
						throw PasswordBasedEncryption.AlgorithmKdfRequiresChars();
					}
					num = PasswordBasedEncryption.Pkcs12PbeDecrypt(algorithmIdentifier, password, hashAlgorithmName, symmetricAlgorithm, encryptedData, destination);
				}
				else
				{
					using (IncrementalHash incrementalHash = IncrementalHash.CreateHash(hashAlgorithmName))
					{
						Span<byte> span = new byte[128];
						ReadOnlySpan<byte> readOnlySpan = default(ReadOnlySpan<byte>);
						byte[] array = null;
						Encoding encoding = null;
						if (passwordBytes.Length > 0 || password.Length == 0)
						{
							readOnlySpan = passwordBytes;
						}
						else
						{
							encoding = Encoding.UTF8;
							int num2 = Utility.EncodingGetByteCount(encoding, password);
							if (num2 > span.Length)
							{
								array = CryptoPool.Rent(num2);
								span = new Span<byte>(array, 0, num2);
							}
							else
							{
								span = span.Slice(0, num2);
							}
						}
						try
						{
							byte[] array2;
							if ((array2 = span.DangerousGetArrayForPinning()) == null || array2.Length == 0)
							{
								byte* ptr = null;
							}
							else
							{
								byte* ptr = &array2[0];
							}
							if (encoding != null)
							{
								int num3 = Utility.EncodingGetBytes(encoding, password, span);
								span = span.Slice(0, num3);
								readOnlySpan = span;
							}
							try
							{
								num = PasswordBasedEncryption.Pbes1Decrypt(algorithmIdentifier.Parameters, readOnlySpan, incrementalHash, symmetricAlgorithm, encryptedData, destination);
							}
							finally
							{
								CryptographicOperations.ZeroMemory(span);
								if (array != null)
								{
									CryptoPool.Return(array, 0);
								}
							}
						}
						finally
						{
							byte[] array2 = null;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00087960 File Offset: 0x00085B60
		[SecuritySafeCritical]
		private unsafe static int Pbes2Decrypt(ReadOnlyMemory<byte>? algorithmParameters, ReadOnlySpan<char> password, ReadOnlySpan<byte> passwordBytes, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			Span<byte> span = new byte[128];
			ReadOnlySpan<byte> readOnlySpan = default(ReadOnlySpan<byte>);
			byte[] array = null;
			Encoding encoding = null;
			if (passwordBytes.Length > 0 || password.Length == 0)
			{
				readOnlySpan = passwordBytes;
			}
			else
			{
				encoding = Encoding.UTF8;
				int num = Utility.EncodingGetByteCount(encoding, password);
				if (num > span.Length)
				{
					array = CryptoPool.Rent(num);
					span = new Span<byte>(array, 0, num);
				}
				else
				{
					span = span.Slice(0, num);
				}
			}
			byte[] array2;
			if ((array2 = span.DangerousGetArrayForPinning()) == null || array2.Length == 0)
			{
				byte* ptr = null;
			}
			else
			{
				byte* ptr = &array2[0];
			}
			if (encoding != null)
			{
				int num2 = Utility.EncodingGetBytes(encoding, password, span);
				span = span.Slice(0, num2);
				readOnlySpan = span;
			}
			int num3;
			try
			{
				num3 = PasswordBasedEncryption.Pbes2Decrypt(algorithmParameters, readOnlySpan, encryptedData, destination);
			}
			finally
			{
				if (array != null)
				{
					CryptoPool.Return(array, span.Length);
				}
			}
			return num3;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00087A50 File Offset: 0x00085C50
		private static int Pbes2Decrypt(ReadOnlyMemory<byte>? algorithmParameters, ReadOnlySpan<byte> password, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			if (algorithmParameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			PBES2Params pbes2Params = PBES2Params.Decode(algorithmParameters.Value, AsnEncodingRules.BER);
			if (!Helpers.SequenceEqual(pbes2Params.KeyDerivationFunc.Algorithm, Oids.Pbkdf2))
			{
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
			int? num;
			int num2;
			ReadOnlyMemory<byte> readOnlyMemory;
			HashAlgorithmName hashAlgorithmName = PasswordBasedEncryption.OpenPbkdf2(pbes2Params.KeyDerivationFunc.Parameters, out num, out num2, out readOnlyMemory);
			Span<byte> span = new byte[16];
			SymmetricAlgorithm symmetricAlgorithm = PasswordBasedEncryption.OpenCipher(pbes2Params.EncryptionScheme, num, ref span);
			int num3;
			using (symmetricAlgorithm)
			{
				byte[] array = new byte[password.Length];
				byte[] array2 = new byte[readOnlyMemory.Length];
				password.CopyTo(array);
				readOnlyMemory.CopyTo(array2);
				byte[] array3 = Pbkdf2.Derive(hashAlgorithmName.Name, array, array2, num2, symmetricAlgorithm.KeySize / 8);
				try
				{
					num3 = PasswordBasedEncryption.Decrypt(symmetricAlgorithm, array3, span, encryptedData, destination);
				}
				finally
				{
					CryptographicOperations.ZeroMemory(array);
					CryptographicOperations.ZeroMemory(array2);
					CryptographicOperations.ZeroMemory(array3);
				}
			}
			return num3;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00087B9C File Offset: 0x00085D9C
		private static SymmetricAlgorithm OpenCipher(AlgorithmIdentifierAsn encryptionScheme, int? requestedKeyLength, ref Span<byte> iv)
		{
			byte[] algorithm = encryptionScheme.Algorithm;
			if (Helpers.SequenceEqual(algorithm, Oids.Aes128Cbc) || Helpers.SequenceEqual(algorithm, Oids.Aes192Cbc) || Helpers.SequenceEqual(algorithm, Oids.Aes256Cbc))
			{
				int num;
				if (Helpers.SequenceEqual(algorithm, Oids.Aes128Cbc))
				{
					num = 16;
				}
				else if (Helpers.SequenceEqual(algorithm, Oids.Aes192Cbc))
				{
					num = 24;
				}
				else
				{
					if (!Helpers.SequenceEqual(algorithm, Oids.Aes256Cbc))
					{
						throw new CryptographicException();
					}
					num = 32;
				}
				if (requestedKeyLength != null)
				{
					int? num2 = requestedKeyLength;
					int num3 = num;
					if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
				PasswordBasedEncryption.ReadIvParameter(encryptionScheme.Parameters, 16, ref iv);
				Rijndael rijndael = Rijndael.Create();
				rijndael.KeySize = num * 8;
				return rijndael;
			}
			if (Helpers.SequenceEqual(algorithm, Oids.TripleDesCbc))
			{
				if (requestedKeyLength != null)
				{
					int? num2 = requestedKeyLength;
					int num3 = 24;
					if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
				PasswordBasedEncryption.ReadIvParameter(encryptionScheme.Parameters, 8, ref iv);
				return TripleDES.Create();
			}
			if (Helpers.SequenceEqual(algorithm, Oids.Rc2Cbc))
			{
				if (encryptionScheme.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (requestedKeyLength == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				Rc2CbcParameters rc2CbcParameters = Rc2CbcParameters.Decode(encryptionScheme.Parameters.Value, AsnEncodingRules.BER);
				if (rc2CbcParameters.Iv.Length != 8)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				RC2 rc = RC2.Create();
				rc.KeySize = requestedKeyLength.Value * 8;
				rc.EffectiveKeySize = rc2CbcParameters.GetEffectiveKeyBits();
				rc2CbcParameters.Iv.Span.CopyTo(iv);
				iv = iv.Slice(0, rc2CbcParameters.Iv.Length);
				return rc;
			}
			else
			{
				if (Helpers.SequenceEqual(algorithm, Oids.DesCbc))
				{
					if (requestedKeyLength != null)
					{
						int? num2 = requestedKeyLength;
						int num3 = 8;
						if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
					}
					PasswordBasedEncryption.ReadIvParameter(encryptionScheme.Parameters, 8, ref iv);
					return DES.Create();
				}
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00087DD4 File Offset: 0x00085FD4
		private static void ReadIvParameter(ReadOnlyMemory<byte>? encryptionSchemeParameters, int length, ref Span<byte> iv)
		{
			if (encryptionSchemeParameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			try
			{
				ReadOnlySpan<byte> span = encryptionSchemeParameters.Value.Span;
				int num;
				int num2;
				bool flag = AsnDecoder.TryReadOctetString(span, iv, AsnEncodingRules.BER, out num, out num2, null);
				if (!flag || num2 != length || num != span.Length)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				iv = iv.Slice(0, num2);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00087E70 File Offset: 0x00086070
		private static HashAlgorithmName OpenPbkdf2(ReadOnlyMemory<byte>? parameters, out int? requestedKeyLength, out int iterationCount, out ReadOnlyMemory<byte> saltMemory)
		{
			if (parameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			Pbkdf2Params pbkdf2Params = Pbkdf2Params.Decode(parameters.Value, AsnEncodingRules.BER);
			if (pbkdf2Params.Salt.OtherSource != null)
			{
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
			if (pbkdf2Params.Salt.Specified == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			HashAlgorithmName hashAlgorithmName;
			if (Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha1))
			{
				hashAlgorithmName = HashAlgorithmName.SHA1;
			}
			else if (Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha256))
			{
				hashAlgorithmName = HashAlgorithmName.SHA256;
			}
			else if (Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha384))
			{
				hashAlgorithmName = HashAlgorithmName.SHA384;
			}
			else
			{
				if (!Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha512))
				{
					throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
				}
				hashAlgorithmName = HashAlgorithmName.SHA512;
			}
			if (!pbkdf2Params.Prf.HasNullEquivalentParameters())
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			requestedKeyLength = pbkdf2Params.KeyLength;
			iterationCount = PasswordBasedEncryption.NormalizeIterationCount(pbkdf2Params.IterationCount, null);
			saltMemory = pbkdf2Params.Salt.Specified.Value;
			return hashAlgorithmName;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00087FB8 File Offset: 0x000861B8
		private static int Pbes1Decrypt(ReadOnlyMemory<byte>? algorithmParameters, ReadOnlySpan<byte> password, IncrementalHash hasher, SymmetricAlgorithm cipher, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			if (algorithmParameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			PBEParameter pbeparameter = PBEParameter.Decode(algorithmParameters.Value, AsnEncodingRules.BER);
			if (pbeparameter.Salt.Length != 8)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (pbeparameter.IterationCount < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = PasswordBasedEncryption.NormalizeIterationCount(pbeparameter.IterationCount, null);
			Span<byte> span = new byte[16];
			int num2;
			try
			{
				PasswordBasedEncryption.Pbkdf1(hasher, password, pbeparameter.Salt.Span, num, span);
				Span<byte> span2 = span.Slice(0, 8);
				Span<byte> span3 = span.Slice(8, 8);
				num2 = PasswordBasedEncryption.Decrypt(cipher, span2, span3, encryptedData, destination);
			}
			finally
			{
				CryptographicOperations.ZeroMemory(span);
			}
			return num2;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00088098 File Offset: 0x00086298
		private static int Pkcs12PbeDecrypt(AlgorithmIdentifierAsn algorithmIdentifier, ReadOnlySpan<char> password, HashAlgorithmName hashAlgorithm, SymmetricAlgorithm cipher, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			if (algorithmIdentifier.Parameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (cipher.KeySize > 256 || cipher.BlockSize > 256)
			{
				throw new CryptographicException();
			}
			PBEParameter pbeparameter = PBEParameter.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.BER);
			int num = PasswordBasedEncryption.NormalizeIterationCount(pbeparameter.IterationCount, new int?(600000));
			Span<byte> span = new byte[cipher.BlockSize / 8];
			Span<byte> span2 = new byte[cipher.KeySize / 8];
			ReadOnlySpan<byte> span3 = pbeparameter.Salt.Span;
			int num2;
			try
			{
				Pkcs12Kdf.DeriveIV(password, hashAlgorithm, num, span3, span);
				Pkcs12Kdf.DeriveCipherKey(password, hashAlgorithm, num, span3, span2);
				num2 = PasswordBasedEncryption.Decrypt(cipher, span2, span, encryptedData, destination);
			}
			finally
			{
				CryptographicOperations.ZeroMemory(span2);
				CryptographicOperations.ZeroMemory(span);
			}
			return num2;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0008818C File Offset: 0x0008638C
		[SecuritySafeCritical]
		private unsafe static int Decrypt(SymmetricAlgorithm cipher, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			byte[] array = new byte[key.Length];
			byte[] array2 = new byte[iv.Length];
			byte[] array3 = CryptoPool.Rent(encryptedData.Length);
			byte[] array4 = CryptoPool.Rent(destination.Length);
			byte[] array5;
			if ((array5 = array) == null || array5.Length == 0)
			{
				byte* ptr = null;
			}
			else
			{
				byte* ptr = &array5[0];
			}
			byte[] array6;
			if ((array6 = array2) == null || array6.Length == 0)
			{
				byte* ptr2 = null;
			}
			else
			{
				byte* ptr2 = &array6[0];
			}
			byte[] array7;
			if ((array7 = array3) == null || array7.Length == 0)
			{
				byte* ptr3 = null;
			}
			else
			{
				byte* ptr3 = &array7[0];
			}
			byte[] array8;
			if ((array8 = array4) == null || array8.Length == 0)
			{
				byte* ptr4 = null;
			}
			else
			{
				byte* ptr4 = &array8[0];
			}
			int num2;
			try
			{
				key.CopyTo(array);
				iv.CopyTo(array2);
				using (ICryptoTransform cryptoTransform = cipher.CreateDecryptor(array, array2))
				{
					encryptedData.CopyTo(array3);
					int num = cryptoTransform.TransformBlock(array3, 0, encryptedData.Length, array4, 0);
					new ReadOnlySpan<byte>(array4, 0, num).CopyTo(destination);
					byte[] array9 = cryptoTransform.TransformFinalBlock(PasswordBasedEncryption.s_Empty, 0, 0);
					try
					{
						byte[] array10;
						if ((array10 = array9) == null || array10.Length == 0)
						{
							byte* ptr5 = null;
						}
						else
						{
							byte* ptr5 = &array10[0];
						}
						Span<byte> span = new Span<byte>(array9);
						span.CopyTo(destination.Slice(num));
						CryptographicOperations.ZeroMemory(span);
					}
					finally
					{
						byte[] array10 = null;
					}
					num2 = num + array9.Length;
				}
			}
			finally
			{
				CryptographicOperations.ZeroMemory(array);
				CryptographicOperations.ZeroMemory(array2);
				CryptoPool.Return(array3, encryptedData.Length);
				CryptoPool.Return(array4, destination.Length);
			}
			return num2;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00088364 File Offset: 0x00086564
		private static void Pbkdf1(IncrementalHash hasher, ReadOnlySpan<byte> password, ReadOnlySpan<byte> salt, int iterationCount, Span<byte> dk)
		{
			Span<byte> span = new byte[20];
			hasher.AppendData(password);
			hasher.AppendData(salt);
			int num;
			if (!hasher.TryGetHashAndReset(span, out num))
			{
				throw new CryptographicException();
			}
			span = span.Slice(0, num);
			KdfWorkLimiter.RecordIterations(iterationCount);
			for (int i = 1; i < iterationCount; i++)
			{
				hasher.AppendData(span);
				if (!hasher.TryGetHashAndReset(span, out num) || num != span.Length)
				{
					throw new CryptographicException();
				}
			}
			span.Slice(0, dk.Length).CopyTo(dk);
			CryptographicOperations.ZeroMemory(span);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000883FF File Offset: 0x000865FF
		internal static int NormalizeIterationCount(int iterationCount, int? iterationLimit)
		{
			if (iterationCount <= 0 || (iterationLimit != null && iterationCount > iterationLimit.Value))
			{
				throw new CryptographicException("Value was invalid.");
			}
			return iterationCount;
		}

		// Token: 0x04000DED RID: 3565
		internal const int IterationLimit = 600000;

		// Token: 0x04000DEE RID: 3566
		private static readonly byte[] s_Empty = new byte[0];
	}
}
