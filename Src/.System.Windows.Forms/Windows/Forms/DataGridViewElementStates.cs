using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the user interface (UI) state of a element within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001D4 RID: 468
	[Flags]
	[ComVisible(true)]
	public enum DataGridViewElementStates
	{
		/// <summary>Indicates that an element is in its default state.</summary>
		// Token: 0x04000DB1 RID: 3505
		None = 0,
		/// <summary>Indicates the an element is currently displayed onscreen.</summary>
		// Token: 0x04000DB2 RID: 3506
		Displayed = 1,
		/// <summary>Indicates that an element cannot be scrolled through the UI.</summary>
		// Token: 0x04000DB3 RID: 3507
		Frozen = 2,
		/// <summary>Indicates that an element will not accept user input to change its value.</summary>
		// Token: 0x04000DB4 RID: 3508
		ReadOnly = 4,
		/// <summary>Indicates that an element can be resized through the UI. This value is ignored except when combined with the <see cref="F:System.Windows.Forms.DataGridViewElementStates.ResizableSet" /> value.</summary>
		// Token: 0x04000DB5 RID: 3509
		Resizable = 8,
		/// <summary>Indicates that an element does not inherit the resizable state of its parent.</summary>
		// Token: 0x04000DB6 RID: 3510
		ResizableSet = 16,
		/// <summary>Indicates that an element is in a selected (highlighted) UI state.</summary>
		// Token: 0x04000DB7 RID: 3511
		Selected = 32,
		/// <summary>Indicates that an element is visible (displayable).</summary>
		// Token: 0x04000DB8 RID: 3512
		Visible = 64
	}
}
