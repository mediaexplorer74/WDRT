using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used to create keys shared by the client and server.</summary>
	// Token: 0x0200043D RID: 1085
	[global::__DynamicallyInvokable]
	public enum ExchangeAlgorithmType
	{
		/// <summary>No key exchange algorithm is used.</summary>
		// Token: 0x0400222E RID: 8750
		[global::__DynamicallyInvokable]
		None,
		/// <summary>The RSA public-key signature algorithm.</summary>
		// Token: 0x0400222F RID: 8751
		[global::__DynamicallyInvokable]
		RsaSign = 9216,
		/// <summary>The RSA public-key exchange algorithm.</summary>
		// Token: 0x04002230 RID: 8752
		[global::__DynamicallyInvokable]
		RsaKeyX = 41984,
		/// <summary>The Diffie Hellman ephemeral key exchange algorithm.</summary>
		// Token: 0x04002231 RID: 8753
		[global::__DynamicallyInvokable]
		DiffieHellman = 43522
	}
}
