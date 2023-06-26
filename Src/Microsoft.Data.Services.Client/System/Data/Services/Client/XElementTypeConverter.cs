using System;
using System.Xml.Linq;

namespace System.Data.Services.Client
{
	// Token: 0x020000A2 RID: 162
	internal sealed class XElementTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0001518F File Offset: 0x0001338F
		internal override object Parse(string text)
		{
			return XElement.Parse(text);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00015197 File Offset: 0x00013397
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}
	}
}
