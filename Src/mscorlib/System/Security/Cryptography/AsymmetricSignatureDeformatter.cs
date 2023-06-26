using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric signature deformatters derive.</summary>
	// Token: 0x0200024C RID: 588
	[ComVisible(true)]
	public abstract class AsymmetricSignatureDeformatter
	{
		/// <summary>When overridden in a derived class, sets the public key to use for verifying the signature.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key.</param>
		// Token: 0x060020E9 RID: 8425
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, sets the hash algorithm to use for verifying the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for verifying the signature.</param>
		// Token: 0x060020EA RID: 8426
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>Verifies the signature from the specified hash value.</summary>
		/// <param name="hash">The hash algorithm to use to verify the signature.</param>
		/// <param name="rgbSignature">The signature to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid for the hash; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060020EB RID: 8427 RVA: 0x00072CB9 File Offset: 0x00070EB9
		public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.VerifySignature(hash.Hash, rgbSignature);
		}

		/// <summary>When overridden in a derived class, verifies the signature for the specified data.</summary>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />.</param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020EC RID: 8428
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
	}
}
