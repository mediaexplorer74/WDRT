using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the input data using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x02000271 RID: 625
	[ComVisible(true)]
	public sealed class MD5CryptoServiceProvider : MD5
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">A FIPS-compliant algorithm policy is not being used.</exception>
		// Token: 0x06002220 RID: 8736 RVA: 0x00078961 File Offset: 0x00076B61
		[SecuritySafeCritical]
		public MD5CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007899C File Offset: 0x00076B9C
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />.</summary>
		// Token: 0x06002222 RID: 8738 RVA: 0x000789C5 File Offset: 0x00076BC5
		[SecuritySafeCritical]
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000789FC File Offset: 0x00076BFC
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00078A0C File Offset: 0x00076C0C
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			return Utils.EndHash(this._safeHashHandle);
		}

		// Token: 0x04000C64 RID: 3172
		[SecurityCritical]
		private SafeHashHandle _safeHashHandle;
	}
}
