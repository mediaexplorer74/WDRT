using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that indicate whether content is copied from a <see cref="T:System.Windows.Forms.DataGridView" /> control to the Clipboard.</summary>
	// Token: 0x020001BC RID: 444
	public enum DataGridViewClipboardCopyMode
	{
		/// <summary>Copying to the Clipboard is disabled.</summary>
		// Token: 0x04000D1A RID: 3354
		Disable,
		/// <summary>The text values of selected cells can be copied to the Clipboard. Row or column header text is included for rows or columns that contain selected cells only when the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property is set to <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" /> and at least one header is selected.</summary>
		// Token: 0x04000D1B RID: 3355
		EnableWithAutoHeaderText,
		/// <summary>The text values of selected cells can be copied to the Clipboard. Header text is not included.</summary>
		// Token: 0x04000D1C RID: 3356
		EnableWithoutHeaderText,
		/// <summary>The text values of selected cells can be copied to the Clipboard. Header text is included for rows and columns that contain selected cells.</summary>
		// Token: 0x04000D1D RID: 3357
		EnableAlwaysIncludeHeaderText
	}
}
