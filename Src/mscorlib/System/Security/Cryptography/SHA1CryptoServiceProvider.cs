using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the input data using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x02000290 RID: 656
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> class.</summary>
		// Token: 0x06002342 RID: 9026 RVA: 0x0007FD21 File Offset: 0x0007DF21
		[SecuritySafeCritical]
		public SHA1CryptoServiceProvider()
		{
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0007FD3E File Offset: 0x0007DF3E
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />.</summary>
		// Token: 0x06002344 RID: 9028 RVA: 0x0007FD67 File Offset: 0x0007DF67
		[SecuritySafeCritical]
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0007FD9E File Offset: 0x0007DF9E
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0007FDAE File Offset: 0x0007DFAE
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			return Utils.EndHash(this._safeHashHandle);
		}

		// Token: 0x04000CDA RID: 3290
		[SecurityCritical]
		private SafeHashHandle _safeHashHandle;
	}
}
