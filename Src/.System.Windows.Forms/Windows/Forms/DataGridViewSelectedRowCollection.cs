using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x02000218 RID: 536
	[ListBindable(false)]
	public class DataGridViewSelectedRowCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <returns>The index at which <paramref name="value" /> was inserted.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06002305 RID: 8965 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06002306 RID: 8966 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Determines whether the specified value is contained in the collection.</summary>
		/// <param name="value">An object to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002307 RID: 8967 RVA: 0x000A717B File Offset: 0x000A537B
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Returns the index of the specified element.</summary>
		/// <param name="value">The element to locate in the collection.</param>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		// Token: 0x06002308 RID: 8968 RVA: 0x000A7189 File Offset: 0x000A5389
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06002309 RID: 8969 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x0600230A RID: 8970 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x0600230B RID: 8971 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x00012E4E File Offset: 0x0001104E
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
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The index of the element to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of rows in the collection.</exception>
		// Token: 0x17000802 RID: 2050
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
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06002310 RID: 8976 RVA: 0x000A71A5 File Offset: 0x000A53A5
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000A71B4 File Offset: 0x000A53B4
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
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</returns>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06002314 RID: 8980 RVA: 0x000A71C1 File Offset: 0x000A53C1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000A71CE File Offset: 0x000A53CE
		internal DataGridViewSelectedRowCollection()
		{
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Windows.Forms.BaseCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns <see langword="null" /> unless overridden in a derived class.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000A71E1 File Offset: 0x000A53E1
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the row at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> in the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the current index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of rows in the collection.</exception>
		// Token: 0x17000807 RID: 2055
		public DataGridViewRow this[int index]
		{
			get
			{
				return (DataGridViewRow)this.items[index];
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000A71FC File Offset: 0x000A53FC
		internal int Add(DataGridViewRow dataGridViewRow)
		{
			return this.items.Add(dataGridViewRow);
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06002319 RID: 8985 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Determines whether the specified row is contained in the collection.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="dataGridViewRow" /> is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600231A RID: 8986 RVA: 0x000A720A File Offset: 0x000A540A
		public bool Contains(DataGridViewRow dataGridViewRow)
		{
			return this.items.IndexOf(dataGridViewRow) != -1;
		}

		/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x0600231B RID: 8987 RVA: 0x000A71A5 File Offset: 0x000A53A5
		public void CopyTo(DataGridViewRow[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Inserts a row into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="dataGridViewRow" /> should be inserted.</param>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x0600231C RID: 8988 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Insert(int index, DataGridViewRow dataGridViewRow)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x04000E5E RID: 3678
		private ArrayList items = new ArrayList();
	}
}
