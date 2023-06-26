using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000075 RID: 117
	internal sealed class PartialSelection : Selection
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A83E File Offset: 0x00008A3E
		public PartialSelection(IEnumerable<SelectItem> selectedItems)
		{
			this.selectedItems = selectedItems ?? ((IEnumerable<SelectItem>)new SelectItem[0]);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A85C File Offset: 0x00008A5C
		public IEnumerable<SelectItem> SelectedItems
		{
			get
			{
				return this.selectedItems;
			}
		}

		// Token: 0x040000C0 RID: 192
		private readonly IEnumerable<SelectItem> selectedItems;
	}
}
