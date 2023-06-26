using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020001AB RID: 427
	internal class DataGridViewCellLinkedListEnumerator : IEnumerator
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x0008F1E3 File Offset: 0x0008D3E3
		public DataGridViewCellLinkedListEnumerator(DataGridViewCellLinkedListElement headElement)
		{
			this.headElement = headElement;
			this.reset = true;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0008F1F9 File Offset: 0x0008D3F9
		object IEnumerator.Current
		{
			get
			{
				return this.current.DataGridViewCell;
			}
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0008F206 File Offset: 0x0008D406
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

		// Token: 0x06001E4C RID: 7756 RVA: 0x0008F23F File Offset: 0x0008D43F
		void IEnumerator.Reset()
		{
			this.reset = true;
			this.current = null;
		}

		// Token: 0x04000CC0 RID: 3264
		private DataGridViewCellLinkedListElement headElement;

		// Token: 0x04000CC1 RID: 3265
		private DataGridViewCellLinkedListElement current;

		// Token: 0x04000CC2 RID: 3266
		private bool reset;
	}
}
