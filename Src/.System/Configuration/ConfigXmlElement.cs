using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000088 RID: 136
	internal sealed class ConfigXmlElement : XmlElement, IConfigErrorInfo
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x0002172F File Offset: 0x0001F92F
		public ConfigXmlElement(string filename, int line, string prefix, string localName, string namespaceUri, XmlDocument doc)
			: base(prefix, localName, namespaceUri, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002174C File Offset: 0x0001F94C
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00021754 File Offset: 0x0001F954
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002175C File Offset: 0x0001F95C
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlElement configXmlElement = xmlNode as ConfigXmlElement;
			if (configXmlElement != null)
			{
				configXmlElement._line = this._line;
				configXmlElement._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C22 RID: 3106
		private int _line;

		// Token: 0x04000C23 RID: 3107
		private string _filename;
	}
}
