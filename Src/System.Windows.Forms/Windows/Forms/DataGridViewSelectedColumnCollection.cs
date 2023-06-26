using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x02000217 RID: 535
	[ListBindable(false)]
	public class DataGridViewSelectedColumnCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <returns>Not applicable. Always throws an exception.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022ED RID: 8941 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022EE RID: 8942 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Determines whether the specified value is contained in the collection.</summary>
		/// <param name="value">An object to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060022EF RID: 8943 RVA: 0x000A70D8 File Offset: 0x000A52D8
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Returns the index of the specified element.</summary>
		/// <param name="value">The element to locate in the collection.</param>
		/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
		// Token: 0x060022F0 RID: 8944 RVA: 0x000A70E6 File Offset: 0x000A52E6
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022F1 RID: 8945 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022F2 RID: 8946 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060022F3 RID: 8947 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x00012E4E File Offset: 0x0001104E
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
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The index of the element to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of columns in the collection.</exception>
		// Token: 0x170007FA RID: 2042
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
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x060022F8 RID: 8952 RVA: 0x000A7102 File Offset: 0x000A5302
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x000A7111 File Offset: 0x000A5311
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
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />.</returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060022FC RID: 8956 RVA: 0x000A711E File Offset: 0x000A531E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000A712B File Offset: 0x000A532B
		internal DataGridViewSelectedColumnCollection()
		{
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Windows.Forms.BaseCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns <see langword="null" /> unless overridden in a derived class.</returns>
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000A713E File Offset: 0x000A533E
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the column at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of columns in the collection.</exception>
		// Token: 0x170007FF RID: 2047
		public DataGridViewColumn this[int index]
		{
			get
			{
				return (DataGridViewColumn)this.items[index];
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000A7159 File Offset: 0x000A5359
		internal int Add(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.Add(dataGridViewColumn);
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06002301 RID: 8961 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		/// <summary>Determines whether the specified column is contained in the collection.</summary>
		/// <param name="dataGridViewColumn">A <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dataGridViewColumn" /> parameter is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002302 RID: 8962 RVA: 0x000A7167 File Offset: 0x000A5367
		public bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.IndexOf(dataGridViewColumn) != -1;
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
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06002303 RID: 8963 RVA: 0x000A7102 File Offset: 0x000A5302
		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Inserts a column into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which the column should be inserted.</param>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06002304 RID: 8964 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Insert(int index, DataGridViewColumn dataGridViewColumn)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x04000E5D RID: 3677
		private ArrayList items = new ArrayList();
	}
}
