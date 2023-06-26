using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000093 RID: 147
	internal sealed class DateTimeTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00014FAE File Offset: 0x000131AE
		internal override object Parse(string text)
		{
			return PlatformHelper.ConvertStringToDateTime(text);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00014FBB File Offset: 0x000131BB
		internal override string ToString(object instance)
		{
			return PlatformHelper.ConvertDateTimeToString((DateTime)instance);
		}
	}
}
