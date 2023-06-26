using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A7 RID: 167
	internal sealed class UInt64TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x0001522F File Offset: 0x0001342F
		internal override object Parse(string text)
		{
			return XmlConvert.ToUInt64(text);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001523C File Offset: 0x0001343C
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((ulong)instance);
		}
	}
}
