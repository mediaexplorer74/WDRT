using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies how much of the X.509 certificate chain should be included in the X.509 data.</summary>
	// Token: 0x02000464 RID: 1124
	public enum X509IncludeOption
	{
		/// <summary>No X.509 chain information is included.</summary>
		// Token: 0x0400259D RID: 9629
		None,
		/// <summary>The entire X.509 chain is included except for the root certificate.</summary>
		// Token: 0x0400259E RID: 9630
		ExcludeRoot,
		/// <summary>Only the end certificate is included in the X.509 chain information.</summary>
		// Token: 0x0400259F RID: 9631
		EndCertOnly,
		/// <summary>The entire X.509 chain is included.</summary>
		// Token: 0x040025A0 RID: 9632
		WholeChain
	}
}
