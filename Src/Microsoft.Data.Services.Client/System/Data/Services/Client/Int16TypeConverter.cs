using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000097 RID: 151
	internal sealed class Int16TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00015031 File Offset: 0x00013231
		internal override object Parse(string text)
		{
			return XmlConvert.ToInt16(text);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001503E File Offset: 0x0001323E
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((short)instance);
		}
	}
}
