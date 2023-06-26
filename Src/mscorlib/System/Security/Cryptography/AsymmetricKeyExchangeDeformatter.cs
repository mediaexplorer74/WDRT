using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all asymmetric key exchange deformatters derive.</summary>
	// Token: 0x0200024A RID: 586
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeDeformatter
	{
		/// <summary>When overridden in a derived class, gets or sets the parameters for the asymmetric key exchange.</summary>
		/// <returns>A string in XML format containing the parameters of the asymmetric key exchange operation.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060020DF RID: 8415
		// (set) Token: 0x060020E0 RID: 8416
		public abstract string Parameters { get; set; }

		/// <summary>When overridden in a derived class, sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the private key.</param>
		// Token: 0x060020E1 RID: 8417
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, extracts secret information from the encrypted key exchange data.</summary>
		/// <param name="rgb">The key exchange data within which the secret information is hidden.</param>
		/// <returns>The secret information derived from the key exchange data.</returns>
		// Token: 0x060020E2 RID: 8418
		public abstract byte[] DecryptKeyExchange(byte[] rgb);
	}
}
