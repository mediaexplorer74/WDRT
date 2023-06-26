using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000085 RID: 133
	internal sealed class ConfigXmlCDataSection : XmlCDataSection, IConfigErrorInfo
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00021494 File Offset: 0x0001F694
		public ConfigXmlCDataSection(string filename, int line, string data, XmlDocument doc)
			: base(data, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000214AD File Offset: 0x0001F6AD
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000214B5 File Offset: 0x0001F6B5
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000214C0 File Offset: 0x0001F6C0
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlCDataSection configXmlCDataSection = xmlNode as ConfigXmlCDataSection;
			if (configXmlCDataSection != null)
			{
				configXmlCDataSection._line = this._line;
				configXmlCDataSection._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C1B RID: 3099
		private int _line;

		// Token: 0x04000C1C RID: 3100
		private string _filename;
	}
}
