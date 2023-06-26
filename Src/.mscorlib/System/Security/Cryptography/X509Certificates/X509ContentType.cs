using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the format of an X.509 certificate.</summary>
	// Token: 0x020002AC RID: 684
	[ComVisible(true)]
	public enum X509ContentType
	{
		/// <summary>An unknown X.509 certificate.</summary>
		// Token: 0x04000D95 RID: 3477
		Unknown,
		/// <summary>A single X.509 certificate.</summary>
		// Token: 0x04000D96 RID: 3478
		Cert,
		/// <summary>A single serialized X.509 certificate.</summary>
		// Token: 0x04000D97 RID: 3479
		SerializedCert,
		/// <summary>A PFX-formatted certificate. The <see langword="Pfx" /> value is identical to the <see langword="Pkcs12" /> value.</summary>
		// Token: 0x04000D98 RID: 3480
		Pfx,
		/// <summary>A PKCS #12-formatted certificate. The <see langword="Pkcs12" /> value is identical to the <see langword="Pfx" /> value.</summary>
		// Token: 0x04000D99 RID: 3481
		Pkcs12 = 3,
		/// <summary>A serialized store.</summary>
		// Token: 0x04000D9A RID: 3482
		SerializedStore,
		/// <summary>A PKCS #7-formatted certificate.</summary>
		// Token: 0x04000D9B RID: 3483
		Pkcs7,
		/// <summary>An Authenticode X.509 certificate.</summary>
		// Token: 0x04000D9C RID: 3484
		Authenticode
	}
}
