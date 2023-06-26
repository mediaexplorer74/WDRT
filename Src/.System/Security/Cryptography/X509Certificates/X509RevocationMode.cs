using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the mode used to check for X509 certificate revocation.</summary>
	// Token: 0x02000470 RID: 1136
	public enum X509RevocationMode
	{
		/// <summary>No revocation check is performed on the certificate.</summary>
		// Token: 0x040025F4 RID: 9716
		NoCheck,
		/// <summary>A revocation check is made using an online certificate revocation list (CRL).</summary>
		// Token: 0x040025F5 RID: 9717
		Online,
		/// <summary>A revocation check is made using a cached certificate revocation list (CRL).</summary>
		// Token: 0x040025F6 RID: 9718
		Offline
	}
}
