using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A4 RID: 164
	internal sealed class TimeSpanTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x000151C9 File Offset: 0x000133C9
		internal override object Parse(string text)
		{
			return XmlConvert.ToTimeSpan(text);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000151D6 File Offset: 0x000133D6
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((TimeSpan)instance);
		}
	}
}
