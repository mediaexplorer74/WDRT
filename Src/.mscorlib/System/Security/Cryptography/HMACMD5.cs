using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) by using the <see cref="T:System.Security.Cryptography.MD5" /> hash function.</summary>
	// Token: 0x02000262 RID: 610
	[ComVisible(true)]
	public class HMACMD5 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACMD5" /> class by using a randomly generated key.</summary>
		// Token: 0x060021B8 RID: 8632 RVA: 0x0007774C File Offset: 0x0007594C
		public HMACMD5()
			: this(Utils.GenerateRandom(64))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACMD5" /> class by using the specified key.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACMD5" /> encryption. The key can be any length, but if it is more than 64 bytes long it will be hashed (using SHA-1) to derive a 64-byte key. Therefore, the recommended size of the secret key is 64 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060021B9 RID: 8633 RVA: 0x0007775B File Offset: 0x0007595B
		public HMACMD5(byte[] key)
		{
			this.m_hashName = "MD5";
			this.m_hash1 = new MD5CryptoServiceProvider();
			this.m_hash2 = new MD5CryptoServiceProvider();
			this.HashSizeValue = 128;
			base.InitializeKey(key);
		}
	}
}
