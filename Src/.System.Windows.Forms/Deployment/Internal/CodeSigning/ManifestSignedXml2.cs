using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000014 RID: 20
	internal class ManifestSignedXml2 : SignedXml
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003F97 File Offset: 0x00002197
		internal ManifestSignedXml2()
		{
			this.init();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003FA5 File Offset: 0x000021A5
		internal ManifestSignedXml2(XmlElement elem)
			: base(elem)
		{
			this.init();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003FB4 File Offset: 0x000021B4
		internal ManifestSignedXml2(XmlDocument document)
			: base(document)
		{
			this.init();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003FC3 File Offset: 0x000021C3
		internal ManifestSignedXml2(XmlDocument document, bool verify)
			: base(document)
		{
			this.m_verify = verify;
			this.init();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FD9 File Offset: 0x000021D9
		private void init()
		{
			CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), new string[] { "http://www.w3.org/2000/09/xmldsig#rsa-sha256" });
			CryptoConfig.AddAlgorithm(typeof(SHA256Cng), new string[] { "http://www.w3.org/2000/09/xmldsig#sha256" });
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004018 File Offset: 0x00002218
		public override XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			if (this.m_verify)
			{
				return base.GetIdElement(document, idValue);
			}
			KeyInfo keyInfo = base.KeyInfo;
			if (keyInfo.Id != idValue)
			{
				return null;
			}
			return keyInfo.GetXml();
		}

		// Token: 0x040000E2 RID: 226
		private bool m_verify;

		// Token: 0x040000E3 RID: 227
		private const string Sha256SignatureMethodUri = "http://www.w3.org/2000/09/xmldsig#rsa-sha256";

		// Token: 0x040000E4 RID: 228
		private const string Sha256DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha256";
	}
}
