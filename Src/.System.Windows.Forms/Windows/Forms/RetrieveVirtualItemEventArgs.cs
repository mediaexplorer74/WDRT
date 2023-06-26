using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event.</summary>
	// Token: 0x02000343 RID: 835
	public class RetrieveVirtualItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> class.</summary>
		/// <param name="itemIndex">The index of the item to retrieve.</param>
		// Token: 0x060035DF RID: 13791 RVA: 0x000F364A File Offset: 0x000F184A
		public RetrieveVirtualItemEventArgs(int itemIndex)
		{
			this.itemIndex = itemIndex;
		}

		/// <summary>Gets the index of the item to retrieve from the cache.</summary>
		/// <returns>The index of the item to retrieve from the cache.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000F3659 File Offset: 0x000F1859
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets or sets the item retrieved from the cache.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> retrieved from the cache.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000F3661 File Offset: 0x000F1861
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x000F3669 File Offset: 0x000F1869
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x04001F67 RID: 8039
		private int itemIndex;

		// Token: 0x04001F68 RID: 8040
		private ListViewItem item;
	}
}
