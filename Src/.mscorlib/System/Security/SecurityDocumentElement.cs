using System;

namespace System.Security
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	internal sealed class SecurityDocumentElement : ISecurityElementFactory
	{
		// Token: 0x06001BFC RID: 7164 RVA: 0x000606F9 File Offset: 0x0005E8F9
		internal SecurityDocumentElement(SecurityDocument document, int position)
		{
			this.m_document = document;
			this.m_position = position;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0006070F File Offset: 0x0005E90F
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this.m_document.GetElement(this.m_position, true);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00060723 File Offset: 0x0005E923
		object ISecurityElementFactory.Copy()
		{
			return new SecurityDocumentElement(this.m_document, this.m_position);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00060736 File Offset: 0x0005E936
		string ISecurityElementFactory.GetTag()
		{
			return this.m_document.GetTagForElement(this.m_position);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00060749 File Offset: 0x0005E949
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.m_document.GetAttributeForElement(this.m_position, attributeName);
		}

		// Token: 0x040009B6 RID: 2486
		private int m_position;

		// Token: 0x040009B7 RID: 2487
		private SecurityDocument m_document;
	}
}
