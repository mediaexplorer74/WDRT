using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerDoubleClick" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x020001C1 RID: 449
	public class DataGridViewColumnDividerDoubleClickEventArgs : HandledMouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs" /> class.</summary>
		/// <param name="columnIndex">The index of the column next to the column divider that was double-clicked.</param>
		/// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> containing the inherited event data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.</exception>
		// Token: 0x06001FA9 RID: 8105 RVA: 0x0009598C File Offset: 0x00093B8C
		public DataGridViewColumnDividerDoubleClickEventArgs(int columnIndex, HandledMouseEventArgs e)
			: base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
		{
			if (columnIndex < -1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			this.columnIndex = columnIndex;
		}

		/// <summary>The index of the column next to the column divider that was double-clicked.</summary>
		/// <returns>The index of the column next to the divider.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000959D9 File Offset: 0x00093BD9
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x04000D43 RID: 3395
		private int columnIndex;
	}
}
