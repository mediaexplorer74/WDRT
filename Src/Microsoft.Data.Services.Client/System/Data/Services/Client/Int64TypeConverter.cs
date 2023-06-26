using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000099 RID: 153
	internal sealed class Int64TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00015075 File Offset: 0x00013275
		internal override object Parse(string text)
		{
			return XmlConvert.ToInt64(text);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00015082 File Offset: 0x00013282
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((long)instance);
		}
	}
}
