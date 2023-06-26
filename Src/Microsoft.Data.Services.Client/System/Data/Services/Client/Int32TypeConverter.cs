using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000098 RID: 152
	internal sealed class Int32TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x00015053 File Offset: 0x00013253
		internal override object Parse(string text)
		{
			return XmlConvert.ToInt32(text);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00015060 File Offset: 0x00013260
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((int)instance);
		}
	}
}
