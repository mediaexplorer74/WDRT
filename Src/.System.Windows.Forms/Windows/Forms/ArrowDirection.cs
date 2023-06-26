using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction to move when getting items with the <see cref="M:System.Windows.Forms.ToolStrip.GetNextItem(System.Windows.Forms.ToolStripItem,System.Windows.Forms.ArrowDirection)" /> method.</summary>
	// Token: 0x02000122 RID: 290
	public enum ArrowDirection
	{
		/// <summary>The direction is up (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
		// Token: 0x040005E6 RID: 1510
		Up = 1,
		/// <summary>The direction is down (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
		// Token: 0x040005E7 RID: 1511
		Down = 17,
		/// <summary>The direction is left (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
		// Token: 0x040005E8 RID: 1512
		Left = 0,
		/// <summary>The direction is right (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
		// Token: 0x040005E9 RID: 1513
		Right = 16
	}
}
