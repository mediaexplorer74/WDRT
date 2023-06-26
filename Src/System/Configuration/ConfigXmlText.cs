using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200008A RID: 138
	internal sealed class ConfigXmlText : XmlText, IConfigErrorInfo
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000217F8 File Offset: 0x0001F9F8
		public ConfigXmlText(string filename, int line, string strData, XmlDocument doc)
			: base(strData, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00021811 File Offset: 0x0001FA11
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00021819 File Offset: 0x0001FA19
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00021824 File Offset: 0x0001FA24
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlText configXmlText = xmlNode as ConfigXmlText;
			if (configXmlText != null)
			{
				configXmlText._line = this._line;
				configXmlText._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C26 RID: 3110
		private int _line;

		// Token: 0x04000C27 RID: 3111
		private string _filename;
	}
}
