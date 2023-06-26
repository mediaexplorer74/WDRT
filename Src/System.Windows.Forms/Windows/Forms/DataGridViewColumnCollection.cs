using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001BE RID: 446
	[ListBindable(false)]
	public class DataGridViewColumnCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0001180C File Offset: 0x0000FA0C
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
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the column to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When getting this property, <paramref name="index" /> is less than zero or greater than the number of columns in the collection minus one.</exception>
		// Token: 0x1700070B RID: 1803
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Adds an object to the end of the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the end of the collection. The value can be <see langword="null" />.</param>
		/// <returns>The index at which <paramref name="value" /> has been added.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The column indicated by <paramref name="value" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is <see langword="false" />.  
		///  -or-  
		///  The column indicated by <paramref name="value" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of <see langword="true" />.  
		///  -or-  
		///  The column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.  
		///  -or-  
		///  The column indicated by <paramref name="value" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and the column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of <see langword="null" />.</exception>
		// Token: 0x06001F63 RID: 8035 RVA: 0x00094447 File Offset: 0x00092647
		int IList.Add(object value)
		{
			return this.Add((DataGridViewColumn)value);
		}

		/// <summary>Removes all elements from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F64 RID: 8036 RVA: 0x00094455 File Offset: 0x00092655
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether an object is in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001F65 RID: 8037 RVA: 0x0009445D File Offset: 0x0009265D
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
		// Token: 0x06001F66 RID: 8038 RVA: 0x0009446B File Offset: 0x0009266B
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Inserts an element into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The column indicated by <paramref name="value" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is <see langword="false" />.  
		///  -or-  
		///  The column indicated by <paramref name="value" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of <see langword="true" />.  
		///  -or-  
		///  The column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.  
		///  -or-  
		///  The column indicated by <paramref name="value" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and the column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of <see langword="null" />.</exception>
		// Token: 0x06001F67 RID: 8039 RVA: 0x00094479 File Offset: 0x00092679
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (DataGridViewColumn)value);
		}

		/// <summary>Removes the first occurrence of the specified object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F68 RID: 8040 RVA: 0x00094488 File Offset: 0x00092688
		void IList.Remove(object value)
		{
			this.Remove((DataGridViewColumn)value);
		}

		/// <summary>Removes the element with the specified index from the collection.</summary>
		/// <param name="index">The location of the element to delete.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of columns in the control minus one.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F69 RID: 8041 RVA: 0x00094496 File Offset: 0x00092696
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0009449F File Offset: 0x0009269F
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the entire contents of the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source element cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06001F6D RID: 8045 RVA: 0x000944AC File Offset: 0x000926AC
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001F6E RID: 8046 RVA: 0x000944BB File Offset: 0x000926BB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> class for the given <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that created this collection.</param>
		// Token: 0x06001F6F RID: 8047 RVA: 0x000944C8 File Offset: 0x000926C8
		public DataGridViewColumnCollection(DataGridView dataGridView)
		{
			this.InvalidateCachedColumnCounts();
			this.InvalidateCachedColumnsWidths();
			this.dataGridView = dataGridView;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000944F5 File Offset: 0x000926F5
		internal static IComparer ColumnCollectionOrderComparer
		{
			get
			{
				return DataGridViewColumnCollection.columnOrderComparer;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Windows.Forms.BaseCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns <see langword="null" /> unless overridden in a derived class.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000944FC File Offset: 0x000926FC
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> upon which the collection performs column-related operations.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x00094504 File Offset: 0x00092704
		protected DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		/// <summary>Gets or sets the column at the given index in the collection.</summary>
		/// <param name="index">The zero-based index of the column to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the given index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of columns in the collection minus one.</exception>
		// Token: 0x17000712 RID: 1810
		public DataGridViewColumn this[int index]
		{
			get
			{
				return (DataGridViewColumn)this.items[index];
			}
		}

		/// <summary>Gets or sets the column of the given name in the collection.</summary>
		/// <param name="columnName">The name of the column to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> identified by the <paramref name="columnName" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="columnName" /> is <see langword="null" />.</exception>
		// Token: 0x17000713 RID: 1811
		public DataGridViewColumn this[string columnName]
		{
			get
			{
				if (columnName == null)
				{
					throw new ArgumentNullException("columnName");
				}
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[i];
					if (string.Equals(dataGridViewColumn.Name, columnName, StringComparison.OrdinalIgnoreCase))
					{
						return dataGridViewColumn;
					}
				}
				return null;
			}
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000185 RID: 389
		// (add) Token: 0x06001F75 RID: 8053 RVA: 0x00094577 File Offset: 0x00092777
		// (remove) Token: 0x06001F76 RID: 8054 RVA: 0x00094590 File Offset: 0x00092790
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

		// Token: 0x06001F77 RID: 8055 RVA: 0x000945AC File Offset: 0x000927AC
		internal int GetVisibleIndex(DataGridViewColumn column)
		{
			for (int i = 0; i < this.Count; i++)
			{
				int num = this.ActualDisplayIndexToColumnIndex(i, DataGridViewElementStates.Visible);
				if (num != -1 && this.items[num] == column)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000945EC File Offset: 0x000927EC
		internal int ActualDisplayIndexToColumnIndex(int actualDisplayIndex, DataGridViewElementStates includeFilter)
		{
			DataGridViewColumn dataGridViewColumn = this.GetFirstColumn(includeFilter);
			for (int i = 0; i < actualDisplayIndex; i++)
			{
				dataGridViewColumn = this.GetNextColumn(dataGridViewColumn, includeFilter, DataGridViewElementStates.None);
			}
			if (!AccessibilityImprovements.Level5 || dataGridViewColumn != null)
			{
				return dataGridViewColumn.Index;
			}
			return -1;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> with the given column name and column header text to the collection.</summary>
		/// <param name="columnName">The name by which the column will be referred.</param>
		/// <param name="headerText">The text for the column's header.</param>
		/// <returns>The index of the column.</returns>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />, which conflicts with the default column <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" />.  
		///  -or-  
		///  The default column <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value of 100 would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.</exception>
		// Token: 0x06001F79 RID: 8057 RVA: 0x0009462C File Offset: 0x0009282C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add(string columnName, string headerText)
		{
			return this.Add(new DataGridViewTextBoxColumn
			{
				Name = columnName,
				HeaderText = headerText
			});
		}

		/// <summary>Adds the given column to the collection.</summary>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to add.</param>
		/// <returns>The index of the column.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values.  
		///  -or-  
		///  The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is <see langword="false" />.  
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of <see langword="true" />.  
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.  
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of <see langword="null" />.</exception>
		// Token: 0x06001F7A RID: 8058 RVA: 0x00094654 File Offset: 0x00092854
		public virtual int Add(DataGridViewColumn dataGridViewColumn)
		{
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			this.DataGridView.OnAddingColumn(dataGridViewColumn);
			this.InvalidateCachedColumnsOrder();
			int num = this.items.Add(dataGridViewColumn);
			dataGridViewColumn.IndexInternal = num;
			dataGridViewColumn.DataGridViewInternal = this.dataGridView;
			this.UpdateColumnCaches(dataGridViewColumn, true);
			this.DataGridView.OnAddedColumn(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewColumn), false, new Point(-1, -1));
			return num;
		}

		/// <summary>Adds a range of columns to the collection.</summary>
		/// <param name="dataGridViewColumns">An array of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumns" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  At least one of the values in <paramref name="dataGridViewColumns" /> is <see langword="null" />.  
		///  -or-  
		///  At least one of the columns in <paramref name="dataGridViewColumns" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  At least one of the columns in <paramref name="dataGridViewColumns" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of <see langword="null" /> and the <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row.  
		///  -or-  
		///  At least one of the columns in <paramref name="dataGridViewColumns" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values.  
		///  -or-  
		///  At least one of the columns in <paramref name="dataGridViewColumns" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is <see langword="false" />.  
		///  -or-  
		///  At least one of the columns in <paramref name="dataGridViewColumns" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of <see langword="true" />.  
		///  -or-  
		///  The columns in <paramref name="dataGridViewColumns" /> have <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.  
		///  -or-  
		///  At least two of the values in <paramref name="dataGridViewColumns" /> are references to the same <see cref="T:System.Windows.Forms.DataGridViewColumn" />.  
		///  -or-  
		///  At least one of the columns in <paramref name="dataGridViewColumns" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.</exception>
		// Token: 0x06001F7B RID: 8059 RVA: 0x000946F8 File Offset: 0x000928F8
		public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
		{
			if (dataGridViewColumns == null)
			{
				throw new ArgumentNullException("dataGridViewColumns");
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			ArrayList arrayList = new ArrayList(dataGridViewColumns.Length);
			ArrayList arrayList2 = new ArrayList(dataGridViewColumns.Length);
			foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumns)
			{
				if (dataGridViewColumn.DisplayIndex != -1)
				{
					arrayList.Add(dataGridViewColumn);
				}
			}
			int j;
			while (arrayList.Count > 0)
			{
				int num = int.MaxValue;
				int num2 = -1;
				for (j = 0; j < arrayList.Count; j++)
				{
					DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)arrayList[j];
					if (dataGridViewColumn2.DisplayIndex < num)
					{
						num = dataGridViewColumn2.DisplayIndex;
						num2 = j;
					}
				}
				arrayList2.Add(arrayList[num2]);
				arrayList.RemoveAt(num2);
			}
			foreach (DataGridViewColumn dataGridViewColumn3 in dataGridViewColumns)
			{
				if (dataGridViewColumn3.DisplayIndex == -1)
				{
					arrayList2.Add(dataGridViewColumn3);
				}
			}
			j = 0;
			foreach (object obj in arrayList2)
			{
				DataGridViewColumn dataGridViewColumn4 = (DataGridViewColumn)obj;
				dataGridViewColumns[j] = dataGridViewColumn4;
				j++;
			}
			this.DataGridView.OnAddingColumns(dataGridViewColumns);
			foreach (DataGridViewColumn dataGridViewColumn5 in dataGridViewColumns)
			{
				this.InvalidateCachedColumnsOrder();
				j = this.items.Add(dataGridViewColumn5);
				dataGridViewColumn5.IndexInternal = j;
				dataGridViewColumn5.DataGridViewInternal = this.dataGridView;
				this.UpdateColumnCaches(dataGridViewColumn5, true);
				this.DataGridView.OnAddedColumn(dataGridViewColumn5);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), false, new Point(-1, -1));
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F7C RID: 8060 RVA: 0x000948F8 File Offset: 0x00092AF8
		public virtual void Clear()
		{
			if (this.Count > 0)
			{
				if (this.DataGridView.NoDimensionChangeAllowed)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
				}
				if (this.DataGridView.InDisplayIndexAdjustments)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
				}
				for (int i = 0; i < this.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = this[i];
					dataGridViewColumn.DataGridViewInternal = null;
					if (dataGridViewColumn.HasHeaderCell)
					{
						dataGridViewColumn.HeaderCell.DataGridViewInternal = null;
					}
				}
				DataGridViewColumn[] array = new DataGridViewColumn[this.items.Count];
				this.CopyTo(array, 0);
				this.DataGridView.OnClearingColumns();
				this.InvalidateCachedColumnsOrder();
				this.items.Clear();
				this.InvalidateCachedColumnCounts();
				this.InvalidateCachedColumnsWidths();
				foreach (DataGridViewColumn dataGridViewColumn2 in array)
				{
					this.DataGridView.OnColumnRemoved(dataGridViewColumn2);
					this.DataGridView.OnColumnHidden(dataGridViewColumn2);
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), false, new Point(-1, -1));
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00094A0C File Offset: 0x00092C0C
		internal int ColumnIndexToActualDisplayIndex(int columnIndex, DataGridViewElementStates includeFilter)
		{
			DataGridViewColumn dataGridViewColumn = this.GetFirstColumn(includeFilter);
			int num = 0;
			while (dataGridViewColumn != null && dataGridViewColumn.Index != columnIndex)
			{
				dataGridViewColumn = this.GetNextColumn(dataGridViewColumn, includeFilter, DataGridViewElementStates.None);
				num++;
			}
			return num;
		}

		/// <summary>Determines whether the collection contains the given column.</summary>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the given column is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F7E RID: 8062 RVA: 0x00094A40 File Offset: 0x00092C40
		public virtual bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.IndexOf(dataGridViewColumn) != -1;
		}

		/// <summary>Determines whether the collection contains the column referred to by the given name.</summary>
		/// <param name="columnName">The name of the column to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the column is contained in the collection; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="columnName" /> is <see langword="null" />.</exception>
		// Token: 0x06001F7F RID: 8063 RVA: 0x00094A54 File Offset: 0x00092C54
		public virtual bool Contains(string columnName)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[i];
				if (string.Compare(dataGridViewColumn.Name, columnName, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the items from the collection to the given array.</summary>
		/// <param name="array">The destination <see cref="T:System.Windows.Forms.DataGridViewColumn" /> array.</param>
		/// <param name="index">The index of the destination array at which to start copying.</param>
		// Token: 0x06001F80 RID: 8064 RVA: 0x000944AC File Offset: 0x000926AC
		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00094AB0 File Offset: 0x00092CB0
		internal bool DisplayInOrder(int columnIndex1, int columnIndex2)
		{
			int displayIndex = ((DataGridViewColumn)this.items[columnIndex1]).DisplayIndex;
			int displayIndex2 = ((DataGridViewColumn)this.items[columnIndex2]).DisplayIndex;
			return displayIndex < displayIndex2;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00094AF0 File Offset: 0x00092CF0
		internal DataGridViewColumn GetColumnAtDisplayIndex(int displayIndex)
		{
			if (displayIndex < 0 || displayIndex >= this.items.Count)
			{
				return null;
			}
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[displayIndex];
			if (dataGridViewColumn.DisplayIndex == displayIndex)
			{
				return dataGridViewColumn;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				dataGridViewColumn = (DataGridViewColumn)this.items[i];
				if (dataGridViewColumn.DisplayIndex == displayIndex)
				{
					return dataGridViewColumn;
				}
			}
			return null;
		}

		/// <summary>Returns the number of columns that meet the given filter requirements.</summary>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter for inclusion.</param>
		/// <returns>The number of columns that meet the filter requirements.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F83 RID: 8067 RVA: 0x00094B64 File Offset: 0x00092D64
		public int GetColumnCount(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
				{
					if (this.columnCountsVisibleSelected != -1)
					{
						return this.columnCountsVisibleSelected;
					}
				}
			}
			else if (this.columnCountsVisible != -1)
			{
				return this.columnCountsVisible;
			}
			int num = 0;
			if ((includeFilter & DataGridViewElementStates.Resizable) == DataGridViewElementStates.None)
			{
				for (int i = 0; i < this.items.Count; i++)
				{
					if (((DataGridViewColumn)this.items[i]).StateIncludes(includeFilter))
					{
						num++;
					}
				}
				if (includeFilter != DataGridViewElementStates.Visible)
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						this.columnCountsVisibleSelected = num;
					}
				}
				else
				{
					this.columnCountsVisible = num;
				}
			}
			else
			{
				DataGridViewElementStates dataGridViewElementStates = includeFilter & ~DataGridViewElementStates.Resizable;
				for (int j = 0; j < this.items.Count; j++)
				{
					if (((DataGridViewColumn)this.items[j]).StateIncludes(dataGridViewElementStates) && ((DataGridViewColumn)this.items[j]).Resizable == DataGridViewTriState.True)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00094C6C File Offset: 0x00092E6C
		internal int GetColumnCount(DataGridViewElementStates includeFilter, int fromColumnIndex, int toColumnIndex)
		{
			int num = 0;
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[fromColumnIndex];
			while (dataGridViewColumn != (DataGridViewColumn)this.items[toColumnIndex])
			{
				dataGridViewColumn = this.GetNextColumn(dataGridViewColumn, includeFilter, DataGridViewElementStates.None);
				if (dataGridViewColumn.StateIncludes(includeFilter))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00094CBC File Offset: 0x00092EBC
		private int GetColumnSortedIndex(DataGridViewColumn dataGridViewColumn)
		{
			if (this.lastAccessedSortedIndex != -1 && this.itemsSorted[this.lastAccessedSortedIndex] == dataGridViewColumn)
			{
				return this.lastAccessedSortedIndex;
			}
			for (int i = 0; i < this.itemsSorted.Count; i++)
			{
				if (dataGridViewColumn.Index == ((DataGridViewColumn)this.itemsSorted[i]).Index)
				{
					this.lastAccessedSortedIndex = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00094D2C File Offset: 0x00092F2C
		internal float GetColumnsFillWeight(DataGridViewElementStates includeFilter)
		{
			float num = 0f;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridViewColumn)this.items[i]).StateIncludes(includeFilter))
				{
					num += ((DataGridViewColumn)this.items[i]).FillWeight;
				}
			}
			return num;
		}

		/// <summary>Returns the width, in pixels, required to display all of the columns that meet the given filter requirements.</summary>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter for inclusion.</param>
		/// <returns>The width, in pixels, that is necessary to display all of the columns that meet the filter requirements.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F87 RID: 8071 RVA: 0x00094D88 File Offset: 0x00092F88
		public int GetColumnsWidth(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (this.columnsWidthVisibleFrozen != -1)
					{
						return this.columnsWidthVisibleFrozen;
					}
				}
			}
			else if (this.columnsWidthVisible != -1)
			{
				return this.columnsWidthVisible;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridViewColumn)this.items[i]).StateIncludes(includeFilter))
				{
					num += ((DataGridViewColumn)this.items[i]).Thickness;
				}
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					this.columnsWidthVisibleFrozen = num;
				}
			}
			else
			{
				this.columnsWidthVisible = num;
			}
			return num;
		}

		/// <summary>Returns the first column in display order that meets the given inclusion-filter requirements.</summary>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represents the filter for inclusion.</param>
		/// <returns>The first column in display order that meets the given filter requirements, or <see langword="null" /> if no column is found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F88 RID: 8072 RVA: 0x00094E4C File Offset: 0x0009304C
		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			for (int i = 0; i < this.itemsSorted.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
				if (dataGridViewColumn.StateIncludes(includeFilter))
				{
					this.lastAccessedSortedIndex = i;
					return dataGridViewColumn;
				}
			}
			return null;
		}

		/// <summary>Returns the first column in display order that meets the given inclusion-filter and exclusion-filter requirements.</summary>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <returns>The first column in display order that meets the given filter requirements, or <see langword="null" /> if no column is found.</returns>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F89 RID: 8073 RVA: 0x00094EC8 File Offset: 0x000930C8
		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetFirstColumn(includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			for (int i = 0; i < this.itemsSorted.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
				if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
				{
					this.lastAccessedSortedIndex = i;
					return dataGridViewColumn;
				}
			}
			return null;
		}

		/// <summary>Returns the last column in display order that meets the given filter requirements.</summary>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <returns>The last displayed column in display order that meets the given filter requirements, or <see langword="null" /> if no column is found.</returns>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F8A RID: 8074 RVA: 0x00094F7C File Offset: 0x0009317C
		public DataGridViewColumn GetLastColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			for (int i = this.itemsSorted.Count - 1; i >= 0; i--)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
				if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
				{
					this.lastAccessedSortedIndex = i;
					return dataGridViewColumn;
				}
			}
			return null;
		}

		/// <summary>Gets the first column after the given column in display order that meets the given filter requirements.</summary>
		/// <param name="dataGridViewColumnStart">The column from which to start searching for the next column.</param>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <returns>The next column that meets the given filter requirements, or <see langword="null" /> if no column is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumnStart" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F8B RID: 8075 RVA: 0x00095028 File Offset: 0x00093228
		public DataGridViewColumn GetNextColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (dataGridViewColumnStart == null)
			{
				throw new ArgumentNullException("dataGridViewColumnStart");
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			int i = this.GetColumnSortedIndex(dataGridViewColumnStart);
			if (i != -1)
			{
				for (i++; i < this.itemsSorted.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
					if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
					{
						this.lastAccessedSortedIndex = i;
						return dataGridViewColumn;
					}
				}
				return null;
			}
			bool flag = false;
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			for (i = 0; i < this.items.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)this.items[i];
				if (dataGridViewColumn2.StateIncludes(includeFilter) && dataGridViewColumn2.StateExcludes(excludeFilter) && (dataGridViewColumn2.DisplayIndex > dataGridViewColumnStart.DisplayIndex || (dataGridViewColumn2.DisplayIndex == dataGridViewColumnStart.DisplayIndex && dataGridViewColumn2.Index > dataGridViewColumnStart.Index)) && (dataGridViewColumn2.DisplayIndex < num2 || (dataGridViewColumn2.DisplayIndex == num2 && dataGridViewColumn2.Index < num)))
				{
					num = i;
					num2 = dataGridViewColumn2.DisplayIndex;
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return (DataGridViewColumn)this.items[num];
		}

		/// <summary>Gets the last column prior to the given column in display order that meets the given filter requirements.</summary>
		/// <param name="dataGridViewColumnStart">The column from which to start searching for the previous column.</param>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
		/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
		/// <returns>The previous column that meets the given filter requirements, or <see langword="null" /> if no column is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumnStart" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06001F8C RID: 8076 RVA: 0x000951B4 File Offset: 0x000933B4
		public DataGridViewColumn GetPreviousColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (dataGridViewColumnStart == null)
			{
				throw new ArgumentNullException("dataGridViewColumnStart");
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			int i = this.GetColumnSortedIndex(dataGridViewColumnStart);
			if (i != -1)
			{
				for (i--; i >= 0; i--)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
					if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
					{
						this.lastAccessedSortedIndex = i;
						return dataGridViewColumn;
					}
				}
				return null;
			}
			bool flag = false;
			int num = -1;
			int num2 = -1;
			for (i = 0; i < this.items.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)this.items[i];
				if (dataGridViewColumn2.StateIncludes(includeFilter) && dataGridViewColumn2.StateExcludes(excludeFilter) && (dataGridViewColumn2.DisplayIndex < dataGridViewColumnStart.DisplayIndex || (dataGridViewColumn2.DisplayIndex == dataGridViewColumnStart.DisplayIndex && dataGridViewColumn2.Index < dataGridViewColumnStart.Index)) && (dataGridViewColumn2.DisplayIndex > num2 || (dataGridViewColumn2.DisplayIndex == num2 && dataGridViewColumn2.Index > num)))
				{
					num = i;
					num2 = dataGridViewColumn2.DisplayIndex;
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return (DataGridViewColumn)this.items[num];
		}

		/// <summary>Gets the index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn" /> in the collection.</summary>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to return the index of.</param>
		/// <returns>The index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</returns>
		// Token: 0x06001F8D RID: 8077 RVA: 0x0009446B File Offset: 0x0009266B
		public int IndexOf(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.IndexOf(dataGridViewColumn);
		}

		/// <summary>Inserts a column at the given index in the collection.</summary>
		/// <param name="columnIndex">The zero-based index at which to insert the given column.</param>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values.  
		///  -or-  
		///  The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is <see langword="false" />.  
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of <see langword="true" />.  
		///  -or-  
		///  <paramref name="dataGridViewColumn" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of <see langword="null" />.</exception>
		// Token: 0x06001F8E RID: 8078 RVA: 0x0009532C File Offset: 0x0009352C
		public virtual void Insert(int columnIndex, DataGridViewColumn dataGridViewColumn)
		{
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			int displayIndex = dataGridViewColumn.DisplayIndex;
			if (displayIndex == -1)
			{
				dataGridViewColumn.DisplayIndex = columnIndex;
			}
			Point point;
			try
			{
				this.DataGridView.OnInsertingColumn(columnIndex, dataGridViewColumn, out point);
			}
			finally
			{
				dataGridViewColumn.DisplayIndexInternal = displayIndex;
			}
			this.InvalidateCachedColumnsOrder();
			this.items.Insert(columnIndex, dataGridViewColumn);
			dataGridViewColumn.IndexInternal = columnIndex;
			dataGridViewColumn.DataGridViewInternal = this.dataGridView;
			this.UpdateColumnCaches(dataGridViewColumn, true);
			this.DataGridView.OnInsertedColumn_PreNotification(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewColumn), true, point);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00095408 File Offset: 0x00093608
		internal void InvalidateCachedColumnCount(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedColumnCounts();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Selected)
			{
				this.columnCountsVisibleSelected = -1;
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00095424 File Offset: 0x00093624
		internal void InvalidateCachedColumnCounts()
		{
			this.columnCountsVisible = (this.columnCountsVisibleSelected = -1);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00095441 File Offset: 0x00093641
		internal void InvalidateCachedColumnsOrder()
		{
			this.itemsSorted = null;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0009544A File Offset: 0x0009364A
		internal void InvalidateCachedColumnsWidth(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedColumnsWidths();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Frozen)
			{
				this.columnsWidthVisibleFrozen = -1;
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00095464 File Offset: 0x00093664
		internal void InvalidateCachedColumnsWidths()
		{
			this.columnsWidthVisible = (this.columnsWidthVisibleFrozen = -1);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewColumnCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06001F94 RID: 8084 RVA: 0x00095481 File Offset: 0x00093681
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00095498 File Offset: 0x00093698
		private void OnCollectionChanged(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
		{
			this.OnCollectionChanged_PreNotification(ccea);
			this.OnCollectionChanged(ccea);
			this.OnCollectionChanged_PostNotification(ccea, changeIsInsertion, newCurrentCell);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000954B1 File Offset: 0x000936B1
		private void OnCollectionChanged_PreNotification(CollectionChangeEventArgs ccea)
		{
			this.DataGridView.OnColumnCollectionChanged_PreNotification(ccea);
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000954C0 File Offset: 0x000936C0
		private void OnCollectionChanged_PostNotification(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)ccea.Element;
			if (ccea.Action == CollectionChangeAction.Add && changeIsInsertion)
			{
				this.DataGridView.OnInsertedColumn_PostNotification(newCurrentCell);
			}
			else if (ccea.Action == CollectionChangeAction.Remove)
			{
				this.DataGridView.OnRemovedColumn_PostNotification(dataGridViewColumn, newCurrentCell);
			}
			this.DataGridView.OnColumnCollectionChanged_PostNotification(dataGridViewColumn);
		}

		/// <summary>Removes the specified column from the collection.</summary>
		/// <param name="dataGridViewColumn">The column to delete.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewColumn" /> is not in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F98 RID: 8088 RVA: 0x00095518 File Offset: 0x00093718
		public virtual void Remove(DataGridViewColumn dataGridViewColumn)
		{
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			if (dataGridViewColumn.DataGridView != this.DataGridView)
			{
				throw new ArgumentException(SR.GetString("DataGridView_ColumnDoesNotBelongToDataGridView"), "dataGridViewColumn");
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == dataGridViewColumn)
				{
					this.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>Removes the column with the specified name from the collection.</summary>
		/// <param name="columnName">The name of the column to delete.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="columnName" /> does not match the name of any column in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="columnName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F99 RID: 8089 RVA: 0x00095588 File Offset: 0x00093788
		public virtual void Remove(string columnName)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[i];
				if (string.Compare(dataGridViewColumn.Name, columnName, true, CultureInfo.InvariantCulture) == 0)
				{
					this.RemoveAt(i);
					return;
				}
			}
			throw new ArgumentException(SR.GetString("DataGridViewColumnCollection_ColumnNotFound", new object[] { columnName }), "columnName");
		}

		/// <summary>Removes the column at the given index in the collection.</summary>
		/// <param name="index">The index of the column to delete.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of columns in the control minus one.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		/// Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06001F9A RID: 8090 RVA: 0x00095608 File Offset: 0x00093808
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			this.RemoveAtInternal(index, false);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00095698 File Offset: 0x00093898
		internal void RemoveAtInternal(int index, bool force)
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[index];
			Point point;
			this.DataGridView.OnRemovingColumn(dataGridViewColumn, out point, force);
			this.InvalidateCachedColumnsOrder();
			this.items.RemoveAt(index);
			dataGridViewColumn.DataGridViewInternal = null;
			this.UpdateColumnCaches(dataGridViewColumn, false);
			this.DataGridView.OnRemovedColumn_PreNotification(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewColumn), false, point);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00095704 File Offset: 0x00093904
		private void UpdateColumnCaches(DataGridViewColumn dataGridViewColumn, bool adding)
		{
			if (this.columnCountsVisible != -1 || this.columnCountsVisibleSelected != -1 || this.columnsWidthVisible != -1 || this.columnsWidthVisibleFrozen != -1)
			{
				DataGridViewElementStates state = dataGridViewColumn.State;
				if ((state & DataGridViewElementStates.Visible) != DataGridViewElementStates.None)
				{
					int num = (adding ? 1 : (-1));
					int num2 = 0;
					if (this.columnsWidthVisible != -1 || (this.columnsWidthVisibleFrozen != -1 && (state & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)))
					{
						num2 = (adding ? dataGridViewColumn.Width : (-dataGridViewColumn.Width));
					}
					if (this.columnCountsVisible != -1)
					{
						this.columnCountsVisible += num;
					}
					if (this.columnsWidthVisible != -1)
					{
						this.columnsWidthVisible += num2;
					}
					if ((state & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible) && this.columnsWidthVisibleFrozen != -1)
					{
						this.columnsWidthVisibleFrozen += num2;
					}
					if ((state & (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible) && this.columnCountsVisibleSelected != -1)
					{
						this.columnCountsVisibleSelected += num;
					}
				}
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000957EB File Offset: 0x000939EB
		private void UpdateColumnOrderCache()
		{
			this.itemsSorted = (ArrayList)this.items.Clone();
			this.itemsSorted.Sort(DataGridViewColumnCollection.columnOrderComparer);
			this.lastAccessedSortedIndex = -1;
		}

		// Token: 0x04000D35 RID: 3381
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000D36 RID: 3382
		private ArrayList items = new ArrayList();

		// Token: 0x04000D37 RID: 3383
		private ArrayList itemsSorted;

		// Token: 0x04000D38 RID: 3384
		private int lastAccessedSortedIndex = -1;

		// Token: 0x04000D39 RID: 3385
		private int columnCountsVisible;

		// Token: 0x04000D3A RID: 3386
		private int columnCountsVisibleSelected;

		// Token: 0x04000D3B RID: 3387
		private int columnsWidthVisible;

		// Token: 0x04000D3C RID: 3388
		private int columnsWidthVisibleFrozen;

		// Token: 0x04000D3D RID: 3389
		private static DataGridViewColumnCollection.ColumnOrderComparer columnOrderComparer = new DataGridViewColumnCollection.ColumnOrderComparer();

		// Token: 0x04000D3E RID: 3390
		private DataGridView dataGridView;

		// Token: 0x02000669 RID: 1641
		private class ColumnOrderComparer : IComparer
		{
			// Token: 0x06006619 RID: 26137 RVA: 0x0017D258 File Offset: 0x0017B458
			public int Compare(object x, object y)
			{
				DataGridViewColumn dataGridViewColumn = x as DataGridViewColumn;
				DataGridViewColumn dataGridViewColumn2 = y as DataGridViewColumn;
				return dataGridViewColumn.DisplayIndex - dataGridViewColumn2.DisplayIndex;
			}
		}
	}
}
