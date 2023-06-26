using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000094 RID: 148
	internal sealed class DecimalTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00014FD0 File Offset: 0x000131D0
		internal override object Parse(string text)
		{
			return XmlConvert.ToDecimal(text);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00014FDD File Offset: 0x000131DD
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((decimal)instance);
		}
	}
}
