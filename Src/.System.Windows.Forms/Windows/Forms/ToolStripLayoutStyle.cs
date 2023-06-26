using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the possible alignments with which the items of a <see cref="T:System.Windows.Forms.ToolStrip" /> can be displayed.</summary>
	// Token: 0x020003DF RID: 991
	public enum ToolStripLayoutStyle
	{
		/// <summary>Specifies that items are laid out automatically.</summary>
		// Token: 0x040025D3 RID: 9683
		StackWithOverflow,
		/// <summary>Specifies that items are laid out horizontally and overflow as necessary.</summary>
		// Token: 0x040025D4 RID: 9684
		HorizontalStackWithOverflow,
		/// <summary>Specifies that items are laid out vertically, are centered within the control, and overflow as necessary.</summary>
		// Token: 0x040025D5 RID: 9685
		VerticalStackWithOverflow,
		/// <summary>Specifies that items flow horizontally or vertically as necessary.</summary>
		// Token: 0x040025D6 RID: 9686
		Flow,
		/// <summary>Specifies that items are laid out flush left.</summary>
		// Token: 0x040025D7 RID: 9687
		Table
	}
}
