using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of cells that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x02000216 RID: 534
	[ListBindable(false)]
	public class DataGridViewSelectedCellCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022D4 RID: 8916 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022D5 RID: 8917 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Determines whether the specified cell is contained in the collection.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060022D6 RID: 8918 RVA: 0x000A6FD6 File Offset: 0x000A51D6
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Returns the index of the specified cell.</summary>
		/// <param name="value">The cell to locate in the collection.</param>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		// Token: 0x060022D7 RID: 8919 RVA: 0x000A6FE4 File Offset: 0x000A51E4
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022D8 RID: 8920 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022D9 RID: 8921 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022DA RID: 8922 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		// Token: 0x170007F2 RID: 2034
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
			}
		}

		/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x060022DF RID: 8927 RVA: 0x000A7000 File Offset: 0x000A5200
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000A700F File Offset: 0x000A520F
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060022E3 RID: 8931 RVA: 0x000A701C File Offset: 0x000A521C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000A7029 File Offset: 0x000A5229
		internal DataGridViewSelectedCellCollection()
		{
		}

		/// <summary>Gets a list of elements in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection.</returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000A703C File Offset: 0x000A523C
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the cell at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		// Token: 0x170007F7 RID: 2039
		public DataGridViewCell this[int index]
		{
			get
			{
				return (DataGridViewCell)this.items[index];
			}
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000A7057 File Offset: 0x000A5257
		internal int Add(DataGridViewCell dataGridViewCell)
		{
			return this.items.Add(dataGridViewCell);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000A7068 File Offset: 0x000A5268
		internal void AddCellLinkedList(DataGridViewCellLinkedList dataGridViewCells)
		{
			foreach (object obj in ((IEnumerable)dataGridViewCells))
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				this.items.Add(dataGridViewCell);
			}
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022E9 RID: 8937 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Determines whether the specified cell is contained in the collection.</summary>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="dataGridViewCell" /> is in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060022EA RID: 8938 RVA: 0x000A70C4 File Offset: 0x000A52C4
		public bool Contains(DataGridViewCell dataGridViewCell)
		{
			return this.items.IndexOf(dataGridViewCell) != -1;
		}

		/// <summary>Copies the elements of the collection to the specified <see cref="T:System.Windows.Forms.DataGridViewCell" /> array, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array of type <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x060022EB RID: 8939 RVA: 0x000A7000 File Offset: 0x000A5200
		public void CopyTo(DataGridViewCell[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Inserts a cell into the collection.</summary>
		/// <param name="index">The index at which <paramref name="dataGridViewCell" /> should be inserted.</param>
		/// <param name="dataGridViewCell">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022EC RID: 8940 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x04000E5C RID: 3676
		private ArrayList items = new ArrayList();
	}
}
