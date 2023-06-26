using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Creates Optimal Asymmetric Encryption Padding (OAEP) key exchange data using <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	// Token: 0x02000283 RID: 643
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> class.</summary>
		// Token: 0x060022E6 RID: 8934 RVA: 0x0007D5F4 File Offset: 0x0007B7F4
		public RSAOAEPKeyExchangeFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060022E7 RID: 8935 RVA: 0x0007D5FC File Offset: 0x0007B7FC
		public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets or sets the parameter used to create padding in the key exchange creation process.</summary>
		/// <returns>The parameter value.</returns>
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x0007D61E File Offset: 0x0007B81E
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x0007D63A File Offset: 0x0007B83A
		public byte[] Parameter
		{
			get
			{
				if (this.ParameterValue != null)
				{
					return (byte[])this.ParameterValue.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.ParameterValue = (byte[])value.Clone();
					return;
				}
				this.ParameterValue = null;
			}
		}

		/// <summary>Gets the parameters for the Optimal Asymmetric Encryption Padding (OAEP) key exchange.</summary>
		/// <returns>An XML string containing the parameters of the OAEP key exchange operation.</returns>
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0007D658 File Offset: 0x0007B858
		public override string Parameters
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0007D65B File Offset: 0x0007B85B
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x0007D663 File Offset: 0x0007B863
		public RandomNumberGenerator Rng
		{
			get
			{
				return this.RngValue;
			}
			set
			{
				this.RngValue = value;
			}
		}

		/// <summary>Sets the public key to use for encrypting the key exchange data.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060022ED RID: 8941 RVA: 0x0007D66C File Offset: 0x0007B86C
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesEncrypt = null;
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="rgbData">The secret information to be passed in the key exchange.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		// Token: 0x060022EE RID: 8942 RVA: 0x0007D694 File Offset: 0x0007B894
		[SecuritySafeCritical]
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this.OverridesEncrypt)
			{
				return this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepEncrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="rgbData">The secret information to be passed in the key exchange.</param>
		/// <param name="symAlgType">This parameter is not used in the current version.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		// Token: 0x060022EF RID: 8943 RVA: 0x0007D6EE File Offset: 0x0007B8EE
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0007D6F8 File Offset: 0x0007B8F8
		private bool OverridesEncrypt
		{
			get
			{
				if (this._rsaOverridesEncrypt == null)
				{
					this._rsaOverridesEncrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Encrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesEncrypt.Value;
			}
		}

		// Token: 0x04000CAD RID: 3245
		private byte[] ParameterValue;

		// Token: 0x04000CAE RID: 3246
		private RSA _rsaKey;

		// Token: 0x04000CAF RID: 3247
		private bool? _rsaOverridesEncrypt;

		// Token: 0x04000CB0 RID: 3248
		private RandomNumberGenerator RngValue;
	}
}
