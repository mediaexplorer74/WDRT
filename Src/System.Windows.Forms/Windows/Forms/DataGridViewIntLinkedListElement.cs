using System;

namespace System.Windows.Forms
{
	// Token: 0x02000202 RID: 514
	internal class DataGridViewIntLinkedListElement
	{
		// Token: 0x0600217A RID: 8570 RVA: 0x0009DEC1 File Offset: 0x0009C0C1
		public DataGridViewIntLinkedListElement(int integer)
		{
			this.integer = integer;
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0009DED0 File Offset: 0x0009C0D0
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x0009DED8 File Offset: 0x0009C0D8
		public int Int
		{
			get
			{
				return this.integer;
			}
			set
			{
				this.integer = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0009DEE1 File Offset: 0x0009C0E1
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x0009DEE9 File Offset: 0x0009C0E9
		public DataGridViewIntLinkedListElement Next
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

		// Token: 0x04000DF3 RID: 3571
		private int integer;

		// Token: 0x04000DF4 RID: 3572
		private DataGridViewIntLinkedListElement next;
	}
}
