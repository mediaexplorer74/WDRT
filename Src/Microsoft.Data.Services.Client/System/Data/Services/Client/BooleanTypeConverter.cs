using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x0200008F RID: 143
	internal sealed class BooleanTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00014ECA File Offset: 0x000130CA
		internal override object Parse(string text)
		{
			return XmlConvert.ToBoolean(text);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00014ED7 File Offset: 0x000130D7
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((bool)instance);
		}
	}
}
