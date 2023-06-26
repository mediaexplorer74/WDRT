using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200009B RID: 155
	internal sealed class StringTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x000150B9 File Offset: 0x000132B9
		internal override object Parse(string text)
		{
			return text;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000150BC File Offset: 0x000132BC
		internal override string ToString(object instance)
		{
			return (string)instance;
		}
	}
}
