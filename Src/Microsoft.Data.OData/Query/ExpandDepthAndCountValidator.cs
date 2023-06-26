using System;
using System.Linq;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000010 RID: 16
	internal sealed class ExpandDepthAndCountValidator
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002F6B File Offset: 0x0000116B
		internal ExpandDepthAndCountValidator(int maxDepth, int maxCount)
		{
			this.maxDepth = maxDepth;
			this.maxCount = maxCount;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F81 File Offset: 0x00001181
		internal void Validate(SelectExpandClause expandTree)
		{
			this.currentCount = 0;
			this.EnsureMaximumCountAndDepthAreNotExceeded(expandTree, 0);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F94 File Offset: 0x00001194
		private void EnsureMaximumCountAndDepthAreNotExceeded(SelectExpandClause expandTree, int currentDepth)
		{
			if (currentDepth > this.maxDepth)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandDepthExceeded(currentDepth, this.maxDepth));
			}
			foreach (ExpandedNavigationSelectItem expandedNavigationSelectItem in expandTree.SelectedItems.OfType<ExpandedNavigationSelectItem>())
			{
				this.currentCount++;
				if (this.currentCount > this.maxCount)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandCountExceeded(this.currentCount, this.maxCount));
				}
				this.EnsureMaximumCountAndDepthAreNotExceeded(expandedNavigationSelectItem.SelectAndExpand, currentDepth + 1);
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly int maxDepth;

		// Token: 0x0400002B RID: 43
		private readonly int maxCount;

		// Token: 0x0400002C RID: 44
		private int currentCount;
	}
}
