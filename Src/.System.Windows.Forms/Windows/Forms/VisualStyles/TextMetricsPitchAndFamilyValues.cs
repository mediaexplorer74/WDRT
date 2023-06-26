using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies information about the pitch, technology, and family of the font specified by a visual style for a particular element.</summary>
	// Token: 0x0200047F RID: 1151
	[Flags]
	public enum TextMetricsPitchAndFamilyValues
	{
		/// <summary>If this value is set, the font is a variable pitch font. Otherwise, the font is a fixed-pitch font.</summary>
		// Token: 0x04003391 RID: 13201
		FixedPitch = 1,
		/// <summary>The font is a vector font.</summary>
		// Token: 0x04003392 RID: 13202
		Vector = 2,
		/// <summary>The font is a TrueType font.</summary>
		// Token: 0x04003393 RID: 13203
		TrueType = 4,
		/// <summary>The font is a device font.</summary>
		// Token: 0x04003394 RID: 13204
		Device = 8
	}
}
