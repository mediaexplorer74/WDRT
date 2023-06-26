using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x020003E3 RID: 995
	internal class MergeHistoryItem
	{
		// Token: 0x060043C9 RID: 17353 RVA: 0x0011EC53 File Offset: 0x0011CE53
		public MergeHistoryItem(MergeAction mergeAction)
		{
			this.mergeAction = mergeAction;
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x0011EC70 File Offset: 0x0011CE70
		public MergeAction MergeAction
		{
			get
			{
				return this.mergeAction;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x0011EC78 File Offset: 0x0011CE78
		// (set) Token: 0x060043CC RID: 17356 RVA: 0x0011EC80 File Offset: 0x0011CE80
		public ToolStripItem TargetItem
		{
			get
			{
				return this.targetItem;
			}
			set
			{
				this.targetItem = value;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x0011EC89 File Offset: 0x0011CE89
		// (set) Token: 0x060043CE RID: 17358 RVA: 0x0011EC91 File Offset: 0x0011CE91
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x0011EC9A File Offset: 0x0011CE9A
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x0011ECA2 File Offset: 0x0011CEA2
		public int PreviousIndex
		{
			get
			{
				return this.previousIndex;
			}
			set
			{
				this.previousIndex = value;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x0011ECAB File Offset: 0x0011CEAB
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x0011ECB3 File Offset: 0x0011CEB3
		public ToolStripItemCollection PreviousIndexCollection
		{
			get
			{
				return this.previousIndexCollection;
			}
			set
			{
				this.previousIndexCollection = value;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x0011ECBC File Offset: 0x0011CEBC
		// (set) Token: 0x060043D4 RID: 17364 RVA: 0x0011ECC4 File Offset: 0x0011CEC4
		public ToolStripItemCollection IndexCollection
		{
			get
			{
				return this.indexCollection;
			}
			set
			{
				this.indexCollection = value;
			}
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x0011ECD0 File Offset: 0x0011CED0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MergeAction: ",
				this.mergeAction.ToString(),
				" | TargetItem: ",
				(this.TargetItem == null) ? "null" : this.TargetItem.Text,
				" Index: ",
				this.index.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x040025E9 RID: 9705
		private MergeAction mergeAction;

		// Token: 0x040025EA RID: 9706
		private ToolStripItem targetItem;

		// Token: 0x040025EB RID: 9707
		private int index = -1;

		// Token: 0x040025EC RID: 9708
		private int previousIndex = -1;

		// Token: 0x040025ED RID: 9709
		private ToolStripItemCollection previousIndexCollection;

		// Token: 0x040025EE RID: 9710
		private ToolStripItemCollection indexCollection;
	}
}
