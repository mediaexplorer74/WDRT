using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
	// Token: 0x020003CD RID: 973
	public class ToolStripItemClickedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> class, specifying the <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</summary>
		/// <param name="clickedItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</param>
		// Token: 0x06004307 RID: 17159 RVA: 0x0011C4F9 File Offset: 0x0011A6F9
		public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
		{
			this.clickedItem = clickedItem;
		}

		/// <summary>Gets the item that was clicked on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</returns>
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x0011C508 File Offset: 0x0011A708
		public ToolStripItem ClickedItem
		{
			get
			{
				return this.clickedItem;
			}
		}

		// Token: 0x04002592 RID: 9618
		private ToolStripItem clickedItem;
	}
}
