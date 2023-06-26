using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the starting position that the system uses to arrange minimized windows.</summary>
	// Token: 0x02000125 RID: 293
	[Flags]
	public enum ArrangeStartingPosition
	{
		/// <summary>Starts at the lower-left corner of the screen, which is the default position.</summary>
		// Token: 0x040005FA RID: 1530
		BottomLeft = 0,
		/// <summary>Starts at the lower-right corner of the screen.</summary>
		// Token: 0x040005FB RID: 1531
		BottomRight = 1,
		/// <summary>Hides minimized windows by moving them off the visible area of the screen.</summary>
		// Token: 0x040005FC RID: 1532
		Hide = 8,
		/// <summary>Starts at the upper-left corner of the screen.</summary>
		// Token: 0x040005FD RID: 1533
		TopLeft = 2,
		/// <summary>Starts at the upper-right corner of the screen.</summary>
		// Token: 0x040005FE RID: 1534
		TopRight = 3
	}
}
