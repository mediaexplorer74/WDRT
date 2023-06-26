using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies which X509 certificates in the chain should be checked for revocation.</summary>
	// Token: 0x02000471 RID: 1137
	public enum X509RevocationFlag
	{
		/// <summary>Only the end certificate is checked for revocation.</summary>
		// Token: 0x040025F8 RID: 9720
		EndCertificateOnly,
		/// <summary>The entire chain of certificates is checked for revocation.</summary>
		// Token: 0x040025F9 RID: 9721
		EntireChain,
		/// <summary>The entire chain, except the root certificate, is checked for revocation.</summary>
		// Token: 0x040025FA RID: 9722
		ExcludeRoot
	}
}
