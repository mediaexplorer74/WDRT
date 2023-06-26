using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000A0 RID: 160
	internal sealed class UriTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x0001514A File Offset: 0x0001334A
		internal override object Parse(string text)
		{
			return UriUtil.CreateUri(text, UriKind.RelativeOrAbsolute);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00015153 File Offset: 0x00013353
		internal override string ToString(object instance)
		{
			return UriUtil.UriToString((Uri)instance);
		}
	}
}
