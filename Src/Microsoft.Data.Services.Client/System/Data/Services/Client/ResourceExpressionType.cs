using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000D5 RID: 213
	internal enum ResourceExpressionType
	{
		// Token: 0x0400042A RID: 1066
		RootResourceSet = 10000,
		// Token: 0x0400042B RID: 1067
		ResourceNavigationProperty,
		// Token: 0x0400042C RID: 1068
		ResourceNavigationPropertySingleton,
		// Token: 0x0400042D RID: 1069
		TakeQueryOption,
		// Token: 0x0400042E RID: 1070
		SkipQueryOption,
		// Token: 0x0400042F RID: 1071
		OrderByQueryOption,
		// Token: 0x04000430 RID: 1072
		FilterQueryOption,
		// Token: 0x04000431 RID: 1073
		InputReference,
		// Token: 0x04000432 RID: 1074
		ProjectionQueryOption,
		// Token: 0x04000433 RID: 1075
		ExpandQueryOption
	}
}
