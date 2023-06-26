using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
	/// <summary>Implements password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on <see cref="T:System.Security.Cryptography.HMACSHA1" />.</summary>
	// Token: 0x02000277 RID: 631
	[ComVisible(true)]
	public class Rfc2898DeriveBytes : DeriveBytes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the password and salt size to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002255 RID: 8789 RVA: 0x000795C3 File Offset: 0x000777C3
		public Rfc2898DeriveBytes(string password, int saltSize)
			: this(password, saltSize, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt size, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iterations" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x06002256 RID: 8790 RVA: 0x000795D2 File Offset: 0x000777D2
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
			: this(password, saltSize, iterations, HashAlgorithmName.SHA1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the specified password, salt size, number of iterations and the hash algorithm name to derive the key.</summary>
		/// <param name="password">The password to use to derive the key.</param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="saltSize" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Hash algorithm name is invalid.</exception>
		// Token: 0x06002257 RID: 8791 RVA: 0x000795E4 File Offset: 0x000777E4
		[SecuritySafeCritical]
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations, HashAlgorithmName hashAlgorithm)
		{
			this.m_cspParams = new CspParameters();
			base..ctor();
			if (saltSize < 0)
			{
				throw new ArgumentOutOfRangeException("saltSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			HMAC hmac = HMAC.Create("HMAC" + hashAlgorithm.Name);
			if (hmac == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", new object[] { hashAlgorithm.Name }));
			}
			byte[] array = new byte[saltSize];
			Utils.StaticRandomNumberGenerator.GetBytes(array);
			this.Salt = array;
			this.IterationCount = iterations;
			this.m_password = new UTF8Encoding(false).GetBytes(password);
			hmac.Key = this.m_password;
			this.m_hmac = hmac;
			this.m_blockSize = hmac.HashSize >> 3;
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password and salt to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002258 RID: 8792 RVA: 0x000796D1 File Offset: 0x000778D1
		public Rfc2898DeriveBytes(string password, byte[] salt)
			: this(password, salt, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002259 RID: 8793 RVA: 0x000796E0 File Offset: 0x000778E0
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
			: this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the specified password, salt, number of iterations and the hash algorithm name to derive the key.</summary>
		/// <param name="password">The password to use to derive the key.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Hash algorithm name is invalid.</exception>
		// Token: 0x0600225A RID: 8794 RVA: 0x000796F0 File Offset: 0x000778F0
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
			: this(new UTF8Encoding(false).GetBytes(password), salt, iterations, hashAlgorithm)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x0600225B RID: 8795 RVA: 0x00079708 File Offset: 0x00077908
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
			: this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the specified password, salt, number of iterations and the hash algorithm name to derive the key.</summary>
		/// <param name="password">The password to use to derive the key.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="saltSize" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Hash algorithm name is invalid.</exception>
		// Token: 0x0600225C RID: 8796 RVA: 0x00079718 File Offset: 0x00077918
		[SecuritySafeCritical]
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
		{
			this.m_cspParams = new CspParameters();
			base..ctor();
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			HMAC hmac = HMAC.Create("HMAC" + hashAlgorithm.Name);
			if (hmac == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", new object[] { hashAlgorithm.Name }));
			}
			this.Salt = salt;
			this.IterationCount = iterations;
			this.m_password = password;
			hmac.Key = password;
			this.m_hmac = hmac;
			this.m_blockSize = hmac.HashSize >> 3;
			this.Initialize();
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of iterations is less than 1.</exception>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000797CA File Offset: 0x000779CA
		// (set) Token: 0x0600225E RID: 8798 RVA: 0x000797D2 File Offset: 0x000779D2
		public int IterationCount
		{
			get
			{
				return (int)this.m_iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
				}
				this.m_iterations = (uint)value;
				this.Initialize();
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">The salt is <see langword="null" />.</exception>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000797FA File Offset: 0x000779FA
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x0007980C File Offset: 0x00077A0C
		public byte[] Salt
		{
			get
			{
				return (byte[])this.m_salt.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length < 8)
				{
					throw new ArgumentException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_FewBytesSalt"));
				}
				this.m_salt = (byte[])value.Clone();
				this.Initialize();
			}
		}

		/// <summary>Returns the pseudo-random key for this object.</summary>
		/// <param name="cb">The number of pseudo-random key bytes to generate.</param>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cb" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x06002261 RID: 8801 RVA: 0x0007984C File Offset: 0x00077A4C
		public override byte[] GetBytes(int cb)
		{
			if (cb <= 0)
			{
				throw new ArgumentOutOfRangeException("cb", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			byte[] array = new byte[cb];
			int i = 0;
			int num = this.m_endIndex - this.m_startIndex;
			if (num > 0)
			{
				if (cb < num)
				{
					Buffer.InternalBlockCopy(this.m_buffer, this.m_startIndex, array, 0, cb);
					this.m_startIndex += cb;
					return array;
				}
				Buffer.InternalBlockCopy(this.m_buffer, this.m_startIndex, array, 0, num);
				this.m_startIndex = (this.m_endIndex = 0);
				i += num;
			}
			while (i < cb)
			{
				byte[] array2 = this.Func();
				int num2 = cb - i;
				if (num2 <= this.m_blockSize)
				{
					Buffer.InternalBlockCopy(array2, 0, array, i, num2);
					i += num2;
					Buffer.InternalBlockCopy(array2, num2, this.m_buffer, this.m_startIndex, this.m_blockSize - num2);
					this.m_endIndex += this.m_blockSize - num2;
					return array;
				}
				Buffer.InternalBlockCopy(array2, 0, array, i, this.m_blockSize);
				i += this.m_blockSize;
			}
			return array;
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x06002262 RID: 8802 RVA: 0x00079963 File Offset: 0x00077B63
		public override void Reset()
		{
			this.Initialize();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002263 RID: 8803 RVA: 0x0007996C File Offset: 0x00077B6C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this.m_hmac != null)
				{
					((IDisposable)this.m_hmac).Dispose();
				}
				if (this.m_buffer != null)
				{
					Array.Clear(this.m_buffer, 0, this.m_buffer.Length);
				}
				if (this.m_salt != null)
				{
					Array.Clear(this.m_salt, 0, this.m_salt.Length);
				}
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000799D0 File Offset: 0x00077BD0
		private void Initialize()
		{
			if (this.m_buffer != null)
			{
				Array.Clear(this.m_buffer, 0, this.m_buffer.Length);
			}
			this.m_buffer = new byte[this.m_blockSize];
			this.m_block = 1U;
			this.m_startIndex = (this.m_endIndex = 0);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00079A24 File Offset: 0x00077C24
		private byte[] Func()
		{
			byte[] array = Utils.Int(this.m_block);
			this.m_hmac.TransformBlock(this.m_salt, 0, this.m_salt.Length, null, 0);
			this.m_hmac.TransformBlock(array, 0, array.Length, null, 0);
			this.m_hmac.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] array2 = this.m_hmac.HashValue;
			this.m_hmac.Initialize();
			byte[] array3 = array2;
			int num = 2;
			while ((long)num <= (long)((ulong)this.m_iterations))
			{
				this.m_hmac.TransformBlock(array2, 0, array2.Length, null, 0);
				this.m_hmac.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
				array2 = this.m_hmac.HashValue;
				for (int i = 0; i < this.m_blockSize; i++)
				{
					byte[] array4 = array3;
					int num2 = i;
					array4[num2] ^= array2[i];
				}
				this.m_hmac.Initialize();
				num++;
			}
			this.m_block += 1U;
			return array3;
		}

		/// <summary>Derives a cryptographic key from the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> object.</summary>
		/// <param name="algname">The algorithm name for which to derive the key.</param>
		/// <param name="alghashname">The hash algorithm name to use to derive the key.</param>
		/// <param name="keySize">The size of the key, in bits, to derive.</param>
		/// <param name="rgbIV">The initialization vector (IV) to use to derive the key.</param>
		/// <returns>The derived key.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="keySize" /> parameter is incorrect.  
		///  -or-  
		///  The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="algname" /> parameter is not a valid algorithm name.  
		///  -or-  
		///  The <paramref name="alghashname" /> parameter is not a valid hash algorithm name.</exception>
		// Token: 0x06002266 RID: 8806 RVA: 0x00079B20 File Offset: 0x00077D20
		[SecuritySafeCritical]
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			int num = X509Utils.NameOrOidToAlgId(alghashname, OidGroup.HashAlgorithm);
			if (num == 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
			}
			int num2 = X509Utils.NameOrOidToAlgId(algname, OidGroup.AllGroups);
			if (num2 == 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
			}
			if (rgbIV == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidIV"));
			}
			byte[] array = null;
			Rfc2898DeriveBytes.DeriveKey(this.ProvHandle, num2, num, this.m_password, this.m_password.Length, keySize << 16, rgbIV, rgbIV.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			return array;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00079BBC File Offset: 0x00077DBC
		private SafeProvHandle ProvHandle
		{
			[SecurityCritical]
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							SafeProvHandle safeProvHandle = Utils.AcquireProvHandle(this.m_cspParams);
							Thread.MemoryBarrier();
							this._safeProvHandle = safeProvHandle;
						}
					}
				}
				return this._safeProvHandle;
			}
		}

		// Token: 0x06002268 RID: 8808
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DeriveKey(SafeProvHandle hProv, int algid, int algidHash, byte[] password, int cbPassword, int dwFlags, byte[] IV, int cbIV, ObjectHandleOnStack retKey);

		// Token: 0x04000C76 RID: 3190
		private byte[] m_buffer;

		// Token: 0x04000C77 RID: 3191
		private byte[] m_salt;

		// Token: 0x04000C78 RID: 3192
		private HMAC m_hmac;

		// Token: 0x04000C79 RID: 3193
		private byte[] m_password;

		// Token: 0x04000C7A RID: 3194
		private CspParameters m_cspParams;

		// Token: 0x04000C7B RID: 3195
		private uint m_iterations;

		// Token: 0x04000C7C RID: 3196
		private uint m_block;

		// Token: 0x04000C7D RID: 3197
		private int m_startIndex;

		// Token: 0x04000C7E RID: 3198
		private int m_endIndex;

		// Token: 0x04000C7F RID: 3199
		private int m_blockSize;

		// Token: 0x04000C80 RID: 3200
		[SecurityCritical]
		private SafeProvHandle _safeProvHandle;
	}
}
