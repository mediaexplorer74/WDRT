using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used for generating message authentication codes (MACs).</summary>
	// Token: 0x0200043F RID: 1087
	[global::__DynamicallyInvokable]
	public enum HashAlgorithmType
	{
		/// <summary>No hashing algorithm is used.</summary>
		// Token: 0x0400223E RID: 8766
		[global::__DynamicallyInvokable]
		None,
		/// <summary>The Message Digest 5 (MD5) hashing algorithm.</summary>
		// Token: 0x0400223F RID: 8767
		[global::__DynamicallyInvokable]
		Md5 = 32771,
		/// <summary>The Secure Hashing Algorithm (SHA1).</summary>
		// Token: 0x04002240 RID: 8768
		[global::__DynamicallyInvokable]
		Sha1,
		/// <summary>The Secure Hashing Algorithm 2 (SHA-2), using a 256-bit digest.</summary>
		// Token: 0x04002241 RID: 8769
		Sha256 = 32780,
		/// <summary>The Secure Hashing Algorithm 2 (SHA-2), using a 384-bit digest.</summary>
		// Token: 0x04002242 RID: 8770
		Sha384,
		/// <summary>The Secure Hashing Algorithm 2 (SHA-2), using a 512-bit digest.</summary>
		// Token: 0x04002243 RID: 8771
		Sha512
	}
}
