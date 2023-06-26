using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric algorithms must inherit.</summary>
	// Token: 0x02000249 RID: 585
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x060020D2 RID: 8402 RVA: 0x00072BA7 File Offset: 0x00070DA7
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x060020D3 RID: 8403 RVA: 0x00072BAF File Offset: 0x00070DAF
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060020D4 RID: 8404 RVA: 0x00072BBE File Offset: 0x00070DBE
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets or sets the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		/// <returns>The size, in bits, of the key modulus used by the asymmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key modulus size is invalid.</exception>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00072BC0 File Offset: 0x00070DC0
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x00072BC8 File Offset: 0x00070DC8
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalKeySizesValue.Length; i++)
				{
					if (this.LegalKeySizesValue[i].SkipSize == 0)
					{
						if (this.LegalKeySizesValue[i].MinSize == value)
						{
							this.KeySizeValue = value;
							return;
						}
					}
					else
					{
						for (int j = this.LegalKeySizesValue[i].MinSize; j <= this.LegalKeySizesValue[i].MaxSize; j += this.LegalKeySizesValue[i].SkipSize)
						{
							if (j == value)
							{
								this.KeySizeValue = value;
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
		}

		/// <summary>Gets the key sizes that are supported by the asymmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the asymmetric algorithm.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00072C5A File Offset: 0x00070E5A
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		/// <summary>When implemented in a derived class, gets the name of the signature algorithm. Otherwise, always throws a <see cref="T:System.NotImplementedException" />.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00072C6C File Offset: 0x00070E6C
		public virtual string SignatureAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the key exchange algorithm. Otherwise, throws an <see cref="T:System.NotImplementedException" />.</summary>
		/// <returns>The name of the key exchange algorithm.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00072C73 File Offset: 0x00070E73
		public virtual string KeyExchangeAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a default cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> instance, unless the default settings have been changed with the &lt;cryptoClass&gt; element.</returns>
		// Token: 0x060020DA RID: 8410 RVA: 0x00072C7A File Offset: 0x00070E7A
		public static AsymmetricAlgorithm Create()
		{
			return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of an asymmetric algorithm.</summary>
		/// <param name="algName">The asymmetric algorithm implementation to use. The following table shows the valid values for the <paramref name="algName" /> parameter and the algorithms they map to.  
		///   Parameter value  
		///
		///   Implements  
		///
		///   System.Security.Cryptography.AsymmetricAlgorithm  
		///
		///  <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> RSA  
		///
		///  <see cref="T:System.Security.Cryptography.RSA" /> System.Security.Cryptography.RSA  
		///
		///  <see cref="T:System.Security.Cryptography.RSA" /> DSA  
		///
		///  <see cref="T:System.Security.Cryptography.DSA" /> System.Security.Cryptography.DSA  
		///
		///  <see cref="T:System.Security.Cryptography.DSA" /> ECDsa  
		///
		///  <see cref="T:System.Security.Cryptography.ECDsa" /> ECDsaCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDsaCng" /> System.Security.Cryptography.ECDsaCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDsaCng" /> ECDH  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> ECDiffieHellman  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> ECDiffieHellmanCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> System.Security.Cryptography.ECDiffieHellmanCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /></param>
		/// <returns>A new instance of the specified asymmetric algorithm implementation.</returns>
		// Token: 0x060020DB RID: 8411 RVA: 0x00072C86 File Offset: 0x00070E86
		public static AsymmetricAlgorithm Create(string algName)
		{
			return (AsymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, reconstructs an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object from an XML string. Otherwise, throws a <see cref="T:System.NotImplementedException" />.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</param>
		// Token: 0x060020DC RID: 8412 RVA: 0x00072C93 File Offset: 0x00070E93
		public virtual void FromXmlString(string xmlString)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object. Otherwise, throws a <see cref="T:System.NotImplementedException" />.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include private parameters; otherwise, <see langword="false" />.</param>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</returns>
		// Token: 0x060020DD RID: 8413 RVA: 0x00072C9A File Offset: 0x00070E9A
		public virtual string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		// Token: 0x04000BE8 RID: 3048
		protected int KeySizeValue;

		/// <summary>Specifies the key sizes that are supported by the asymmetric algorithm.</summary>
		// Token: 0x04000BE9 RID: 3049
		protected KeySizes[] LegalKeySizesValue;
	}
}
