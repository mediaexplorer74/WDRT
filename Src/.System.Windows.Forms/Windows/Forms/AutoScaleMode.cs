using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the different types of automatic scaling modes supported by Windows Forms.</summary>
	// Token: 0x0200012A RID: 298
	public enum AutoScaleMode
	{
		/// <summary>Automatic scaling is disabled.</summary>
		// Token: 0x04000616 RID: 1558
		None,
		/// <summary>Controls scale relative to the dimensions of the font the classes are using, which is typically the system font.</summary>
		// Token: 0x04000617 RID: 1559
		Font,
		/// <summary>Controls scale relative to the display resolution. Common resolutions are 96 and 120 DPI.</summary>
		// Token: 0x04000618 RID: 1560
		Dpi,
		/// <summary>Controls scale according to the classes' parent's scaling mode. If there is no parent, automatic scaling is disabled.</summary>
		// Token: 0x04000619 RID: 1561
		Inherit
	}
}
