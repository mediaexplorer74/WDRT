using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of asymmetric signature formatters derive.</summary>
	// Token: 0x0200024D RID: 589
	[ComVisible(true)]
	public abstract class AsymmetricSignatureFormatter
	{
		/// <summary>When overridden in a derived class, sets the asymmetric algorithm to use to create the signature.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> to use to create the signature.</param>
		// Token: 0x060020EE RID: 8430
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, sets the hash algorithm to use for creating the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for creating the signature.</param>
		// Token: 0x060020EF RID: 8431
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>Creates the signature from the specified hash value.</summary>
		/// <param name="hash">The hash algorithm to use to create the signature.</param>
		/// <returns>The signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060020F0 RID: 8432 RVA: 0x00072CEA File Offset: 0x00070EEA
		public virtual byte[] CreateSignature(HashAlgorithm hash)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.CreateSignature(hash.Hash);
		}

		/// <summary>When overridden in a derived class, creates the signature for the specified data.</summary>
		/// <param name="rgbHash">The data to be signed.</param>
		/// <returns>The digital signature for the <paramref name="rgbHash" /> parameter.</returns>
		// Token: 0x060020F1 RID: 8433
		public abstract byte[] CreateSignature(byte[] rgbHash);
	}
}
