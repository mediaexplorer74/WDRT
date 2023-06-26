using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.ListView" /> control or a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	// Token: 0x020002D8 RID: 728
	public class ListViewHitTestInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewHitTestInfo" /> class.</summary>
		/// <param name="hitItem">The <see cref="T:System.Windows.Forms.ListViewItem" /> located at the position indicated by the hit test.</param>
		/// <param name="hitSubItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> located at the position indicated by the hit test.</param>
		/// <param name="hitLocation">One of the <see cref="T:System.Windows.Forms.ListViewHitTestLocations" /> values.</param>
		// Token: 0x06002E27 RID: 11815 RVA: 0x000D173A File Offset: 0x000CF93A
		public ListViewHitTestInfo(ListViewItem hitItem, ListViewItem.ListViewSubItem hitSubItem, ListViewHitTestLocations hitLocation)
		{
			this.item = hitItem;
			this.subItem = hitSubItem;
			this.loc = hitLocation;
		}

		/// <summary>Gets the location of a hit test on a <see cref="T:System.Windows.Forms.ListView" /> control, in relation to the <see cref="T:System.Windows.Forms.ListView" /> and the items it contains.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ListViewHitTestLocations" /> values.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000D1757 File Offset: 0x000CF957
		public ListViewHitTestLocations Location
		{
			get
			{
				return this.loc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000D175F File Offset: 0x000CF95F
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000D1767 File Offset: 0x000CF967
		public ListViewItem.ListViewSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
		}

		// Token: 0x0400130E RID: 4878
		private ListViewHitTestLocations loc;

		// Token: 0x0400130F RID: 4879
		private ListViewItem item;

		// Token: 0x04001310 RID: 4880
		private ListViewItem.ListViewSubItem subItem;
	}
}
