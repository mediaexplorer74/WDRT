using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies how elements with a bitmap background will adjust to fill a bounds.</summary>
	// Token: 0x02000466 RID: 1126
	public enum SizingType
	{
		/// <summary>The element cannot be resized.</summary>
		// Token: 0x040032C4 RID: 12996
		FixedSize,
		/// <summary>The background image stretches to fill the bounds.</summary>
		// Token: 0x040032C5 RID: 12997
		Stretch,
		/// <summary>The background image repeats the pattern to fill the bounds.</summary>
		// Token: 0x040032C6 RID: 12998
		Tile
	}
}
