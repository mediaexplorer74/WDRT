using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a user starts cell editing in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001D2 RID: 466
	public enum DataGridViewEditMode
	{
		/// <summary>Editing begins when the cell receives focus. This mode is useful when pressing the TAB key to enter values across a row, or when pressing the ENTER key to enter values down a column.</summary>
		// Token: 0x04000DA9 RID: 3497
		EditOnEnter,
		/// <summary>Editing begins when any alphanumeric key is pressed while the cell has focus.</summary>
		// Token: 0x04000DAA RID: 3498
		EditOnKeystroke,
		/// <summary>Editing begins when any alphanumeric key or F2 is pressed while the cell has focus.</summary>
		// Token: 0x04000DAB RID: 3499
		EditOnKeystrokeOrF2,
		/// <summary>Editing begins when F2 is pressed while the cell has focus. This mode places the selection point at the end of the cell contents.</summary>
		// Token: 0x04000DAC RID: 3500
		EditOnF2,
		/// <summary>Editing begins only when the <see cref="M:System.Windows.Forms.DataGridView.BeginEdit(System.Boolean)" /> method is called.</summary>
		// Token: 0x04000DAD RID: 3501
		EditProgrammatically
	}
}
