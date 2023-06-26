using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies which edges of a visual style element to draw.</summary>
	// Token: 0x0200047B RID: 1147
	[Flags]
	public enum Edges
	{
		/// <summary>The left edge of the element.</summary>
		// Token: 0x0400336C RID: 13164
		Left = 1,
		/// <summary>The top edge of the element.</summary>
		// Token: 0x0400336D RID: 13165
		Top = 2,
		/// <summary>The right edge of the element.</summary>
		// Token: 0x0400336E RID: 13166
		Right = 4,
		/// <summary>The bottom edge of the element.</summary>
		// Token: 0x0400336F RID: 13167
		Bottom = 8,
		/// <summary>A diagonal border.</summary>
		// Token: 0x04003370 RID: 13168
		Diagonal = 16
	}
}
