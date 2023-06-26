using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200009F RID: 159
	internal sealed class ClrTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0001512D File Offset: 0x0001332D
		internal override object Parse(string text)
		{
			return PlatformHelper.GetTypeOrThrow(text);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00015135 File Offset: 0x00013335
		internal override string ToString(object instance)
		{
			return ((Type)instance).AssemblyQualifiedName;
		}
	}
}
