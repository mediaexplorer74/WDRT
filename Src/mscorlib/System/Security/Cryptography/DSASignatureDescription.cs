using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200029E RID: 670
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x060023A4 RID: 9124 RVA: 0x00082042 File Offset: 0x00080242
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
