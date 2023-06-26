using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all asymmetric key exchange formatters derive.</summary>
	// Token: 0x0200024B RID: 587
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeFormatter
	{
		/// <summary>When overridden in a derived class, gets the parameters for the asymmetric key exchange.</summary>
		/// <returns>A string in XML format containing the parameters of the asymmetric key exchange operation.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060020E4 RID: 8420
		public abstract string Parameters { get; }

		/// <summary>When overridden in a derived class, sets the public key to use for encrypting the secret information.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key.</param>
		// Token: 0x060020E5 RID: 8421
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="data">The secret information to be passed in the key exchange.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		// Token: 0x060020E6 RID: 8422
		public abstract byte[] CreateKeyExchange(byte[] data);

		/// <summary>When overridden in a derived class, creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="data">The secret information to be passed in the key exchange.</param>
		/// <param name="symAlgType">This parameter is not used in the current version.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		// Token: 0x060020E7 RID: 8423
		public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
	}
}
