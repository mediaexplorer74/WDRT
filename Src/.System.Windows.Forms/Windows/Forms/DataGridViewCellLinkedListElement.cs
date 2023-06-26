using System;

namespace System.Windows.Forms
{
	// Token: 0x020001AC RID: 428
	internal class DataGridViewCellLinkedListElement
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x0008F24F File Offset: 0x0008D44F
		public DataGridViewCellLinkedListElement(DataGridViewCell dataGridViewCell)
		{
			this.dataGridViewCell = dataGridViewCell;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0008F25E File Offset: 0x0008D45E
		public DataGridViewCell DataGridViewCell
		{
			get
			{
				return this.dataGridViewCell;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0008F266 File Offset: 0x0008D466
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x0008F26E File Offset: 0x0008D46E
		public DataGridViewCellLinkedListElement Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x04000CC3 RID: 3267
		private DataGridViewCell dataGridViewCell;

		// Token: 0x04000CC4 RID: 3268
		private DataGridViewCellLinkedListElement next;
	}
}
