using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event.</summary>
	// Token: 0x020002DE RID: 734
	public class ListViewItemSelectionChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> class.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
		/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
		/// <param name="isSelected">
		///   <see langword="true" /> to indicate the item's state has changed to selected; <see langword="false" /> to indicate the item's state has changed to deselected.</param>
		// Token: 0x06002E9B RID: 11931 RVA: 0x000D3128 File Offset: 0x000D1328
		public ListViewItemSelectionChangedEventArgs(ListViewItem item, int itemIndex, bool isSelected)
		{
			this.item = item;
			this.itemIndex = itemIndex;
			this.isSelected = isSelected;
		}

		/// <summary>Gets a value indicating whether the item's state has changed to selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the item's state has changed to selected; <see langword="false" /> if the item's state has changed to deselected.</returns>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000D3145 File Offset: 0x000D1345
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		/// <summary>Gets the item whose selection state has changed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000D314D File Offset: 0x000D134D
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the index of the item whose selection state has changed.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000D3155 File Offset: 0x000D1355
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x04001332 RID: 4914
		private ListViewItem item;

		// Token: 0x04001333 RID: 4915
		private int itemIndex;

		// Token: 0x04001334 RID: 4916
		private bool isSelected;
	}
}
