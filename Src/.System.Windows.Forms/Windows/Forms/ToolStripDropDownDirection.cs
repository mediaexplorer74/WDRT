using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction in which a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is displayed relative to its parent control.</summary>
	// Token: 0x020003BD RID: 957
	public enum ToolStripDropDownDirection
	{
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed above and to the left of its parent control.</summary>
		// Token: 0x040024D6 RID: 9430
		AboveLeft,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed above and to the right of its parent control.</summary>
		// Token: 0x040024D7 RID: 9431
		AboveRight,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed below and to the left of its parent control.</summary>
		// Token: 0x040024D8 RID: 9432
		BelowLeft,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed below and to the right of its parent control.</summary>
		// Token: 0x040024D9 RID: 9433
		BelowRight,
		/// <summary>Compensates for nested drop-down controls and specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed to the left of its parent control.</summary>
		// Token: 0x040024DA RID: 9434
		Left,
		/// <summary>Compensates for nested drop-down controls and specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed to the right of its parent control.</summary>
		// Token: 0x040024DB RID: 9435
		Right,
		/// <summary>Compensates for nested drop-down controls and responds to the <see cref="T:System.Windows.Forms.RightToLeft" /> setting, specifying either <see cref="F:System.Windows.Forms.ToolStripDropDownDirection.Left" /> or <see cref="F:System.Windows.Forms.ToolStripDropDownDirection.Right" /> accordingly.</summary>
		// Token: 0x040024DC RID: 9436
		Default = 7
	}
}
