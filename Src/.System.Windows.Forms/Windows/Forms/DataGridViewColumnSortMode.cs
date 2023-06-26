using System;

namespace System.Windows.Forms
{
	/// <summary>Defines how a <see cref="T:System.Windows.Forms.DataGridView" /> column can be sorted by the user.</summary>
	// Token: 0x020001C4 RID: 452
	public enum DataGridViewColumnSortMode
	{
		/// <summary>The column can only be sorted programmatically, but it is not intended for sorting, so the column header will not include space for a sorting glyph.</summary>
		// Token: 0x04000D55 RID: 3413
		NotSortable,
		/// <summary>The user can sort the column by clicking the column header (or pressing F3 on a cell) unless the column headers are used for selection. A sorting glyph will be displayed automatically.</summary>
		// Token: 0x04000D56 RID: 3414
		Automatic,
		/// <summary>The column can only be sorted programmatically, and the column header will include space for a sorting glyph.</summary>
		// Token: 0x04000D57 RID: 3415
		Programmatic
	}
}
