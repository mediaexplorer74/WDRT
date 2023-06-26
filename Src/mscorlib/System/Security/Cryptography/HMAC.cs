using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of Hash-based Message Authentication Code (HMAC) must derive.</summary>
	// Token: 0x02000261 RID: 609
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		/// <summary>Gets or sets the block size to use in the hash value.</summary>
		/// <returns>The block size to use in the hash value.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x000773A7 File Offset: 0x000755A7
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x000773AF File Offset: 0x000755AF
		protected int BlockSizeValue
		{
			get
			{
				return this.blockSizeValue;
			}
			set
			{
				this.blockSizeValue = value;
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000773B8 File Offset: 0x000755B8
		private void UpdateIOPadBuffers()
		{
			if (this.m_inner == null)
			{
				this.m_inner = new byte[this.BlockSizeValue];
			}
			if (this.m_outer == null)
			{
				this.m_outer = new byte[this.BlockSizeValue];
			}
			for (int i = 0; i < this.BlockSizeValue; i++)
			{
				this.m_inner[i] = 54;
				this.m_outer[i] = 92;
			}
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				byte[] inner = this.m_inner;
				int num = i;
				inner[num] ^= this.KeyValue[i];
				byte[] outer = this.m_outer;
				int num2 = i;
				outer[num2] ^= this.KeyValue[i];
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00077464 File Offset: 0x00075664
		internal void InitializeKey(byte[] key)
		{
			this.m_inner = null;
			this.m_outer = null;
			if (key.Length > this.BlockSizeValue)
			{
				this.KeyValue = this.m_hash1.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			this.UpdateIOPadBuffers();
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt is made to change the <see cref="P:System.Security.Cryptography.HMAC.Key" /> property after hashing has begun.</exception>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000774B5 File Offset: 0x000756B5
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x000774C7 File Offset: 0x000756C7
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.InitializeKey(value);
			}
		}

		/// <summary>Gets or sets the name of the hash algorithm to use for hashing.</summary>
		/// <returns>The name of the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current hash algorithm cannot be changed.</exception>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000774E8 File Offset: 0x000756E8
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x000774F0 File Offset: 0x000756F0
		public string HashName
		{
			get
			{
				return this.m_hashName;
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashNameSet"));
				}
				this.m_hashName = value;
				this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
				this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
			}
		}

		/// <summary>Creates an instance of the default implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <returns>A new SHA-1 instance, unless the default settings have been changed by using the &lt;cryptoClass&gt; element.</returns>
		// Token: 0x060021B0 RID: 8624 RVA: 0x0007753E File Offset: 0x0007573E
		public new static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		/// <summary>Creates an instance of the specified implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <param name="algorithmName">The HMAC implementation to use. The following table shows the valid values for the <paramref name="algorithmName" /> parameter and the algorithms they map to.  
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
		/// <returns>A new instance of the specified HMAC implementation.</returns>
		// Token: 0x060021B1 RID: 8625 RVA: 0x0007754A File Offset: 0x0007574A
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		/// <summary>Initializes an instance of the default implementation of <see cref="T:System.Security.Cryptography.HMAC" />.</summary>
		// Token: 0x060021B2 RID: 8626 RVA: 0x00077557 File Offset: 0x00075757
		public override void Initialize()
		{
			this.m_hash1.Initialize();
			this.m_hash2.Initialize();
			this.m_hashing = false;
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the default <see cref="T:System.Security.Cryptography.HMAC" /> hash algorithm for computing the hash value.</summary>
		/// <param name="rgb">The input data.</param>
		/// <param name="ib">The offset into the byte array from which to begin using data.</param>
		/// <param name="cb">The number of bytes in the array to use as data.</param>
		// Token: 0x060021B3 RID: 8627 RVA: 0x00077578 File Offset: 0x00075778
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code in a byte array.</returns>
		// Token: 0x060021B4 RID: 8628 RVA: 0x000775C8 File Offset: 0x000757C8
		protected override byte[] HashFinal()
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] hashValue = this.m_hash1.HashValue;
			this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
			this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
			this.m_hashing = false;
			this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			return this.m_hash2.HashValue;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HMAC" /> class when a key change is legitimate and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060021B5 RID: 8629 RVA: 0x00077680 File Offset: 0x00075880
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_hash1 != null)
				{
					((IDisposable)this.m_hash1).Dispose();
				}
				if (this.m_hash2 != null)
				{
					((IDisposable)this.m_hash2).Dispose();
				}
				if (this.m_inner != null)
				{
					Array.Clear(this.m_inner, 0, this.m_inner.Length);
				}
				if (this.m_outer != null)
				{
					Array.Clear(this.m_outer, 0, this.m_outer.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000776F8 File Offset: 0x000758F8
		internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				try
				{
					return createFipsHashAlgorithmCallback();
				}
				catch (PlatformNotSupportedException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return createStandardHashAlgorithmCallback();
		}

		// Token: 0x04000C46 RID: 3142
		private int blockSizeValue = 64;

		// Token: 0x04000C47 RID: 3143
		internal string m_hashName;

		// Token: 0x04000C48 RID: 3144
		internal HashAlgorithm m_hash1;

		// Token: 0x04000C49 RID: 3145
		internal HashAlgorithm m_hash2;

		// Token: 0x04000C4A RID: 3146
		private byte[] m_inner;

		// Token: 0x04000C4B RID: 3147
		private byte[] m_outer;

		// Token: 0x04000C4C RID: 3148
		private bool m_hashing;
	}
}
