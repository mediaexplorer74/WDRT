using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the style of a three-dimensional border.</summary>
	// Token: 0x0200013E RID: 318
	[ComVisible(true)]
	public enum Border3DStyle
	{
		/// <summary>The border is drawn outside the specified rectangle, preserving the dimensions of the rectangle for drawing.</summary>
		// Token: 0x04000708 RID: 1800
		Adjust = 8192,
		/// <summary>The inner and outer edges of the border have a raised appearance.</summary>
		// Token: 0x04000709 RID: 1801
		Bump = 9,
		/// <summary>The inner and outer edges of the border have an etched appearance.</summary>
		// Token: 0x0400070A RID: 1802
		Etched = 6,
		/// <summary>The border has no three-dimensional effects.</summary>
		// Token: 0x0400070B RID: 1803
		Flat = 16394,
		/// <summary>The border has raised inner and outer edges.</summary>
		// Token: 0x0400070C RID: 1804
		Raised = 5,
		/// <summary>The border has a raised inner edge and no outer edge.</summary>
		// Token: 0x0400070D RID: 1805
		RaisedInner = 4,
		/// <summary>The border has a raised outer edge and no inner edge.</summary>
		// Token: 0x0400070E RID: 1806
		RaisedOuter = 1,
		/// <summary>The border has sunken inner and outer edges.</summary>
		// Token: 0x0400070F RID: 1807
		Sunken = 10,
		/// <summary>The border has a sunken inner edge and no outer edge.</summary>
		// Token: 0x04000710 RID: 1808
		SunkenInner = 8,
		/// <summary>The border has a sunken outer edge and no inner edge.</summary>
		// Token: 0x04000711 RID: 1809
		SunkenOuter = 2
	}
}
