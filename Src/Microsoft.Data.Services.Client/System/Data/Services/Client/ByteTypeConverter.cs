using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000090 RID: 144
	internal sealed class ByteTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00014EEC File Offset: 0x000130EC
		internal override object Parse(string text)
		{
			return XmlConvert.ToByte(text);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00014EF9 File Offset: 0x000130F9
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((byte)instance);
		}
	}
}
