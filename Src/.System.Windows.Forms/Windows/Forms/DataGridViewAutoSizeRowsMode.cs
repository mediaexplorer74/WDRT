using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the heights of rows are adjusted.</summary>
	// Token: 0x02000199 RID: 409
	public enum DataGridViewAutoSizeRowsMode
	{
		/// <summary>The row heights adjust to fit the contents of all cells in the rows, including header cells.</summary>
		// Token: 0x04000C57 RID: 3159
		AllCells = 7,
		/// <summary>The row heights adjust to fit the contents of all cells in the rows, excluding header cells.</summary>
		// Token: 0x04000C58 RID: 3160
		AllCellsExceptHeaders = 6,
		/// <summary>The row heights adjust to fit the contents of the row header.</summary>
		// Token: 0x04000C59 RID: 3161
		AllHeaders = 5,
		/// <summary>The row heights adjust to fit the contents of all cells in rows currently displayed onscreen, including header cells.</summary>
		// Token: 0x04000C5A RID: 3162
		DisplayedCells = 11,
		/// <summary>The row heights adjust to fit the contents of all cells in rows currently displayed onscreen, excluding header cells.</summary>
		// Token: 0x04000C5B RID: 3163
		DisplayedCellsExceptHeaders = 10,
		/// <summary>The row heights adjust to fit the contents of the row headers currently displayed onscreen.</summary>
		// Token: 0x04000C5C RID: 3164
		DisplayedHeaders = 9,
		/// <summary>The row heights do not automatically adjust.</summary>
		// Token: 0x04000C5D RID: 3165
		None = 0
	}
}
