using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event.</summary>
	// Token: 0x020001A5 RID: 421
	public class DataGridViewCellContextMenuStripNeededEventArgs : DataGridViewCellEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs" /> class.</summary>
		/// <param name="columnIndex">The column index of cell that the event occurred for.</param>
		/// <param name="rowIndex">The row index of the cell that the event occurred for.</param>
		// Token: 0x06001E2A RID: 7722 RVA: 0x0008EE00 File Offset: 0x0008D000
		public DataGridViewCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex)
			: base(columnIndex, rowIndex)
		{
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0008EE0A File Offset: 0x0008D00A
		internal DataGridViewCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex, ContextMenuStrip contextMenuStrip)
			: base(columnIndex, rowIndex)
		{
			this.contextMenuStrip = contextMenuStrip;
		}

		/// <summary>Gets or sets the shortcut menu for the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for the cell.</returns>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0008EE1B File Offset: 0x0008D01B
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0008EE23 File Offset: 0x0008D023
		public ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		// Token: 0x04000CB4 RID: 3252
		private ContextMenuStrip contextMenuStrip;
	}
}
