using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000220 RID: 544
	internal sealed class CachedPrimitiveKeepInContentAnnotation
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x0003FC4F File Offset: 0x0003DE4F
		internal CachedPrimitiveKeepInContentAnnotation(IEnumerable<string> keptInContentPropertyNames)
		{
			this.keptInContentPropertyNames = ((keptInContentPropertyNames == null) ? null : new HashSet<string>(keptInContentPropertyNames, StringComparer.Ordinal));
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003FC6E File Offset: 0x0003DE6E
		internal bool IsKeptInContent(string propertyName)
		{
			return this.keptInContentPropertyNames != null && this.keptInContentPropertyNames.Contains(propertyName);
		}

		// Token: 0x04000636 RID: 1590
		private readonly HashSet<string> keptInContentPropertyNames;
	}
}
