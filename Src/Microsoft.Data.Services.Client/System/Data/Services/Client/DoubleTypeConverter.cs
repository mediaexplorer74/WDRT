using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000095 RID: 149
	internal sealed class DoubleTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00014FF2 File Offset: 0x000131F2
		internal override object Parse(string text)
		{
			return XmlConvert.ToDouble(text);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00014FFF File Offset: 0x000131FF
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((double)instance);
		}
	}
}
