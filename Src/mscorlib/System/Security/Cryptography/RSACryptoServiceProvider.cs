using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Performs asymmetric encryption and decryption using the implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x0200027F RID: 639
	[ComVisible(true)]
	public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
	{
		// Token: 0x060022A0 RID: 8864
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DecryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbEncryptedKey, int cbEncryptedKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetDecryptedKey);

		// Token: 0x060022A1 RID: 8865
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void EncryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbKey, int cbKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetEncryptedKey);

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class using the default key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		// Token: 0x060022A2 RID: 8866 RVA: 0x0007C7AD File Offset: 0x0007A9AD
		[SecuritySafeCritical]
		public RSACryptoServiceProvider()
			: this(0, new CspParameters(24, null, null, RSACryptoServiceProvider.s_UseMachineKeyStore), true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		// Token: 0x060022A3 RID: 8867 RVA: 0x0007C7C7 File Offset: 0x0007A9C7
		[SecuritySafeCritical]
		public RSACryptoServiceProvider(int dwKeySize)
			: this(dwKeySize, new CspParameters(24, null, null, RSACryptoServiceProvider.s_UseMachineKeyStore), false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP).</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.</exception>
		// Token: 0x060022A4 RID: 8868 RVA: 0x0007C7E1 File Offset: 0x0007A9E1
		[SecuritySafeCritical]
		public RSACryptoServiceProvider(CspParameters parameters)
			: this(0, parameters, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size and parameters.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits.</param>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP).</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.  
		///  -or-  
		///  The key cannot be created.</exception>
		// Token: 0x060022A5 RID: 8869 RVA: 0x0007C7EC File Offset: 0x0007A9EC
		[SecuritySafeCritical]
		public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
			: this(dwKeySize, parameters, false)
		{
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		[SecurityCritical]
		private RSACryptoServiceProvider(int dwKeySize, CspParameters parameters, bool useDefaultKeySize)
		{
			if (dwKeySize < 0)
			{
				throw new ArgumentOutOfRangeException("dwKeySize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Rsa, parameters, RSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
			this.LegalKeySizesValue = new KeySizes[]
			{
				new KeySizes(384, 16384, 8)
			};
			this._dwKeySize = (useDefaultKeySize ? 1024 : dwKeySize);
			if (!this._randomKeyContainer || Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
			{
				this.GetKeyPair();
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0007C884 File Offset: 0x0007AA84
		[SecurityCritical]
		private void GetKeyPair()
		{
			if (this._safeKeyHandle == null)
			{
				lock (this)
				{
					if (this._safeKeyHandle == null)
					{
						Utils.GetKeyPairHelper(CspAlgorithmType.Rsa, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
					}
				}
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0007C8F0 File Offset: 0x0007AAF0
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
			}
			if (this._safeProvHandle != null && !this._safeProvHandle.IsClosed)
			{
				this._safeProvHandle.Dispose();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0007C944 File Offset: 0x0007AB44
		[ComVisible(false)]
		public bool PublicOnly
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 2U);
				return array[0] == 1;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0007C96A File Offset: 0x0007AB6A
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				return new CspKeyContainerInfo(this._parameters, this._randomKeyContainer);
			}
		}

		/// <summary>Gets the size of the current key.</summary>
		/// <returns>The size of the key in bits.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0007C984 File Offset: 0x0007AB84
		public override int KeySize
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 1U);
				this._dwKeySize = (int)array[0] | ((int)array[1] << 8) | ((int)array[2] << 16) | ((int)array[3] << 24);
				return this._dwKeySize;
			}
		}

		/// <summary>Gets the name of the key exchange algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>The name of the key exchange algorithm if it exists; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x0007C9C7 File Offset: 0x0007ABC7
		public override string KeyExchangeAlgorithm
		{
			get
			{
				if (this._parameters.KeyNumber == 1)
				{
					return "RSA-PKCS1-KeyEx";
				}
				return null;
			}
		}

		/// <summary>Gets the name of the signature algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0007C9DE File Offset: 0x0007ABDE
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be persisted in the computer key store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x0007C9E5 File Offset: 0x0007ABE5
		// (set) Token: 0x060022AF RID: 8879 RVA: 0x0007C9F1 File Offset: 0x0007ABF1
		public static bool UseMachineKeyStore
		{
			get
			{
				return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
			set
			{
				RSACryptoServiceProvider.s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be persisted in the CSP; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0007CA04 File Offset: 0x0007AC04
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0007CA6C File Offset: 0x0007AC6C
		public bool PersistKeyInCsp
		{
			[SecuritySafeCritical]
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
						}
					}
				}
				return Utils.GetPersistKeyInCsp(this._safeProvHandle);
			}
			[SecuritySafeCritical]
			set
			{
				bool persistKeyInCsp = this.PersistKeyInCsp;
				if (value == persistKeyInCsp)
				{
					return;
				}
				if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
					if (!value)
					{
						KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Delete);
						keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
					}
					else
					{
						KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Create);
						keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry2);
					}
					keyContainerPermission.Demand();
				}
				Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
			}
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include private parameters; otherwise, <see langword="false" />.</param>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported.</exception>
		// Token: 0x060022B2 RID: 8882 RVA: 0x0007CAE0 File Offset: 0x0007ACE0
		[SecuritySafeCritical]
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			this.GetKeyPair();
			if (includePrivateParameters && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			RSACspObject rsacspObject = new RSACspObject();
			int num = (includePrivateParameters ? 7 : 6);
			Utils._ExportKey(this._safeKeyHandle, num, rsacspObject);
			return RSACryptoServiceProvider.RSAObjectToStruct(rsacspObject);
		}

		/// <summary>Exports a blob containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include the private key; otherwise, <see langword="false" />.</param>
		/// <returns>A byte array containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</returns>
		// Token: 0x060022B3 RID: 8883 RVA: 0x0007CB46 File Offset: 0x0007AD46
		[SecuritySafeCritical]
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			this.GetKeyPair();
			return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="parameters" /> parameter has missing fields.</exception>
		// Token: 0x060022B4 RID: 8884 RVA: 0x0007CB60 File Offset: 0x0007AD60
		[SecuritySafeCritical]
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
				this._safeKeyHandle = null;
			}
			RSACspObject rsacspObject = RSACryptoServiceProvider.RSAStructToObject(parameters);
			this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
			if (RSACryptoServiceProvider.IsPublic(parameters))
			{
				Utils._ImportKey(Utils.StaticProvHandle, 41984, CspProviderFlags.NoFlags, rsacspObject, ref this._safeKeyHandle);
				return;
			}
			if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			if (this._safeProvHandle == null)
			{
				this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
			}
			Utils._ImportKey(this._safeProvHandle, 41984, this._parameters.Flags, rsacspObject, ref this._safeKeyHandle);
		}

		/// <summary>Imports a blob that represents RSA key information.</summary>
		/// <param name="keyBlob">A byte array that represents an RSA key blob.</param>
		// Token: 0x060022B5 RID: 8885 RVA: 0x0007CC36 File Offset: 0x0007AE36
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			Utils.ImportCspBlobHelper(CspAlgorithmType.Rsa, keyBlob, RSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
		}

		/// <summary>Computes the hash value of the specified input stream using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <param name="inputStream">The input data for which to compute the hash.</param>
		/// <param name="halg">The hash algorithm to use to create the hash value.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x060022B6 RID: 8886 RVA: 0x0007CC60 File Offset: 0x0007AE60
		public byte[] SignData(Stream inputStream, object halg)
		{
			int num = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] array = hashAlgorithm.ComputeHash(inputStream);
			return this.SignHash(array, num);
		}

		/// <summary>Computes the hash value of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <param name="buffer">The input data for which to compute the hash.</param>
		/// <param name="halg">The hash algorithm to use to create the hash value.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x060022B7 RID: 8887 RVA: 0x0007CC8C File Offset: 0x0007AE8C
		public byte[] SignData(byte[] buffer, object halg)
		{
			int num = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] array = hashAlgorithm.ComputeHash(buffer);
			return this.SignHash(array, num);
		}

		/// <summary>Computes the hash value of a subset of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <param name="buffer">The input data for which to compute the hash.</param>
		/// <param name="offset">The offset into the array from which to begin using data.</param>
		/// <param name="count">The number of bytes in the array to use as data.</param>
		/// <param name="halg">The hash algorithm to use to create the hash value.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x060022B8 RID: 8888 RVA: 0x0007CCB8 File Offset: 0x0007AEB8
		public byte[] SignData(byte[] buffer, int offset, int count, object halg)
		{
			int num = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] array = hashAlgorithm.ComputeHash(buffer, offset, count);
			return this.SignHash(array, num);
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the hash value of the provided data.</summary>
		/// <param name="buffer">The data that was signed.</param>
		/// <param name="halg">The name of the hash algorithm used to create the hash value of the data.</param>
		/// <param name="signature">The signature data to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x060022B9 RID: 8889 RVA: 0x0007CCE8 File Offset: 0x0007AEE8
		public bool VerifyData(byte[] buffer, object halg, byte[] signature)
		{
			int num = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] array = hashAlgorithm.ComputeHash(buffer);
			return this.VerifyHash(array, num, signature);
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
		/// <param name="rgbHash">The hash value of the data to be signed.</param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  There is no private key.</exception>
		// Token: 0x060022BA RID: 8890 RVA: 0x0007CD18 File Offset: 0x0007AF18
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (this.PublicOnly)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
			}
			int num = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
			return this.SignHash(rgbHash, num);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007CD5C File Offset: 0x0007AF5C
		[SecuritySafeCritical]
		internal byte[] SignHash(byte[] rgbHash, int calgHash)
		{
			this.GetKeyPair();
			if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 9216, calgHash, rgbHash);
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the provided hash value.</summary>
		/// <param name="rgbHash">The hash value of the signed data.</param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data.</param>
		/// <param name="rgbSignature">The signature data to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="rgbSignature" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The signature cannot be verified.</exception>
		// Token: 0x060022BC RID: 8892 RVA: 0x0007CDCC File Offset: 0x0007AFCC
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			int num = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
			return this.VerifyHash(rgbHash, num, rgbSignature);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007CE06 File Offset: 0x0007B006
		[SecuritySafeCritical]
		internal bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
		{
			this.GetKeyPair();
			return Utils.VerifySign(this._safeKeyHandle, 9216, calgHash, rgbHash, rgbSignature);
		}

		/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <param name="rgb">The data to be encrypted.</param>
		/// <param name="fOAEP">
		///   <see langword="true" /> to perform direct <see cref="T:System.Security.Cryptography.RSA" /> encryption using OAEP padding (only available on a computer running Windows XP or later); otherwise, <see langword="false" /> to use PKCS#1 v1.5 padding.</param>
		/// <returns>The encrypted data.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The length of the <paramref name="rgb" /> parameter is greater than the maximum allowed length.  
		///  -or-  
		///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and OAEP padding is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgb" /> is <see langword="null" />.</exception>
		// Token: 0x060022BE RID: 8894 RVA: 0x0007CE24 File Offset: 0x0007B024
		[SecuritySafeCritical]
		public byte[] Encrypt(byte[] rgb, bool fOAEP)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			this.GetKeyPair();
			byte[] array = null;
			RSACryptoServiceProvider.EncryptKey(this._safeKeyHandle, rgb, rgb.Length, fOAEP, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			return array;
		}

		/// <summary>Decrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <param name="rgb">The data to be decrypted.</param>
		/// <param name="fOAEP">
		///   <see langword="true" /> to perform direct <see cref="T:System.Security.Cryptography.RSA" /> decryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, <see langword="false" /> to use PKCS#1 v1.5 padding.</param>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and the length of the <paramref name="rgb" /> parameter is greater than <see cref="P:System.Security.Cryptography.RSACryptoServiceProvider.KeySize" />.  
		///  -or-  
		///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and OAEP is not supported.  
		///  -or-  
		///  The key does not match the encrypted data. However, the exception wording may not be accurate. For example, it may say Not enough storage is available to process this command.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgb" /> is <see langword="null" />.</exception>
		// Token: 0x060022BF RID: 8895 RVA: 0x0007CE60 File Offset: 0x0007B060
		[SecuritySafeCritical]
		public byte[] Decrypt(byte[] rgb, bool fOAEP)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			this.GetKeyPair();
			if (rgb.Length > this.KeySize / 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_DecDataTooBig", new object[] { this.KeySize / 8 }));
			}
			if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Decrypt);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			byte[] array = null;
			RSACryptoServiceProvider.DecryptKey(this._safeKeyHandle, rgb, rgb.Length, fOAEP, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			return array;
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <param name="rgb">The data to be decrypted.</param>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version.</exception>
		// Token: 0x060022C0 RID: 8896 RVA: 0x0007CF0D File Offset: 0x0007B10D
		public override byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <param name="rgb">The data to be encrypted.</param>
		/// <returns>The encrypted data.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version.</exception>
		// Token: 0x060022C1 RID: 8897 RVA: 0x0007CF1E File Offset: 0x0007B11E
		public override byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007CF30 File Offset: 0x0007B130
		private static RSAParameters RSAObjectToStruct(RSACspObject rsaCspObject)
		{
			return new RSAParameters
			{
				Exponent = rsaCspObject.Exponent,
				Modulus = rsaCspObject.Modulus,
				P = rsaCspObject.P,
				Q = rsaCspObject.Q,
				DP = rsaCspObject.DP,
				DQ = rsaCspObject.DQ,
				InverseQ = rsaCspObject.InverseQ,
				D = rsaCspObject.D
			};
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007CFB0 File Offset: 0x0007B1B0
		private static RSACspObject RSAStructToObject(RSAParameters rsaParams)
		{
			return new RSACspObject
			{
				Exponent = rsaParams.Exponent,
				Modulus = rsaParams.Modulus,
				P = rsaParams.P,
				Q = rsaParams.Q,
				DP = rsaParams.DP,
				DQ = rsaParams.DQ,
				InverseQ = rsaParams.InverseQ,
				D = rsaParams.D
			};
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007D024 File Offset: 0x0007B224
		private static bool IsPublic(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			return keyBlob[0] == 6 && keyBlob[11] == 49 && keyBlob[10] == 65 && keyBlob[9] == 83 && keyBlob[8] == 82;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007D05E File Offset: 0x0007B25E
		private static bool IsPublic(RSAParameters rsaParams)
		{
			return rsaParams.P == null;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0007D06C File Offset: 0x0007B26C
		[SecuritySafeCritical]
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			byte[] array;
			using (SafeHashHandle safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
			{
				Utils.HashData(safeHashHandle, data, offset, count);
				array = Utils.EndHash(safeHashHandle);
			}
			return array;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007D0B8 File Offset: 0x0007B2B8
		[SecuritySafeCritical]
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			byte[] array2;
			using (SafeHashHandle safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
			{
				byte[] array = new byte[4096];
				int num;
				do
				{
					num = data.Read(array, 0, array.Length);
					if (num > 0)
					{
						Utils.HashData(safeHashHandle, array, 0, num);
					}
				}
				while (num > 0);
				array2 = Utils.EndHash(safeHashHandle);
			}
			return array2;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0007D124 File Offset: 0x0007B324
		private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
		{
			string name = hashAlgorithm.Name;
			if (name == "MD5")
			{
				return 32771;
			}
			if (name == "SHA1")
			{
				return 32772;
			}
			if (name == "SHA256")
			{
				return 32780;
			}
			if (name == "SHA384")
			{
				return 32781;
			}
			if (!(name == "SHA512"))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", new object[] { hashAlgorithm.Name }));
			}
			return 32782;
		}

		/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm using the specified padding.</summary>
		/// <param name="data">The data to encrypt.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>The encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not supported.</exception>
		// Token: 0x060022C9 RID: 8905 RVA: 0x0007D1BC File Offset: 0x0007B3BC
		public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Encrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Encrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		/// <summary>Decrypts data that was previously encrypted with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm by using the specified padding.</summary>
		/// <param name="data">The data to decrypt.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>The decrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not supported.</exception>
		// Token: 0x060022CA RID: 8906 RVA: 0x0007D21C File Offset: 0x0007B41C
		public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Decrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Decrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key using the specified padding.</summary>
		/// <param name="hash">The hash value of the data to be signed.</param>
		/// <param name="hashAlgorithm">The hash algorithm name used to create the hash value of the data.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hashAlgorithm" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hash" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="padding" /> does not equal <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.</exception>
		// Token: 0x060022CB RID: 8907 RVA: 0x0007D27C File Offset: 0x0007B47C
		public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.SignHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm));
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the specified hashing algorithm and padding, and comparing it to the provided hash value.</summary>
		/// <param name="hash">The hash value of the signed data.</param>
		/// <param name="signature">The signature data to be verified.</param>
		/// <param name="hashAlgorithm">The hash algorithm name used to create the hash value.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hashAlgorithm" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hash" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="padding" /> does not equal <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.</exception>
		// Token: 0x060022CC RID: 8908 RVA: 0x0007D2E0 File Offset: 0x0007B4E0
		public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.VerifyHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm), signature);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007D354 File Offset: 0x0007B554
		private static Exception PaddingModeNotSupported()
		{
			return new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
		}

		// Token: 0x04000C9B RID: 3227
		private int _dwKeySize;

		// Token: 0x04000C9C RID: 3228
		private CspParameters _parameters;

		// Token: 0x04000C9D RID: 3229
		private bool _randomKeyContainer;

		// Token: 0x04000C9E RID: 3230
		[SecurityCritical]
		private SafeProvHandle _safeProvHandle;

		// Token: 0x04000C9F RID: 3231
		[SecurityCritical]
		private SafeKeyHandle _safeKeyHandle;

		// Token: 0x04000CA0 RID: 3232
		private static volatile CspProviderFlags s_UseMachineKeyStore;
	}
}
