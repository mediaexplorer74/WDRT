using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Describes the location of a point in the background specified by a visual style.</summary>
	// Token: 0x02000482 RID: 1154
	public enum HitTestCode
	{
		/// <summary>The hit test succeeded outside the control or on a transparent area.</summary>
		// Token: 0x040033B5 RID: 13237
		Nowhere,
		/// <summary>The hit test succeeded in the middle background segment.</summary>
		// Token: 0x040033B6 RID: 13238
		Client,
		/// <summary>The hit test succeeded in the left border segment.</summary>
		// Token: 0x040033B7 RID: 13239
		Left = 10,
		/// <summary>The hit test succeeded in the right border segment.</summary>
		// Token: 0x040033B8 RID: 13240
		Right,
		/// <summary>The hit test succeeded in the top border segment.</summary>
		// Token: 0x040033B9 RID: 13241
		Top,
		/// <summary>The hit test succeeded in the bottom border segment.</summary>
		// Token: 0x040033BA RID: 13242
		Bottom = 15,
		/// <summary>The hit test succeeded in the top and left border intersection.</summary>
		// Token: 0x040033BB RID: 13243
		TopLeft = 13,
		/// <summary>The hit test succeeded in the top and right border intersection.</summary>
		// Token: 0x040033BC RID: 13244
		TopRight,
		/// <summary>The hit test succeeded in the bottom and left border intersection.</summary>
		// Token: 0x040033BD RID: 13245
		BottomLeft = 16,
		/// <summary>The hit test succeeded in the bottom and right border intersection.</summary>
		// Token: 0x040033BE RID: 13246
		BottomRight
	}
}
