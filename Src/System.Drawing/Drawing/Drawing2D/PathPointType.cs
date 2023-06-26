using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of point in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
	// Token: 0x020000CE RID: 206
	public enum PathPointType
	{
		/// <summary>The starting point of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
		// Token: 0x040009F4 RID: 2548
		Start,
		/// <summary>A line segment.</summary>
		// Token: 0x040009F5 RID: 2549
		Line,
		/// <summary>A default Bézier curve.</summary>
		// Token: 0x040009F6 RID: 2550
		Bezier = 3,
		/// <summary>A mask point.</summary>
		// Token: 0x040009F7 RID: 2551
		PathTypeMask = 7,
		/// <summary>The corresponding segment is dashed.</summary>
		// Token: 0x040009F8 RID: 2552
		DashMode = 16,
		/// <summary>A path marker.</summary>
		// Token: 0x040009F9 RID: 2553
		PathMarker = 32,
		/// <summary>The endpoint of a subpath.</summary>
		// Token: 0x040009FA RID: 2554
		CloseSubpath = 128,
		/// <summary>A cubic Bézier curve.</summary>
		// Token: 0x040009FB RID: 2555
		Bezier3 = 3
	}
}
