using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Decrypts the PKCS #1 key exchange data.</summary>
	// Token: 0x02000284 RID: 644
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> class.</summary>
		// Token: 0x060022F1 RID: 8945 RVA: 0x0007D758 File Offset: 0x0007B958
		public RSAPKCS1KeyExchangeDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060022F2 RID: 8946 RVA: 0x0007D760 File Offset: 0x0007B960
		public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0007D782 File Offset: 0x0007B982
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x0007D78A File Offset: 0x0007B98A
		public RandomNumberGenerator RNG
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

		/// <summary>Gets the parameters for the PKCS #1 key exchange.</summary>
		/// <returns>An XML string containing the parameters of the PKCS #1 key exchange operation.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0007D793 File Offset: 0x0007B993
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x0007D796 File Offset: 0x0007B996
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Extracts secret information from the encrypted key exchange data.</summary>
		/// <param name="rgbIn">The key exchange data within which the secret information is hidden.</param>
		/// <returns>The secret information derived from the key exchange data.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		// Token: 0x060022F7 RID: 8951 RVA: 0x0007D798 File Offset: 0x0007B998
		public override byte[] DecryptKeyExchange(byte[] rgbIn)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			byte[] array;
			if (this.OverridesDecrypt)
			{
				array = this._rsaKey.Decrypt(rgbIn, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				byte[] array2 = this._rsaKey.DecryptValue(rgbIn);
				int num = 2;
				while (num < array2.Length && array2[num] != 0)
				{
					num++;
				}
				if (num >= array2.Length)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_PKCS1Decoding"));
				}
				num++;
				array = new byte[array2.Length - num];
				Buffer.InternalBlockCopy(array2, num, array, 0, array.Length);
			}
			return array;
		}

		/// <summary>Sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060022F8 RID: 8952 RVA: 0x0007D82B File Offset: 0x0007BA2B
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0007D854 File Offset: 0x0007BA54
		private bool OverridesDecrypt
		{
			get
			{
				if (this._rsaOverridesDecrypt == null)
				{
					this._rsaOverridesDecrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Decrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesDecrypt.Value;
			}
		}

		// Token: 0x04000CB1 RID: 3249
		private RSA _rsaKey;

		// Token: 0x04000CB2 RID: 3250
		private bool? _rsaOverridesDecrypt;

		// Token: 0x04000CB3 RID: 3251
		private RandomNumberGenerator RngValue;
	}
}
