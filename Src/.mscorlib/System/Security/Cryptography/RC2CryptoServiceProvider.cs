using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x02000276 RID: 630
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RC2CryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		/// <exception cref="T:System.InvalidOperationException">A non-compliant FIPS algorithm was found.</exception>
		// Token: 0x0600224A RID: 8778 RVA: 0x00079328 File Offset: 0x00077528
		[SecuritySafeCritical]
		public RC2CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Gets or sets the effective size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</summary>
		/// <returns>The effective key size, in bits, used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <see cref="P:System.Security.Cryptography.RC2CryptoServiceProvider.EffectiveKeySize" /> property was set to a value other than the <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.KeySizeValue" /> property.</exception>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x00079388 File Offset: 0x00077588
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x00079390 File Offset: 0x00077590
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value != this.KeySizeValue)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_RC2_EKSKS2"));
				}
			}
		}

		/// <summary>Gets or sets a value that determines whether to create a key with an 11-byte-long, zero-value salt.</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be created with an 11-byte-long, zero-value salt; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000793AB File Offset: 0x000775AB
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x000793B3 File Offset: 0x000775B3
		[ComVisible(false)]
		public bool UseSalt
		{
			get
			{
				return this.m_use40bitSalt;
			}
			set
			{
				this.m_use40bitSalt = value;
			}
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.  
		///  -or-  
		///  A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x0600224F RID: 8783 RVA: 0x000793BC File Offset: 0x000775BC
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.  
		///  -or-  
		///  A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x06002250 RID: 8784 RVA: 0x000793D9 File Offset: 0x000775D9
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
		}

		/// <summary>Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to be used for the algorithm.</summary>
		// Token: 0x06002251 RID: 8785 RVA: 0x000793F6 File Offset: 0x000775F6
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x06002252 RID: 8786 RVA: 0x0007941B File Offset: 0x0007761B
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0007943C File Offset: 0x0007763C
		[SecurityCritical]
		private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int effectiveKeySize, int feedbackSize, CryptoAPITransformMode encryptMode)
		{
			int num = 0;
			int[] array = new int[10];
			object[] array2 = new object[10];
			if (mode == CipherMode.OFB)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
			}
			if (mode == CipherMode.CFB && feedbackSize != 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
			}
			if (rgbKey == null)
			{
				rgbKey = new byte[this.KeySizeValue / 8];
				Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
			}
			int num2 = rgbKey.Length * 8;
			if (!base.ValidKeySize(num2))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			array[num] = 19;
			if (this.EffectiveKeySizeValue == 0)
			{
				array2[num] = num2;
			}
			else
			{
				array2[num] = effectiveKeySize;
			}
			num++;
			if (mode != CipherMode.CBC)
			{
				array[num] = 4;
				array2[num] = mode;
				num++;
			}
			if (mode != CipherMode.ECB)
			{
				if (rgbIV == null)
				{
					rgbIV = new byte[8];
					Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
				}
				if (rgbIV.Length < 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
				}
				array[num] = 1;
				array2[num] = rgbIV;
				num++;
			}
			if (mode == CipherMode.OFB || mode == CipherMode.CFB)
			{
				array[num] = 5;
				array2[num] = feedbackSize;
				num++;
			}
			if (!Utils.HasAlgorithm(26114, num2))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgKeySizeNotAvailable", new object[] { num2 }));
			}
			return new CryptoAPITransform(26114, num, array, array2, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, this.m_use40bitSalt, encryptMode);
		}

		// Token: 0x04000C74 RID: 3188
		private bool m_use40bitSalt;

		// Token: 0x04000C75 RID: 3189
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}
