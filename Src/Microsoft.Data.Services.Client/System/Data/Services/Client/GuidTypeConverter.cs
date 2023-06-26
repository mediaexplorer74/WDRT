using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000096 RID: 150
	internal sealed class GuidTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00015014 File Offset: 0x00013214
		internal override object Parse(string text)
		{
			return new Guid(text);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00015021 File Offset: 0x00013221
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}
	}
}
