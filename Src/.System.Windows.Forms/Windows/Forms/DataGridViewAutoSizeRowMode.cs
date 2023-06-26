using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the height of a row is adjusted.</summary>
	// Token: 0x0200019B RID: 411
	public enum DataGridViewAutoSizeRowMode
	{
		/// <summary>The row height adjusts to fit the contents of all cells in the row, including the header cell.</summary>
		// Token: 0x04000C62 RID: 3170
		AllCells = 3,
		/// <summary>The row height adjusts to fit the contents of all cells in the row, excluding the header cell.</summary>
		// Token: 0x04000C63 RID: 3171
		AllCellsExceptHeader = 2,
		/// <summary>The row height adjusts to fit the contents of the row header.</summary>
		// Token: 0x04000C64 RID: 3172
		RowHeader = 1
	}
}
