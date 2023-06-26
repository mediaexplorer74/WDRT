using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the sides of a rectangle to apply a three-dimensional border to.</summary>
	// Token: 0x0200013D RID: 317
	[ComVisible(true)]
	[Flags]
	public enum Border3DSide
	{
		/// <summary>A three-dimensional border on the left edge of the rectangle.</summary>
		// Token: 0x04000701 RID: 1793
		Left = 1,
		/// <summary>A three-dimensional border on the top edge of the rectangle.</summary>
		// Token: 0x04000702 RID: 1794
		Top = 2,
		/// <summary>A three-dimensional border on the right side of the rectangle.</summary>
		// Token: 0x04000703 RID: 1795
		Right = 4,
		/// <summary>A three-dimensional border on the bottom side of the rectangle.</summary>
		// Token: 0x04000704 RID: 1796
		Bottom = 8,
		/// <summary>The interior of the rectangle is filled with the color defined for three-dimensional controls instead of the background color for the form.</summary>
		// Token: 0x04000705 RID: 1797
		Middle = 2048,
		/// <summary>A three-dimensional border on all four sides of the rectangle. The middle of the rectangle is filled with the color defined for three-dimensional controls.</summary>
		// Token: 0x04000706 RID: 1798
		All = 2063
	}
}
