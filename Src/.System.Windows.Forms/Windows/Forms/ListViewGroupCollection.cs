using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents the collection of groups within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	// Token: 0x020002D5 RID: 725
	[ListBindable(false)]
	public class ListViewGroupCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x000D0CF3 File Offset: 0x000CEEF3
		internal ListViewGroupCollection(ListView listView)
		{
			this.listView = listView;
		}

		/// <summary>Gets the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000D0D02 File Offset: 0x000CEF02
		public int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object used to synchronize the collection.</returns>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x00006A49 File Offset: 0x00004C49
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x00012E4E File Offset: 0x0001104E
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x0001180C File Offset: 0x0000FA0C
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
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000D0D0F File Offset: 0x000CEF0F
		private ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ListViewGroupCollection.Count" />.</exception>
		// Token: 0x17000ABD RID: 2749
		public ListViewGroup this[int index]
		{
			get
			{
				return (ListViewGroup)this.List[index];
			}
			set
			{
				if (this.List.Contains(value))
				{
					return;
				}
				this.List[index] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> with the specified <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property value.</summary>
		/// <param name="key">The name of the group to get or set.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> with the specified name, or <see langword="null" /> if no such <see cref="T:System.Windows.Forms.ListViewGroup" /> exists.</returns>
		// Token: 0x17000ABE RID: 2750
		public ListViewGroup this[string key]
		{
			get
			{
				if (this.list == null)
				{
					return null;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					if (string.Compare(key, this[i].Name, false, CultureInfo.CurrentCulture) == 0)
					{
						return this[i];
					}
				}
				return null;
			}
			set
			{
				int num = -1;
				if (this.list == null)
				{
					return;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					if (string.Compare(key, this[i].Name, false, CultureInfo.CurrentCulture) == 0)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					this.list[num] = value;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroup" /> that represents the item located at the specified index within the collection.</returns>
		// Token: 0x17000ABF RID: 2751
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is ListViewGroup)
				{
					this[index] = (ListViewGroup)value;
				}
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection.</summary>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection.</param>
		/// <returns>The index of the group within the collection, or -1 if the group is already present in the collection.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="group" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		// Token: 0x06002DF8 RID: 11768 RVA: 0x000D0E2C File Offset: 0x000CF02C
		public int Add(ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return -1;
			}
			this.CheckListViewItems(group);
			group.ListViewInternal = this.listView;
			int num = this.List.Add(group);
			if (this.listView.IsHandleCreated)
			{
				this.listView.InsertGroupInListView(this.List.Count, group);
				this.MoveGroupItems(group);
			}
			return num;
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection using the specified values to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> and <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> properties</summary>
		/// <param name="key">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property for the new group.</param>
		/// <param name="headerText">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property for the new group.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.ListViewGroup" />.</returns>
		// Token: 0x06002DF9 RID: 11769 RVA: 0x000D0E90 File Offset: 0x000CF090
		public ListViewGroup Add(string key, string headerText)
		{
			ListViewGroup listViewGroup = new ListViewGroup(key, headerText);
			this.Add(listViewGroup);
			return listViewGroup;
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		/// <returns>The index at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> has been added.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ListViewGroup" />.  
		/// -or-  
		/// <paramref name="value" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		// Token: 0x06002DFA RID: 11770 RVA: 0x000D0EAE File Offset: 0x000CF0AE
		int IList.Add(object value)
		{
			if (value is ListViewGroup)
			{
				return this.Add((ListViewGroup)value);
			}
			throw new ArgumentException("value");
		}

		/// <summary>Adds an array of groups to the collection.</summary>
		/// <param name="groups">An array of type <see cref="T:System.Windows.Forms.ListViewGroup" /> that specifies the groups to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="groups" /> contains at least one group with at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		// Token: 0x06002DFB RID: 11771 RVA: 0x000D0ED0 File Offset: 0x000CF0D0
		public void AddRange(ListViewGroup[] groups)
		{
			for (int i = 0; i < groups.Length; i++)
			{
				this.Add(groups[i]);
			}
		}

		/// <summary>Adds the groups in an existing <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> to the collection.</summary>
		/// <param name="groups">A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> containing the groups to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="groups" /> contains at least one group with at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		// Token: 0x06002DFC RID: 11772 RVA: 0x000D0EF8 File Offset: 0x000CF0F8
		public void AddRange(ListViewGroupCollection groups)
		{
			for (int i = 0; i < groups.Count; i++)
			{
				this.Add(groups[i]);
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000D0F24 File Offset: 0x000CF124
		private void CheckListViewItems(ListViewGroup group)
		{
			for (int i = 0; i < group.Items.Count; i++)
			{
				ListViewItem listViewItem = group.Items[i];
				if (listViewItem.ListView != null && listViewItem.ListView != this.listView)
				{
					throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { listViewItem.Text }));
				}
			}
		}

		/// <summary>Removes all groups from the collection.</summary>
		// Token: 0x06002DFE RID: 11774 RVA: 0x000D0F8C File Offset: 0x000CF18C
		public void Clear()
		{
			if (this.listView.IsHandleCreated)
			{
				for (int i = 0; i < this.Count; i++)
				{
					this.listView.RemoveGroupFromListView(this[i]);
				}
			}
			for (int j = 0; j < this.Count; j++)
			{
				this[j].ListViewInternal = null;
			}
			this.List.Clear();
			this.listView.UpdateGroupView();
		}

		/// <summary>Determines whether the specified group is located in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the group is in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002DFF RID: 11775 RVA: 0x000D0FFD File Offset: 0x000CF1FD
		public bool Contains(ListViewGroup value)
		{
			return this.List.Contains(value);
		}

		/// <summary>Determines whether the specified value is located in the collection.</summary>
		/// <param name="value">An object that represents the <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ListViewGroup" /> contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E00 RID: 11776 RVA: 0x000D100B File Offset: 0x000CF20B
		bool IList.Contains(object value)
		{
			return value is ListViewGroup && this.Contains((ListViewGroup)value);
		}

		/// <summary>Copies the groups in the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to which the groups are copied.</param>
		/// <param name="index">The first index within the array to which the groups are copied.</param>
		// Token: 0x06002E01 RID: 11777 RVA: 0x000D1023 File Offset: 0x000CF223
		public void CopyTo(Array array, int index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection.</returns>
		// Token: 0x06002E02 RID: 11778 RVA: 0x000D1032 File Offset: 0x000CF232
		public IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> within the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection.</param>
		/// <returns>The zero-based index of the group within the collection, or -1 if the group is not in the collection.</returns>
		// Token: 0x06002E03 RID: 11779 RVA: 0x000D103F File Offset: 0x000CF23F
		public int IndexOf(ListViewGroup value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>Returns the index within the collection of the specified value.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to find in the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		/// <returns>The zero-based index of <paramref name="value" /> if it is in the collection; otherwise, -1.</returns>
		// Token: 0x06002E04 RID: 11780 RVA: 0x000D104D File Offset: 0x000CF24D
		int IList.IndexOf(object value)
		{
			if (value is ListViewGroup)
			{
				return this.IndexOf((ListViewGroup)value);
			}
			return -1;
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> into the collection at the specified index.</summary>
		/// <param name="index">The index within the collection at which to insert the group.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to insert into the collection.</param>
		// Token: 0x06002E05 RID: 11781 RVA: 0x000D1068 File Offset: 0x000CF268
		public void Insert(int index, ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return;
			}
			group.ListViewInternal = this.listView;
			this.List.Insert(index, group);
			if (this.listView.IsHandleCreated)
			{
				this.listView.InsertGroupInListView(index, group);
				this.MoveGroupItems(group);
			}
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.ListViewGroup" /> into the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="index">The position at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> is added to the collection.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection.</param>
		// Token: 0x06002E06 RID: 11782 RVA: 0x000D10B9 File Offset: 0x000CF2B9
		void IList.Insert(int index, object value)
		{
			if (value is ListViewGroup)
			{
				this.Insert(index, (ListViewGroup)value);
			}
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000D10D0 File Offset: 0x000CF2D0
		private void MoveGroupItems(ListViewGroup group)
		{
			foreach (object obj in group.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ListView == this.listView)
				{
					listViewItem.UpdateStateToListView(listViewItem.Index);
				}
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> from the collection.</summary>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the collection.</param>
		// Token: 0x06002E08 RID: 11784 RVA: 0x000D113C File Offset: 0x000CF33C
		public void Remove(ListViewGroup group)
		{
			group.ListViewInternal = null;
			this.List.Remove(group);
			if (this.listView.IsHandleCreated)
			{
				this.listView.RemoveGroupFromListView(group);
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		// Token: 0x06002E09 RID: 11785 RVA: 0x000D116A File Offset: 0x000CF36A
		void IList.Remove(object value)
		{
			if (value is ListViewGroup)
			{
				this.Remove((ListViewGroup)value);
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove.</param>
		// Token: 0x06002E0A RID: 11786 RVA: 0x000D1180 File Offset: 0x000CF380
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x0400130A RID: 4874
		private ListView listView;

		// Token: 0x0400130B RID: 4875
		private ArrayList list;
	}
}
