using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>A collection of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</summary>
	// Token: 0x02000208 RID: 520
	[ListBindable(false)]
	[DesignerSerializer("System.Windows.Forms.Design.DataGridViewRowCollectionCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataGridViewRowCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridViewRow" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="value" /> is not <see langword="null" />.  
		///  -or-  
		///  <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of <see langword="true" />.  
		///  -or-  
		///  This operation would add a frozen row after unfrozen rows.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> has more cells than there are columns in the control.</exception>
		// Token: 0x06002210 RID: 8720 RVA: 0x000A1D1D File Offset: 0x0009FF1D
		int IList.Add(object value)
		{
			return this.Add((DataGridViewRow)value);
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection is data bound and the underlying data source does not support clearing the row data.  
		///  -or-  
		///  The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06002211 RID: 8721 RVA: 0x000A1D2B File Offset: 0x0009FF2B
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains the specified item.</summary>
		/// <param name="value">The item to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002212 RID: 8722 RVA: 0x000A1D33 File Offset: 0x0009FF33
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		/// <summary>Returns the index of a specified item in the collection.</summary>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the list; otherwise, -1.</returns>
		// Token: 0x06002213 RID: 8723 RVA: 0x000A1D41 File Offset: 0x0009FF41
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.DataGridViewRow" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  <paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="value" /> is not <see langword="null" />.  
		///  -or-  
		///  <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of <see langword="true" />.  
		///  -or-  
		///  This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> has more cells than there are columns in the control.</exception>
		// Token: 0x06002214 RID: 8724 RVA: 0x000A1D4F File Offset: 0x0009FF4F
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (DataGridViewRow)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not contained in this collection.  
		/// -or-  
		/// <paramref name="value" /> is a shared row.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="value" /> is the row for new records.  
		///  -or-  
		///  The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both <see langword="true" />.</exception>
		// Token: 0x06002215 RID: 8725 RVA: 0x000A1D5E File Offset: 0x0009FF5E
		void IList.Remove(object value)
		{
			this.Remove((DataGridViewRow)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridViewRow" /> from the collection at the specified position.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero and greater than the number of rows in the collection minus one.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both <see langword="true" />.</exception>
		// Token: 0x06002216 RID: 8726 RVA: 0x000A1D6C File Offset: 0x0009FF6C
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0001180C File Offset: 0x0000FA0C
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
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The user tried to set this property.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-
		///  <paramref name="index" /> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count" />.</exception>
		// Token: 0x170007BA RID: 1978
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

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x0600221B RID: 8731 RVA: 0x000A1D7E File Offset: 0x0009FF7E
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000A1D8D File Offset: 0x0009FF8D
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600221F RID: 8735 RVA: 0x000A1D95 File Offset: 0x0009FF95
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DataGridViewRowCollection.UnsharingRowEnumerator(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> class.</summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		// Token: 0x06002220 RID: 8736 RVA: 0x000A1D9D File Offset: 0x0009FF9D
		public DataGridViewRowCollection(DataGridView dataGridView)
		{
			this.InvalidateCachedRowCounts();
			this.InvalidateCachedRowsHeights();
			this.dataGridView = dataGridView;
			this.rowStates = new List<DataGridViewElementStates>();
			this.items = new DataGridViewRowCollection.RowArrayList(this);
		}

		/// <summary>Gets the number of rows in the collection.</summary>
		/// <returns>The number of rows in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000A1DCF File Offset: 0x0009FFCF
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000A1DDC File Offset: 0x0009FFDC
		internal bool IsCollectionChangedListenedTo
		{
			get
			{
				return this.onCollectionChanged != null;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000A1DE8 File Offset: 0x0009FFE8
		protected ArrayList List
		{
			get
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridViewRow dataGridViewRow = this[i];
				}
				return this.items;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000A1E16 File Offset: 0x000A0016
		internal ArrayList SharedList
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Returns the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</summary>
		/// <param name="rowIndex">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> positioned at the specified index.</returns>
		// Token: 0x06002225 RID: 8741 RVA: 0x000A1E1E File Offset: 0x000A001E
		public DataGridViewRow SharedRow(int rowIndex)
		{
			return (DataGridViewRow)this.SharedList[rowIndex];
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> that owns the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x000A1E31 File Offset: 0x000A0031
		protected DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index. Accessing a <see cref="T:System.Windows.Forms.DataGridViewRow" /> with this indexer causes the row to become unshared. To keep the row shared, use the <see cref="M:System.Windows.Forms.DataGridViewRowCollection.SharedRow(System.Int32)" /> method. For more information, see Best Practices for Scaling the Windows Forms DataGridView Control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-
		///  <paramref name="index" /> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count" />.</exception>
		// Token: 0x170007C3 RID: 1987
		public DataGridViewRow this[int index]
		{
			get
			{
				DataGridViewRow dataGridViewRow = this.SharedRow(index);
				if (dataGridViewRow.Index != -1)
				{
					return dataGridViewRow;
				}
				if (index == 0 && this.items.Count == 1)
				{
					dataGridViewRow.IndexInternal = 0;
					dataGridViewRow.StateInternal = this.SharedRowState(0);
					if (this.DataGridView != null)
					{
						this.DataGridView.OnRowUnshared(dataGridViewRow);
					}
					return dataGridViewRow;
				}
				DataGridViewRow dataGridViewRow2 = (DataGridViewRow)dataGridViewRow.Clone();
				dataGridViewRow2.IndexInternal = index;
				dataGridViewRow2.DataGridViewInternal = dataGridViewRow.DataGridView;
				dataGridViewRow2.StateInternal = this.SharedRowState(index);
				this.SharedList[index] = dataGridViewRow2;
				int num = 0;
				foreach (object obj in dataGridViewRow2.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = dataGridViewRow.DataGridView;
					dataGridViewCell.OwningRowInternal = dataGridViewRow2;
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
					num++;
				}
				if (dataGridViewRow2.HasHeaderCell)
				{
					dataGridViewRow2.HeaderCell.DataGridViewInternal = dataGridViewRow.DataGridView;
					dataGridViewRow2.HeaderCell.OwningRowInternal = dataGridViewRow2;
				}
				if (this.DataGridView != null)
				{
					this.DataGridView.OnRowUnshared(dataGridViewRow2);
				}
				return dataGridViewRow2;
			}
		}

		/// <summary>Occurs when the contents of the collection change.</summary>
		// Token: 0x14000186 RID: 390
		// (add) Token: 0x06002228 RID: 8744 RVA: 0x000A1F8C File Offset: 0x000A018C
		// (remove) Token: 0x06002229 RID: 8745 RVA: 0x000A1FA5 File Offset: 0x000A01A5
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

		/// <summary>Adds a new row to the collection.</summary>
		/// <returns>The index of the new row.</returns>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  This operation would add a frozen row after unfrozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.</exception>
		// Token: 0x0600222A RID: 8746 RVA: 0x000A1FC0 File Offset: 0x000A01C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add()
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddInternal(false, null);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000A2010 File Offset: 0x000A0210
		internal int AddInternal(bool newRow, object[] values)
		{
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.RowTemplate.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_RowTemplateTooManyCells"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			if (newRow)
			{
				rowTemplateClone.StateInternal = rowTemplateClone.State | DataGridViewElementStates.Visible;
				foreach (object obj in rowTemplateClone.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.Value = dataGridViewCell.DefaultNewRowValue;
				}
			}
			if (values != null)
			{
				rowTemplateClone.SetValuesInternal(values);
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.Insert(num, rowTemplateClone);
				return num;
			}
			DataGridViewElementStates state = rowTemplateClone.State;
			this.DataGridView.OnAddingRow(rowTemplateClone, state, true);
			rowTemplateClone.DataGridViewInternal = this.dataGridView;
			int num2 = 0;
			foreach (object obj2 in rowTemplateClone.Cells)
			{
				DataGridViewCell dataGridViewCell2 = (DataGridViewCell)obj2;
				dataGridViewCell2.DataGridViewInternal = this.dataGridView;
				dataGridViewCell2.OwningColumnInternal = this.DataGridView.Columns[num2];
				num2++;
			}
			if (rowTemplateClone.HasHeaderCell)
			{
				rowTemplateClone.HeaderCell.DataGridViewInternal = this.DataGridView;
				rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
			}
			int num3 = this.SharedList.Add(rowTemplateClone);
			this.rowStates.Add(state);
			if (values != null || !this.RowIsSharable(num3) || DataGridViewRowCollection.RowHasValueOrToolTipText(rowTemplateClone) || this.IsCollectionChangedListenedTo)
			{
				rowTemplateClone.IndexInternal = num3;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, rowTemplateClone), num3, 1);
			return num3;
		}

		/// <summary>Adds a new row to the collection, and populates the cells with the specified objects.</summary>
		/// <param name="values">A variable number of objects that populate the cells of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <returns>The index of the new row.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		/// -or-
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.  
		///  -or-  
		///  This operation would add a frozen row after unfrozen rows.</exception>
		// Token: 0x0600222C RID: 8748 RVA: 0x000A2224 File Offset: 0x000A0424
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (this.DataGridView.VirtualMode)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationInVirtualMode"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddInternal(false, values);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> to the collection.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="dataGridViewRow" /> is not <see langword="null" />.  
		///  -or-  
		///  <paramref name="dataGridViewRow" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of <see langword="true" />.  
		///  -or-  
		///  This operation would add a frozen row after unfrozen rows.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRow" /> has more cells than there are columns in the control.</exception>
		// Token: 0x0600222D RID: 8749 RVA: 0x000A22A0 File Offset: 0x000A04A0
		public virtual int Add(DataGridViewRow dataGridViewRow)
		{
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddInternal(dataGridViewRow);
		}

		/// <summary>Adds the specified number of new rows to the collection.</summary>
		/// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>The index of the last row that was added.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.  
		///  -or-  
		///  This operation would add frozen rows after unfrozen rows.</exception>
		// Token: 0x0600222E RID: 8750 RVA: 0x000A2310 File Offset: 0x000A0510
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add(int count)
		{
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.RowTemplate.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_RowTemplateTooManyCells"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			DataGridViewElementStates state = rowTemplateClone.State;
			rowTemplateClone.DataGridViewInternal = this.dataGridView;
			int num = 0;
			foreach (object obj in rowTemplateClone.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			if (rowTemplateClone.HasHeaderCell)
			{
				rowTemplateClone.HeaderCell.DataGridViewInternal = this.dataGridView;
				rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num2 = this.Count - 1;
				this.InsertCopiesPrivate(rowTemplateClone, state, num2, count);
				return num2 + count - 1;
			}
			return this.AddCopiesPrivate(rowTemplateClone, state, count);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000A24B4 File Offset: 0x000A06B4
		internal int AddInternal(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			if (dataGridViewRow.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowAlreadyBelongsToDataGridView"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (dataGridViewRow.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new ArgumentException(SR.GetString("DataGridViewRowCollection_TooManyCells"), "dataGridViewRow");
			}
			if (dataGridViewRow.Selected)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CannotAddOrInsertSelectedRow"));
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.InsertInternal(num, dataGridViewRow);
				return num;
			}
			this.DataGridView.CompleteCellsCollection(dataGridViewRow);
			this.DataGridView.OnAddingRow(dataGridViewRow, dataGridViewRow.State, true);
			int num2 = 0;
			foreach (object obj in dataGridViewRow.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				if (dataGridViewCell.ColumnIndex == -1)
				{
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num2];
				}
				num2++;
			}
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.DataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			int num3 = this.SharedList.Add(dataGridViewRow);
			this.rowStates.Add(dataGridViewRow.State);
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			if (!this.RowIsSharable(num3) || DataGridViewRowCollection.RowHasValueOrToolTipText(dataGridViewRow) || this.IsCollectionChangedListenedTo)
			{
				dataGridViewRow.IndexInternal = num3;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow), num3, 1);
			return num3;
		}

		/// <summary>Adds a new row based on the row at the specified index.</summary>
		/// <param name="indexSource">The index of the row on which to base the new row.</param>
		/// <returns>The index of the new row.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than or equal to the number of rows in the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  This operation would add a frozen row after unfrozen rows.</exception>
		// Token: 0x06002230 RID: 8752 RVA: 0x000A2690 File Offset: 0x000A0890
		public virtual int AddCopy(int indexSource)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddCopyInternal(indexSource, DataGridViewElementStates.None, DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected, false);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000A26E4 File Offset: 0x000A08E4
		internal int AddCopyInternal(int indexSource, DataGridViewElementStates dgvesAdd, DataGridViewElementStates dgvesRemove, bool newRow)
		{
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.InsertCopy(indexSource, num);
				return num;
			}
			if (indexSource < 0 || indexSource >= this.Count)
			{
				throw new ArgumentOutOfRangeException("indexSource", SR.GetString("DataGridViewRowCollection_IndexSourceOutOfRange"));
			}
			DataGridViewRow dataGridViewRow = this.SharedRow(indexSource);
			int num2;
			if (dataGridViewRow.Index == -1 && !this.IsCollectionChangedListenedTo && !newRow)
			{
				DataGridViewElementStates dataGridViewElementStates = this.rowStates[indexSource] & ~dgvesRemove;
				dataGridViewElementStates |= dgvesAdd;
				this.DataGridView.OnAddingRow(dataGridViewRow, dataGridViewElementStates, true);
				num2 = this.SharedList.Add(dataGridViewRow);
				this.rowStates.Add(dataGridViewElementStates);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow), num2, 1);
				return num2;
			}
			num2 = this.AddDuplicateRow(dataGridViewRow, newRow);
			if (!this.RowIsSharable(num2) || DataGridViewRowCollection.RowHasValueOrToolTipText(this.SharedRow(num2)) || this.IsCollectionChangedListenedTo)
			{
				this.UnshareRow(num2);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(num2)), num2, 1);
			return num2;
		}

		/// <summary>Adds the specified number of rows to the collection based on the row at the specified index.</summary>
		/// <param name="indexSource">The index of the row on which to base the new rows.</param>
		/// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>The index of the last row that was added.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than or equal to the number of rows in the control.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  This operation would add a frozen row after unfrozen rows.</exception>
		// Token: 0x06002232 RID: 8754 RVA: 0x000A27E4 File Offset: 0x000A09E4
		public virtual int AddCopies(int indexSource, int count)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			return this.AddCopiesInternal(indexSource, count);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000A2834 File Offset: 0x000A0A34
		internal int AddCopiesInternal(int indexSource, int count)
		{
			if (this.DataGridView.NewRowIndex != -1)
			{
				int num = this.Count - 1;
				this.InsertCopiesPrivate(indexSource, num, count);
				return num + count - 1;
			}
			return this.AddCopiesInternal(indexSource, count, DataGridViewElementStates.None, DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000A2874 File Offset: 0x000A0A74
		internal int AddCopiesInternal(int indexSource, int count, DataGridViewElementStates dgvesAdd, DataGridViewElementStates dgvesRemove)
		{
			if (indexSource < 0 || this.Count <= indexSource)
			{
				throw new ArgumentOutOfRangeException("indexSource", SR.GetString("DataGridViewRowCollection_IndexSourceOutOfRange"));
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			DataGridViewElementStates dataGridViewElementStates = this.rowStates[indexSource] & ~dgvesRemove;
			dataGridViewElementStates |= dgvesAdd;
			return this.AddCopiesPrivate(this.SharedRow(indexSource), dataGridViewElementStates, count);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000A28E0 File Offset: 0x000A0AE0
		private int AddCopiesPrivate(DataGridViewRow rowTemplate, DataGridViewElementStates rowTemplateState, int count)
		{
			int count2 = this.items.Count;
			int num;
			if (rowTemplate.Index == -1)
			{
				this.DataGridView.OnAddingRow(rowTemplate, rowTemplateState, true);
				for (int i = 0; i < count - 1; i++)
				{
					this.SharedList.Add(rowTemplate);
					this.rowStates.Add(rowTemplateState);
				}
				num = this.SharedList.Add(rowTemplate);
				this.rowStates.Add(rowTemplateState);
				this.DataGridView.OnAddedRow_PreNotification(num);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count2, count);
				for (int j = 0; j < count; j++)
				{
					this.DataGridView.OnAddedRow_PostNotification(num - (count - 1) + j);
				}
				return num;
			}
			num = this.AddDuplicateRow(rowTemplate, false);
			if (count > 1)
			{
				this.DataGridView.OnAddedRow_PreNotification(num);
				if (this.RowIsSharable(num))
				{
					DataGridViewRow dataGridViewRow = this.SharedRow(num);
					this.DataGridView.OnAddingRow(dataGridViewRow, rowTemplateState, true);
					for (int k = 1; k < count - 1; k++)
					{
						this.SharedList.Add(dataGridViewRow);
						this.rowStates.Add(rowTemplateState);
					}
					num = this.SharedList.Add(dataGridViewRow);
					this.rowStates.Add(rowTemplateState);
					this.DataGridView.OnAddedRow_PreNotification(num);
				}
				else
				{
					this.UnshareRow(num);
					for (int l = 1; l < count; l++)
					{
						num = this.AddDuplicateRow(rowTemplate, false);
						this.UnshareRow(num);
						this.DataGridView.OnAddedRow_PreNotification(num);
					}
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count2, count);
				for (int m = 0; m < count; m++)
				{
					this.DataGridView.OnAddedRow_PostNotification(num - (count - 1) + m);
				}
				return num;
			}
			if (this.IsCollectionChangedListenedTo)
			{
				this.UnshareRow(num);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(num)), num, 1);
			return num;
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000A2AAC File Offset: 0x000A0CAC
		private int AddDuplicateRow(DataGridViewRow rowTemplate, bool newRow)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)rowTemplate.Clone();
			dataGridViewRow.StateInternal = DataGridViewElementStates.None;
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			int num = 0;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if (newRow)
				{
					dataGridViewCell.Value = dataGridViewCell.DefaultNewRowValue;
				}
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			DataGridViewElementStates dataGridViewElementStates = rowTemplate.State & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.dataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			this.DataGridView.OnAddingRow(dataGridViewRow, dataGridViewElementStates, true);
			this.rowStates.Add(dataGridViewElementStates);
			return this.SharedList.Add(dataGridViewRow);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to the collection.</summary>
		/// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to be added to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRows" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRows" /> contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  At least one entry in the <paramref name="dataGridViewRows" /> array is <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not <see langword="null" />.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of <see langword="true" />.  
		///  -or-  
		///  Two or more rows in the <paramref name="dataGridViewRows" /> array are identical.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array contains more cells than there are columns in the control.  
		///  -or-  
		///  This operation would add frozen rows after unfrozen rows.</exception>
		// Token: 0x06002237 RID: 8759 RVA: 0x000A2BB8 File Offset: 0x000A0DB8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual void AddRange(params DataGridViewRow[] dataGridViewRows)
		{
			if (dataGridViewRows == null)
			{
				throw new ArgumentNullException("dataGridViewRows");
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NewRowIndex != -1)
			{
				this.InsertRange(this.Count - 1, dataGridViewRows);
				return;
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			int count = this.items.Count;
			this.DataGridView.OnAddingRows(dataGridViewRows, true);
			foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
			{
				int num = 0;
				foreach (object obj in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = this.dataGridView;
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
					num++;
				}
				if (dataGridViewRow.HasHeaderCell)
				{
					dataGridViewRow.HeaderCell.DataGridViewInternal = this.dataGridView;
					dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
				}
				int num2 = this.SharedList.Add(dataGridViewRow);
				this.rowStates.Add(dataGridViewRow.State);
				dataGridViewRow.IndexInternal = num2;
				dataGridViewRow.DataGridViewInternal = this.dataGridView;
			}
			this.DataGridView.OnAddedRows_PreNotification(dataGridViewRows);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), count, dataGridViewRows.Length);
			this.DataGridView.OnAddedRows_PostNotification(dataGridViewRows);
		}

		/// <summary>Clears the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection is data bound and the underlying data source does not support clearing the row data.  
		///  -or-  
		///  The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents the row collection from being modified:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
		// Token: 0x06002238 RID: 8760 RVA: 0x000A2D80 File Offset: 0x000A0F80
		public virtual void Clear()
		{
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.DataSource == null)
			{
				this.ClearInternal(true);
				return;
			}
			IBindingList bindingList = this.DataGridView.DataConnection.List as IBindingList;
			if (bindingList != null && bindingList.AllowRemove && bindingList.SupportsChangeNotification)
			{
				bindingList.Clear();
				return;
			}
			throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CantClearRowCollectionWithWrongSource"));
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000A2E00 File Offset: 0x000A1000
		internal void ClearInternal(bool recreateNewRow)
		{
			int count = this.items.Count;
			if (count > 0)
			{
				this.DataGridView.OnClearingRows();
				for (int i = 0; i < count; i++)
				{
					this.SharedRow(i).DetachFromDataGridView();
				}
				this.SharedList.Clear();
				this.rowStates.Clear();
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), 0, count, true, false, recreateNewRow, new Point(-1, -1));
				return;
			}
			if (recreateNewRow && this.DataGridView.Columns.Count != 0 && this.DataGridView.AllowUserToAddRowsInternal && this.items.Count == 0)
			{
				this.DataGridView.AddNewRow(false);
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> is in the collection.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.DataGridViewRow" /> is in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600223A RID: 8762 RVA: 0x000A2EAB File Offset: 0x000A10AB
		public virtual bool Contains(DataGridViewRow dataGridViewRow)
		{
			return this.items.IndexOf(dataGridViewRow) != -1;
		}

		/// <summary>Copies the items from the collection into the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> array, starting at the specified index.</summary>
		/// <param name="array">A <see cref="T:System.Windows.Forms.DataGridViewRow" /> array that is the destination of the items copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		// Token: 0x0600223B RID: 8763 RVA: 0x000A1D7E File Offset: 0x0009FF7E
		public void CopyTo(DataGridViewRow[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000A2EC0 File Offset: 0x000A10C0
		internal int DisplayIndexToRowIndex(int visibleRowIndex)
		{
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				if ((this.GetRowState(i) & DataGridViewElementStates.Visible) == DataGridViewElementStates.Visible)
				{
					num++;
				}
				if (num == visibleRowIndex)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x0600223D RID: 8765 RVA: 0x000A2EFC File Offset: 0x000A10FC
		public int GetFirstRow(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected == 0)
						{
							return -1;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen == 0)
				{
					return -1;
				}
			}
			else if (this.rowCountsVisible == 0)
			{
				return -1;
			}
			int num = 0;
			while (num < this.items.Count && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x0600223E RID: 8766 RVA: 0x000A2F90 File Offset: 0x000A1190
		public int GetFirstRow(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetFirstRow(includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected == 0)
						{
							return -1;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen == 0)
				{
					return -1;
				}
			}
			else if (this.rowCountsVisible == 0)
			{
				return -1;
			}
			int num = 0;
			while (num < this.items.Count && ((this.GetRowState(num) & includeFilter) != includeFilter || (this.GetRowState(num) & excludeFilter) != DataGridViewElementStates.None))
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the index of the last <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the last <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x0600223F RID: 8767 RVA: 0x000A3060 File Offset: 0x000A1260
		public int GetLastRow(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected == 0)
						{
							return -1;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen == 0)
				{
					return -1;
				}
			}
			else if (this.rowCountsVisible == 0)
			{
				return -1;
			}
			int num = this.items.Count - 1;
			while (num >= 0 && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num--;
			}
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000A30EC File Offset: 0x000A12EC
		internal int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, int skipRows)
		{
			int num = indexStart;
			do
			{
				num = this.GetNextRow(num, includeFilter);
				skipRows--;
			}
			while (skipRows >= 0 && num != -1);
			return num;
		}

		/// <summary>Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> after <paramref name="indexStart" /> that has the attributes specified by <paramref name="includeFilter" />, or -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06002241 RID: 8769 RVA: 0x000A3114 File Offset: 0x000A1314
		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (indexStart < -1)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					(-1).ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart + 1;
			while (num < this.items.Count && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
		/// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06002242 RID: 8770 RVA: 0x000A31C4 File Offset: 0x000A13C4
		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetNextRow(indexStart, includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (indexStart < -1)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					(-1).ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart + 1;
			while (num < this.items.Count && ((this.GetRowState(num) & includeFilter) != includeFilter || (this.GetRowState(num) & excludeFilter) != DataGridViewElementStates.None))
			{
				num++;
			}
			if (num >= this.items.Count)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
		/// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06002243 RID: 8771 RVA: 0x000A32AC File Offset: 0x000A14AC
		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (indexStart > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					this.items.Count.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart - 1;
			while (num >= 0 && (this.GetRowState(num) & includeFilter) != includeFilter)
			{
				num--;
			}
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
		/// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexStart" /> is greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06002244 RID: 8772 RVA: 0x000A335C File Offset: 0x000A155C
		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetPreviousRow(indexStart, includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "excludeFilter" }));
			}
			if (indexStart > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("indexStart", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"indexStart",
					indexStart.ToString(CultureInfo.CurrentCulture),
					this.items.Count.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int num = indexStart - 1;
			while (num >= 0 && ((this.GetRowState(num) & includeFilter) != includeFilter || (this.GetRowState(num) & excludeFilter) != DataGridViewElementStates.None))
			{
				num--;
			}
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000A3444 File Offset: 0x000A1644
		internal int GetVisibleIndex(DataGridViewRow row)
		{
			for (int i = 0; i < this.Count; i++)
			{
				int num = this.DisplayIndexToRowIndex(i);
				if (num != -1 && this.items[num] == row)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the collection that meet the specified criteria.</summary>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that have the attributes specified by <paramref name="includeFilter" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06002246 RID: 8774 RVA: 0x000A3480 File Offset: 0x000A1680
		public int GetRowCount(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleSelected != -1)
						{
							return this.rowCountsVisibleSelected;
						}
					}
				}
				else if (this.rowCountsVisibleFrozen != -1)
				{
					return this.rowCountsVisibleFrozen;
				}
			}
			else if (this.rowCountsVisible != -1)
			{
				return this.rowCountsVisible;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num++;
				}
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter != (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						this.rowCountsVisibleSelected = num;
					}
				}
				else
				{
					this.rowCountsVisibleFrozen = num;
				}
			}
			else
			{
				this.rowCountsVisible = num;
			}
			return num;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000A3548 File Offset: 0x000A1748
		internal int GetRowCount(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex)
		{
			int num = 0;
			for (int i = fromRowIndex + 1; i <= toRowIndex; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Returns the cumulative height of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects that meet the specified criteria.</summary>
		/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		/// <returns>The cumulative height of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that have the attributes specified by <paramref name="includeFilter" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
		// Token: 0x06002248 RID: 8776 RVA: 0x000A3578 File Offset: 0x000A1778
		public int GetRowsHeight(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[] { "includeFilter" }));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (this.rowsHeightVisibleFrozen != -1)
					{
						return this.rowsHeightVisibleFrozen;
					}
				}
			}
			else if (this.rowsHeightVisible != -1)
			{
				return this.rowsHeightVisible;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num += ((DataGridViewRow)this.items[i]).GetHeight(i);
				}
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					this.rowsHeightVisibleFrozen = num;
				}
			}
			else
			{
				this.rowsHeightVisible = num;
			}
			return num;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000A3630 File Offset: 0x000A1830
		internal int GetRowsHeight(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex)
		{
			int num = 0;
			for (int i = fromRowIndex; i < toRowIndex; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num += ((DataGridViewRow)this.items[i]).GetHeight(i);
				}
			}
			return num;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000A3674 File Offset: 0x000A1874
		private bool GetRowsHeightExceedLimit(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex, int heightLimit)
		{
			int num = 0;
			for (int i = fromRowIndex; i < toRowIndex; i++)
			{
				if ((this.GetRowState(i) & includeFilter) == includeFilter)
				{
					num += ((DataGridViewRow)this.items[i]).GetHeight(i);
					if (num > heightLimit)
					{
						return true;
					}
				}
			}
			return num > heightLimit;
		}

		/// <summary>Gets the state of the row with the specified index.</summary>
		/// <param name="rowIndex">The index of the row.</param>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state of the specified row.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero and greater than the number of rows in the collection minus one.</exception>
		// Token: 0x0600224B RID: 8779 RVA: 0x000A36C4 File Offset: 0x000A18C4
		public virtual DataGridViewElementStates GetRowState(int rowIndex)
		{
			if (rowIndex < 0 || rowIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_RowIndexOutOfRange"));
			}
			DataGridViewRow dataGridViewRow = this.SharedRow(rowIndex);
			if (dataGridViewRow.Index == -1)
			{
				return this.SharedRowState(rowIndex);
			}
			return dataGridViewRow.GetState(rowIndex);
		}

		/// <summary>Returns the index of a specified item in the collection.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, -1.</returns>
		// Token: 0x0600224C RID: 8780 RVA: 0x000A1D41 File Offset: 0x0009FF41
		public int IndexOf(DataGridViewRow dataGridViewRow)
		{
			return this.items.IndexOf(dataGridViewRow);
		}

		/// <summary>Inserts a row into the collection at the specified position, and populates the cells with the specified objects.</summary>
		/// <param name="rowIndex">The position at which to insert the row.</param>
		/// <param name="values">A variable number of objects that populate the cells of the new row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  <paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property is not <see langword="null" />.  
		///  -or-  
		///  This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">The row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.</exception>
		// Token: 0x0600224D RID: 8781 RVA: 0x000A3718 File Offset: 0x000A1918
		public virtual void Insert(int rowIndex, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (this.DataGridView.VirtualMode)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationInVirtualMode"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			rowTemplateClone.SetValuesInternal(values);
			this.Insert(rowIndex, rowTemplateClone);
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> into the collection.</summary>
		/// <param name="rowIndex">The position at which to insert the row.</param>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  <paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of <paramref name="dataGridViewRow" /> is not <see langword="null" />.  
		///  -or-  
		///  <paramref name="dataGridViewRow" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of <see langword="true" />.  
		///  -or-  
		///  This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRow" /> has more cells than there are columns in the control.</exception>
		// Token: 0x0600224E RID: 8782 RVA: 0x000A378C File Offset: 0x000A198C
		public virtual void Insert(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			this.InsertInternal(rowIndex, dataGridViewRow);
		}

		/// <summary>Inserts the specified number of rows into the collection at the specified location.</summary>
		/// <param name="rowIndex">The position at which to insert the rows.</param>
		/// <param name="count">The number of rows to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection.  
		/// -or-  
		/// <paramref name="count" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  <paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.  
		///  -or-  
		///  This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		// Token: 0x0600224F RID: 8783 RVA: 0x000A37DC File Offset: 0x000A19DC
		public virtual void Insert(int rowIndex, int count)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (rowIndex < 0 || this.Count < rowIndex)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_IndexDestinationOutOfRange"));
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (this.DataGridView.RowTemplate.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_RowTemplateTooManyCells"));
			}
			if (this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			DataGridViewRow rowTemplateClone = this.DataGridView.RowTemplateClone;
			DataGridViewElementStates state = rowTemplateClone.State;
			rowTemplateClone.DataGridViewInternal = this.dataGridView;
			int num = 0;
			foreach (object obj in rowTemplateClone.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			if (rowTemplateClone.HasHeaderCell)
			{
				rowTemplateClone.HeaderCell.DataGridViewInternal = this.dataGridView;
				rowTemplateClone.HeaderCell.OwningRowInternal = rowTemplateClone;
			}
			this.InsertCopiesPrivate(rowTemplateClone, state, rowIndex, count);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000A39A0 File Offset: 0x000A1BA0
		internal void InsertInternal(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			if (rowIndex < 0 || this.Count < rowIndex)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_RowIndexOutOfRange"));
			}
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			if (dataGridViewRow.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowAlreadyBelongsToDataGridView"));
			}
			if (this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			if (dataGridViewRow.Cells.Count > this.DataGridView.Columns.Count)
			{
				throw new ArgumentException(SR.GetString("DataGridViewRowCollection_TooManyCells"), "dataGridViewRow");
			}
			if (dataGridViewRow.Selected)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CannotAddOrInsertSelectedRow"));
			}
			this.InsertInternal(rowIndex, dataGridViewRow, false);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000A3A94 File Offset: 0x000A1C94
		internal void InsertInternal(int rowIndex, DataGridViewRow dataGridViewRow, bool force)
		{
			Point point = new Point(-1, -1);
			if (force)
			{
				if (this.DataGridView.Columns.Count == 0)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
				}
				if (dataGridViewRow.Cells.Count > this.DataGridView.Columns.Count)
				{
					throw new ArgumentException(SR.GetString("DataGridViewRowCollection_TooManyCells"), "dataGridViewRow");
				}
			}
			this.DataGridView.CompleteCellsCollection(dataGridViewRow);
			this.DataGridView.OnInsertingRow(rowIndex, dataGridViewRow, dataGridViewRow.State, ref point, true, 1, force);
			int num = 0;
			foreach (object obj in dataGridViewRow.Cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				if (dataGridViewCell.ColumnIndex == -1)
				{
					dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				}
				num++;
			}
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.DataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			this.SharedList.Insert(rowIndex, dataGridViewRow);
			this.rowStates.Insert(rowIndex, dataGridViewRow.State);
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			if (!this.RowIsSharable(rowIndex) || DataGridViewRowCollection.RowHasValueOrToolTipText(dataGridViewRow) || this.IsCollectionChangedListenedTo)
			{
				dataGridViewRow.IndexInternal = rowIndex;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow), rowIndex, 1, false, true, false, point);
		}

		/// <summary>Inserts a row into the collection at the specified position, based on the row at specified position.</summary>
		/// <param name="indexSource">The index of the row on which to base the new row.</param>
		/// <param name="indexDestination">The position at which to insert the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than the number of rows in the collection minus one.  
		/// -or-  
		/// <paramref name="indexDestination" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="indexDestination" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is <see langword="true" />.  
		///  -or-  
		///  This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
		// Token: 0x06002252 RID: 8786 RVA: 0x000A3C24 File Offset: 0x000A1E24
		public virtual void InsertCopy(int indexSource, int indexDestination)
		{
			this.InsertCopies(indexSource, indexDestination, 1);
		}

		/// <summary>Inserts rows into the collection at the specified position.</summary>
		/// <param name="indexSource">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> on which to base the new rows.</param>
		/// <param name="indexDestination">The position at which to insert the rows.</param>
		/// <param name="count">The number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexSource" /> is less than zero or greater than the number of rows in the collection minus one.  
		/// -or-  
		/// <paramref name="indexDestination" /> is less than zero or greater than the number of rows in the collection.  
		/// -or-  
		/// <paramref name="count" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="indexDestination" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is <see langword="true" />.  
		///  -or-  
		///  This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
		// Token: 0x06002253 RID: 8787 RVA: 0x000A3C30 File Offset: 0x000A1E30
		public virtual void InsertCopies(int indexSource, int indexDestination, int count)
		{
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			this.InsertCopiesPrivate(indexSource, indexDestination, count);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000A3C80 File Offset: 0x000A1E80
		private void InsertCopiesPrivate(int indexSource, int indexDestination, int count)
		{
			if (indexSource < 0 || this.Count <= indexSource)
			{
				throw new ArgumentOutOfRangeException("indexSource", SR.GetString("DataGridViewRowCollection_IndexSourceOutOfRange"));
			}
			if (indexDestination < 0 || this.Count < indexDestination)
			{
				throw new ArgumentOutOfRangeException("indexDestination", SR.GetString("DataGridViewRowCollection_IndexDestinationOutOfRange"));
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("DataGridViewRowCollection_CountOutOfRange"));
			}
			if (this.DataGridView.NewRowIndex != -1 && indexDestination == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			DataGridViewElementStates dataGridViewElementStates = this.GetRowState(indexSource) & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			this.InsertCopiesPrivate(this.SharedRow(indexSource), dataGridViewElementStates, indexDestination, count);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000A3D2C File Offset: 0x000A1F2C
		private void InsertCopiesPrivate(DataGridViewRow rowTemplate, DataGridViewElementStates rowTemplateState, int indexDestination, int count)
		{
			Point point = new Point(-1, -1);
			if (rowTemplate.Index == -1)
			{
				if (count > 1)
				{
					this.DataGridView.OnInsertingRow(indexDestination, rowTemplate, rowTemplateState, ref point, true, count, false);
					for (int i = 0; i < count; i++)
					{
						this.SharedList.Insert(indexDestination + i, rowTemplate);
						this.rowStates.Insert(indexDestination + i, rowTemplateState);
					}
					this.DataGridView.OnInsertedRow_PreNotification(indexDestination, count);
					this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, point);
					for (int j = 0; j < count; j++)
					{
						this.DataGridView.OnInsertedRow_PostNotification(indexDestination + j, point, j == count - 1);
					}
					return;
				}
				this.DataGridView.OnInsertingRow(indexDestination, rowTemplate, rowTemplateState, ref point, true, 1, false);
				this.SharedList.Insert(indexDestination, rowTemplate);
				this.rowStates.Insert(indexDestination, rowTemplateState);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(indexDestination)), indexDestination, count, false, true, false, point);
				return;
			}
			else
			{
				this.InsertDuplicateRow(indexDestination, rowTemplate, true, ref point);
				if (count > 1)
				{
					this.DataGridView.OnInsertedRow_PreNotification(indexDestination, 1);
					if (this.RowIsSharable(indexDestination))
					{
						DataGridViewRow dataGridViewRow = this.SharedRow(indexDestination);
						this.DataGridView.OnInsertingRow(indexDestination + 1, dataGridViewRow, rowTemplateState, ref point, false, count - 1, false);
						for (int k = 1; k < count; k++)
						{
							this.SharedList.Insert(indexDestination + k, dataGridViewRow);
							this.rowStates.Insert(indexDestination + k, rowTemplateState);
						}
						this.DataGridView.OnInsertedRow_PreNotification(indexDestination + 1, count - 1);
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, point);
					}
					else
					{
						this.UnshareRow(indexDestination);
						for (int l = 1; l < count; l++)
						{
							this.InsertDuplicateRow(indexDestination + l, rowTemplate, false, ref point);
							this.UnshareRow(indexDestination + l);
							this.DataGridView.OnInsertedRow_PreNotification(indexDestination + l, 1);
						}
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), indexDestination, count, false, true, false, point);
					}
					for (int m = 0; m < count; m++)
					{
						this.DataGridView.OnInsertedRow_PostNotification(indexDestination + m, point, m == count - 1);
					}
					return;
				}
				if (this.IsCollectionChangedListenedTo)
				{
					this.UnshareRow(indexDestination);
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this.SharedRow(indexDestination)), indexDestination, 1, false, true, false, point);
				return;
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000A3F6C File Offset: 0x000A216C
		private void InsertDuplicateRow(int indexDestination, DataGridViewRow rowTemplate, bool firstInsertion, ref Point newCurrentCell)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)rowTemplate.Clone();
			dataGridViewRow.StateInternal = DataGridViewElementStates.None;
			dataGridViewRow.DataGridViewInternal = this.dataGridView;
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			int num = 0;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.DataGridViewInternal = this.dataGridView;
				dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num];
				num++;
			}
			DataGridViewElementStates dataGridViewElementStates = rowTemplate.State & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			if (dataGridViewRow.HasHeaderCell)
			{
				dataGridViewRow.HeaderCell.DataGridViewInternal = this.dataGridView;
				dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
			}
			this.DataGridView.OnInsertingRow(indexDestination, dataGridViewRow, dataGridViewElementStates, ref newCurrentCell, firstInsertion, 1, false);
			this.SharedList.Insert(indexDestination, dataGridViewRow);
			this.rowStates.Insert(indexDestination, dataGridViewElementStates);
		}

		/// <summary>Inserts the <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects into the collection at the specified position.</summary>
		/// <param name="rowIndex">The position at which to insert the rows.</param>
		/// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRows" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRows" /> contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="rowIndex" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is <see langword="true" />.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not <see langword="null" />.  
		///  -or-  
		///  At least one entry in the <paramref name="dataGridViewRows" /> array is <see langword="null" />.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not <see langword="null" />.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of <see langword="true" />.  
		///  -or-  
		///  Two or more rows in the <paramref name="dataGridViewRows" /> array are identical.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.  
		///  -or-  
		///  At least one row in the <paramref name="dataGridViewRows" /> array contains more cells than there are columns in the control.  
		///  -or-  
		///  This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
		// Token: 0x06002257 RID: 8791 RVA: 0x000A4070 File Offset: 0x000A2270
		public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
		{
			if (dataGridViewRows == null)
			{
				throw new ArgumentNullException("dataGridViewRows");
			}
			if (dataGridViewRows.Length == 1)
			{
				this.Insert(rowIndex, dataGridViewRows[0]);
				return;
			}
			if (rowIndex < 0 || rowIndex > this.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("DataGridViewRowCollection_IndexDestinationOutOfRange"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoInsertionAfterNewRow"));
			}
			if (this.DataGridView.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_AddUnboundRow"));
			}
			if (this.DataGridView.Columns.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_NoColumns"));
			}
			Point point = new Point(-1, -1);
			this.DataGridView.OnInsertingRows(rowIndex, dataGridViewRows, ref point);
			int num = rowIndex;
			foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
			{
				int num2 = 0;
				foreach (object obj in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = this.dataGridView;
					if (dataGridViewCell.ColumnIndex == -1)
					{
						dataGridViewCell.OwningColumnInternal = this.DataGridView.Columns[num2];
					}
					num2++;
				}
				if (dataGridViewRow.HasHeaderCell)
				{
					dataGridViewRow.HeaderCell.DataGridViewInternal = this.DataGridView;
					dataGridViewRow.HeaderCell.OwningRowInternal = dataGridViewRow;
				}
				this.SharedList.Insert(num, dataGridViewRow);
				this.rowStates.Insert(num, dataGridViewRow.State);
				dataGridViewRow.IndexInternal = num;
				dataGridViewRow.DataGridViewInternal = this.dataGridView;
				num++;
			}
			this.DataGridView.OnInsertedRows_PreNotification(rowIndex, dataGridViewRows);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), rowIndex, dataGridViewRows.Length, false, true, false, point);
			this.DataGridView.OnInsertedRows_PostNotification(dataGridViewRows, point);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000A4290 File Offset: 0x000A2490
		internal void InvalidateCachedRowCount(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedRowCounts();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Frozen)
			{
				this.rowCountsVisibleFrozen = -1;
				return;
			}
			if (includeFilter == DataGridViewElementStates.Selected)
			{
				this.rowCountsVisibleSelected = -1;
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000A42B8 File Offset: 0x000A24B8
		internal void InvalidateCachedRowCounts()
		{
			this.rowCountsVisible = (this.rowCountsVisibleFrozen = (this.rowCountsVisibleSelected = -1));
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000A42DE File Offset: 0x000A24DE
		internal void InvalidateCachedRowsHeight(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedRowsHeights();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Frozen)
			{
				this.rowsHeightVisibleFrozen = -1;
			}
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000A42F8 File Offset: 0x000A24F8
		internal void InvalidateCachedRowsHeights()
		{
			this.rowsHeightVisible = (this.rowsHeightVisibleFrozen = -1);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewRowCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x0600225C RID: 8796 RVA: 0x000A4315 File Offset: 0x000A2515
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000A432C File Offset: 0x000A252C
		private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount)
		{
			Point point = new Point(-1, -1);
			DataGridViewRow dataGridViewRow = (DataGridViewRow)e.Element;
			int num = 0;
			if (dataGridViewRow != null && e.Action == CollectionChangeAction.Add)
			{
				num = this.SharedRow(rowIndex).Index;
			}
			this.OnCollectionChanged_PreNotification(e.Action, rowIndex, rowCount, ref dataGridViewRow, false);
			if (num == -1 && this.SharedRow(rowIndex).Index != -1)
			{
				e = new CollectionChangeEventArgs(e.Action, dataGridViewRow);
			}
			this.OnCollectionChanged(e);
			this.OnCollectionChanged_PostNotification(e.Action, rowIndex, rowCount, dataGridViewRow, false, false, false, point);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000A43B8 File Offset: 0x000A25B8
		private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)e.Element;
			int num = 0;
			if (dataGridViewRow != null && e.Action == CollectionChangeAction.Add)
			{
				num = this.SharedRow(rowIndex).Index;
			}
			this.OnCollectionChanged_PreNotification(e.Action, rowIndex, rowCount, ref dataGridViewRow, changeIsInsertion);
			if (num == -1 && this.SharedRow(rowIndex).Index != -1)
			{
				e = new CollectionChangeEventArgs(e.Action, dataGridViewRow);
			}
			this.OnCollectionChanged(e);
			this.OnCollectionChanged_PostNotification(e.Action, rowIndex, rowCount, dataGridViewRow, changeIsDeletion, changeIsInsertion, recreateNewRow, newCurrentCell);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000A4440 File Offset: 0x000A2640
		private void OnCollectionChanged_PreNotification(CollectionChangeAction cca, int rowIndex, int rowCount, ref DataGridViewRow dataGridViewRow, bool changeIsInsertion)
		{
			bool flag = false;
			bool flag2 = false;
			switch (cca)
			{
			case CollectionChangeAction.Add:
			{
				int num = 0;
				this.UpdateRowCaches(rowIndex, ref dataGridViewRow, true);
				if ((this.GetRowState(rowIndex) & DataGridViewElementStates.Visible) == DataGridViewElementStates.None)
				{
					flag = true;
					flag2 = changeIsInsertion;
				}
				else
				{
					int firstDisplayedRowIndex = this.DataGridView.FirstDisplayedRowIndex;
					if (firstDisplayedRowIndex != -1)
					{
						num = this.SharedRow(firstDisplayedRowIndex).GetHeight(firstDisplayedRowIndex);
					}
				}
				if (changeIsInsertion)
				{
					this.DataGridView.OnInsertedRow_PreNotification(rowIndex, 1);
					if (!flag)
					{
						if ((this.GetRowState(rowIndex) & DataGridViewElementStates.Frozen) != DataGridViewElementStates.None)
						{
							flag = this.DataGridView.FirstDisplayedScrollingRowIndex == -1 && this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height);
						}
						else if (this.DataGridView.FirstDisplayedScrollingRowIndex != -1 && rowIndex > this.DataGridView.FirstDisplayedScrollingRowIndex)
						{
							flag = this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height + this.DataGridView.VerticalScrollingOffset) && num <= this.DataGridView.LayoutInfo.Data.Height;
						}
					}
				}
				else
				{
					this.DataGridView.OnAddedRow_PreNotification(rowIndex);
					if (!flag)
					{
						int num2 = this.GetRowsHeight(DataGridViewElementStates.Visible) - this.DataGridView.VerticalScrollingOffset - dataGridViewRow.GetHeight(rowIndex);
						dataGridViewRow = this.SharedRow(rowIndex);
						flag = this.DataGridView.LayoutInfo.Data.Height < num2 && num <= this.DataGridView.LayoutInfo.Data.Height;
					}
				}
				break;
			}
			case CollectionChangeAction.Remove:
			{
				DataGridViewElementStates rowState = this.GetRowState(rowIndex);
				bool flag3 = (rowState & DataGridViewElementStates.Visible) > DataGridViewElementStates.None;
				bool flag4 = (rowState & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None;
				this.rowStates.RemoveAt(rowIndex);
				this.SharedList.RemoveAt(rowIndex);
				this.DataGridView.OnRemovedRow_PreNotification(rowIndex);
				if (flag3)
				{
					if (flag4)
					{
						flag = this.DataGridView.FirstDisplayedScrollingRowIndex == -1 && this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height + SystemInformation.HorizontalScrollBarHeight);
					}
					else if (this.DataGridView.FirstDisplayedScrollingRowIndex != -1 && rowIndex > this.DataGridView.FirstDisplayedScrollingRowIndex)
					{
						int num3 = 0;
						int firstDisplayedRowIndex2 = this.DataGridView.FirstDisplayedRowIndex;
						if (firstDisplayedRowIndex2 != -1)
						{
							num3 = this.SharedRow(firstDisplayedRowIndex2).GetHeight(firstDisplayedRowIndex2);
						}
						flag = this.GetRowsHeightExceedLimit(DataGridViewElementStates.Visible, 0, rowIndex, this.DataGridView.LayoutInfo.Data.Height + this.DataGridView.VerticalScrollingOffset + SystemInformation.HorizontalScrollBarHeight) && num3 <= this.DataGridView.LayoutInfo.Data.Height;
					}
				}
				else
				{
					flag = true;
				}
				break;
			}
			case CollectionChangeAction.Refresh:
				this.InvalidateCachedRowCounts();
				this.InvalidateCachedRowsHeights();
				break;
			}
			this.DataGridView.ResetUIState(flag, flag2);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000A4730 File Offset: 0x000A2930
		private void OnCollectionChanged_PostNotification(CollectionChangeAction cca, int rowIndex, int rowCount, DataGridViewRow dataGridViewRow, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
		{
			if (changeIsDeletion)
			{
				this.DataGridView.OnRowsRemovedInternal(rowIndex, rowCount);
			}
			else
			{
				this.DataGridView.OnRowsAddedInternal(rowIndex, rowCount);
			}
			switch (cca)
			{
			case CollectionChangeAction.Add:
				if (changeIsInsertion)
				{
					this.DataGridView.OnInsertedRow_PostNotification(rowIndex, newCurrentCell, true);
				}
				else
				{
					this.DataGridView.OnAddedRow_PostNotification(rowIndex);
				}
				break;
			case CollectionChangeAction.Remove:
				this.DataGridView.OnRemovedRow_PostNotification(dataGridViewRow, newCurrentCell);
				break;
			case CollectionChangeAction.Refresh:
				if (changeIsDeletion)
				{
					this.DataGridView.OnClearedRows();
				}
				break;
			}
			this.DataGridView.OnRowCollectionChanged_PostNotification(recreateNewRow, newCurrentCell.X == -1, cca, dataGridViewRow, rowIndex);
		}

		/// <summary>Removes the row from the collection.</summary>
		/// <param name="dataGridViewRow">The row to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewRow" /> is not contained in this collection.  
		/// -or-  
		/// <paramref name="dataGridViewRow" /> is a shared row.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="dataGridViewRow" /> is the row for new records.  
		///  -or-  
		///  The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both <see langword="true" />.</exception>
		// Token: 0x06002261 RID: 8801 RVA: 0x000A47D4 File Offset: 0x000A29D4
		public virtual void Remove(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			if (dataGridViewRow.DataGridView != this.DataGridView)
			{
				throw new ArgumentException(SR.GetString("DataGridView_RowDoesNotBelongToDataGridView"), "dataGridViewRow");
			}
			if (dataGridViewRow.Index == -1)
			{
				throw new ArgumentException(SR.GetString("DataGridView_RowMustBeUnshared"), "dataGridViewRow");
			}
			this.RemoveAt(dataGridViewRow.Index);
		}

		/// <summary>Removes the row at the specified position from the collection.</summary>
		/// <param name="index">The position of the row to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero and greater than the number of rows in the collection minus one.</exception>
		/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:  
		///
		/// Selecting all cells in the control.  
		///
		/// Clearing the selection.  
		///
		///
		///  -or-  
		///  This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:  
		///
		/// <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />  
		///
		///
		///  -or-  
		///  <paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to <see langword="true" />.  
		///  -or-  
		///  The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both <see langword="true" />.</exception>
		// Token: 0x06002262 RID: 8802 RVA: 0x000A483C File Offset: 0x000A2A3C
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("DataGridViewRowCollection_RowIndexOutOfRange"));
			}
			if (this.DataGridView.NewRowIndex == index)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CannotDeleteNewRow"));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.DataSource == null)
			{
				this.RemoveAtInternal(index, false);
				return;
			}
			IBindingList bindingList = this.DataGridView.DataConnection.List as IBindingList;
			if (bindingList != null && bindingList.AllowRemove && bindingList.SupportsChangeNotification)
			{
				bindingList.RemoveAt(index);
				return;
			}
			throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_CantRemoveRowsWithWrongSource"));
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000A48FC File Offset: 0x000A2AFC
		internal void RemoveAtInternal(int index, bool force)
		{
			DataGridViewRow dataGridViewRow = this.SharedRow(index);
			Point point = new Point(-1, -1);
			if (this.IsCollectionChangedListenedTo || dataGridViewRow.GetDisplayed(index))
			{
				dataGridViewRow = this[index];
			}
			dataGridViewRow = this.SharedRow(index);
			this.DataGridView.OnRemovingRow(index, out point, force);
			this.UpdateRowCaches(index, ref dataGridViewRow, false);
			if (dataGridViewRow.Index != -1)
			{
				this.rowStates[index] = dataGridViewRow.State;
				dataGridViewRow.DetachFromDataGridView();
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewRow), index, 1, true, false, false, point);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000A4988 File Offset: 0x000A2B88
		private static bool RowHasValueOrToolTipText(DataGridViewRow dataGridViewRow)
		{
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if (dataGridViewCell.HasValue || dataGridViewCell.HasToolTipText)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000A49F8 File Offset: 0x000A2BF8
		internal bool RowIsSharable(int index)
		{
			DataGridViewRow dataGridViewRow = this.SharedRow(index);
			if (dataGridViewRow.Index != -1)
			{
				return false;
			}
			DataGridViewCellCollection cells = dataGridViewRow.Cells;
			foreach (object obj in cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				if ((dataGridViewCell.State & ~(dataGridViewCell.CellStateFromColumnRowStates(this.rowStates[index]) != DataGridViewElementStates.None)) != DataGridViewElementStates.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000A4A88 File Offset: 0x000A2C88
		internal void SetRowState(int rowIndex, DataGridViewElementStates state, bool value)
		{
			DataGridViewRow dataGridViewRow = this.SharedRow(rowIndex);
			if (dataGridViewRow.Index == -1)
			{
				if ((this.rowStates[rowIndex] & state) > DataGridViewElementStates.None != value)
				{
					if (state == DataGridViewElementStates.Frozen || state == DataGridViewElementStates.Visible || state == DataGridViewElementStates.ReadOnly)
					{
						dataGridViewRow.OnSharedStateChanging(rowIndex, state);
					}
					if (value)
					{
						this.rowStates[rowIndex] = this.rowStates[rowIndex] | state;
					}
					else
					{
						this.rowStates[rowIndex] = this.rowStates[rowIndex] & ~state;
					}
					dataGridViewRow.OnSharedStateChanged(rowIndex, state);
					return;
				}
			}
			else if (state <= DataGridViewElementStates.Resizable)
			{
				switch (state)
				{
				case DataGridViewElementStates.Displayed:
					dataGridViewRow.DisplayedInternal = value;
					return;
				case DataGridViewElementStates.Frozen:
					dataGridViewRow.Frozen = value;
					return;
				case DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen:
					break;
				case DataGridViewElementStates.ReadOnly:
					dataGridViewRow.ReadOnlyInternal = value;
					return;
				default:
					if (state != DataGridViewElementStates.Resizable)
					{
						return;
					}
					dataGridViewRow.Resizable = (value ? DataGridViewTriState.True : DataGridViewTriState.False);
					break;
				}
			}
			else
			{
				if (state == DataGridViewElementStates.Selected)
				{
					dataGridViewRow.SelectedInternal = value;
					return;
				}
				if (state != DataGridViewElementStates.Visible)
				{
					return;
				}
				dataGridViewRow.Visible = value;
				return;
			}
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000A4B76 File Offset: 0x000A2D76
		internal DataGridViewElementStates SharedRowState(int rowIndex)
		{
			return this.rowStates[rowIndex];
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000A4B84 File Offset: 0x000A2D84
		internal void Sort(IComparer customComparer, bool ascending)
		{
			if (this.items.Count > 0)
			{
				DataGridViewRowCollection.RowComparer rowComparer = new DataGridViewRowCollection.RowComparer(this, customComparer, ascending);
				this.items.CustomSort(rowComparer);
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000A4BB4 File Offset: 0x000A2DB4
		internal void SwapSortedRows(int rowIndex1, int rowIndex2)
		{
			this.DataGridView.SwapSortedRows(rowIndex1, rowIndex2);
			DataGridViewRow dataGridViewRow = this.SharedRow(rowIndex1);
			DataGridViewRow dataGridViewRow2 = this.SharedRow(rowIndex2);
			if (dataGridViewRow.Index != -1)
			{
				dataGridViewRow.IndexInternal = rowIndex2;
			}
			if (dataGridViewRow2.Index != -1)
			{
				dataGridViewRow2.IndexInternal = rowIndex1;
			}
			if (this.DataGridView.VirtualMode)
			{
				int count = this.DataGridView.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[i];
					DataGridViewCell dataGridViewCell2 = dataGridViewRow2.Cells[i];
					object valueInternal = dataGridViewCell.GetValueInternal(rowIndex1);
					object valueInternal2 = dataGridViewCell2.GetValueInternal(rowIndex2);
					dataGridViewCell.SetValueInternal(rowIndex1, valueInternal2);
					dataGridViewCell2.SetValueInternal(rowIndex2, valueInternal);
				}
			}
			object obj = this.items[rowIndex1];
			this.items[rowIndex1] = this.items[rowIndex2];
			this.items[rowIndex2] = obj;
			DataGridViewElementStates dataGridViewElementStates = this.rowStates[rowIndex1];
			this.rowStates[rowIndex1] = this.rowStates[rowIndex2];
			this.rowStates[rowIndex2] = dataGridViewElementStates;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000A4CDA File Offset: 0x000A2EDA
		private void UnshareRow(int rowIndex)
		{
			this.SharedRow(rowIndex).IndexInternal = rowIndex;
			this.SharedRow(rowIndex).StateInternal = this.SharedRowState(rowIndex);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000A4CFC File Offset: 0x000A2EFC
		private void UpdateRowCaches(int rowIndex, ref DataGridViewRow dataGridViewRow, bool adding)
		{
			if (this.rowCountsVisible != -1 || this.rowCountsVisibleFrozen != -1 || this.rowCountsVisibleSelected != -1 || this.rowsHeightVisible != -1 || this.rowsHeightVisibleFrozen != -1)
			{
				DataGridViewElementStates rowState = this.GetRowState(rowIndex);
				if ((rowState & DataGridViewElementStates.Visible) != DataGridViewElementStates.None)
				{
					int num = (adding ? 1 : (-1));
					int num2 = 0;
					if (this.rowsHeightVisible != -1 || (this.rowsHeightVisibleFrozen != -1 && (rowState & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)))
					{
						num2 = (adding ? dataGridViewRow.GetHeight(rowIndex) : (-dataGridViewRow.GetHeight(rowIndex)));
						dataGridViewRow = this.SharedRow(rowIndex);
					}
					if (this.rowCountsVisible != -1)
					{
						this.rowCountsVisible += num;
					}
					if (this.rowsHeightVisible != -1)
					{
						this.rowsHeightVisible += num2;
					}
					if ((rowState & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
					{
						if (this.rowCountsVisibleFrozen != -1)
						{
							this.rowCountsVisibleFrozen += num;
						}
						if (this.rowsHeightVisibleFrozen != -1)
						{
							this.rowsHeightVisibleFrozen += num2;
						}
					}
					if ((rowState & (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible) && this.rowCountsVisibleSelected != -1)
					{
						this.rowCountsVisibleSelected += num;
					}
				}
			}
		}

		// Token: 0x04000E1C RID: 3612
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000E1D RID: 3613
		private DataGridViewRowCollection.RowArrayList items;

		// Token: 0x04000E1E RID: 3614
		private List<DataGridViewElementStates> rowStates;

		// Token: 0x04000E1F RID: 3615
		private int rowCountsVisible;

		// Token: 0x04000E20 RID: 3616
		private int rowCountsVisibleFrozen;

		// Token: 0x04000E21 RID: 3617
		private int rowCountsVisibleSelected;

		// Token: 0x04000E22 RID: 3618
		private int rowsHeightVisible;

		// Token: 0x04000E23 RID: 3619
		private int rowsHeightVisibleFrozen;

		// Token: 0x04000E24 RID: 3620
		private DataGridView dataGridView;

		// Token: 0x02000675 RID: 1653
		private class RowArrayList : ArrayList
		{
			// Token: 0x0600668D RID: 26253 RVA: 0x0017EFCB File Offset: 0x0017D1CB
			public RowArrayList(DataGridViewRowCollection owner)
			{
				this.owner = owner;
			}

			// Token: 0x0600668E RID: 26254 RVA: 0x0017EFDA File Offset: 0x0017D1DA
			public void CustomSort(DataGridViewRowCollection.RowComparer rowComparer)
			{
				this.rowComparer = rowComparer;
				this.CustomQuickSort(0, this.Count - 1);
			}

			// Token: 0x0600668F RID: 26255 RVA: 0x0017EFF4 File Offset: 0x0017D1F4
			private void CustomQuickSort(int left, int right)
			{
				while (right - left >= 2)
				{
					int num = left + right >> 1;
					object obj = this.Pivot(left, num, right);
					int num2 = left + 1;
					int num3 = right - 1;
					do
					{
						if (num != num2)
						{
							if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(num2), obj, num2, num) < 0)
							{
								num2++;
								continue;
							}
						}
						while (num != num3 && this.rowComparer.CompareObjects(obj, this.rowComparer.GetComparedObject(num3), num, num3) < 0)
						{
							num3--;
						}
						if (num2 > num3)
						{
							break;
						}
						if (num2 < num3)
						{
							this.owner.SwapSortedRows(num2, num3);
							if (num2 == num)
							{
								num = num3;
							}
							else if (num3 == num)
							{
								num = num2;
							}
						}
						num2++;
						num3--;
					}
					while (num2 <= num3);
					if (num3 - left <= right - num2)
					{
						if (left < num3)
						{
							this.CustomQuickSort(left, num3);
						}
						left = num2;
					}
					else
					{
						if (num2 < right)
						{
							this.CustomQuickSort(num2, right);
						}
						right = num3;
					}
					if (left >= right)
					{
						return;
					}
				}
				if (right - left > 0 && this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(right), left, right) > 0)
				{
					this.owner.SwapSortedRows(left, right);
				}
			}

			// Token: 0x06006690 RID: 26256 RVA: 0x0017F108 File Offset: 0x0017D308
			private object Pivot(int left, int center, int right)
			{
				if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(center), left, center) > 0)
				{
					this.owner.SwapSortedRows(left, center);
				}
				if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(left), this.rowComparer.GetComparedObject(right), left, right) > 0)
				{
					this.owner.SwapSortedRows(left, right);
				}
				if (this.rowComparer.CompareObjects(this.rowComparer.GetComparedObject(center), this.rowComparer.GetComparedObject(right), center, right) > 0)
				{
					this.owner.SwapSortedRows(center, right);
				}
				return this.rowComparer.GetComparedObject(center);
			}

			// Token: 0x04003A6C RID: 14956
			private DataGridViewRowCollection owner;

			// Token: 0x04003A6D RID: 14957
			private DataGridViewRowCollection.RowComparer rowComparer;
		}

		// Token: 0x02000676 RID: 1654
		private class RowComparer
		{
			// Token: 0x06006691 RID: 26257 RVA: 0x0017F1C0 File Offset: 0x0017D3C0
			public RowComparer(DataGridViewRowCollection dataGridViewRows, IComparer customComparer, bool ascending)
			{
				this.dataGridView = dataGridViewRows.DataGridView;
				this.dataGridViewRows = dataGridViewRows;
				this.dataGridViewSortedColumn = this.dataGridView.SortedColumn;
				if (this.dataGridViewSortedColumn == null)
				{
					this.sortedColumnIndex = -1;
				}
				else
				{
					this.sortedColumnIndex = this.dataGridViewSortedColumn.Index;
				}
				this.customComparer = customComparer;
				this.ascending = ascending;
			}

			// Token: 0x06006692 RID: 26258 RVA: 0x0017F228 File Offset: 0x0017D428
			internal object GetComparedObject(int rowIndex)
			{
				if (this.dataGridView.NewRowIndex != -1 && rowIndex == this.dataGridView.NewRowIndex)
				{
					return DataGridViewRowCollection.RowComparer.max;
				}
				if (this.customComparer == null)
				{
					DataGridViewRow dataGridViewRow = this.dataGridViewRows.SharedRow(rowIndex);
					return dataGridViewRow.Cells[this.sortedColumnIndex].GetValueInternal(rowIndex);
				}
				return this.dataGridViewRows[rowIndex];
			}

			// Token: 0x06006693 RID: 26259 RVA: 0x0017F290 File Offset: 0x0017D490
			internal int CompareObjects(object value1, object value2, int rowIndex1, int rowIndex2)
			{
				if (value1 is DataGridViewRowCollection.RowComparer.ComparedObjectMax)
				{
					return 1;
				}
				if (value2 is DataGridViewRowCollection.RowComparer.ComparedObjectMax)
				{
					return -1;
				}
				int num = 0;
				if (this.customComparer == null)
				{
					if (!this.dataGridView.OnSortCompare(this.dataGridViewSortedColumn, value1, value2, rowIndex1, rowIndex2, out num))
					{
						if (!(value1 is IComparable) && !(value2 is IComparable))
						{
							if (value1 == null)
							{
								if (value2 == null)
								{
									num = 0;
								}
								else
								{
									num = 1;
								}
							}
							else if (value2 == null)
							{
								num = -1;
							}
							else
							{
								num = Comparer.Default.Compare(value1.ToString(), value2.ToString());
							}
						}
						else
						{
							num = Comparer.Default.Compare(value1, value2);
						}
						if (num == 0)
						{
							if (this.ascending)
							{
								num = rowIndex1 - rowIndex2;
							}
							else
							{
								num = rowIndex2 - rowIndex1;
							}
						}
					}
				}
				else
				{
					num = this.customComparer.Compare(value1, value2);
				}
				if (this.ascending)
				{
					return num;
				}
				return -num;
			}

			// Token: 0x04003A6E RID: 14958
			private DataGridView dataGridView;

			// Token: 0x04003A6F RID: 14959
			private DataGridViewRowCollection dataGridViewRows;

			// Token: 0x04003A70 RID: 14960
			private DataGridViewColumn dataGridViewSortedColumn;

			// Token: 0x04003A71 RID: 14961
			private int sortedColumnIndex;

			// Token: 0x04003A72 RID: 14962
			private IComparer customComparer;

			// Token: 0x04003A73 RID: 14963
			private bool ascending;

			// Token: 0x04003A74 RID: 14964
			private static DataGridViewRowCollection.RowComparer.ComparedObjectMax max = new DataGridViewRowCollection.RowComparer.ComparedObjectMax();

			// Token: 0x020008B7 RID: 2231
			private class ComparedObjectMax
			{
			}
		}

		// Token: 0x02000677 RID: 1655
		private class UnsharingRowEnumerator : IEnumerator
		{
			// Token: 0x06006695 RID: 26261 RVA: 0x0017F35F File Offset: 0x0017D55F
			public UnsharingRowEnumerator(DataGridViewRowCollection owner)
			{
				this.owner = owner;
				this.current = -1;
			}

			// Token: 0x06006696 RID: 26262 RVA: 0x0017F375 File Offset: 0x0017D575
			bool IEnumerator.MoveNext()
			{
				if (this.current < this.owner.Count - 1)
				{
					this.current++;
					return true;
				}
				this.current = this.owner.Count;
				return false;
			}

			// Token: 0x06006697 RID: 26263 RVA: 0x0017F3AE File Offset: 0x0017D5AE
			void IEnumerator.Reset()
			{
				this.current = -1;
			}

			// Token: 0x1700165A RID: 5722
			// (get) Token: 0x06006698 RID: 26264 RVA: 0x0017F3B8 File Offset: 0x0017D5B8
			object IEnumerator.Current
			{
				get
				{
					if (this.current == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_EnumNotStarted"));
					}
					if (this.current == this.owner.Count)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowCollection_EnumFinished"));
					}
					return this.owner[this.current];
				}
			}

			// Token: 0x04003A75 RID: 14965
			private DataGridViewRowCollection owner;

			// Token: 0x04003A76 RID: 14966
			private int current;
		}
	}
}
