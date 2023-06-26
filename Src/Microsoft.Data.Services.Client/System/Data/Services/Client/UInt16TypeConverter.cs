using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A5 RID: 165
	internal sealed class UInt16TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x000151EB File Offset: 0x000133EB
		internal override object Parse(string text)
		{
			return XmlConvert.ToUInt16(text);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000151F8 File Offset: 0x000133F8
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((ushort)instance);
		}
	}
}
