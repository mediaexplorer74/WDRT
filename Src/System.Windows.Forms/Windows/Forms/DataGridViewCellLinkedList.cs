using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020001AA RID: 426
	internal class DataGridViewCellLinkedList : IEnumerable
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x0008EF8A File Offset: 0x0008D18A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DataGridViewCellLinkedListEnumerator(this.headElement);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0008EF97 File Offset: 0x0008D197
		public DataGridViewCellLinkedList()
		{
			this.lastAccessedIndex = -1;
		}

		// Token: 0x17000694 RID: 1684
		public DataGridViewCell this[int index]
		{
			get
			{
				if (this.lastAccessedIndex == -1 || index < this.lastAccessedIndex)
				{
					DataGridViewCellLinkedListElement next = this.headElement;
					for (int i = index; i > 0; i--)
					{
						next = next.Next;
					}
					this.lastAccessedElement = next;
					this.lastAccessedIndex = index;
					return next.DataGridViewCell;
				}
				while (this.lastAccessedIndex < index)
				{
					this.lastAccessedElement = this.lastAccessedElement.Next;
					this.lastAccessedIndex++;
				}
				return this.lastAccessedElement.DataGridViewCell;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0008F029 File Offset: 0x0008D229
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0008F031 File Offset: 0x0008D231
		public DataGridViewCell HeadCell
		{
			get
			{
				return this.headElement.DataGridViewCell;
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0008F040 File Offset: 0x0008D240
		public void Add(DataGridViewCell dataGridViewCell)
		{
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement = new DataGridViewCellLinkedListElement(dataGridViewCell);
			if (this.headElement != null)
			{
				dataGridViewCellLinkedListElement.Next = this.headElement;
			}
			this.headElement = dataGridViewCellLinkedListElement;
			this.count++;
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0008F08B File Offset: 0x0008D28B
		public void Clear()
		{
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
			this.headElement = null;
			this.count = 0;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0008F0AC File Offset: 0x0008D2AC
		public bool Contains(DataGridViewCell dataGridViewCell)
		{
			int num = 0;
			DataGridViewCellLinkedListElement next = this.headElement;
			while (next != null)
			{
				if (next.DataGridViewCell == dataGridViewCell)
				{
					this.lastAccessedElement = next;
					this.lastAccessedIndex = num;
					return true;
				}
				next = next.Next;
				num++;
			}
			return false;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0008F0EC File Offset: 0x0008D2EC
		public bool Remove(DataGridViewCell dataGridViewCell)
		{
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement = null;
			DataGridViewCellLinkedListElement next = this.headElement;
			while (next != null && next.DataGridViewCell != dataGridViewCell)
			{
				dataGridViewCellLinkedListElement = next;
				next = next.Next;
			}
			if (next.DataGridViewCell == dataGridViewCell)
			{
				DataGridViewCellLinkedListElement next2 = next.Next;
				if (dataGridViewCellLinkedListElement == null)
				{
					this.headElement = next2;
				}
				else
				{
					dataGridViewCellLinkedListElement.Next = next2;
				}
				this.count--;
				this.lastAccessedElement = null;
				this.lastAccessedIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0008F15C File Offset: 0x0008D35C
		public int RemoveAllCellsAtBand(bool column, int bandIndex)
		{
			int num = 0;
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement = null;
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement2 = this.headElement;
			while (dataGridViewCellLinkedListElement2 != null)
			{
				if ((column && dataGridViewCellLinkedListElement2.DataGridViewCell.ColumnIndex == bandIndex) || (!column && dataGridViewCellLinkedListElement2.DataGridViewCell.RowIndex == bandIndex))
				{
					DataGridViewCellLinkedListElement next = dataGridViewCellLinkedListElement2.Next;
					if (dataGridViewCellLinkedListElement == null)
					{
						this.headElement = next;
					}
					else
					{
						dataGridViewCellLinkedListElement.Next = next;
					}
					dataGridViewCellLinkedListElement2 = next;
					this.count--;
					this.lastAccessedElement = null;
					this.lastAccessedIndex = -1;
					num++;
				}
				else
				{
					dataGridViewCellLinkedListElement = dataGridViewCellLinkedListElement2;
					dataGridViewCellLinkedListElement2 = dataGridViewCellLinkedListElement2.Next;
				}
			}
			return num;
		}

		// Token: 0x04000CBC RID: 3260
		private DataGridViewCellLinkedListElement lastAccessedElement;

		// Token: 0x04000CBD RID: 3261
		private DataGridViewCellLinkedListElement headElement;

		// Token: 0x04000CBE RID: 3262
		private int count;

		// Token: 0x04000CBF RID: 3263
		private int lastAccessedIndex;
	}
}
