using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the text orientation to use with a particular <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" />.</summary>
	// Token: 0x02000407 RID: 1031
	public enum ToolStripTextDirection
	{
		/// <summary>Specifies that the text direction is inherited from the parent control.</summary>
		// Token: 0x040026DA RID: 9946
		Inherit,
		/// <summary>Specifies horizontal text orientation.</summary>
		// Token: 0x040026DB RID: 9947
		Horizontal,
		/// <summary>Specifies that text is to be rotated 90 degrees.</summary>
		// Token: 0x040026DC RID: 9948
		Vertical90,
		/// <summary>Specifies that text is to be rotated 270 degrees.</summary>
		// Token: 0x040026DD RID: 9949
		Vertical270
	}
}
