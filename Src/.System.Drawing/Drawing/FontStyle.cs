﻿using System;

namespace System.Drawing
{
	/// <summary>Specifies style information applied to text.</summary>
	// Token: 0x0200003D RID: 61
	[Flags]
	public enum FontStyle
	{
		/// <summary>Normal text.</summary>
		// Token: 0x04000347 RID: 839
		Regular = 0,
		/// <summary>Bold text.</summary>
		// Token: 0x04000348 RID: 840
		Bold = 1,
		/// <summary>Italic text.</summary>
		// Token: 0x04000349 RID: 841
		Italic = 2,
		/// <summary>Underlined text.</summary>
		// Token: 0x0400034A RID: 842
		Underline = 4,
		/// <summary>Text with a line through the middle.</summary>
		// Token: 0x0400034B RID: 843
		Strikeout = 8
	}
}
