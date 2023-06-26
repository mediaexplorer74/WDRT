using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies when the visual style selects a different glyph font size.</summary>
	// Token: 0x02000471 RID: 1137
	public enum GlyphFontSizingType
	{
		/// <summary>Glyph font sizes do not change.</summary>
		// Token: 0x040032FF RID: 13055
		None,
		/// <summary>Glyph font size changes are based on font size settings.</summary>
		// Token: 0x04003300 RID: 13056
		Size,
		/// <summary>Glyph font size changes are based on dots per inch (DPI) settings.</summary>
		// Token: 0x04003301 RID: 13057
		Dpi
	}
}
