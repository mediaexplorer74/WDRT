using System;

namespace System.Windows.Forms
{
	/// <summary>Describes how cells of a DataGridView control can be selected.</summary>
	// Token: 0x02000219 RID: 537
	public enum DataGridViewSelectionMode
	{
		/// <summary>One or more individual cells can be selected.</summary>
		// Token: 0x04000E60 RID: 3680
		CellSelect,
		/// <summary>The entire row will be selected by clicking its row's header or a cell contained in that row.</summary>
		// Token: 0x04000E61 RID: 3681
		FullRowSelect,
		/// <summary>The entire column will be selected by clicking the column's header or a cell contained in that column.</summary>
		// Token: 0x04000E62 RID: 3682
		FullColumnSelect,
		/// <summary>The row will be selected by clicking in the row's header cell. An individual cell can be selected by clicking that cell.</summary>
		// Token: 0x04000E63 RID: 3683
		RowHeaderSelect,
		/// <summary>The column will be selected by clicking in the column's header cell. An individual cell can be selected by clicking that cell.</summary>
		// Token: 0x04000E64 RID: 3684
		ColumnHeaderSelect
	}
}
