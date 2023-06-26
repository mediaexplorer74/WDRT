using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A3 RID: 163
	internal sealed class DateTimeOffsetTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x000151A7 File Offset: 0x000133A7
		internal override object Parse(string text)
		{
			return PlatformHelper.ConvertStringToDateTimeOffset(text);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000151B4 File Offset: 0x000133B4
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((DateTimeOffset)instance);
		}
	}
}
