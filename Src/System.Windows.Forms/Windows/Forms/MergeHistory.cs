using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	// Token: 0x020003E2 RID: 994
	internal class MergeHistory
	{
		// Token: 0x060043C6 RID: 17350 RVA: 0x0011EC21 File Offset: 0x0011CE21
		public MergeHistory(ToolStrip mergedToolStrip)
		{
			this.mergedToolStrip = mergedToolStrip;
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x0011EC30 File Offset: 0x0011CE30
		public Stack<MergeHistoryItem> MergeHistoryItemsStack
		{
			get
			{
				if (this.mergeHistoryItemsStack == null)
				{
					this.mergeHistoryItemsStack = new Stack<MergeHistoryItem>();
				}
				return this.mergeHistoryItemsStack;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060043C8 RID: 17352 RVA: 0x0011EC4B File Offset: 0x0011CE4B
		public ToolStrip MergedToolStrip
		{
			get
			{
				return this.mergedToolStrip;
			}
		}

		// Token: 0x040025E7 RID: 9703
		private Stack<MergeHistoryItem> mergeHistoryItemsStack;

		// Token: 0x040025E8 RID: 9704
		private ToolStrip mergedToolStrip;
	}
}
