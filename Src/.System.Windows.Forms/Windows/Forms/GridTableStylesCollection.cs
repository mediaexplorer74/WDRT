using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	// Token: 0x02000189 RID: 393
	[ListBindable(false)]
	public class GridTableStylesCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to this collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to add to the collection.</param>
		/// <returns>The index of the newly added object.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> has already been assigned to a <see cref="T:System.Windows.Forms.GridTableStylesCollection" />.  
		/// -or-  
		/// A <see cref="T:System.Windows.Forms.DataGridTableStyle" /> in <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> has the same <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> property value as <paramref name="value" />.</exception>
		// Token: 0x0600181C RID: 6172 RVA: 0x0005698D File Offset: 0x00054B8D
		int IList.Add(object value)
		{
			return this.Add((DataGridTableStyle)value);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x0600181D RID: 6173 RVA: 0x0005699B File Offset: 0x00054B9B
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether an element is in the collection.</summary>
		/// <param name="value">The object to locate in the collection. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if value is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600181E RID: 6174 RVA: 0x000569A3 File Offset: 0x00054BA3
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified object in the collection.</summary>
		/// <param name="value">The object to locate in the collection. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
		// Token: 0x0600181F RID: 6175 RVA: 0x000569B1 File Offset: 0x00054BB1
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The object to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06001820 RID: 6176 RVA: 0x0000A337 File Offset: 0x00008537
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</exception>
		// Token: 0x06001821 RID: 6177 RVA: 0x000569BF File Offset: 0x00054BBF
		void IList.Remove(object value)
		{
			this.Remove((DataGridTableStyle)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove.</param>
		// Token: 0x06001822 RID: 6178 RVA: 0x000569CD File Offset: 0x00054BCD
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The item property cannot be set.</exception>
		// Token: 0x17000570 RID: 1392
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Copies the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> is greater than the available space from index to the end of the destination array.</exception>
		/// <exception cref="T:System.InvalidCastException">The type in the collection cannot be cast automatically to the type of the destination array.</exception>
		// Token: 0x06001827 RID: 6183 RVA: 0x000569E4 File Offset: 0x00054BE4
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items contained in the collection.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000569F3 File Offset: 0x00054BF3
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize access to the collection.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x0600182B RID: 6187 RVA: 0x00056A00 File Offset: 0x00054C00
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00056A0D File Offset: 0x00054C0D
		internal GridTableStylesCollection(DataGrid grid)
		{
			this.owner = grid;
		}

		/// <summary>Gets the underlying list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the table data.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00056A27 File Offset: 0x00054C27
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> specified by index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">No item exists at the specified index.</exception>
		// Token: 0x17000575 RID: 1397
		public DataGridTableStyle this[int index]
		{
			get
			{
				return (DataGridTableStyle)this.items[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> with the specified name.</summary>
		/// <param name="tableName">The <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to retrieve.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> with the specified <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" />.</returns>
		// Token: 0x17000576 RID: 1398
		public DataGridTableStyle this[string tableName]
		{
			get
			{
				if (tableName == null)
				{
					throw new ArgumentNullException("tableName");
				}
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
					if (string.Equals(dataGridTableStyle.MappingName, tableName, StringComparison.OrdinalIgnoreCase))
					{
						return dataGridTableStyle;
					}
				}
				return null;
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00056A9C File Offset: 0x00054C9C
		internal void CheckForMappingNameDuplicates(DataGridTableStyle table)
		{
			if (string.IsNullOrEmpty(table.MappingName))
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridTableStyle)this.items[i]).MappingName.Equals(table.MappingName) && table != this.items[i])
				{
					throw new ArgumentException(SR.GetString("DataGridTableStyleDuplicateMappingName"), "table");
				}
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to this collection.</summary>
		/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to add to the collection.</param>
		/// <returns>The index of the newly added object.</returns>
		// Token: 0x06001831 RID: 6193 RVA: 0x00056B14 File Offset: 0x00054D14
		public virtual int Add(DataGridTableStyle table)
		{
			if (this.owner != null && this.owner.MinimumRowHeaderWidth() > table.RowHeaderWidth)
			{
				table.RowHeaderWidth = this.owner.MinimumRowHeaderWidth();
			}
			if (table.DataGrid != this.owner && table.DataGrid != null)
			{
				throw new ArgumentException(SR.GetString("DataGridTableStyleCollectionAddedParentedTableStyle"), "table");
			}
			table.DataGrid = this.owner;
			this.CheckForMappingNameDuplicates(table);
			table.MappingNameChanged += this.TableStyleMappingNameChanged;
			int num = this.items.Add(table);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			return num;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00056BB8 File Offset: 0x00054DB8
		private void TableStyleMappingNameChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Adds an array of table styles to the collection.</summary>
		/// <param name="tables">An array of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects.</param>
		// Token: 0x06001833 RID: 6195 RVA: 0x00056BC8 File Offset: 0x00054DC8
		public virtual void AddRange(DataGridTableStyle[] tables)
		{
			if (tables == null)
			{
				throw new ArgumentNullException("tables");
			}
			foreach (DataGridTableStyle dataGridTableStyle in tables)
			{
				dataGridTableStyle.DataGrid = this.owner;
				dataGridTableStyle.MappingNameChanged += this.TableStyleMappingNameChanged;
				this.items.Add(dataGridTableStyle);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Occurs when the collection has changed.</summary>
		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06001834 RID: 6196 RVA: 0x00056C2F File Offset: 0x00054E2F
		// (remove) Token: 0x06001835 RID: 6197 RVA: 0x00056C48 File Offset: 0x00054E48
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x06001836 RID: 6198 RVA: 0x00056C64 File Offset: 0x00054E64
		public void Clear()
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
				dataGridTableStyle.MappingNameChanged -= this.TableStyleMappingNameChanged;
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> contains the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified table style exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001837 RID: 6199 RVA: 0x00056CC4 File Offset: 0x00054EC4
		public bool Contains(DataGridTableStyle table)
		{
			int num = this.items.IndexOf(table);
			return num != -1;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> contains the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> specified by name.</summary>
		/// <param name="name">The <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified table style exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001838 RID: 6200 RVA: 0x00056CE8 File Offset: 0x00054EE8
		public bool Contains(string name)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
				if (string.Compare(dataGridTableStyle.MappingName, name, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.GridTableStylesCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> containing the event data.</param>
		// Token: 0x06001839 RID: 6201 RVA: 0x00056D38 File Offset: 0x00054F38
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
			DataGrid dataGrid = this.owner;
			if (dataGrid != null)
			{
				dataGrid.checkHierarchy = true;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove.</param>
		// Token: 0x0600183A RID: 6202 RVA: 0x00056D6C File Offset: 0x00054F6C
		public void Remove(DataGridTableStyle table)
		{
			int num = -1;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == table)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("DataGridTableCollectionMissingTable"), "table");
			}
			this.RemoveAt(num);
		}

		/// <summary>Removes a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove.</param>
		// Token: 0x0600183B RID: 6203 RVA: 0x00056DC8 File Offset: 0x00054FC8
		public void RemoveAt(int index)
		{
			DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[index];
			dataGridTableStyle.MappingNameChanged -= this.TableStyleMappingNameChanged;
			this.items.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridTableStyle));
		}

		// Token: 0x04000AC0 RID: 2752
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000AC1 RID: 2753
		private ArrayList items = new ArrayList();

		// Token: 0x04000AC2 RID: 2754
		private DataGrid owner;
	}
}
