using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the width of a column is adjusted.</summary>
	// Token: 0x02000197 RID: 407
	public enum DataGridViewAutoSizeColumnMode
	{
		/// <summary>The sizing behavior of the column is inherited from the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeColumnsMode" /> property.</summary>
		// Token: 0x04000C48 RID: 3144
		NotSet,
		/// <summary>The column width does not automatically adjust.</summary>
		// Token: 0x04000C49 RID: 3145
		None,
		/// <summary>The column width adjusts to fit the contents of all cells in the column, including the header cell.</summary>
		// Token: 0x04000C4A RID: 3146
		AllCells = 6,
		/// <summary>The column width adjusts to fit the contents of all cells in the column, excluding the header cell.</summary>
		// Token: 0x04000C4B RID: 3147
		AllCellsExceptHeader = 4,
		/// <summary>The column width adjusts to fit the contents of all cells in the column that are in rows currently displayed onscreen, including the header cell.</summary>
		// Token: 0x04000C4C RID: 3148
		DisplayedCells = 10,
		/// <summary>The column width adjusts to fit the contents of all cells in the column that are in rows currently displayed onscreen, excluding the header cell.</summary>
		// Token: 0x04000C4D RID: 3149
		DisplayedCellsExceptHeader = 8,
		/// <summary>The column width adjusts to fit the contents of the column header cell.</summary>
		// Token: 0x04000C4E RID: 3150
		ColumnHeader = 2,
		/// <summary>The column width adjusts so that the widths of all columns exactly fills the display area of the control, requiring horizontal scrolling only to keep column widths above the <see cref="P:System.Windows.Forms.DataGridViewColumn.MinimumWidth" /> property values. Relative column widths are determined by the relative <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values.</summary>
		// Token: 0x04000C4F RID: 3151
		Fill = 16
	}
}
