using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the widths of columns are adjusted.</summary>
	// Token: 0x02000196 RID: 406
	public enum DataGridViewAutoSizeColumnsMode
	{
		/// <summary>The column widths adjust to fit the contents of all cells in the columns, including header cells.</summary>
		// Token: 0x04000C40 RID: 3136
		AllCells = 6,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns, excluding header cells.</summary>
		// Token: 0x04000C41 RID: 3137
		AllCellsExceptHeader = 4,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns that are in rows currently displayed onscreen, including header cells.</summary>
		// Token: 0x04000C42 RID: 3138
		DisplayedCells = 10,
		/// <summary>The column widths adjust to fit the contents of all cells in the columns that are in rows currently displayed onscreen, excluding header cells.</summary>
		// Token: 0x04000C43 RID: 3139
		DisplayedCellsExceptHeader = 8,
		/// <summary>The column widths do not automatically adjust.</summary>
		// Token: 0x04000C44 RID: 3140
		None = 1,
		/// <summary>The column widths adjust to fit the contents of the column header cells.</summary>
		// Token: 0x04000C45 RID: 3141
		ColumnHeader,
		/// <summary>The column widths adjust so that the widths of all columns exactly fill the display area of the control, requiring horizontal scrolling only to keep column widths above the <see cref="P:System.Windows.Forms.DataGridViewColumn.MinimumWidth" /> property values. Relative column widths are determined by the relative <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values.</summary>
		// Token: 0x04000C46 RID: 3142
		Fill = 16
	}
}
