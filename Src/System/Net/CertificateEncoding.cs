using System;

namespace System.Net
{
	// Token: 0x02000134 RID: 308
	internal enum CertificateEncoding
	{
		// Token: 0x0400103C RID: 4156
		Zero,
		// Token: 0x0400103D RID: 4157
		X509AsnEncoding,
		// Token: 0x0400103E RID: 4158
		X509NdrEncoding,
		// Token: 0x0400103F RID: 4159
		Pkcs7AsnEncoding = 65536,
		// Token: 0x04001040 RID: 4160
		Pkcs7NdrEncoding = 131072,
		// Token: 0x04001041 RID: 4161
		AnyAsnEncoding = 65537
	}
}
