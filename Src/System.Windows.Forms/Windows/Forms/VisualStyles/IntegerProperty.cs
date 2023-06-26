using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies the integer properties of a visual style element.</summary>
	// Token: 0x02000476 RID: 1142
	public enum IntegerProperty
	{
		/// <summary>The number of state images in multiple-image file.</summary>
		// Token: 0x04003336 RID: 13110
		ImageCount = 2401,
		/// <summary>The alpha value for an icon, between 0 and 255.</summary>
		// Token: 0x04003337 RID: 13111
		AlphaLevel,
		/// <summary>The size of the border line for elements with a filled-border background.</summary>
		// Token: 0x04003338 RID: 13112
		BorderSize,
		/// <summary>A percentage value that represents the width of a rounded corner, from 0 to 100.</summary>
		// Token: 0x04003339 RID: 13113
		RoundCornerWidth,
		/// <summary>A percentage value that represents the height of a rounded corner, from 0 to 100.</summary>
		// Token: 0x0400333A RID: 13114
		RoundCornerHeight,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor1" /> to use in a color gradient. The sum of the five <see langword="GradientRatio" /> properties must equal 255.</summary>
		// Token: 0x0400333B RID: 13115
		GradientRatio1,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor2" /> to use in a color gradient. The sum of the five <see langword="GradientRatio" /> properties must equal 255.</summary>
		// Token: 0x0400333C RID: 13116
		GradientRatio2,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor3" /> to use in a color gradient. The sum of the five <see langword="GradientRatio" /> properties must equal 255.</summary>
		// Token: 0x0400333D RID: 13117
		GradientRatio3,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor4" /> to use in a color gradient. The sum of the five <see langword="GradientRatio" /> properties must equal 255.</summary>
		// Token: 0x0400333E RID: 13118
		GradientRatio4,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor5" /> to use in a color gradient. The sum of the five <see langword="GradientRatio" /> properties must equal 255.</summary>
		// Token: 0x0400333F RID: 13119
		GradientRatio5,
		/// <summary>The size of progress bar elements.</summary>
		// Token: 0x04003340 RID: 13120
		ProgressChunkSize,
		/// <summary>The size of spaces between progress bar elements.</summary>
		// Token: 0x04003341 RID: 13121
		ProgressSpaceSize,
		/// <summary>The amount of saturation for an image, between 0 and 255.</summary>
		// Token: 0x04003342 RID: 13122
		Saturation,
		/// <summary>The size of the border around text characters.</summary>
		// Token: 0x04003343 RID: 13123
		TextBorderSize,
		/// <summary>The minimum alpha value of a solid pixel, between 0 and 255.</summary>
		// Token: 0x04003344 RID: 13124
		AlphaThreshold,
		/// <summary>The width of an element.</summary>
		// Token: 0x04003345 RID: 13125
		Width,
		/// <summary>The height of an element.</summary>
		// Token: 0x04003346 RID: 13126
		Height,
		/// <summary>The index into the font for font-based glyphs.</summary>
		// Token: 0x04003347 RID: 13127
		GlyphIndex,
		/// <summary>A percentage value indicating how far a fixed-size element will stretch when the target exceeds the source.</summary>
		// Token: 0x04003348 RID: 13128
		TrueSizeStretchMark,
		/// <summary>The minimum dots per inch (DPI) that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile1" /> was designed for.</summary>
		// Token: 0x04003349 RID: 13129
		MinDpi1,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile2" /> was designed for.</summary>
		// Token: 0x0400334A RID: 13130
		MinDpi2,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile3" /> was designed for.</summary>
		// Token: 0x0400334B RID: 13131
		MinDpi3,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile4" /> was designed for.</summary>
		// Token: 0x0400334C RID: 13132
		MinDpi4,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile5" /> was designed for.</summary>
		// Token: 0x0400334D RID: 13133
		MinDpi5
	}
}
