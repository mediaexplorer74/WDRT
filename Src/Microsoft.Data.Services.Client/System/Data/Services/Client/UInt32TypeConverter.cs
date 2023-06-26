using System;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x020000A6 RID: 166
	internal sealed class UInt32TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0001520D File Offset: 0x0001340D
		internal override object Parse(string text)
		{
			return XmlConvert.ToUInt32(text);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001521A File Offset: 0x0001341A
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((uint)instance);
		}
	}
}
