using System;

namespace System.Windows.Forms
{
	// Token: 0x020003CA RID: 970
	internal class ToolStripItemImageIndexer : ImageList.Indexer
	{
		// Token: 0x060042F4 RID: 17140 RVA: 0x0011C13F File Offset: 0x0011A33F
		public ToolStripItemImageIndexer(ToolStripItem item)
		{
			this.item = item;
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x0011C14E File Offset: 0x0011A34E
		// (set) Token: 0x060042F6 RID: 17142 RVA: 0x000070A6 File Offset: 0x000052A6
		public override ImageList ImageList
		{
			get
			{
				if (this.item != null && this.item.Owner != null)
				{
					return this.item.Owner.ImageList;
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x04002586 RID: 9606
		private ToolStripItem item;
	}
}
