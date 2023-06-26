using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	// Token: 0x020002A8 RID: 680
	public class ItemCheckedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> class.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that is being checked or unchecked.</param>
		// Token: 0x06002A3E RID: 10814 RVA: 0x000BF8FD File Offset: 0x000BDAFD
		public ItemCheckedEventArgs(ListViewItem item)
		{
			this.lvi = item;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> whose checked state is changing.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose checked state is changing.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000BF90C File Offset: 0x000BDB0C
		public ListViewItem Item
		{
			get
			{
				return this.lvi;
			}
		}

		// Token: 0x04001120 RID: 4384
		private ListViewItem lvi;
	}
}
