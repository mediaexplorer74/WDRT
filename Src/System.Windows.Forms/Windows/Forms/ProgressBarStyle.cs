using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the style that a <see cref="T:System.Windows.Forms.ProgressBar" /> uses to indicate the progress of an operation.</summary>
	// Token: 0x02000329 RID: 809
	public enum ProgressBarStyle
	{
		/// <summary>Indicates progress by increasing the number of segmented blocks in a <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		// Token: 0x04001ECE RID: 7886
		Blocks,
		/// <summary>Indicates progress by increasing the size of a smooth, continuous bar in a <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		// Token: 0x04001ECF RID: 7887
		Continuous,
		/// <summary>Indicates progress by continuously scrolling a block across a <see cref="T:System.Windows.Forms.ProgressBar" /> in a marquee fashion.</summary>
		// Token: 0x04001ED0 RID: 7888
		Marquee
	}
}
