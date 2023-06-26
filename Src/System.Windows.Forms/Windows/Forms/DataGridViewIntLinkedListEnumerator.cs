using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000201 RID: 513
	internal class DataGridViewIntLinkedListEnumerator : IEnumerator
	{
		// Token: 0x06002176 RID: 8566 RVA: 0x0009DE50 File Offset: 0x0009C050
		public DataGridViewIntLinkedListEnumerator(DataGridViewIntLinkedListElement headElement)
		{
			this.headElement = headElement;
			this.reset = true;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x0009DE66 File Offset: 0x0009C066
		object IEnumerator.Current
		{
			get
			{
				return this.current.Int;
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0009DE78 File Offset: 0x0009C078
		bool IEnumerator.MoveNext()
		{
			if (this.reset)
			{
				this.current = this.headElement;
				this.reset = false;
			}
			else
			{
				this.current = this.current.Next;
			}
			return this.current != null;
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0009DEB1 File Offset: 0x0009C0B1
		void IEnumerator.Reset()
		{
			this.reset = true;
			this.current = null;
		}

		// Token: 0x04000DF0 RID: 3568
		private DataGridViewIntLinkedListElement headElement;

		// Token: 0x04000DF1 RID: 3569
		private DataGridViewIntLinkedListElement current;

		// Token: 0x04000DF2 RID: 3570
		private bool reset;
	}
}
