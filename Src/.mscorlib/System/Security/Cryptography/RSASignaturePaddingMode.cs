using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the padding mode to use with RSA signature creation or verification operations.</summary>
	// Token: 0x0200027D RID: 637
	public enum RSASignaturePaddingMode
	{
		/// <summary>PKCS #1 v1.5</summary>
		// Token: 0x04000C91 RID: 3217
		Pkcs1,
		/// <summary>Probabilistic Signature Scheme</summary>
		// Token: 0x04000C92 RID: 3218
		Pss
	}
}
