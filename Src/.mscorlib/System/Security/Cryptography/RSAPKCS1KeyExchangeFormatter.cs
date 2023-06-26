using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Creates the PKCS#1 key exchange data using <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	// Token: 0x02000285 RID: 645
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> class.</summary>
		// Token: 0x060022FA RID: 8954 RVA: 0x0007D8B4 File Offset: 0x0007BAB4
		public RSAPKCS1KeyExchangeFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060022FB RID: 8955 RVA: 0x0007D8BC File Offset: 0x0007BABC
		public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets the parameters for the PKCS #1 key exchange.</summary>
		/// <returns>An XML string containing the parameters of the PKCS #1 key exchange operation.</returns>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x0007D8DE File Offset: 0x0007BADE
		public override string Parameters
		{
			get
			{
				return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
			}
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0007D8E5 File Offset: 0x0007BAE5
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x0007D8ED File Offset: 0x0007BAED
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
		// Token: 0x060022FF RID: 8959 RVA: 0x0007D8F6 File Offset: 0x0007BAF6
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
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbData" /> is too big.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is <see langword="null" />.</exception>
		// Token: 0x06002300 RID: 8960 RVA: 0x0007D920 File Offset: 0x0007BB20
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			byte[] array;
			if (this.OverridesEncrypt)
			{
				array = this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				int num = this._rsaKey.KeySize / 8;
				if (rgbData.Length + 11 > num)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_EncDataTooBig", new object[] { num - 11 }));
				}
				byte[] array2 = new byte[num];
				if (this.RngValue == null)
				{
					this.RngValue = RandomNumberGenerator.Create();
				}
				this.Rng.GetNonZeroBytes(array2);
				array2[0] = 0;
				array2[1] = 2;
				array2[num - rgbData.Length - 1] = 0;
				Buffer.InternalBlockCopy(rgbData, 0, array2, num - rgbData.Length, rgbData.Length);
				array = this._rsaKey.EncryptValue(array2);
			}
			return array;
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="rgbData">The secret information to be passed in the key exchange.</param>
		/// <param name="symAlgType">This parameter is not used in the current version.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		// Token: 0x06002301 RID: 8961 RVA: 0x0007D9F3 File Offset: 0x0007BBF3
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0007D9FC File Offset: 0x0007BBFC
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

		// Token: 0x04000CB4 RID: 3252
		private RandomNumberGenerator RngValue;

		// Token: 0x04000CB5 RID: 3253
		private RSA _rsaKey;

		// Token: 0x04000CB6 RID: 3254
		private bool? _rsaOverridesEncrypt;
	}
}
