using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the display and layout information for text strings.</summary>
	// Token: 0x0200044C RID: 1100
	[Flags]
	public enum TextFormatFlags
	{
		/// <summary>Aligns the text on the bottom of the bounding rectangle. Applied only when the text is a single line.</summary>
		// Token: 0x040028AC RID: 10412
		Bottom = 8,
		/// <summary>Removes the end of trimmed lines, and replaces them with an ellipsis.</summary>
		// Token: 0x040028AD RID: 10413
		EndEllipsis = 32768,
		/// <summary>Expands tab characters. The default number of characters per tab is eight. The <see cref="F:System.Windows.Forms.TextFormatFlags.WordEllipsis" />, <see cref="F:System.Windows.Forms.TextFormatFlags.PathEllipsis" />, and <see cref="F:System.Windows.Forms.TextFormatFlags.EndEllipsis" /> values cannot be used with <see cref="F:System.Windows.Forms.TextFormatFlags.ExpandTabs" />.</summary>
		// Token: 0x040028AE RID: 10414
		ExpandTabs = 64,
		/// <summary>Includes the font external leading in line height. Typically, external leading is not included in the height of a line of text.</summary>
		// Token: 0x040028AF RID: 10415
		ExternalLeading = 512,
		/// <summary>Applies the default formatting, which is left-aligned.</summary>
		// Token: 0x040028B0 RID: 10416
		Default = 0,
		/// <summary>Applies to Windows 2000 and Windows XP only:</summary>
		// Token: 0x040028B1 RID: 10417
		HidePrefix = 1048576,
		/// <summary>Centers the text horizontally within the bounding rectangle.</summary>
		// Token: 0x040028B2 RID: 10418
		HorizontalCenter = 1,
		/// <summary>Uses the system font to calculate text metrics.</summary>
		// Token: 0x040028B3 RID: 10419
		Internal = 4096,
		/// <summary>Aligns the text on the left side of the clipping area.</summary>
		// Token: 0x040028B4 RID: 10420
		Left = 0,
		/// <summary>Has no effect on the drawn text.</summary>
		// Token: 0x040028B5 RID: 10421
		ModifyString = 65536,
		/// <summary>Allows the overhanging parts of glyphs and unwrapped text reaching outside the formatting rectangle to show.</summary>
		// Token: 0x040028B6 RID: 10422
		NoClipping = 256,
		/// <summary>Turns off processing of prefix characters. Typically, the ampersand (&amp;) mnemonic-prefix character is interpreted as a directive to underscore the character that follows, and the double-ampersand (&amp;&amp;) mnemonic-prefix characters as a directive to print a single ampersand. By specifying <see cref="F:System.Windows.Forms.TextFormatFlags.NoPrefix" />, this processing is turned off. For example, an input string of "A&amp;bc&amp;&amp;d" with <see cref="F:System.Windows.Forms.TextFormatFlags.NoPrefix" /> applied would result in output of "A&amp;bc&amp;&amp;d".</summary>
		// Token: 0x040028B7 RID: 10423
		NoPrefix = 2048,
		/// <summary>Applies to Windows 98, Windows Me, Windows 2000, or Windows XP only:</summary>
		// Token: 0x040028B8 RID: 10424
		NoFullWidthCharacterBreak = 524288,
		/// <summary>Removes the center of trimmed lines and replaces it with an ellipsis.</summary>
		// Token: 0x040028B9 RID: 10425
		PathEllipsis = 16384,
		/// <summary>Applies to Windows 2000 or Windows XP only:</summary>
		// Token: 0x040028BA RID: 10426
		PrefixOnly = 2097152,
		/// <summary>Aligns the text on the right side of the clipping area.</summary>
		// Token: 0x040028BB RID: 10427
		Right = 2,
		/// <summary>Displays the text from right to left.</summary>
		// Token: 0x040028BC RID: 10428
		RightToLeft = 131072,
		/// <summary>Displays the text in a single line.</summary>
		// Token: 0x040028BD RID: 10429
		SingleLine = 32,
		/// <summary>Specifies the text should be formatted for display on a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		// Token: 0x040028BE RID: 10430
		TextBoxControl = 8192,
		/// <summary>Aligns the text on the top of the bounding rectangle.</summary>
		// Token: 0x040028BF RID: 10431
		Top = 0,
		/// <summary>Centers the text vertically, within the bounding rectangle.</summary>
		// Token: 0x040028C0 RID: 10432
		VerticalCenter = 4,
		/// <summary>Breaks the text at the end of a word.</summary>
		// Token: 0x040028C1 RID: 10433
		WordBreak = 16,
		/// <summary>Trims the line to the nearest word and an ellipsis is placed at the end of a trimmed line.</summary>
		// Token: 0x040028C2 RID: 10434
		WordEllipsis = 262144,
		/// <summary>Preserves the clipping specified by a <see cref="T:System.Drawing.Graphics" /> object. Applies only to methods receiving an <see cref="T:System.Drawing.IDeviceContext" /> that is a <see cref="T:System.Drawing.Graphics" />.</summary>
		// Token: 0x040028C3 RID: 10435
		PreserveGraphicsClipping = 16777216,
		/// <summary>Preserves the transformation specified by a <see cref="T:System.Drawing.Graphics" />. Applies only to methods receiving an <see cref="T:System.Drawing.IDeviceContext" /> that is a <see cref="T:System.Drawing.Graphics" />.</summary>
		// Token: 0x040028C4 RID: 10436
		PreserveGraphicsTranslateTransform = 33554432,
		/// <summary>Adds padding to the bounding rectangle to accommodate overhanging glyphs.</summary>
		// Token: 0x040028C5 RID: 10437
		GlyphOverhangPadding = 0,
		/// <summary>Does not add padding to the bounding rectangle.</summary>
		// Token: 0x040028C6 RID: 10438
		NoPadding = 268435456,
		/// <summary>Adds padding to both sides of the bounding rectangle.</summary>
		// Token: 0x040028C7 RID: 10439
		LeftAndRightPadding = 536870912
	}
}
