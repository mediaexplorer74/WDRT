using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x0200009C RID: 156
	internal sealed class SByteTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x000150CC File Offset: 0x000132CC
		internal override object Parse(string text)
		{
			return XmlConvert.ToSByte(text);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000150D9 File Offset: 0x000132D9
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((sbyte)instance);
		}
	}
}
