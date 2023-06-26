using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies where a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out.</summary>
	// Token: 0x020003D8 RID: 984
	public enum ToolStripItemPlacement
	{
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out on the main <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x040025B4 RID: 9652
		Main,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out on the overflow <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x040025B5 RID: 9653
		Overflow,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is not to be layed out on the screen.</summary>
		// Token: 0x040025B6 RID: 9654
		None
	}
}
