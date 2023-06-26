using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200009E RID: 158
	internal sealed class CharArrayTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x00015110 File Offset: 0x00013310
		internal override object Parse(string text)
		{
			return text.ToCharArray();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00015118 File Offset: 0x00013318
		internal override string ToString(object instance)
		{
			return new string((char[])instance);
		}
	}
}
