using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position of the image on the control.</summary>
	// Token: 0x02000292 RID: 658
	public enum ImageLayout
	{
		/// <summary>The image is left-aligned at the top across the control's client rectangle.</summary>
		// Token: 0x040010E4 RID: 4324
		None,
		/// <summary>The image is tiled across the control's client rectangle.</summary>
		// Token: 0x040010E5 RID: 4325
		Tile,
		/// <summary>The image is centered within the control's client rectangle.</summary>
		// Token: 0x040010E6 RID: 4326
		Center,
		/// <summary>The image is streched across the control's client rectangle.</summary>
		// Token: 0x040010E7 RID: 4327
		Stretch,
		/// <summary>The image is enlarged within the control's client rectangle.</summary>
		// Token: 0x040010E8 RID: 4328
		Zoom
	}
}
