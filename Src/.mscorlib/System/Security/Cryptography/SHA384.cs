using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA384" /> hash for the input data.</summary>
	// Token: 0x02000294 RID: 660
	[ComVisible(true)]
	public abstract class SHA384 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.SHA384" />.</summary>
		// Token: 0x06002366 RID: 9062 RVA: 0x00080DEE File Offset: 0x0007EFEE
		protected SHA384()
		{
			this.HashSizeValue = 384;
		}

		/// <summary>Creates an instance of the default implementation of <see cref="T:System.Security.Cryptography.SHA384" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA384" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06002367 RID: 9063 RVA: 0x00080E01 File Offset: 0x0007F001
		public new static SHA384 Create()
		{
			return SHA384.Create("System.Security.Cryptography.SHA384");
		}

		/// <summary>Creates an instance of a specified implementation of <see cref="T:System.Security.Cryptography.SHA384" />.</summary>
		/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.SHA384" /> to be used.</param>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA384" /> using the specified implementation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="hashName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06002368 RID: 9064 RVA: 0x00080E0D File Offset: 0x0007F00D
		public new static SHA384 Create(string hashName)
		{
			return (SHA384)CryptoConfig.CreateFromName(hashName);
		}
	}
}
