using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the visual effects that can be applied to the edges of a visual style element.</summary>
	// Token: 0x0200047D RID: 1149
	[Flags]
	public enum EdgeEffects
	{
		/// <summary>The border is drawn without any effects.</summary>
		// Token: 0x04003377 RID: 13175
		None = 0,
		/// <summary>The area within the element borders is filled.</summary>
		// Token: 0x04003378 RID: 13176
		FillInterior = 2048,
		/// <summary>The border is flat.</summary>
		// Token: 0x04003379 RID: 13177
		Flat = 4096,
		/// <summary>The border is soft.</summary>
		// Token: 0x0400337A RID: 13178
		Soft = 16384,
		/// <summary>The border is one-dimensional.</summary>
		// Token: 0x0400337B RID: 13179
		Mono = 32768
	}
}
