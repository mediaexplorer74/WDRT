using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how an image is positioned within a <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
	// Token: 0x0200031B RID: 795
	public enum PictureBoxSizeMode
	{
		/// <summary>The image is placed in the upper-left corner of the <see cref="T:System.Windows.Forms.PictureBox" />. The image is clipped if it is larger than the <see cref="T:System.Windows.Forms.PictureBox" /> it is contained in.</summary>
		// Token: 0x04001E96 RID: 7830
		Normal,
		/// <summary>The image within the <see cref="T:System.Windows.Forms.PictureBox" /> is stretched or shrunk to fit the size of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		// Token: 0x04001E97 RID: 7831
		StretchImage,
		/// <summary>The <see cref="T:System.Windows.Forms.PictureBox" /> is sized equal to the size of the image that it contains.</summary>
		// Token: 0x04001E98 RID: 7832
		AutoSize,
		/// <summary>The image is displayed in the center if the <see cref="T:System.Windows.Forms.PictureBox" /> is larger than the image. If the image is larger than the <see cref="T:System.Windows.Forms.PictureBox" />, the picture is placed in the center of the <see cref="T:System.Windows.Forms.PictureBox" /> and the outside edges are clipped.</summary>
		// Token: 0x04001E99 RID: 7833
		CenterImage,
		/// <summary>The size of the image is increased or decreased maintaining the size ratio.</summary>
		// Token: 0x04001E9A RID: 7834
		Zoom
	}
}
