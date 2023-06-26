using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.GridItem" /> objects.</summary>
	// Token: 0x02000269 RID: 617
	public class GridItemCollection : ICollection, IEnumerable
	{
		// Token: 0x060027A1 RID: 10145 RVA: 0x000B8D39 File Offset: 0x000B6F39
		internal GridItemCollection(GridItem[] entries)
		{
			if (entries == null)
			{
				this.entries = new GridItem[0];
				return;
			}
			this.entries = entries;
		}

		/// <summary>Gets the number of grid items in the collection.</summary>
		/// <returns>The number of grid items in the collection.</returns>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000B8D58 File Offset: 0x000B6F58
		public int Count
		{
			get
			{
				return this.entries.Length;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> at the specified index.</summary>
		/// <param name="index">The index of the grid item to return.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.GridItem" /> at the specified index.</returns>
		// Token: 0x17000932 RID: 2354
		public GridItem this[int index]
		{
			get
			{
				return this.entries[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> with the matching label.</summary>
		/// <param name="label">A string value to match to a grid item label</param>
		/// <returns>The grid item whose label matches the <paramref name="label" /> parameter.</returns>
		// Token: 0x17000933 RID: 2355
		public GridItem this[string label]
		{
			get
			{
				foreach (GridItem gridItem in this.entries)
				{
					if (gridItem.Label == label)
					{
						return gridItem;
					}
				}
				return null;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x060027A7 RID: 10151 RVA: 0x000B8DA3 File Offset: 0x000B6FA3
		void ICollection.CopyTo(Array dest, int index)
		{
			if (this.entries.Length != 0)
			{
				Array.Copy(this.entries, 0, dest, index, this.entries.Length);
			}
		}

		/// <summary>Returns an enumeration of all the grid items in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
		// Token: 0x060027A8 RID: 10152 RVA: 0x000B8DC4 File Offset: 0x000B6FC4
		public IEnumerator GetEnumerator()
		{
			return this.entries.GetEnumerator();
		}

		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.GridItemCollection" /> has no entries.</summary>
		// Token: 0x0400104B RID: 4171
		public static GridItemCollection Empty = new GridItemCollection(new GridItem[0]);

		// Token: 0x0400104C RID: 4172
		internal GridItem[] entries;
	}
}
