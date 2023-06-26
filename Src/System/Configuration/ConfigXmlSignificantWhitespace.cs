using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000089 RID: 137
	internal sealed class ConfigXmlSignificantWhitespace : XmlSignificantWhitespace, IConfigErrorInfo
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x00021794 File Offset: 0x0001F994
		public ConfigXmlSignificantWhitespace(string filename, int line, string strData, XmlDocument doc)
			: base(strData, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000217AD File Offset: 0x0001F9AD
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000217B5 File Offset: 0x0001F9B5
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000217C0 File Offset: 0x0001F9C0
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlSignificantWhitespace configXmlSignificantWhitespace = xmlNode as ConfigXmlSignificantWhitespace;
			if (configXmlSignificantWhitespace != null)
			{
				configXmlSignificantWhitespace._line = this._line;
				configXmlSignificantWhitespace._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C24 RID: 3108
		private int _line;

		// Token: 0x04000C25 RID: 3109
		private string _filename;
	}
}
