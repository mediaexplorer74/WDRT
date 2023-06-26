using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the layout for an image contained in a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	// Token: 0x020001FE RID: 510
	public enum DataGridViewImageCellLayout
	{
		/// <summary>The layout specification has not been set.</summary>
		// Token: 0x04000DE5 RID: 3557
		NotSet,
		/// <summary>The graphic is displayed centered using its native resolution.</summary>
		// Token: 0x04000DE6 RID: 3558
		Normal,
		/// <summary>The graphic is stretched by the percentages required to fit the width and height of the containing cell.</summary>
		// Token: 0x04000DE7 RID: 3559
		Stretch,
		/// <summary>The graphic is uniformly enlarged until it fills the width or height of the containing cell.</summary>
		// Token: 0x04000DE8 RID: 3560
		Zoom
	}
}
