using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the source of a visual style element's background.</summary>
	// Token: 0x02000463 RID: 1123
	public enum BackgroundType
	{
		/// <summary>The background of the element is a bitmap. If this value is set, then the property corresponding to the <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile" /> value will contain the name of a valid image file.</summary>
		// Token: 0x040032B9 RID: 12985
		ImageFile,
		/// <summary>The background of the element is a rectangle filled with a color or pattern.</summary>
		// Token: 0x040032BA RID: 12986
		BorderFill,
		/// <summary>The element has no background.</summary>
		// Token: 0x040032BB RID: 12987
		None
	}
}
