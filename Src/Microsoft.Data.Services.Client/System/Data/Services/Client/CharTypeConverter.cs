using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x0200009D RID: 157
	internal sealed class CharTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x000150EE File Offset: 0x000132EE
		internal override object Parse(string text)
		{
			return XmlConvert.ToChar(text);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000150FB File Offset: 0x000132FB
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((char)instance);
		}
	}
}
