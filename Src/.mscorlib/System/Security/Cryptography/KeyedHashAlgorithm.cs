using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of keyed hash algorithms must derive.</summary>
	// Token: 0x0200026D RID: 621
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002200 RID: 8704 RVA: 0x000783B0 File Offset: 0x000765B0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				this.KeyValue = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to change the <see cref="P:System.Security.Cryptography.KeyedHashAlgorithm.Key" /> property after hashing has begun.</exception>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000783DF File Offset: 0x000765DF
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x000783F1 File Offset: 0x000765F1
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		/// <summary>Creates an instance of the default implementation of a keyed hash algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.HMACSHA1" /> instance, unless the default settings have been changed.</returns>
		// Token: 0x06002203 RID: 8707 RVA: 0x0007841C File Offset: 0x0007661C
		public new static KeyedHashAlgorithm Create()
		{
			return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of a keyed hash algorithm.</summary>
		/// <param name="algName">The keyed hash algorithm implementation to use. The following table shows the valid values for the <paramref name="algName" /> parameter and the algorithms they map to.  
		///   Parameter value  
		///
		///   Implements  
		///
		///   System.Security.Cryptography.HMAC  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> System.Security.Cryptography.KeyedHashAlgorithm  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> HMACMD5  
		///
		///  <see cref="T:System.Security.Cryptography.HMACMD5" /> System.Security.Cryptography.HMACMD5  
		///
		///  <see cref="T:System.Security.Cryptography.HMACMD5" /> HMACRIPEMD160  
		///
		///  <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> System.Security.Cryptography.HMACRIPEMD160  
		///
		///  <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> HMACSHA1  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> System.Security.Cryptography.HMACSHA1  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> HMACSHA256  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA256" /> System.Security.Cryptography.HMACSHA256  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA256" /> HMACSHA384  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA384" /> System.Security.Cryptography.HMACSHA384  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA384" /> HMACSHA512  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA512" /> System.Security.Cryptography.HMACSHA512  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA512" /> MACTripleDES  
		///
		///  <see cref="T:System.Security.Cryptography.MACTripleDES" /> System.Security.Cryptography.MACTripleDES  
		///
		///  <see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
		/// <returns>A new instance of the specified keyed hash algorithm.</returns>
		// Token: 0x06002204 RID: 8708 RVA: 0x00078428 File Offset: 0x00076628
		public new static KeyedHashAlgorithm Create(string algName)
		{
			return (KeyedHashAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>The key to use in the hash algorithm.</summary>
		// Token: 0x04000C59 RID: 3161
		protected byte[] KeyValue;
	}
}
