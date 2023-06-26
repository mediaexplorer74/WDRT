using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x0200008E RID: 142
	internal class PrimitiveTypeConverter
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00014E8E File Offset: 0x0001308E
		protected PrimitiveTypeConverter()
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00014E98 File Offset: 0x00013098
		internal virtual PrimitiveParserToken TokenizeFromXml(XmlReader reader)
		{
			string text = MaterializeAtom.ReadElementString(reader, true);
			if (text != null)
			{
				return new TextPrimitiveParserToken(text);
			}
			return null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00014EB8 File Offset: 0x000130B8
		internal virtual PrimitiveParserToken TokenizeFromText(string text)
		{
			return new TextPrimitiveParserToken(text);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00014EC0 File Offset: 0x000130C0
		internal virtual object Parse(string text)
		{
			return text;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00014EC3 File Offset: 0x000130C3
		internal virtual string ToString(object instance)
		{
			throw new NotImplementedException();
		}
	}
}
