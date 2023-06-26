using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000200 RID: 512
	internal class DataGridViewIntLinkedList : IEnumerable
	{
		// Token: 0x06002169 RID: 8553 RVA: 0x0009DBAB File Offset: 0x0009BDAB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DataGridViewIntLinkedListEnumerator(this.headElement);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0009DBB8 File Offset: 0x0009BDB8
		public DataGridViewIntLinkedList()
		{
			this.lastAccessedIndex = -1;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0009DBC8 File Offset: 0x0009BDC8
		public DataGridViewIntLinkedList(DataGridViewIntLinkedList source)
		{
			int num = source.Count;
			for (int i = 0; i < num; i++)
			{
				this.Add(source[i]);
			}
		}

		// Token: 0x17000782 RID: 1922
		public int this[int index]
		{
			get
			{
				if (this.lastAccessedIndex == -1 || index < this.lastAccessedIndex)
				{
					DataGridViewIntLinkedListElement next = this.headElement;
					for (int i = index; i > 0; i--)
					{
						next = next.Next;
					}
					this.lastAccessedElement = next;
					this.lastAccessedIndex = index;
					return next.Int;
				}
				while (this.lastAccessedIndex < index)
				{
					this.lastAccessedElement = this.lastAccessedElement.Next;
					this.lastAccessedIndex++;
				}
				return this.lastAccessedElement.Int;
			}
			set
			{
				if (index != this.lastAccessedIndex)
				{
					int num = this[index];
				}
				this.lastAccessedElement.Int = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0009DCAA File Offset: 0x0009BEAA
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x0009DCB2 File Offset: 0x0009BEB2
		public int HeadInt
		{
			get
			{
				return this.headElement.Int;
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0009DCC0 File Offset: 0x0009BEC0
		public void Add(int integer)
		{
			DataGridViewIntLinkedListElement dataGridViewIntLinkedListElement = new DataGridViewIntLinkedListElement(integer);
			if (this.headElement != null)
			{
				dataGridViewIntLinkedListElement.Next = this.headElement;
			}
			this.headElement = dataGridViewIntLinkedListElement;
			this.count++;
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0009DD0B File Offset: 0x0009BF0B
		public void Clear()
		{
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
			this.headElement = null;
			this.count = 0;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0009DD2C File Offset: 0x0009BF2C
		public bool Contains(int integer)
		{
			int num = 0;
			DataGridViewIntLinkedListElement next = this.headElement;
			while (next != null)
			{
				if (next.Int == integer)
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

		// Token: 0x06002173 RID: 8563 RVA: 0x0009DD6C File Offset: 0x0009BF6C
		public int IndexOf(int integer)
		{
			if (this.Contains(integer))
			{
				return this.lastAccessedIndex;
			}
			return -1;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0009DD80 File Offset: 0x0009BF80
		public bool Remove(int integer)
		{
			DataGridViewIntLinkedListElement dataGridViewIntLinkedListElement = null;
			DataGridViewIntLinkedListElement next = this.headElement;
			while (next != null && next.Int != integer)
			{
				dataGridViewIntLinkedListElement = next;
				next = next.Next;
			}
			if (next.Int == integer)
			{
				DataGridViewIntLinkedListElement next2 = next.Next;
				if (dataGridViewIntLinkedListElement == null)
				{
					this.headElement = next2;
				}
				else
				{
					dataGridViewIntLinkedListElement.Next = next2;
				}
				this.count--;
				this.lastAccessedElement = null;
				this.lastAccessedIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0009DDF0 File Offset: 0x0009BFF0
		public void RemoveAt(int index)
		{
			DataGridViewIntLinkedListElement dataGridViewIntLinkedListElement = null;
			DataGridViewIntLinkedListElement next = this.headElement;
			while (index > 0)
			{
				dataGridViewIntLinkedListElement = next;
				next = next.Next;
				index--;
			}
			DataGridViewIntLinkedListElement next2 = next.Next;
			if (dataGridViewIntLinkedListElement == null)
			{
				this.headElement = next2;
			}
			else
			{
				dataGridViewIntLinkedListElement.Next = next2;
			}
			this.count--;
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
		}

		// Token: 0x04000DEC RID: 3564
		private DataGridViewIntLinkedListElement lastAccessedElement;

		// Token: 0x04000DED RID: 3565
		private DataGridViewIntLinkedListElement headElement;

		// Token: 0x04000DEE RID: 3566
		private int count;

		// Token: 0x04000DEF RID: 3567
		private int lastAccessedIndex;
	}
}
