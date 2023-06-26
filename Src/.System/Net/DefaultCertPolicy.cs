using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000145 RID: 325
	internal class DefaultCertPolicy : ICertificatePolicy
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x0003E390 File Offset: 0x0003C590
		public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest request, int problem)
		{
			return problem == 0;
		}
	}
}
