using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002D7 RID: 727
	internal class ListViewGroupItemCollection : ListView.ListViewItemCollection.IInnerList
	{
		// Token: 0x06002E13 RID: 11795 RVA: 0x000D1444 File Offset: 0x000CF644
		public ListViewGroupItemCollection(ListViewGroup group)
		{
			this.group = group;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000D1453 File Offset: 0x000CF653
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000D1460 File Offset: 0x000CF660
		private ArrayList Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ArrayList();
				}
				return this.items;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000D147B File Offset: 0x000CF67B
		public bool OwnerIsVirtualListView
		{
			get
			{
				return this.group.ListView != null && this.group.ListView.VirtualMode;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000D149C File Offset: 0x000CF69C
		public bool OwnerIsDesignMode
		{
			get
			{
				if (this.group.ListView != null)
				{
					ISite site = this.group.ListView.Site;
					return site != null && site.DesignMode;
				}
				return false;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		public ListViewItem this[int index]
		{
			get
			{
				return (ListViewItem)this.Items[index];
			}
			set
			{
				if (value != this.Items[index])
				{
					this.MoveToGroup((ListViewItem)this.Items[index], null);
					this.Items[index] = value;
					this.MoveToGroup((ListViewItem)this.Items[index], this.group);
				}
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000D1546 File Offset: 0x000CF746
		public ListViewItem Add(ListViewItem value)
		{
			this.CheckListViewItem(value);
			this.MoveToGroup(value, this.group);
			this.Items.Add(value);
			return value;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000D156C File Offset: 0x000CF76C
		public void AddRange(ListViewItem[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				this.CheckListViewItem(items[i]);
			}
			this.Items.AddRange(items);
			for (int j = 0; j < items.Length; j++)
			{
				this.MoveToGroup(items[j], this.group);
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000D15BC File Offset: 0x000CF7BC
		private void CheckListViewItem(ListViewItem item)
		{
			if (item.ListView != null && item.ListView != this.group.ListView)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { item.Text }), "item");
			}
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000D1608 File Offset: 0x000CF808
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.MoveToGroup(this[i], null);
			}
			this.Items.Clear();
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000D163F File Offset: 0x000CF83F
		public bool Contains(ListViewItem item)
		{
			return this.Items.Contains(item);
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000D164D File Offset: 0x000CF84D
		public void CopyTo(Array dest, int index)
		{
			this.Items.CopyTo(dest, index);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000D165C File Offset: 0x000CF85C
		public IEnumerator GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000D1669 File Offset: 0x000CF869
		public int IndexOf(ListViewItem item)
		{
			return this.Items.IndexOf(item);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000D1677 File Offset: 0x000CF877
		public ListViewItem Insert(int index, ListViewItem item)
		{
			this.CheckListViewItem(item);
			this.MoveToGroup(item, this.group);
			this.Items.Insert(index, item);
			return item;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000D169C File Offset: 0x000CF89C
		private void MoveToGroup(ListViewItem item, ListViewGroup newGroup)
		{
			ListViewGroup listViewGroup = item.Group;
			if (listViewGroup != newGroup)
			{
				item.group = newGroup;
				if (listViewGroup != null)
				{
					listViewGroup.Items.Remove(item);
				}
				this.UpdateNativeListViewItem(item);
			}
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000D16D1 File Offset: 0x000CF8D1
		public void Remove(ListViewItem item)
		{
			this.Items.Remove(item);
			if (item.group == this.group)
			{
				item.group = null;
				this.UpdateNativeListViewItem(item);
			}
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000D16FB File Offset: 0x000CF8FB
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000D170A File Offset: 0x000CF90A
		private void UpdateNativeListViewItem(ListViewItem item)
		{
			if (item.ListView != null && item.ListView.IsHandleCreated && !item.ListView.InsertingItemsNatively)
			{
				item.UpdateStateToListView(item.Index);
			}
		}

		// Token: 0x0400130C RID: 4876
		private ListViewGroup group;

		// Token: 0x0400130D RID: 4877
		private ArrayList items;
	}
}
