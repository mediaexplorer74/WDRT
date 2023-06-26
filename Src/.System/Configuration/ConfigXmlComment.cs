using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000086 RID: 134
	internal sealed class ConfigXmlComment : XmlComment, IConfigErrorInfo
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x000214F8 File Offset: 0x0001F6F8
		public ConfigXmlComment(string filename, int line, string comment, XmlDocument doc)
			: base(comment, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00021511 File Offset: 0x0001F711
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00021519 File Offset: 0x0001F719
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00021524 File Offset: 0x0001F724
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlComment configXmlComment = xmlNode as ConfigXmlComment;
			if (configXmlComment != null)
			{
				configXmlComment._line = this._line;
				configXmlComment._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x04000C1D RID: 3101
		private int _line;

		// Token: 0x04000C1E RID: 3102
		private string _filename;
	}
}
