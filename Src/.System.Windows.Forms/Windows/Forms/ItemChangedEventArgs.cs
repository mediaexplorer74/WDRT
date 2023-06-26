using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
	// Token: 0x020002A6 RID: 678
	public class ItemChangedEventArgs : EventArgs
	{
		// Token: 0x06002A38 RID: 10808 RVA: 0x000BF8E6 File Offset: 0x000BDAE6
		internal ItemChangedEventArgs(int index)
		{
			this.index = index;
		}

		/// <summary>Indicates the position of the item being changed within the list.</summary>
		/// <returns>The zero-based index to the item being changed.</returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x000BF8F5 File Offset: 0x000BDAF5
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0400111F RID: 4383
		private int index;
	}
}
