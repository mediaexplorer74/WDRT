using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000084 RID: 132
	internal sealed class ConfigXmlAttribute : XmlAttribute, IConfigErrorInfo
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0002142E File Offset: 0x0001F62E
		public ConfigXmlAttribute(string filename, int line, string prefix, string localName, string namespaceUri, XmlDocument doc)
			: base(prefix, localName, namespaceUri, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0002144B File Offset: 0x0001F64B
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00021453 File Offset: 0x0001F653
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002145C File Offset: 0x0001F65C
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlAttribute configXmlAttribute = xmlNode as ConfigXmlAttribute;
			if (configXmlAttribute != null)
			{
				configXmlAttribute._line = this._line;
				configXmlAttribute._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C19 RID: 3097
		private int _line;

		// Token: 0x04000C1A RID: 3098
		private string _filename;
	}
}
