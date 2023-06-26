using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA512" /> hash for the input data.</summary>
	// Token: 0x02000296 RID: 662
	[ComVisible(true)]
	public abstract class SHA512 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.SHA512" />.</summary>
		// Token: 0x0600237B RID: 9083 RVA: 0x0008162A File Offset: 0x0007F82A
		protected SHA512()
		{
			this.HashSizeValue = 512;
		}

		/// <summary>Creates an instance of the default implementation of <see cref="T:System.Security.Cryptography.SHA512" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA512" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x0600237C RID: 9084 RVA: 0x0008163D File Offset: 0x0007F83D
		public new static SHA512 Create()
		{
			return SHA512.Create("System.Security.Cryptography.SHA512");
		}

		/// <summary>Creates an instance of a specified implementation of <see cref="T:System.Security.Cryptography.SHA512" />.</summary>
		/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.SHA512" /> to be used.</param>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA512" /> using the specified implementation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="hashName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x0600237D RID: 9085 RVA: 0x00081649 File Offset: 0x0007F849
		public new static SHA512 Create(string hashName)
		{
			return (SHA512)CryptoConfig.CreateFromName(hashName);
		}
	}
}
