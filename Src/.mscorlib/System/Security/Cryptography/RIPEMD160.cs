using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of the MD160 hash algorithm inherit.</summary>
	// Token: 0x02000278 RID: 632
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> class.</summary>
		// Token: 0x06002269 RID: 8809 RVA: 0x00079C20 File Offset: 0x00077E20
		protected RIPEMD160()
		{
			this.HashSizeValue = 160;
		}

		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm.</summary>
		/// <returns>A new instance of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but it is not FIPS-compatible.</exception>
		// Token: 0x0600226A RID: 8810 RVA: 0x00079C33 File Offset: 0x00077E33
		public new static RIPEMD160 Create()
		{
			return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
		}

		/// <summary>Creates an instance of the specified implementation of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm.</summary>
		/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.RIPEMD160" /> to use.</param>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.RIPEMD160" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="hashName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but it is not FIPS-compatible.</exception>
		// Token: 0x0600226B RID: 8811 RVA: 0x00079C3F File Offset: 0x00077E3F
		public new static RIPEMD160 Create(string hashName)
		{
			return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
		}
	}
}
