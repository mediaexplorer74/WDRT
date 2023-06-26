using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x0200009A RID: 154
	internal sealed class SingleTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00015097 File Offset: 0x00013297
		internal override object Parse(string text)
		{
			return XmlConvert.ToSingle(text);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000150A4 File Offset: 0x000132A4
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((float)instance);
		}
	}
}
