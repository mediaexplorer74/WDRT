using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.SHA512" /> hash function.</summary>
	// Token: 0x02000267 RID: 615
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> class with a randomly generated key.</summary>
		// Token: 0x060021C6 RID: 8646 RVA: 0x00077A54 File Offset: 0x00075C54
		public HMACSHA512()
			: this(Utils.GenerateRandom(128))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA512" /> encryption. The key can be any length. However, the recommended size is 128 bytes. If the key is more than 128 bytes long, it is hashed (using SHA-512) to derive a 128-byte key. If it is less than 128 bytes long, it is padded to 128 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060021C7 RID: 8647 RVA: 0x00077A68 File Offset: 0x00075C68
		[SecuritySafeCritical]
		public HMACSHA512(byte[] key)
		{
			this.m_hashName = "SHA512";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.HashSizeValue = 512;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x00077B41 File Offset: 0x00075D41
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		/// <summary>Provides a workaround for the .NET Framework 2.0 implementation of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> algorithm, which is inconsistent with the .NET Framework 2.0 Service Pack 1 implementation.</summary>
		/// <returns>
		///   <see langword="true" /> to enable .NET Framework 2.0 Service Pack 1 applications to interact with .NET Framework 2.0 applications; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x00077B53 File Offset: 0x00075D53
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x00077B5B File Offset: 0x00075D5B
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				base.BlockSizeValue = this.BlockSize;
				base.InitializeKey(this.KeyValue);
			}
		}

		// Token: 0x04000C4E RID: 3150
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}
