using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemMouseHover" /> event.</summary>
	// Token: 0x020002DC RID: 732
	[ComVisible(true)]
	public class ListViewItemMouseHoverEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemMouseHoverEventArgs" /> class.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> the mouse pointer is currently hovering over.</param>
		// Token: 0x06002E95 RID: 11925 RVA: 0x000D3111 File Offset: 0x000D1311
		public ListViewItemMouseHoverEventArgs(ListViewItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the item the mouse pointer is currently hovering over.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that the mouse pointer is currently hovering over.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x000D3120 File Offset: 0x000D1320
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001331 RID: 4913
		private readonly ListViewItem item;
	}
}
