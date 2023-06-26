using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction in which the system arranges minimized windows.</summary>
	// Token: 0x02000124 RID: 292
	[ComVisible(true)]
	[Flags]
	public enum ArrangeDirection
	{
		/// <summary>Arranged vertically, from top to bottom. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopLeft" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopRight" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
		// Token: 0x040005F5 RID: 1525
		Down = 4,
		/// <summary>Arranged horizontally, from left to right. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomRight" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopRight" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
		// Token: 0x040005F6 RID: 1526
		Left = 0,
		/// <summary>Arranged horizontally, from right to left. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomLeft" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopLeft" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
		// Token: 0x040005F7 RID: 1527
		Right = 0,
		/// <summary>Arranged vertically, from bottom to top. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomLeft" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomRight" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
		// Token: 0x040005F8 RID: 1528
		Up = 4
	}
}
