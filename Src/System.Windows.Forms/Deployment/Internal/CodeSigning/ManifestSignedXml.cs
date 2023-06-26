using System;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x0200000B RID: 11
	internal class ManifestSignedXml : SignedXml
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000276E File Offset: 0x0000096E
		internal ManifestSignedXml()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002776 File Offset: 0x00000976
		internal ManifestSignedXml(XmlElement elem)
			: base(elem)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000277F File Offset: 0x0000097F
		internal ManifestSignedXml(XmlDocument document)
			: base(document)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002788 File Offset: 0x00000988
		internal ManifestSignedXml(XmlDocument document, bool verify)
			: base(document)
		{
			this.m_verify = verify;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002798 File Offset: 0x00000998
		private static XmlElement FindIdElement(XmlElement context, string idValue)
		{
			if (context == null)
			{
				return null;
			}
			XmlElement xmlElement = context.SelectSingleNode("//*[@Id=\"" + idValue + "\"]") as XmlElement;
			if (xmlElement != null)
			{
				return xmlElement;
			}
			xmlElement = context.SelectSingleNode("//*[@id=\"" + idValue + "\"]") as XmlElement;
			if (xmlElement != null)
			{
				return xmlElement;
			}
			return context.SelectSingleNode("//*[@ID=\"" + idValue + "\"]") as XmlElement;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002808 File Offset: 0x00000A08
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

		// Token: 0x040000B1 RID: 177
		private bool m_verify;
	}
}
