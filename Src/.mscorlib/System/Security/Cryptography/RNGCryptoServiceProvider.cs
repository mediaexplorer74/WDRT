using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Implements a cryptographic Random Number Generator (RNG) using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x02000247 RID: 583
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		// Token: 0x060020C4 RID: 8388 RVA: 0x00072A40 File Offset: 0x00070C40
		public RNGCryptoServiceProvider()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="str">The string input. This parameter is ignored.</param>
		// Token: 0x060020C5 RID: 8389 RVA: 0x00072A49 File Offset: 0x00070C49
		public RNGCryptoServiceProvider(string str)
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="rgb">A byte array. This value is ignored.</param>
		// Token: 0x060020C6 RID: 8390 RVA: 0x00072A52 File Offset: 0x00070C52
		public RNGCryptoServiceProvider(byte[] rgb)
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="cspParams">The parameters to pass to the cryptographic service provider (CSP).</param>
		// Token: 0x060020C7 RID: 8391 RVA: 0x00072A5B File Offset: 0x00070C5B
		[SecuritySafeCritical]
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			if (cspParams != null)
			{
				this.m_safeProvHandle = Utils.AcquireProvHandle(cspParams);
				this.m_ownsHandle = true;
				return;
			}
			this.m_safeProvHandle = Utils.StaticProvHandle;
			this.m_ownsHandle = false;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00072A8C File Offset: 0x00070C8C
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.m_ownsHandle)
			{
				this.m_safeProvHandle.Dispose();
			}
		}

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random values.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.</exception>
		// Token: 0x060020C9 RID: 8393 RVA: 0x00072AAB File Offset: 0x00070CAB
		[SecuritySafeCritical]
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RNGCryptoServiceProvider.GetBytes(this.m_safeProvHandle, data, data.Length);
		}

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random nonzero values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random nonzero values.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.</exception>
		// Token: 0x060020CA RID: 8394 RVA: 0x00072ACA File Offset: 0x00070CCA
		[SecuritySafeCritical]
		public override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RNGCryptoServiceProvider.GetNonZeroBytes(this.m_safeProvHandle, data, data.Length);
		}

		// Token: 0x060020CB RID: 8395
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

		// Token: 0x060020CC RID: 8396
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetNonZeroBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

		// Token: 0x04000BE4 RID: 3044
		[SecurityCritical]
		private SafeProvHandle m_safeProvHandle;

		// Token: 0x04000BE5 RID: 3045
		private bool m_ownsHandle;
	}
}
