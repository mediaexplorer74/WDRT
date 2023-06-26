using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200006C RID: 108
	internal sealed class Expansion
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000A3FB File Offset: 0x000085FB
		public Expansion(IEnumerable<ExpandedNavigationSelectItem> expandItems)
		{
			this.expandItems = (expandItems as ExpandedNavigationSelectItem[]) ?? expandItems.ToArray<ExpandedNavigationSelectItem>();
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000A419 File Offset: 0x00008619
		public IEnumerable<ExpandedNavigationSelectItem> ExpandItems
		{
			get
			{
				return this.expandItems;
			}
		}

		// Token: 0x040000B5 RID: 181
		private readonly IEnumerable<ExpandedNavigationSelectItem> expandItems;
	}
}
