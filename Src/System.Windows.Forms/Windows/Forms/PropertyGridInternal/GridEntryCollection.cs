using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000504 RID: 1284
	internal class GridEntryCollection : GridItemCollection
	{
		// Token: 0x0600547A RID: 21626 RVA: 0x00161868 File Offset: 0x0015FA68
		public GridEntryCollection(GridEntry owner, GridEntry[] entries)
			: base(entries)
		{
			this.owner = owner;
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x00161888 File Offset: 0x0015FA88
		public void AddRange(GridEntry[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			GridItem[] array2;
			if (this.entries != null)
			{
				GridEntry[] array = new GridEntry[this.entries.Length + value.Length];
				this.entries.CopyTo(array, 0);
				value.CopyTo(array, this.entries.Length);
				array2 = array;
				this.entries = array2;
				return;
			}
			array2 = (GridEntry[])value.Clone();
			this.entries = array2;
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x001618F8 File Offset: 0x0015FAF8
		public void Clear()
		{
			GridItem[] array = new GridEntry[0];
			this.entries = array;
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x00161913 File Offset: 0x0015FB13
		public void CopyTo(Array dest, int index)
		{
			this.entries.CopyTo(dest, index);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00161922 File Offset: 0x0015FB22
		internal GridEntry GetEntry(int index)
		{
			return (GridEntry)this.entries[index];
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x00161931 File Offset: 0x0015FB31
		internal int GetEntry(GridEntry child)
		{
			return Array.IndexOf<GridItem>(this.entries, child);
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0016193F File Offset: 0x0015FB3F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x00161950 File Offset: 0x0015FB50
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.owner != null && this.entries != null)
			{
				for (int i = 0; i < this.entries.Length; i++)
				{
					if (this.entries[i] != null)
					{
						((GridEntry)this.entries[i]).Dispose();
						this.entries[i] = null;
					}
				}
				GridItem[] array = new GridEntry[0];
				this.entries = array;
			}
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x001619B8 File Offset: 0x0015FBB8
		~GridEntryCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x040036FE RID: 14078
		private GridEntry owner;
	}
}
