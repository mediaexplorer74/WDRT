using System;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x02000276 RID: 630
	internal static class DebugUtils
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x0004E281 File Offset: 0x0004C481
		[Conditional("DEBUG")]
		internal static void CheckNoExternalCallers()
		{
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004E283 File Offset: 0x0004C483
		[Conditional("DEBUG")]
		internal static void CheckNoExternalCallers(bool checkPublicMethods)
		{
		}
	}
}
