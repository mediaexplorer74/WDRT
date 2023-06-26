using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000091 RID: 145
	internal sealed class ByteArrayTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x00014F0E File Offset: 0x0001310E
		internal override object Parse(string text)
		{
			return Convert.FromBase64String(text);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00014F16 File Offset: 0x00013116
		internal override string ToString(object instance)
		{
			return Convert.ToBase64String((byte[])instance);
		}
	}
}
