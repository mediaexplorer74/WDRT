using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200008B RID: 139
	internal sealed class ConfigXmlWhitespace : XmlWhitespace, IConfigErrorInfo
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0002185C File Offset: 0x0001FA5C
		public ConfigXmlWhitespace(string filename, int line, string comment, XmlDocument doc)
			: base(comment, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00021875 File Offset: 0x0001FA75
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0002187D File Offset: 0x0001FA7D
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00021888 File Offset: 0x0001FA88
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlWhitespace configXmlWhitespace = xmlNode as ConfigXmlWhitespace;
			if (configXmlWhitespace != null)
			{
				configXmlWhitespace._line = this._line;
				configXmlWhitespace._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C28 RID: 3112
		private int _line;

		// Token: 0x04000C29 RID: 3113
		private string _filename;
	}
}
