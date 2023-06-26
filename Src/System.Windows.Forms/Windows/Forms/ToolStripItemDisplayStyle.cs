using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies what to render (image or text) for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	// Token: 0x020003D0 RID: 976
	public enum ToolStripItemDisplayStyle
	{
		/// <summary>Specifies that neither image nor text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04002598 RID: 9624
		None,
		/// <summary>Specifies that only text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04002599 RID: 9625
		Text,
		/// <summary>Specifies that only an image is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x0400259A RID: 9626
		Image,
		/// <summary>Specifies that both an image and text are to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x0400259B RID: 9627
		ImageAndText
	}
}
