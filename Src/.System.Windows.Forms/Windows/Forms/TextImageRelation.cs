using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position of the text and image relative to each other on a control.</summary>
	// Token: 0x020003A3 RID: 931
	public enum TextImageRelation
	{
		/// <summary>Specifies that the image and text share the same space on a control.</summary>
		// Token: 0x040023B5 RID: 9141
		Overlay,
		/// <summary>Specifies that the image is displayed horizontally before the text of a control.</summary>
		// Token: 0x040023B6 RID: 9142
		ImageBeforeText = 4,
		/// <summary>Specifies that the text is displayed horizontally before the image of a control.</summary>
		// Token: 0x040023B7 RID: 9143
		TextBeforeImage = 8,
		/// <summary>Specifies that the image is displayed vertically above the text of a control.</summary>
		// Token: 0x040023B8 RID: 9144
		ImageAboveText = 1,
		/// <summary>Specifies that the text is displayed vertically above the image of a control.</summary>
		// Token: 0x040023B9 RID: 9145
		TextAboveImage
	}
}
