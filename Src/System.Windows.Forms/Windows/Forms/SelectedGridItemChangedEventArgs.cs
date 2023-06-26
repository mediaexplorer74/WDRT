using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedGridItemChanged" /> event of the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	// Token: 0x02000362 RID: 866
	public class SelectedGridItemChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectedGridItemChangedEventArgs" /> class.</summary>
		/// <param name="oldSel">The previously selected grid item.</param>
		/// <param name="newSel">The newly selected grid item.</param>
		// Token: 0x0600387C RID: 14460 RVA: 0x000FA828 File Offset: 0x000F8A28
		public SelectedGridItemChangedEventArgs(GridItem oldSel, GridItem newSel)
		{
			this.oldSelection = oldSel;
			this.newSelection = newSel;
		}

		/// <summary>Gets the newly selected <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000FA83E File Offset: 0x000F8A3E
		public GridItem NewSelection
		{
			get
			{
				return this.newSelection;
			}
		}

		/// <summary>Gets the previously selected <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The old <see cref="T:System.Windows.Forms.GridItem" />. This can be <see langword="null" />.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x000FA846 File Offset: 0x000F8A46
		public GridItem OldSelection
		{
			get
			{
				return this.oldSelection;
			}
		}

		// Token: 0x040021C3 RID: 8643
		private GridItem oldSelection;

		// Token: 0x040021C4 RID: 8644
		private GridItem newSelection;
	}
}
