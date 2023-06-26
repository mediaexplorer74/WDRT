using System;
using System.Xml.Linq;

namespace System.Data.Services.Client
{
	// Token: 0x020000A1 RID: 161
	internal sealed class XDocumentTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00015168 File Offset: 0x00013368
		internal override object Parse(string text)
		{
			if (text.Length <= 0)
			{
				return new XDocument();
			}
			return XDocument.Parse(text);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001517F File Offset: 0x0001337F
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}
	}
}
