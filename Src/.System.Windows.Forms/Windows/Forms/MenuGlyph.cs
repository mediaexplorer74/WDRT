using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the image to draw when drawing a menu with the <see cref="M:System.Windows.Forms.ControlPaint.DrawMenuGlyph(System.Drawing.Graphics,System.Drawing.Rectangle,System.Windows.Forms.MenuGlyph)" /> method.</summary>
	// Token: 0x020002F2 RID: 754
	public enum MenuGlyph
	{
		/// <summary>Draws a submenu arrow.</summary>
		// Token: 0x040013B1 RID: 5041
		Arrow,
		/// <summary>Draws a menu check mark.</summary>
		// Token: 0x040013B2 RID: 5042
		Checkmark,
		/// <summary>Draws a menu bullet.</summary>
		// Token: 0x040013B3 RID: 5043
		Bullet,
		/// <summary>The minimum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Arrow" /> value).</summary>
		// Token: 0x040013B4 RID: 5044
		Min = 0,
		/// <summary>The maximum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Bullet" /> value).</summary>
		// Token: 0x040013B5 RID: 5045
		Max = 2
	}
}
