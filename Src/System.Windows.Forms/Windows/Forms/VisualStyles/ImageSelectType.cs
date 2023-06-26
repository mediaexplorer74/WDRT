using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies when the visual style selects a different multiple-image file to draw an element.</summary>
	// Token: 0x0200046F RID: 1135
	public enum ImageSelectType
	{
		/// <summary>The image file does not change.</summary>
		// Token: 0x040032F7 RID: 13047
		None,
		/// <summary>Image file changes are based on size settings.</summary>
		// Token: 0x040032F8 RID: 13048
		Size,
		/// <summary>Image file changes are based on dots per inch (DPI) settings.</summary>
		// Token: 0x040032F9 RID: 13049
		Dpi
	}
}
