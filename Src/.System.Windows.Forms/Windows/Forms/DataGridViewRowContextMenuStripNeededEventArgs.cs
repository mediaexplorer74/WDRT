using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event.</summary>
	// Token: 0x02000209 RID: 521
	public class DataGridViewRowContextMenuStripNeededEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x0600226C RID: 8812 RVA: 0x000A4E11 File Offset: 0x000A3011
		public DataGridViewRowContextMenuStripNeededEventArgs(int rowIndex)
		{
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.rowIndex = rowIndex;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000A4E2F File Offset: 0x000A302F
		internal DataGridViewRowContextMenuStripNeededEventArgs(int rowIndex, ContextMenuStrip contextMenuStrip)
			: this(rowIndex)
		{
			this.contextMenuStrip = contextMenuStrip;
		}

		/// <summary>Gets the index of the row that is requesting a shortcut menu.</summary>
		/// <returns>The zero-based index of the row that is requesting a shortcut menu.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000A4E3F File Offset: 0x000A303F
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the row that raised the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> in use.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000A4E47 File Offset: 0x000A3047
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x000A4E4F File Offset: 0x000A304F
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

		// Token: 0x04000E25 RID: 3621
		private int rowIndex;

		// Token: 0x04000E26 RID: 3622
		private ContextMenuStrip contextMenuStrip;
	}
}
