using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies the Boolean properties of a visual style element.</summary>
	// Token: 0x0200047A RID: 1146
	public enum BooleanProperty
	{
		/// <summary>The image has transparent areas.</summary>
		// Token: 0x0400335E RID: 13150
		Transparent = 2201,
		/// <summary>The width of nonclient captions varies with the extent of the text.</summary>
		// Token: 0x0400335F RID: 13151
		AutoSize,
		/// <summary>Only the border of an image is drawn.</summary>
		// Token: 0x04003360 RID: 13152
		BorderOnly,
		/// <summary>The control will handle composite drawing.</summary>
		// Token: 0x04003361 RID: 13153
		Composited,
		/// <summary>The background of a fixed-size element is a filled rectangle.</summary>
		// Token: 0x04003362 RID: 13154
		BackgroundFill,
		/// <summary>The glyph has transparent areas.</summary>
		// Token: 0x04003363 RID: 13155
		GlyphTransparent,
		/// <summary>Only the glyph should be drawn, not the background.</summary>
		// Token: 0x04003364 RID: 13156
		GlyphOnly,
		/// <summary>The sizing handle will always be displayed.</summary>
		// Token: 0x04003365 RID: 13157
		AlwaysShowSizingBar,
		/// <summary>The image is mirrored in right-to-left display modes.</summary>
		// Token: 0x04003366 RID: 13158
		MirrorImage,
		/// <summary>The height and width must be sized equally.</summary>
		// Token: 0x04003367 RID: 13159
		UniformSizing,
		/// <summary>The scaling factor must be an integer for fixed-size elements.</summary>
		// Token: 0x04003368 RID: 13160
		IntegralSizing,
		/// <summary>The source image will scale larger when needed.</summary>
		// Token: 0x04003369 RID: 13161
		SourceGrow,
		/// <summary>The source image will scale smaller when needed.</summary>
		// Token: 0x0400336A RID: 13162
		SourceShrink
	}
}
