using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	// Token: 0x02000180 RID: 384
	[Editor("System.Windows.Forms.Design.DataGridColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ListBindable(false)]
	public class GridColumnStylesCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Adds an object to the collection.</summary>
		/// <param name="value">The object to be added to the collection. The value can be <see langword="null" />.</param>
		/// <returns>The index at which the value has been added.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</exception>
		// Token: 0x060016A6 RID: 5798 RVA: 0x00050FAB File Offset: 0x0004F1AB
		int IList.Add(object value)
		{
			return this.Add((DataGridColumnStyle)value);
		}

		/// <summary>Clears the collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects.</summary>
		// Token: 0x060016A7 RID: 5799 RVA: 0x00050FB9 File Offset: 0x0004F1B9
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether an element is in the collection.</summary>
		/// <param name="value">The object to locate in the collection. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the element is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060016A8 RID: 5800 RVA: 0x00050FC1 File Offset: 0x0004F1C1
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified object in the collection.</summary>
		/// <param name="value">The object to locate in the collection. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter within the collection, if found; otherwise, -1.</returns>
		// Token: 0x060016A9 RID: 5801 RVA: 0x00050FCF File Offset: 0x0004F1CF
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>This method is not supported by this control.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x060016AA RID: 5802 RVA: 0x0000A337 File Offset: 0x00008537
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</exception>
		// Token: 0x060016AB RID: 5803 RVA: 0x00050FDD File Offset: 0x0004F1DD
		void IList.Remove(object value)
		{
			this.Remove((DataGridColumnStyle)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> at the specified index from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove.</param>
		// Token: 0x060016AC RID: 5804 RVA: 0x00050FEB File Offset: 0x0004F1EB
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0001180C File Offset: 0x0000FA0C
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
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">An operation attempts to set this property.</exception>
		// Token: 0x17000521 RID: 1313
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
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x060016B1 RID: 5809 RVA: 0x00051002 File Offset: 0x0004F202
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00051011 File Offset: 0x0004F211
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <returns>The object used to synchronize access to the collection.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x060016B5 RID: 5813 RVA: 0x0005101E File Offset: 0x0004F21E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0005102B File Offset: 0x0004F22B
		internal GridColumnStylesCollection(DataGridTableStyle table)
		{
			this.owner = table;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00051045 File Offset: 0x0004F245
		internal GridColumnStylesCollection(DataGridTableStyle table, bool isDefault)
			: this(table)
		{
			this.isDefault = isDefault;
		}

		/// <summary>Gets the list of items in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the collection items.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00051055 File Offset: 0x0004F255
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to return.</param>
		/// <returns>The specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x17000526 RID: 1318
		public DataGridColumnStyle this[int index]
		{
			get
			{
				return (DataGridColumnStyle)this.items[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified name.</summary>
		/// <param name="columnName">The <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to retrieve.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified column header.</returns>
		// Token: 0x17000527 RID: 1319
		public DataGridColumnStyle this[string columnName]
		{
			get
			{
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
					if (string.Equals(dataGridColumnStyle.MappingName, columnName, StringComparison.OrdinalIgnoreCase))
					{
						return dataGridColumnStyle;
					}
				}
				return null;
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000510BC File Offset: 0x0004F2BC
		internal DataGridColumnStyle MapColumnStyleToPropertyName(string mappingName)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
				if (string.Equals(dataGridColumnStyle.MappingName, mappingName, StringComparison.OrdinalIgnoreCase))
				{
					return dataGridColumnStyle;
				}
			}
			return null;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="propertyDesciptor">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		// Token: 0x17000528 RID: 1320
		public DataGridColumnStyle this[PropertyDescriptor propertyDesciptor]
		{
			get
			{
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
					if (propertyDesciptor.Equals(dataGridColumnStyle.PropertyDescriptor))
					{
						return dataGridColumnStyle;
					}
				}
				return null;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00051150 File Offset: 0x0004F350
		internal DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00051158 File Offset: 0x0004F358
		internal void CheckForMappingNameDuplicates(DataGridColumnStyle column)
		{
			if (string.IsNullOrEmpty(column.MappingName))
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridColumnStyle)this.items[i]).MappingName.Equals(column.MappingName) && column != this.items[i])
				{
					throw new ArgumentException(SR.GetString("DataGridColumnStyleDuplicateMappingName"), "column");
				}
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000511D0 File Offset: 0x0004F3D0
		private void ColumnStyleMappingNameChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000511DF File Offset: 0x0004F3DF
		private void ColumnStylePropDescChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, (DataGridColumnStyle)sender));
		}

		/// <summary>Adds a column style to the collection.</summary>
		/// <param name="column">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to add.</param>
		/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x060016C1 RID: 5825 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public virtual int Add(DataGridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException(SR.GetString("DataGridDefaultColumnCollectionChanged"));
			}
			this.CheckForMappingNameDuplicates(column);
			column.SetDataGridTableInColumn(this.owner, true);
			column.MappingNameChanged += this.ColumnStyleMappingNameChanged;
			column.PropertyDescriptorChanged += this.ColumnStylePropDescChanged;
			if (this.DataGridTableStyle != null && column.Width == -1)
			{
				column.width = this.DataGridTableStyle.PreferredColumnWidth;
			}
			int num = this.items.Add(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
			return num;
		}

		/// <summary>Adds an array of column style objects to the collection.</summary>
		/// <param name="columns">An array of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects to add to the collection.</param>
		// Token: 0x060016C2 RID: 5826 RVA: 0x00051290 File Offset: 0x0004F490
		public void AddRange(DataGridColumnStyle[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			for (int i = 0; i < columns.Length; i++)
			{
				this.Add(columns[i]);
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000512C3 File Offset: 0x0004F4C3
		internal void AddDefaultColumn(DataGridColumnStyle column)
		{
			column.SetDataGridTableInColumn(this.owner, true);
			this.items.Add(column);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000512E0 File Offset: 0x0004F4E0
		internal void ResetDefaultColumnCollection()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ReleaseHostedControl();
			}
			this.items.Clear();
		}

		/// <summary>Occurs when a change is made to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		// Token: 0x140000EF RID: 239
		// (add) Token: 0x060016C5 RID: 5829 RVA: 0x00051315 File Offset: 0x0004F515
		// (remove) Token: 0x060016C6 RID: 5830 RVA: 0x0005132E File Offset: 0x0004F52E
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

		/// <summary>Clears the collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects.</summary>
		// Token: 0x060016C7 RID: 5831 RVA: 0x00051348 File Offset: 0x0004F548
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ReleaseHostedControl();
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="propertyDescriptor">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060016C8 RID: 5832 RVA: 0x0005138A File Offset: 0x0004F58A
		public bool Contains(PropertyDescriptor propertyDescriptor)
		{
			return this[propertyDescriptor] != null;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <param name="column">The desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060016C9 RID: 5833 RVA: 0x00051398 File Offset: 0x0004F598
		public bool Contains(DataGridColumnStyle column)
		{
			int num = this.items.IndexOf(column);
			return num != -1;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified name.</summary>
		/// <param name="name">The <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> of the desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060016CA RID: 5834 RVA: 0x000513BC File Offset: 0x0004F5BC
		public bool Contains(string name)
		{
			foreach (object obj in this.items)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)obj;
				if (string.Compare(dataGridColumnStyle.MappingName, name, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the index of a specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <param name="element">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to find.</param>
		/// <returns>The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> within the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> or -1 if no corresponding <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> exists.</returns>
		// Token: 0x060016CB RID: 5835 RVA: 0x00051404 File Offset: 0x0004F604
		public int IndexOf(DataGridColumnStyle element)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
				if (element == dataGridColumnStyle)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.GridColumnStylesCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data event.</param>
		// Token: 0x060016CC RID: 5836 RVA: 0x00051444 File Offset: 0x0004F644
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
			DataGrid dataGrid = this.owner.DataGrid;
			if (dataGrid != null)
			{
				dataGrid.checkHierarchy = true;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="column">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove from the collection.</param>
		// Token: 0x060016CD RID: 5837 RVA: 0x0005147C File Offset: 0x0004F67C
		public void Remove(DataGridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException(SR.GetString("DataGridDefaultColumnCollectionChanged"));
			}
			int num = -1;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == column)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				throw new InvalidOperationException(SR.GetString("DataGridColumnCollectionMissing"));
			}
			this.RemoveAt(num);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified index from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove.</param>
		// Token: 0x060016CE RID: 5838 RVA: 0x000514EC File Offset: 0x0004F6EC
		public void RemoveAt(int index)
		{
			if (this.isDefault)
			{
				throw new ArgumentException(SR.GetString("DataGridDefaultColumnCollectionChanged"));
			}
			DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[index];
			dataGridColumnStyle.SetDataGridTableInColumn(null, true);
			dataGridColumnStyle.MappingNameChanged -= this.ColumnStyleMappingNameChanged;
			dataGridColumnStyle.PropertyDescriptorChanged -= this.ColumnStylePropDescChanged;
			this.items.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridColumnStyle));
		}

		/// <summary>Sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for each column style in the collection to <see langword="null" />.</summary>
		// Token: 0x060016CF RID: 5839 RVA: 0x00051568 File Offset: 0x0004F768
		public void ResetPropertyDescriptors()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].PropertyDescriptor = null;
			}
		}

		// Token: 0x04000A48 RID: 2632
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000A49 RID: 2633
		private ArrayList items = new ArrayList();

		// Token: 0x04000A4A RID: 2634
		private DataGridTableStyle owner;

		// Token: 0x04000A4B RID: 2635
		private bool isDefault;
	}
}
