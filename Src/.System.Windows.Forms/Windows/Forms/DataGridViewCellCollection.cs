using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of cells in a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	// Token: 0x020001A4 RID: 420
	[ListBindable(false)]
	public class DataGridViewCellCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Adds an item to the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to add to the collection.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  <paramref name="value" /> represents a cell that already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x06001E05 RID: 7685 RVA: 0x0008E6CE File Offset: 0x0008C8CE
		int IList.Add(object value)
		{
			return this.Add((DataGridViewCell)value);
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E06 RID: 7686 RVA: 0x0008E6DC File Offset: 0x0008C8DC
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains the specified value.</summary>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> is found in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E07 RID: 7687 RVA: 0x0008E6E4 File Offset: 0x0008C8E4
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Determines the index of a specific item in a collection.</summary>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</param>
		/// <returns>The index of value if found in the list; otherwise, -1.</returns>
		// Token: 0x06001E08 RID: 7688 RVA: 0x0008E6F2 File Offset: 0x0008C8F2
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Inserts an item into the collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to insert into the <see cref="M:System.Windows.Forms.DataGridViewCellCollection.System#Collections#IList#Insert(System.Int32,System.Object)" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  <paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x06001E09 RID: 7689 RVA: 0x0008E700 File Offset: 0x0008C900
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (DataGridViewCell)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cell" /> could not be found in the collection.</exception>
		// Token: 0x06001E0A RID: 7690 RVA: 0x0008E70F File Offset: 0x0008C90F
		void IList.Remove(object value)
		{
			this.Remove((DataGridViewCell)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</summary>
		/// <param name="index">The position at which to remove the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E0B RID: 7691 RVA: 0x0008E71D File Offset: 0x0008C91D
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The index of the item to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		// Token: 0x17000685 RID: 1669
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (DataGridViewCell)value;
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
		// Token: 0x06001E10 RID: 7696 RVA: 0x0008E73E File Offset: 0x0008C93E
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</returns>
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0008E74D File Offset: 0x0008C94D
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
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001E14 RID: 7700 RVA: 0x0008E75A File Offset: 0x0008C95A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns the collection.</param>
		// Token: 0x06001E15 RID: 7701 RVA: 0x0008E767 File Offset: 0x0008C967
		public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
		{
			this.owner = dataGridViewRow;
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> objects.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.ArrayList" />.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0008E781 File Offset: 0x0008C981
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the cell at the provided index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
		/// <param name="index">The zero-based index of the cell to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> stored at the given index.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
		// Token: 0x1700068A RID: 1674
		public DataGridViewCell this[int index]
		{
			get
			{
				return (DataGridViewCell)this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.DataGridView != null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridView"));
				}
				if (value.OwningRow != null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
				}
				if (this.owner.DataGridView != null)
				{
					this.owner.DataGridView.OnReplacingCell(this.owner, index);
				}
				DataGridViewCell dataGridViewCell = (DataGridViewCell)this.items[index];
				this.items[index] = value;
				value.OwningRowInternal = this.owner;
				value.StateInternal = dataGridViewCell.State;
				if (this.owner.DataGridView != null)
				{
					value.DataGridViewInternal = this.owner.DataGridView;
					value.OwningColumnInternal = this.owner.DataGridView.Columns[index];
					this.owner.DataGridView.OnReplacedCell(this.owner, index);
				}
				dataGridViewCell.DataGridViewInternal = null;
				dataGridViewCell.OwningRowInternal = null;
				dataGridViewCell.OwningColumnInternal = null;
				if (dataGridViewCell.ReadOnly)
				{
					dataGridViewCell.ReadOnlyInternal = false;
				}
				if (dataGridViewCell.Selected)
				{
					dataGridViewCell.SelectedInternal = false;
				}
			}
		}

		/// <summary>Gets or sets the cell in the column with the provided name. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
		/// <param name="columnName">The name of the column in which to get or set the cell.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> stored in the column with the given name.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="columnName" /> does not match the name of any columns in the control.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x1700068B RID: 1675
		public DataGridViewCell this[string columnName]
		{
			get
			{
				DataGridViewColumn dataGridViewColumn = null;
				if (this.owner.DataGridView != null)
				{
					dataGridViewColumn = this.owner.DataGridView.Columns[columnName];
				}
				if (dataGridViewColumn == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewColumnCollection_ColumnNotFound", new object[] { columnName }), "columnName");
				}
				return (DataGridViewCell)this.items[dataGridViewColumn.Index];
			}
			set
			{
				DataGridViewColumn dataGridViewColumn = null;
				if (this.owner.DataGridView != null)
				{
					dataGridViewColumn = this.owner.DataGridView.Columns[columnName];
				}
				if (dataGridViewColumn == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewColumnCollection_ColumnNotFound", new object[] { columnName }), "columnName");
				}
				this[dataGridViewColumn.Index] = value;
			}
		}

		/// <summary>Occurs when the collection is changed.</summary>
		// Token: 0x14000183 RID: 387
		// (add) Token: 0x06001E1B RID: 7707 RVA: 0x0008E996 File Offset: 0x0008CB96
		// (remove) Token: 0x06001E1C RID: 7708 RVA: 0x0008E9AF File Offset: 0x0008CBAF
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

		/// <summary>Adds a cell to the collection.</summary>
		/// <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to add to the collection.</param>
		/// <returns>The position in which to insert the new element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  <paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x06001E1D RID: 7709 RVA: 0x0008E9C8 File Offset: 0x0008CBC8
		public virtual int Add(DataGridViewCell dataGridViewCell)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			if (dataGridViewCell.OwningRow != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
			}
			return this.AddInternal(dataGridViewCell);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0008EA08 File Offset: 0x0008CC08
		internal int AddInternal(DataGridViewCell dataGridViewCell)
		{
			int num = this.items.Add(dataGridViewCell);
			dataGridViewCell.OwningRowInternal = this.owner;
			DataGridView dataGridView = this.owner.DataGridView;
			if (dataGridView != null && dataGridView.Columns.Count > num)
			{
				dataGridViewCell.OwningColumnInternal = dataGridView.Columns[num];
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
			return num;
		}

		/// <summary>Adds an array of cells to the collection.</summary>
		/// <param name="dataGridViewCells">The array of <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCells" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  At least one value in <paramref name="dataGridViewCells" /> is <see langword="null" />.  
		///  -or-  
		///  At least one cell in <paramref name="dataGridViewCells" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.  
		///  -or-  
		///  At least two values in <paramref name="dataGridViewCells" /> are references to the same <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
		// Token: 0x06001E1F RID: 7711 RVA: 0x0008EA6C File Offset: 0x0008CC6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual void AddRange(params DataGridViewCell[] dataGridViewCells)
		{
			if (dataGridViewCells == null)
			{
				throw new ArgumentNullException("dataGridViewCells");
			}
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			foreach (DataGridViewCell dataGridViewCell in dataGridViewCells)
			{
				if (dataGridViewCell == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_AtLeastOneCellIsNull"));
				}
				if (dataGridViewCell.OwningRow != null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
				}
			}
			int num = dataGridViewCells.Length;
			for (int j = 0; j < num - 1; j++)
			{
				for (int k = j + 1; k < num; k++)
				{
					if (dataGridViewCells[j] == dataGridViewCells[k])
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CannotAddIdenticalCells"));
					}
				}
			}
			this.items.AddRange(dataGridViewCells);
			foreach (DataGridViewCell dataGridViewCell2 in dataGridViewCells)
			{
				dataGridViewCell2.OwningRowInternal = this.owner;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Clears all cells from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E20 RID: 7712 RVA: 0x0008EB6C File Offset: 0x0008CD6C
		public virtual void Clear()
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			foreach (object obj in this.items)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.OwningRowInternal = null;
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Copies the entire collection of cells into an array at a specified location within the array.</summary>
		/// <param name="array">The destination array to which the contents will be copied.</param>
		/// <param name="index">The index of the element in <paramref name="array" /> at which to start copying.</param>
		// Token: 0x06001E21 RID: 7713 RVA: 0x0008E73E File Offset: 0x0008C93E
		public void CopyTo(DataGridViewCell[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Determines whether the specified cell is contained in the collection.</summary>
		/// <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="dataGridViewCell" /> is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E22 RID: 7714 RVA: 0x0008EBFC File Offset: 0x0008CDFC
		public virtual bool Contains(DataGridViewCell dataGridViewCell)
		{
			int num = this.items.IndexOf(dataGridViewCell);
			return num != -1;
		}

		/// <summary>Returns the index of the specified cell.</summary>
		/// <param name="dataGridViewCell">The cell to locate in the collection.</param>
		/// <returns>The zero-based index of the value of <paramref name="dataGridViewCell" /> parameter, if it is found in the collection; otherwise, -1.</returns>
		// Token: 0x06001E23 RID: 7715 RVA: 0x0008E6F2 File Offset: 0x0008C8F2
		public int IndexOf(DataGridViewCell dataGridViewCell)
		{
			return this.items.IndexOf(dataGridViewCell);
		}

		/// <summary>Inserts a cell into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which to place <paramref name="dataGridViewCell" />.</param>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to insert.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  <paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x06001E24 RID: 7716 RVA: 0x0008EC20 File Offset: 0x0008CE20
		public virtual void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			if (dataGridViewCell.OwningRow != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
			}
			this.items.Insert(index, dataGridViewCell);
			dataGridViewCell.OwningRowInternal = this.owner;
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0008EC88 File Offset: 0x0008CE88
		internal void InsertInternal(int index, DataGridViewCell dataGridViewCell)
		{
			this.items.Insert(index, dataGridViewCell);
			dataGridViewCell.OwningRowInternal = this.owner;
			DataGridView dataGridView = this.owner.DataGridView;
			if (dataGridView != null && dataGridView.Columns.Count > index)
			{
				dataGridViewCell.OwningColumnInternal = dataGridView.Columns[index];
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewCellCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06001E26 RID: 7718 RVA: 0x0008ECEA File Offset: 0x0008CEEA
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		/// <summary>Removes the specified cell from the collection.</summary>
		/// <param name="cell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cell" /> could not be found in the collection.</exception>
		// Token: 0x06001E27 RID: 7719 RVA: 0x0008ED04 File Offset: 0x0008CF04
		public virtual void Remove(DataGridViewCell cell)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			int num = -1;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == cell)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("DataGridViewCellCollection_CellNotFound"));
			}
			this.RemoveAt(num);
		}

		/// <summary>Removes the cell at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to be removed.</param>
		/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E28 RID: 7720 RVA: 0x0008ED76 File Offset: 0x0008CF76
		public virtual void RemoveAt(int index)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			this.RemoveAtInternal(index);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0008ED9C File Offset: 0x0008CF9C
		internal void RemoveAtInternal(int index)
		{
			DataGridViewCell dataGridViewCell = (DataGridViewCell)this.items[index];
			this.items.RemoveAt(index);
			dataGridViewCell.DataGridViewInternal = null;
			dataGridViewCell.OwningRowInternal = null;
			if (dataGridViewCell.ReadOnly)
			{
				dataGridViewCell.ReadOnlyInternal = false;
			}
			if (dataGridViewCell.Selected)
			{
				dataGridViewCell.SelectedInternal = false;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewCell));
		}

		// Token: 0x04000CB1 RID: 3249
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000CB2 RID: 3250
		private ArrayList items = new ArrayList();

		// Token: 0x04000CB3 RID: 3251
		private DataGridViewRow owner;
	}
}
