using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000248 RID: 584
	internal static class UriUtilsCommon
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x000467E2 File Offset: 0x000449E2
		internal static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}
	}
}
